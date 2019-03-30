using RadioStore.DataAccessLayer.Abstracts;
using RadioStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.DataAccessLayer.Concrete
{
    public class ProductRepository : IRepository<Product>
    {
        private RadioStoreEntities db;
        public ProductRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(Product obj)
        {
            db.Products.Add(obj);
        }

        public void Update(Product obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public IQueryable<Product> GetAll()
        {
            var list = db.Products;
            return list;
        }

        public void Remove(Product obj)
        {
            db.Entry(obj).State = EntityState.Deleted; 
        }
    }
}
