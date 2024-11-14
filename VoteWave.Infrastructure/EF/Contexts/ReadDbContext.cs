using Microsoft.EntityFrameworkCore;
using VoteWave.Infrastructure.EF.Models;
using VoteWave.Infrastructure.EF.Config;

namespace VoteWave.Infrastructure.EF.Contexts;

public sealed class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options)
{
    public DbSet<UserReadModel> Users { get; set; }
    public DbSet<RoleReadModel> Roles { get; set; }
    public DbSet<PollReadModel> Polls { get; set; }
    public DbSet<OptionReadModel> Options { get; set; }
    public DbSet<VoteReadModel> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Polling");

        // Apply Read-specific configurations
        var configuration = new ReadConfiguration();
        modelBuilder.ApplyConfiguration<UserReadModel>(configuration);
        modelBuilder.ApplyConfiguration<RoleReadModel>(configuration);
        modelBuilder.ApplyConfiguration<PollReadModel>(configuration);
        modelBuilder.ApplyConfiguration<OptionReadModel>(configuration);
        modelBuilder.ApplyConfiguration<VoteReadModel>(configuration);

        base.OnModelCreating(modelBuilder);
    }
}
