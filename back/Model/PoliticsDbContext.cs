using Microsoft.EntityFrameworkCore;

namespace Politics.Model
{
  public class PoliticsDbContext : DbContext
  {
    public PoliticsDbContext(DbContextOptions<PoliticsDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Politician> Politicians { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Statement> Statements { get; set; }
    public DbSet<StatementTag> StatementTags { get; set; }
    public DbSet<ActivationToken> ActivationTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Statement>()
          .HasMany(p => p.Tags)
          .WithMany(p => p.Statements)
          .UsingEntity<StatementTag>(
              j => j
                  .HasOne(st => st.Tag)
                  .WithMany(t => t.StatementTags)
                  .HasForeignKey(pt => pt.TagId),
              j => j
                  .HasOne(st => st.Statement)
                  .WithMany(p => p.StatementTags)
                  .HasForeignKey(st => st.StatementId));
      modelBuilder.Entity<Party>()
        .HasOne(t => t.CreatedBy)
        .WithMany()
        .HasForeignKey(nameof(Party.CreatedById));
      modelBuilder.Entity<Party>()
        .HasOne(t => t.UpdatedBy)
        .WithMany()
        .HasForeignKey(nameof(Party.UpdatedById));

      modelBuilder.Entity<Politician>()
        .HasOne(t => t.CreatedBy)
        .WithMany()
        .HasForeignKey(nameof(Politician.CreatedById));
      modelBuilder.Entity<Politician>()
        .HasOne(t => t.UpdatedBy)
        .WithMany()
        .HasForeignKey(nameof(Politician.UpdatedById));

      modelBuilder.Entity<Statement>()
        .HasOne(t => t.CreatedBy)
        .WithMany()
        .HasForeignKey(nameof(Statement.CreatedById));
      modelBuilder.Entity<Statement>()
        .HasOne(t => t.UpdatedBy)
        .WithMany()
        .HasForeignKey(nameof(Statement.UpdatedById));
    }
  }
}
