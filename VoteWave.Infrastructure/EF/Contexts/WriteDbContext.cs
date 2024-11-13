using Microsoft.EntityFrameworkCore;
using VoteWave.Domain.Entities;
using VoteWave.Infrastructure.EF.Config;

namespace VoteWave.Infrastructure.EF.Contexts;

public sealed class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Polling");

        // Apply Write-specific configurations
        var configuration = new WriteConfiguration();
        modelBuilder.ApplyConfiguration<User>(configuration);
        modelBuilder.ApplyConfiguration<Role>(configuration);

        base.OnModelCreating(modelBuilder);
    }
}
