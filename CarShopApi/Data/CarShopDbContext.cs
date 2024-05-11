using Microsoft.EntityFrameworkCore;

namespace CarShopApi.Data
{
    public partial class CarShopDbContext : DbContext
    {
        public CarShopDbContext()
        {
        }

        public CarShopDbContext(DbContextOptions<CarShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Producer> Producers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.Number, "UQ__Cars__78A1A19D234B0319")
                    .IsUnique();

                entity.Property(e => e.Colour).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Number).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Series).HasMaxLength(50);

                entity.HasOne(d => d.Producer)
                    .WithMany(p => p.InverseProducer)
                    .HasForeignKey(d => d.ProducerId)
                    .HasConstraintName("FK_Cars_ToTable");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Info).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
