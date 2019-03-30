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
    public class NewsRepository : IRepository<News>
    {
        private RadioStoreEntities db;
        public NewsRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(News obj)
        {
            db.News.Add(obj);
        }

        public void Update(News obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public News Get(int id)
        {
            return db.News.Find(id);
        }

        public IQueryable<News> GetAll()
        {
            var list = db.News;
            return list;
        }

        public void Remove(News obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
