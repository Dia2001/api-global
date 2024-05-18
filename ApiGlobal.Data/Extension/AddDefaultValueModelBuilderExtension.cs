using ApiGlobal.Model.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Extension
{
    public static class AddDefaultValueModelBuilderExtension
    {
        public static ModelBuilder AddDefaultValueModelBuilder(this ModelBuilder builder)
        {
            builder.Entity<User>().Property(p => p.Active).HasDefaultValue(true);
            builder.Entity<User>().Property(p => p.Deleted).HasDefaultValue(false);

            return builder;
        }
    }
}
