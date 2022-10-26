using FluxoCaixa.Chassis.Persistence.Configuration;
using FluxoCaixa.Services.Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Services.Authentication.Infrastructure.Configuration
{
    public class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Password).IsRequired().HasMaxLength(2000);
            builder.Property(p => p.Role).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Username).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(70);
            builder.Property(p => p.Fullname).IsRequired().HasMaxLength(70);
            builder.Property(p => p.Phone).IsRequired().HasMaxLength(14);

            builder.HasIndex(h => h.Username).IsUnique();
            builder.HasIndex(h => h.Email).IsUnique();
            builder.HasIndex(h => h.Phone).IsUnique();

            builder.HasData(new User
            {
                BirthDate = DateTime.UtcNow.AddYears(-25),
                Username = "root",
                Password = "AQAAAAEAACcQAAAAEPck747NoF+LzBWfDLoLIND6Zxy0YgYkOWRpSf4mAnMahoDRqdHrue/dvw43yyDfRg==",
                Email = "root@test.com",
                Phone = "+5511900000000",
                Role = "user",
                Fullname = "root",
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid()
            });

            base.Configure(builder);
        }
    }
}
