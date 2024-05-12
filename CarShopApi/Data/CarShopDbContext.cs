using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarShopApi.Data
{
    public partial class CarShopDbContext : IdentityDbContext<ApiUser>
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
            base.OnModelCreating(modelBuilder);

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
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ProducerId)
                    .HasConstraintName("FK_Cars_ToTable");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Info).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "a890e379-b7c7-4b9b-a928-0e847f83f6a9"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "23caf552-39b5-4464-a221-37d264ce204c"
                }
            );
            var hasher = new PasswordHasher<ApiUser>();
            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id = "55b4f1ed-8f01-4fea-a62a-1aa8cd9b6187",
                    Email = "admin@carshop.com",
                    NormalizedEmail = "ADMIN@CARSHOP.COM",
                    UserName = "admin@carshop.com",
                    NormalizedUserName = "ADMIN@CARSHOP.COM",
                    Name = "System",
                    Surname = "Admin",
                    PasswordHash = hasher.HashPassword(null, "admin123")
                },
                new ApiUser
                {
                    Id = "d828ec99-761d-40a2-bae6-53e38cd37d5c",
                    Email = "user@carshop.com",
                    NormalizedEmail = "USER@CARSHOP.COM",
                    UserName = "user@carshop.com",
                    NormalizedUserName = "USER@CARSHOP.COM",
                    Name = "System",
                    Surname = "User",
                    PasswordHash = hasher.HashPassword(null, "user123")
                }
            ); ;

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "a890e379-b7c7-4b9b-a928-0e847f83f6a9",
                    UserId = "d828ec99-761d-40a2-bae6-53e38cd37d5c"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "23caf552-39b5-4464-a221-37d264ce204c",
                    UserId = "55b4f1ed-8f01-4fea-a62a-1aa8cd9b6187"
                }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
