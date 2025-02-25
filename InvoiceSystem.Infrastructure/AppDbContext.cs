using InvoiceSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                .ValueGeneratedOnAdd();
            entity.Property(c => c.Name).IsRequired(); 
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.PasswordHash)
                .IsRequired();
            entity.Property(u => u.IssuerCompanyId).IsRequired();
        });


        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.HasOne(i => i.IssuerCompany)
                  .WithMany()
                  .HasForeignKey(i => i.IssuerCompanyId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(i => i.CounterPartyCompany)
                  .WithMany()
                  .HasForeignKey(i => i.CounterPartyCompanyId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.Property(i => i.DateIssued).IsRequired();
            entity.Property(i => i.NetAmount).IsRequired();
            entity.Property(i => i.VatAmount).IsRequired();
            entity.Property(i => i.TotalAmount).IsRequired();
            entity.Property(i => i.Description).IsRequired();
        });
    }
}