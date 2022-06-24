using CleaningService.Models;
using Microsoft.EntityFrameworkCore;

namespace CleaningService.Core
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CleanPricing> CleanPricings { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<PayList> PayLists { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlServer("Server=localhost;Database=CleaningService;User=sa;Password=qweASD123");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CleanPricing>(entity =>
            {
                entity.ToTable("CleanPricing");

                entity.Property(e => e.PriceForM2).HasColumnType("money");

                entity.Property(e => e.Time).HasColumnType("time(0)");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.IdCleanPriceNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCleanPrice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_CleanPricing_Id_FK");

                entity.HasOne(d => d.IdPayListNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdPayList)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_PayList_Id_FK");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Orders_User_Id_FK");
            });

            modelBuilder.Entity<PayList>(entity =>
            {
                entity.ToTable("PayList");

                entity.Property(e => e.Sum).HasColumnType("money");
            });

            modelBuilder.Entity<Role>(entity => { entity.Property(e => e.Title).HasMaxLength(100); });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Login)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Users_Roles_Id_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}