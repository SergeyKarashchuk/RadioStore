using LinqKit;
using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace RadioStore.WebApplication.Infrastructure
{
    public class ItemSelector<T>
    {
        private IService<T> service;
        public ItemSelector(IService<T> service)
        {
            this.service = service;
        }

        public IQueryable<T> GetItemList(ExpressionStarter<T> expression)
        {
            var list = service.GetAll().Where(expression);                               
            return list;
        }

        public int GetPageCount(int itemsPerPage, ExpressionStarter<T> expression)
        {
            var list = service.GetAll()
                                .Where(expression)
                                .ToList();

            int pageCount = (int)Math.Ceiling((double)list.Count / (double)itemsPerPage);
            return pageCount;
        }
    }
}