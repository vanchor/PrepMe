using Microsoft.EntityFrameworkCore;
using PrepMe.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepMe.DAL
{
    public class PrepMeDbContext : DbContext
    {
        public PrepMeDbContext(DbContextOptions<PrepMeDbContext> options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductRecipe> ProductRecipes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasMany(p => p.Recipes)
            .WithMany(p => p.Products)
            .UsingEntity<ProductRecipe>(
                j => j
                    .HasOne(pt => pt.Recipe)
                    .WithMany(t => t.ProductRecipes)
                    .HasForeignKey(pt => pt.RecipeId),
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.ProductRecipes)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.Property(pt => pt.Gramms);
                    j.HasKey(t => new { t.ProductId, t.RecipeId });
                });

            modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(p => p.Products)
            .UsingEntity<ProductCategory>(
                j => j
                    .HasOne(pt => pt.Category)
                    .WithMany(t => t.ProductCategories)
                    .HasForeignKey(pt => pt.CategoryId),
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.HasKey(t => new { t.ProductId, t.CategoryId });
                });
        }

    }
}
