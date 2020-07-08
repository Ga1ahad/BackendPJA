using System;
using Clothesy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Clothesy.Persistence
{
    public partial class s15264Context : DbContext
    {
        public s15264Context()
        {
        }

        public s15264Context(DbContextOptions<s15264Context> options)
            : base(options)
        {
        }

        public virtual DbSet<BodyPart> BodyPart { get; set; }
        public virtual DbSet<Clothing> Clothing { get; set; }
        public virtual DbSet<ClothingColor> ClothingColor { get; set; }
        public virtual DbSet<ClothingPicture> ClothingPicture { get; set; }
        public virtual DbSet<ClothingSuitcase> ClothingSuitcase { get; set; }
        public virtual DbSet<ClothingTag> ClothingTag { get; set; }
        public virtual DbSet<ClothingType> ClothingType { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Suitcase> Suitcase { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s15264;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BodyPart>(entity =>
            {
                entity.HasKey(e => e.IdBodyPart)
                    .HasName("BodyPart_pk");

                entity.Property(e => e.IdBodyPart).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Clothing>(entity =>
            {
                entity.HasKey(e => e.IdClothing)
                    .HasName("Clothing_pk");

                entity.Property(e => e.IdClothing).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBodyPartNavigation)
                    .WithMany(p => p.Clothing)
                    .HasForeignKey(d => d.IdBodyPart)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_BodyPart");

                entity.HasOne(d => d.IdClothingTypeNavigation)
                    .WithMany(p => p.Clothing)
                    .HasForeignKey(d => d.IdClothingType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_ClothingType");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Clothing)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_User");
            });

            modelBuilder.Entity<ClothingColor>(entity =>
            {
                entity.HasKey(e => new { e.ClothingIdClothing, e.ColorIdColor })
                    .HasName("Clothing_Color_pk");

                entity.ToTable("Clothing_Color");

                entity.Property(e => e.ClothingIdClothing).HasColumnName("Clothing_IdClothing");

                entity.Property(e => e.ColorIdColor).HasColumnName("Color_IdColor");

                entity.HasOne(d => d.ClothingIdClothingNavigation)
                    .WithMany(p => p.ClothingColor)
                    .HasForeignKey(d => d.ClothingIdClothing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_22_Clothing");

                entity.HasOne(d => d.ColorIdColorNavigation)
                    .WithMany(p => p.ClothingColor)
                    .HasForeignKey(d => d.ColorIdColor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_22_Color");
            });

            modelBuilder.Entity<ClothingPicture>(entity =>
            {
                entity.HasKey(e => e.IdClothingPicture)
                    .HasName("ClothingPicture_pk");

                entity.Property(e => e.IdClothingPicture).ValueGeneratedNever();

                entity.Property(e => e.ClothImg).HasColumnType("image");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClothinNavigation)
                    .WithMany(p => p.ClothingPicture)
                    .HasForeignKey(d => d.IdClothin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Picture_Clothing");
            });

            modelBuilder.Entity<ClothingSuitcase>(entity =>
            {
                entity.HasKey(e => new { e.IdSuitcase, e.IdClothing })
                    .HasName("Clothing_Suitcase_pk");

                entity.ToTable("Clothing_Suitcase");

                entity.HasOne(d => d.IdClothingNavigation)
                    .WithMany(p => p.ClothingSuitcase)
                    .HasForeignKey(d => d.IdClothing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Suitcase_Clothing");

                entity.HasOne(d => d.IdSuitcaseNavigation)
                    .WithMany(p => p.ClothingSuitcase)
                    .HasForeignKey(d => d.IdSuitcase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Suitcase_Suitcase");
            });

            modelBuilder.Entity<ClothingTag>(entity =>
            {
                entity.HasKey(e => new { e.IdClothing, e.IdTag })
                    .HasName("Clothing_Tag_pk");

                entity.ToTable("Clothing_Tag");

                entity.HasOne(d => d.IdClothingNavigation)
                    .WithMany(p => p.ClothingTag)
                    .HasForeignKey(d => d.IdClothing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Tag_Clothing");

                entity.HasOne(d => d.IdTagNavigation)
                    .WithMany(p => p.ClothingTag)
                    .HasForeignKey(d => d.IdTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Tag_Tags");
            });

            modelBuilder.Entity<ClothingType>(entity =>
            {
                entity.HasKey(e => e.IdClothingType)
                    .HasName("ClothingType_pk");

                entity.Property(e => e.IdClothingType).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.IdColor)
                    .HasName("Color_pk");

                entity.Property(e => e.IdColor).ValueGeneratedNever();

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Suitcase>(entity =>
            {
                entity.HasKey(e => e.IdSuitcase)
                    .HasName("Suitcase_pk");

                entity.Property(e => e.IdSuitcase).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTripNavigation)
                    .WithMany(p => p.Suitcase)
                    .HasForeignKey(d => d.IdTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Suitcase_Trip");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Suitcase)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Suitcase_User");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasKey(e => e.IdTag)
                    .HasName("Tags_pk");

                entity.Property(e => e.IdTag).ValueGeneratedNever();

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.IdTrip)
                    .HasName("Trip_pk");

                entity.Property(e => e.IdTrip).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EndTrip).HasColumnType("date");

                entity.Property(e => e.StartTrip).HasColumnType("date");

                entity.Property(e => e.TripName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("User_pk");

                entity.Property(e => e.IdUser).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.DeletedAt).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
