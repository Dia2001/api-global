using ApiGlobal.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Model.Entities.Identity
{
    public enum TYPE_LOGIN
    {
        SYSTEM = 1,
        GOOGLE = 2,
    }

    [Table("Users")]
    public class User : IdentityUser<Guid>, IEntity<Guid>, IAudit
    {
        [Required]
        public string FullName { get; set; }
        [MaxLength(12)]
        public string? RefCode { get; set; }
        public string? Avatar { get; set; }
        public TYPE_LOGIN TYPE_LOGIN { get; set; } = TYPE_LOGIN.SYSTEM;
        public string? OAuthId { get; set; } //google/facebook...
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsAdmin { get; set; } = false;
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public int TotalDateStudied { get; set; } = 0;
        public DateTime? LastTimeStudy { get; set; }
        public int TotalWords { get; set; } = 0;
        public int DateTimeOffset { get; set; } = -420;
        [MaxLength(256)]
        public string? Introduce { get; set; }
        [MaxLength(256)]
        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public bool? IsNotify { get; set; }
        public int? TimeRemind { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
