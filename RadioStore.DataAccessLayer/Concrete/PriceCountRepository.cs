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
    public class PriceCountRepository : IRepository<PriceCount>
    {
        private RadioStoreEntities db;
        public PriceCountRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(PriceCount obj)
        {
            db.PriceCounts.Add(obj);
        }

        public void Update(PriceCount obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public PriceCount Get(int id)
        {
            return db.PriceCounts.Find(id);
        }

        public IQueryable<PriceCount> GetAll()
        {
            var list = db.PriceCounts;
            return list;
        }

        public void Remove(PriceCount obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
