using ApiGlobal.Data;
using ApiGlobal.Data.Extension;
using ApiGlobal.DTO.User;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ApiGlobal.Sercurity;
using ApiGlobal.Service.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Extensions.Options;

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
builder.Services.AddRepositoryDependencyExtensition();
builder.Services.AddServiceDependencyExtension();
#endregion

#region Logging
string outputFormat = "{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
string[] excludedKeywords = { "Route matched with", "Executing JsonResult", "Executed action", "Executed endpoint", "Executing endpoint" };
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Filter.ByExcluding(logEvent => excludedKeywords.Any(keyword => logEvent.MessageTemplate.Text.Contains(keyword)))
    .WriteTo.Console(outputTemplate: outputFormat)
    .WriteTo.RollingFile("Logs\\Log-{Date}.txt", retainedFileCountLimit: null, outputTemplate: outputFormat)
    .CreateLogger();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});
#endregion

#region Identity
builder.Services.AddIdentityFrameworkCore();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ClockSkew = TimeSpan.Zero
    };
});



builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Eng Word Sync API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer",
                }
            },
            new string[]{}
        }
    });
});
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
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "API GLOBAL V1");
});

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
