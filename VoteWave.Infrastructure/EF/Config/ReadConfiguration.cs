using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoteWave.Infrastructure.EF.Models;

namespace VoteWave.Infrastructure.EF.Config;

internal sealed class ReadConfiguration : IEntityTypeConfiguration<UserReadModel>,
                                          IEntityTypeConfiguration<RoleReadModel>,
                                          IEntityTypeConfiguration<PollReadModel>,
                                          IEntityTypeConfiguration<OptionReadModel>,
                                          IEntityTypeConfiguration<VoteReadModel>
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

    public void Configure(EntityTypeBuilder<PollReadModel> builder)
    {
        builder.ToTable("Polls");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired();

        builder.HasMany(p => p.Options)
               .WithOne()
               .HasForeignKey(o => o.PollId);
    }

    public void Configure(EntityTypeBuilder<OptionReadModel> builder)
    {
        builder.ToTable("Options");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Text).IsRequired();
        builder.Property(o => o.VoteCount).IsRequired();

        builder.HasOne<PollReadModel>()
               .WithMany(p => p.Options)
               .HasForeignKey(o => o.PollId);

        builder.HasMany(o => o.Votes)
               .WithOne()
               .HasForeignKey(v => v.OptionId);
    }

    public void Configure(EntityTypeBuilder<VoteReadModel> builder)
    {
        builder.ToTable("Votes");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.OptionId).IsRequired();
        builder.Property(v => v.UserId).IsRequired();
        builder.Property(v => v.VotedAt).IsRequired();
    }
}
