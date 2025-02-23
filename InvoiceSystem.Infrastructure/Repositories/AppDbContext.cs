using InvoiceSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Invoice>()
            .HasOne<Company>(i => i.CounterPartyCompany) // Explicitly specify the type argument
            .WithMany()
            .HasForeignKey(i => i.CounterPartyCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}