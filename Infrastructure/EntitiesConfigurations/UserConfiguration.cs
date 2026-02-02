using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementTask.Core.Entities;

namespace UserManagementTask.Infrastructure.EntitiesConfigurations
{
    public class UserConfiguration: BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(e => e.Username).IsUnique();

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.UserFullName)
                .IsRequired()
                .HasMaxLength(200); 

            builder.Property(e => e.DateOfBirth)
                .IsRequired();

            builder.Property(e => e.CreationDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);
        }
    }
}
