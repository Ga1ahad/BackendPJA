﻿using Clothesy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Persistence
{
    public interface IClothesyDb
    {
        public DbSet<Clothing> Clothing { get; set; }
        public DbSet<ClothingColor> ClothingColor { get; set; }
        public DbSet<ClothingPicture> ClothingPicture { get; set; }
        public DbSet<ClothingSuitcase> ClothingSuitcase { get; set; }
        public DbSet<ClothingTag> ClothingTag { get; set; }
        public DbSet<ClothingType> ClothingType { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Suitcase> Suitcase { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<User> User { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
