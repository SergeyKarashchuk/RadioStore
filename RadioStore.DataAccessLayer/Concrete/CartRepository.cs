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
    public class CartRepository : IRepository<Cart>
    {
        private RadioStoreEntities db;
        public CartRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(Cart obj)
        {
            db.Carts.Add(obj);
        }

        public void Update(Cart obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public Cart Get(int id)
        {
            return db.Carts.Find(id);
        }

        public IQueryable<Cart> GetAll()
        {
            var list = db.Carts;
            return list;
        }

        public void Remove(Cart obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
