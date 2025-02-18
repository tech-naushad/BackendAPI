using IdentityManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Persistence.Configurations
{
    public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => new { x.UserName, x.Email });
            builder.Property(x => x.Name).HasColumnType("varchar").IsRequired().HasMaxLength(30);
            builder.Property(x => x.UserName).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).HasColumnType("varchar").IsRequired().HasMaxLength(30);
            builder.Property(x => x.Password).HasColumnType("varchar").IsRequired().HasMaxLength(20); 
        }
    }
}
