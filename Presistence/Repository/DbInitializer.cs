using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Persistence.Data.Context;
using System.Text.Json;

namespace Persistence.Repository
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DbInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Apply pending migrations (properly awaited)
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                // Get dynamic path to the DataSeeding folder
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataSeeding");

                // Seed Product Brands
                if (!await _dbContext.ProductBrands.AnyAsync())
                {
                    var brandData = await File.ReadAllTextAsync(Path.Combine(path, "brands.json"));
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    if (brands is not null && brands.Count > 0)
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(brands);
                        await _dbContext.SaveChangesAsync(); // Save after each seeding
                    }
                }

                // Seed Product Types
                if (!await _dbContext.ProductTypes.AnyAsync())
                {
                    var typesData = await File.ReadAllTextAsync(Path.Combine(path, "types.json"));
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null && types.Count > 0)
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(types);
                        await _dbContext.SaveChangesAsync(); // Save after each seeding
                    }
                }

                // Seed Products
                if (!await _dbContext.Products.AnyAsync())
                {
                    var productData = await File.ReadAllTextAsync(Path.Combine(path, "products.json"));
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    if (products is not null && products.Count > 0)
                    {
                        await _dbContext.Products.AddRangeAsync(products);
                        await _dbContext.SaveChangesAsync(); // Save after each seeding
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or rethrow
                throw new Exception($"An error occurred while initializing the database: {ex.Message}", ex);
            }
        }
    }
}