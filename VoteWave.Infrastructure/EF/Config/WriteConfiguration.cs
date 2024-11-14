using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VoteWave.Domain.Entities;
using VoteWave.Domain.ValueObjects;

namespace VoteWave.Infrastructure.EF.Config;

internal sealed class WriteConfiguration : IEntityTypeConfiguration<User>,
                                           IEntityTypeConfiguration<Role>,
                                           IEntityTypeConfiguration<Poll>,
                                           IEntityTypeConfiguration<Option>,
                                           IEntityTypeConfiguration<Vote>
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

    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.ToTable("Polls");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired();

        var titleConverter = new ValueConverter<PollTitle, string>(
            title => title.Value,
            value => new PollTitle(value)
        );

        builder.Property(p => p.Title)
               .HasConversion(titleConverter)
               .HasColumnName("Title")
               .IsRequired();

        builder.HasMany(p => p.Options)
               .WithOne()
               .HasForeignKey(o => o.PollId)
               .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.ToTable("Options");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).IsRequired();

        var textConverter = new ValueConverter<OptionText, string>(
            text => text.Value,
            value => new OptionText(value)
        );

        builder.Property(o => o.Text)
               .HasConversion(textConverter)
               .HasColumnName("Text")
               .IsRequired();

        builder.Property(o => o.VoteCount)
               .HasColumnName("VoteCount")
               .IsRequired();

        builder.HasOne<Poll>()
               .WithMany(p => p.Options)
               .HasForeignKey(o => o.PollId)
               .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("Votes");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).IsRequired();

        builder.Property(v => v.PollId)
               .HasColumnName("PollId")
               .IsRequired();

        builder.Property(v => v.OptionId)
               .HasColumnName("OptionId")
               .IsRequired();

        builder.Property(v => v.UserId)
               .HasColumnName("UserId")
               .IsRequired();

        builder.Property(v => v.VotedAt)
               .HasColumnName("VotedAt")
               .IsRequired();
    }
}
