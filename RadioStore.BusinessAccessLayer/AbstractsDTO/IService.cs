using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.AbstractsDTO
{
    public interface IService<T>
    {
        IQueryable<T> GetAll();
        void Add(T obj);
        void Update(T obj);
        void Remove(T obj);
        T Get(int? id);
    }
}
