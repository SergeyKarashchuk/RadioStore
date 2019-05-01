using RadioStore.DataAccessLayer.Abstracts;
using RadioStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.DataAccessLayer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Product> Products { get; }
        public IRepository<Cart> Carts { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<News> News { get; }
        public IRepository<PriceCount> PriceCounts { get; }
        public IRepository<CartItem> CartItems { get; }
        public IRepository<PriceProduct> PriceProducts { get; }
        public IRepository<SpecificationType> SpecificationTypes { get; }
        public IRepository<SpecificationsToCategory> SpecificationsToCategories { get; }

        private RadioStoreEntities db;

        public UnitOfWork()
        {
            db = new RadioStoreEntities();
            Products = new ProductRepository(db);
            Carts = new CartRepository(db);
            Categories = new CategoryRepository(db);
            News = new NewsRepository(db);
            PriceCounts = new PriceCountRepository(db);
            CartItems = new CartItemRepository(db);
            PriceProducts = new PriceProductRepository(db);
            SpecificationTypes = new SpecificationTypesRepository(db);
            SpecificationsToCategories = new SpecificationsToCategoryRepository(db);
        }
        
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        ~UnitOfWork()
        {
            db.Dispose();
        }
    }
}
