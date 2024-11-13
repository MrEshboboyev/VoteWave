using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Infrastructure.EF.Config;

internal sealed class WriteConfiguration : IEntityTypeConfiguration<User>,
                                           IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).IsRequired();

        var usernameConverter = new ValueConverter<Username, string>(
            u => u.Value,
            u => new Username(u)
        );

        builder.Property(u => u.Username)
            .HasConversion(usernameConverter)
            .HasColumnName("Username")
            .IsRequired();


        var emailConverter = new ValueConverter<Email, string>(
            e => e.Value,
            e => new Email(e)
        );

        builder.Property(u => u.Email)
            .HasConversion(emailConverter)
            .HasColumnName("Email")
            .IsRequired();


        var passwordHashConverter = new ValueConverter<PasswordHash, string>(
            ph => ph.Value,
            ph => new PasswordHash(ph)
        );

        builder.Property(u => u.PasswordHash)
            .HasConversion(passwordHashConverter)
            .HasColumnName("PasswordHash")
            .IsRequired();


        // Many-to-many relationship with Role
        builder.HasMany(u => u.Roles)
               .WithMany(r => r.Users)
               .UsingEntity<Dictionary<string, object>>(
                   "UserRoles",
                   j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                   j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
               );
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).IsRequired();

        var roleNameConverter = new ValueConverter<RoleName, string>(
            rn => rn.Value,
            rn => new RoleName(rn)
        );

        builder.Property(u => u.Name)
            .HasConversion(roleNameConverter)
            .HasColumnName("Name")
            .IsRequired();
    }
}
