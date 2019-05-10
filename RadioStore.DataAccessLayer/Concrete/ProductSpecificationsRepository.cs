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
    public class ProductSpecificationsRepository : IRepository<ProductSpecification>
    {
        private RadioStoreEntities db;
        public ProductSpecificationsRepository(RadioStoreEntities db)
        {
            this.db = db;
        }

        public void Create(ProductSpecification obj)
        {
            db.ProductSpecifications.Add(obj);
        }

        public void Update(ProductSpecification obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public ProductSpecification Get(int id)
        {
            return db.ProductSpecifications.Find(id);
        }

        public IQueryable<ProductSpecification> GetAll()
        {
            var list = db.ProductSpecifications;
            return list;
        }

        public void Remove(ProductSpecification obj)
        {
            db.Entry(obj).State = EntityState.Deleted;
        }
    }
}
