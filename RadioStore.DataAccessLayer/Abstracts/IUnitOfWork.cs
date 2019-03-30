using RadioStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.DataAccessLayer.Abstracts
{
    public interface IUnitOfWork
    {
        IRepository<Cart> Carts { get; }
        IRepository<Category> Categories { get; }
        IRepository<News> News { get; }
        IRepository<PriceCount> PriceCounts { get; }
        IRepository<Product> Products { get; }
        IRepository<CartItem> CartItems { get; }
        IRepository<PriceProduct> PriceProducts { get; }
        void SaveChangesAsync();
        void SaveChanges();
    }
}
