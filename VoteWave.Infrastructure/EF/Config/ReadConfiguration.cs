using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoteWave.Infrastructure.EF.Models;

namespace VoteWave.Infrastructure.EF.Config;

internal sealed class ReadConfiguration : IEntityTypeConfiguration<UserReadModel>,
                                          IEntityTypeConfiguration<RoleReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        // Many-to-many relationship with Role via UserRoles join table
        builder.HasMany(u => u.Roles)
               .WithMany()
               .UsingEntity<Dictionary<string, object>>(
                   "UserRoles",
                   j => j.HasOne<RoleReadModel>().WithMany().HasForeignKey("RoleId"),
                   j => j.HasOne<UserReadModel>().WithMany().HasForeignKey("UserId")
               );
    }

    public void Configure(EntityTypeBuilder<RoleReadModel> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(r => r.Id);
    }
}
