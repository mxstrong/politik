using Microsoft.EntityFrameworkCore;

namespace Politics.Model
{
  public interface IPoliticsDbContext
  {
  }

  public class PoliticsDbContext : DbContext, IPoliticsDbContext
  {
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Party> Parties { get; set; }
    public virtual DbSet<Politician> Politicians { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Statement> Statements { get; set; }
    public virtual DbSet<StatementTag> StatementTags { get; set; }
    public virtual DbSet<ActivationToken> ActivationTokens { get; set; }
    public virtual DbSet<EmailChangeToken> EmailChangeTokens { get; set; }
    public virtual DbSet<Like> Likes { get; set; }
    public virtual DbSet<Report> Reports { get; set; }

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

      modelBuilder.Entity<Report>()
        .HasOne(t => t.CreatedBy)
        .WithMany()
        .HasForeignKey(nameof(Report.CreatedById));
      modelBuilder.Entity<Report>()
        .HasOne(t => t.ReviewedBy)
        .WithMany()
        .HasForeignKey(nameof(Report.ReviewedById));
    }
  }
}
