using ApiGlobal.DTO.Base;
using ApiGlobal.Model.Abstracts;
using ApiGlobal.Model.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.DTO.User
{
    public class UserAdminSeedDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginSuccessDTO
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public Guid UserId { get; set; }
        public string? Avatar { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
    public class UserSignUpDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? RefCode { get; set; }
    }
    public class UserLoginSocialDTO
    {
        public string AccessToken { get; set; }
        public string Uid { get; set; }
        public TYPE_LOGIN TypeLogin { get; set; }
    }
    public class UserItemDTO : AuditBase
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? Avatar { get; set; }
        public bool Active { get; set; }
        public bool Deteted { get; set; }
        public string Email { get; set; }
        public string? OAuthId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RefCode { get; set; }
        public TYPE_LOGIN TypeLogin { get; set; }
        public int TotalDateStudied { get; set; } = 0;
        public DateTime? LastTimeStudy { get; set; }
        public int TotalWords { get; set; } = 0;
        public int DateTimeOffset { get; set; } = -420;
        public string? Address { get; set; }
        public int? TimeRemind { get; set; } = 20;
        public bool? IsNotify { get; set; }
        public string? Introduce { get; set; }
        public bool? Gender { set; get; }
    }
    public class UserRequest : BaseRequest
    {
        public bool? Active { get; set; }
        public TYPE_LOGIN? TypeLogin { get; set; }
    }
}
