using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ApiGlobal.Data.Extension;
using ApiGlobal.Model.Interfaces;
using ApiGlobal.Model.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ApiGlobal.Data
{
    public class BuildDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;
        public BuildDbContext(DbContextOptions<BuildDbContext> options, IHttpContextAccessor? httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region Dbset


        #endregion

        #region override method
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var entityTypes = builder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
                builder.Entity(entityType.ClrType)
                        .ToTable(entityType.GetTableName().Replace("AspNet", ""));

            builder.Entity<UserRole>().HasKey(x => x.Id);
            builder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                        .IsRequired();

            builder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.AddDefaultValueModelBuilder();
        }

        public override int SaveChanges()
        {
            BeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforeSaving()
        {
            ChangeTracker.AutoDetectChangesEnabled = true;
            var entities = ChangeTracker.Entries();
            foreach (var entity in entities)
            {
                if (entity.Entity is IAudit && _httpContextAccessor?.HttpContext != null && _httpContextAccessor?.HttpContext.User != null && _httpContextAccessor.HttpContext.User.FindFirst("UserId") != null)
                {
                    IAudit audit = (IAudit)entity.Entity;
                    string userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value;
                    var now = DateTime.UtcNow;
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            audit.CreatedAt = now;
                            audit.CreatedBy = userId;
                            audit.UpdatedAt = now;
                            audit.UpdatedBy = userId;
                            break;
                        case EntityState.Modified:
                            audit.UpdatedAt = now;
                            audit.UpdatedBy = userId;
                            break;
                    }
                }
            }
        }
        #endregion
    }

}
