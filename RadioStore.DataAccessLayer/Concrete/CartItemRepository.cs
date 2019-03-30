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
    public class CartItemRepository : IRepository<CartItem>
    {
        private RadioStoreEntities db;
        public CartItemRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(CartItem obj)
        {
            db.CartItems.Add(obj);
        }

        public void Update(CartItem obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public CartItem Get(int id)
        {
            return db.CartItems.Find(id);
        }

        public IQueryable<CartItem> GetAll()
        {
            var list = db.CartItems;
            return list;
        }

        public void Remove(CartItem obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
