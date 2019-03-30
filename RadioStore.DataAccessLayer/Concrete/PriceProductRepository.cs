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
    public class PriceProductRepository : IRepository<PriceProduct>
    {
        private RadioStoreEntities db;
        public PriceProductRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(PriceProduct obj)
        {
            db.PriceProducts.Add(obj);
        }

        public void Update(PriceProduct obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public PriceProduct Get(int id)
        {
            return db.PriceProducts.Find(id);
        }

        public IQueryable<PriceProduct> GetAll()
        {
            var list = db.PriceProducts;
            return list;
        }

        public void Remove(PriceProduct obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
