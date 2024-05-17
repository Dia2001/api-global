using ApiGlobal.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Model.Entities.Identity
{
    public enum GROUP_ROLE
    {
        Admin = 1,
        User = 2,
    }
    [Table("Roles")]
    public class Role : IdentityRole<Guid>, IEntity<Guid>
    {
        public GROUP_ROLE Group { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
