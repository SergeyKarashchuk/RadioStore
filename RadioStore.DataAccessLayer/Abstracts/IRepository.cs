using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.DataAccessLayer.Abstracts
{
    public interface IRepository<T>
    {
        void Create(T obj);
        void Update(T obj);
        T Get(int id);
        IQueryable<T> GetAll();
        void Remove(T obj);
    }
}
