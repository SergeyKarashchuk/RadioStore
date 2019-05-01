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
    class SpecificationTypesRepository : IRepository<SpecificationType>
    {
        private RadioStoreEntities db;
        public SpecificationTypesRepository(RadioStoreEntities db)
        {
            this.db = db;
        }
        public void Create(SpecificationType obj)
        {
            db.SpecificationTypes.Add(obj);
        }

        public SpecificationType Get(int id)
        {
            return db.SpecificationTypes.Find(id);
        }

        public IQueryable<SpecificationType> GetAll()
        {
            return db.SpecificationTypes;
        }

        public void Remove(SpecificationType obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }

        public void Update(SpecificationType obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}
