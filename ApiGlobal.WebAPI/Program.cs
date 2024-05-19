using ApiGlobal.Data;
using ApiGlobal.Data.Extension;
using ApiGlobal.DTO.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<BuildDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("ApiGlobal.WebAPI"));
});

#region Dependency Injection
builder.Services.AddHttpContextAccessor();
#endregion

var app = builder.Build();
#region Seed Data
if (bool.Parse(builder.Configuration["SeedData"]) == true)
{
    using (var scope = app.Services.CreateScope())
    {
        var adminDTO = new UserAdminSeedDTO()
        {
            Email = builder.Configuration["AdminAccount:Email"],
            UserName = builder.Configuration["AdminAccount:UserName"],
            Password = builder.Configuration["AdminAccount:Password"],
        };
        SeedData.Initialize(scope.ServiceProvider, adminDTO);
    }
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
