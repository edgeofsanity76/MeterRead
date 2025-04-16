using MeterRead.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeterRead.Data.Context;

public partial class MeterReadDbContext : DbContext
{
    public MeterReadDbContext()
    {
    }

    public MeterReadDbContext(DbContextOptions<MeterReadDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Reading> Readings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.HasIndex(e => e.AccountId, "IX_Account_AccountId").IsUnique();

            entity.HasIndex(e => e.Id, "IX_Account_Id").IsUnique();
        });

        modelBuilder.Entity<Reading>(entity =>
        {
            entity.ToTable("Reading");

            entity.HasIndex(e => e.Id, "IX_Reading_Id").IsUnique();

            entity.HasOne(d => d.Account).WithMany(p => p.Readings)
                .HasPrincipalKey(p => p.AccountId)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


