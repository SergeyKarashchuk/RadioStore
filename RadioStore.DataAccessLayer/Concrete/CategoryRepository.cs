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
    public class CategoryRepository : IRepository<Category>
    {
        private RadioStoreEntities db;
        public CategoryRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(Category obj)
        {
            db.Categories.Add(obj);
        }

        public void Update(Category obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public IQueryable<Category> GetAll()
        {
            var list = db.Categories;
            return list;
        }

        public void Remove(Category obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
