﻿using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clothesy.Persistence
{
    public partial class ClothesyDbContext : DbContext, IClothesyDb
    {
        public ClothesyDbContext()
        {
        }

        public ClothesyDbContext(DbContextOptions<ClothesyDbContext> options)
            : base(options)
        {
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Clothing>(entity =>
            {
                entity.HasKey(e => e.idClothing)
                    .HasName("Clothing_pk");

                entity.Property(e => e.idClothing).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.idClothingTypeNavigation)
                    .WithMany(p => p.Clothing)
                    .HasForeignKey(d => d.idClothingType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_ClothingType");

                entity.HasOne(d => d.idUserNavigation)
                    .WithMany(p => p.Clothing)
                    .HasForeignKey(d => d.idUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_User");
            });

            modelBuilder.Entity<ClothingColor>(entity =>
            {
                entity.HasKey(e => new { e.ClothingIdClothing, e.ColorIdColor })
                    .HasName("Clothing_Color_pk");

                entity.ToTable("Clothing_Color");

                entity.Property(e => e.ClothingIdClothing).HasColumnName("Clothing_idClothing");

                entity.Property(e => e.ColorIdColor).HasColumnName("Color_idColor");

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
                entity.HasKey(e => e.idClothingPicture)
                    .HasName("ClothingPicture_pk");

                entity.Property(e => e.idClothingPicture).ValueGeneratedOnAdd();

                entity.Property(e => e.ClothingUrl).HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.idClothinNavigation)
                    .WithMany(p => p.ClothingPicture)
                    .HasForeignKey(d => d.idClothin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Picture_Clothing");
            });

            modelBuilder.Entity<ClothingSuitcase>(entity =>
            {
                entity.HasKey(e => new { e.idSuitcase, e.idClothing })
                    .HasName("Clothing_Suitcase_pk");

                entity.ToTable("Clothing_Suitcase");

                entity.HasOne(d => d.idClothingNavigation)
                    .WithMany(p => p.ClothingSuitcase)
                    .HasForeignKey(d => d.idClothing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Suitcase_Clothing");

                entity.HasOne(d => d.idSuitcaseNavigation)
                    .WithMany(p => p.ClothingSuitcase)
                    .HasForeignKey(d => d.idSuitcase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Suitcase_Suitcase");
            });

            modelBuilder.Entity<ClothingTag>(entity =>
            {
                entity.HasKey(e => new { e.idClothing, e.idTag })
                    .HasName("Clothing_Tag_pk");

                entity.ToTable("Clothing_Tag");

                entity.HasOne(d => d.idClothingNavigation)
                    .WithMany(p => p.ClothingTag)
                    .HasForeignKey(d => d.idClothing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Tag_Clothing");

                entity.HasOne(d => d.idTagNavigation)
                    .WithMany(p => p.ClothingTag)
                    .HasForeignKey(d => d.idTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clothing_Tag_Tags");
            });

            modelBuilder.Entity<ClothingType>(entity =>
            {
                entity.HasKey(e => e.idClothingType)
                    .HasName("ClothingType_pk");

                entity.Property(e => e.idClothingType).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.idColor)
                    .HasName("Color_pk");

                entity.Property(e => e.idColor).ValueGeneratedOnAdd();

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Suitcase>(entity =>
            {
                entity.HasKey(e => e.idSuitcase)
                    .HasName("Suitcase_pk");

                entity.Property(e => e.idSuitcase).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.idTripNavigation)
                    .WithMany(p => p.Suitcase)
                    .HasForeignKey(d => d.idTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Suitcase_Trip");

            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasKey(e => e.idTag)
                    .HasName("Tags_pk");

                entity.Property(e => e.idTag).ValueGeneratedOnAdd();

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.idTrip)
                    .HasName("Trip_pk");

                entity.Property(e => e.idTrip).ValueGeneratedOnAdd();

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

                entity.HasOne(d => d.idUserNavigation)
                    .WithMany(p => p.Trip)
                    .HasForeignKey(d => d.idUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Suitcase_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.idUser)
                    .HasName("User_pk");

                entity.Property(e => e.idUser).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.DeletedAt).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("date");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
