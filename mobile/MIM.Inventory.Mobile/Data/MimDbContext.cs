using Microsoft.EntityFrameworkCore;
using MIM.Inventory.Mobile.Models;

namespace MIM.Inventory.Mobile.Data
{
    public class MimDbContext : DbContext
    {
        public MimDbContext(DbContextOptions<MimDbContext> options) : base(options)
        {
        }

        public DbSet<IssuingOrder> IssuingOrders => Set<IssuingOrder>();
        public DbSet<IssuingDetail> IssuingDetails => Set<IssuingDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssuingOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DiffVoucherno).IsRequired();
                entity.Property(e => e.MachineNo).IsRequired();
                entity.Property(e => e.IsValid)
                    .HasColumnName("is_valid")
                    .HasDefaultValue(true)
                    .IsRequired()
                    .ValueGeneratedNever();
                entity.Property(e => e.IssuingDate).HasColumnType("date");
                entity.Property(e => e.CreateTime).HasColumnType("timestamp");
                entity.Property(e => e.UpdateTime).HasColumnType("timestamp");
                entity.HasMany(e => e.Details)
                    .WithOne(d => d.IssuingOrder)
                    .HasForeignKey(d => d.IssuingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IssuingDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Unit).HasDefaultValue("G");
            });
        }
    }
}
