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
    class SpecificationsToCategoryRepository : IRepository<SpecificationsToCategory>
    {
        private RadioStoreEntities db;
        public SpecificationsToCategoryRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(SpecificationsToCategory obj)
        {
            db.SpecificationsToCategories.Add(obj);
        }

        public void Update(SpecificationsToCategory obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public SpecificationsToCategory Get(int id)
        {
            return db.SpecificationsToCategories.Find(id);
        }

        public IQueryable<SpecificationsToCategory> GetAll()
        {
            var list = db.SpecificationsToCategories;
            return list;
        }

        public void Remove(SpecificationsToCategory obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
