using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.DataAccessLayer.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioStore.BusinessAccessLayer.CocreteDTO
{
    public class ProductService : IService<ProductDTO>
    {
        private IUnitOfWork uof;
        public ProductService(IUnitOfWork uof)
        {
            this.uof = uof;
        }

        public void Add(ProductDTO obj)
        {
            throw new NotImplementedException();
        }

        public ProductDTO Get(int? id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductDTO> GetAll()
        {
            var productList = uof.Products.GetAll()
                                    .Select(x => new ProductDTO
                                    {
                                        ProductId = x.ProductId,
                                        ProductName = x.ProductName,
                                        ProductImage = x.ProductImage,
                                        CategoryId = x.CategoryId,
                                        Prices = x.PriceProducts.Select(p => new PriceProductDTO
                                        {
                                            ProductCount = p.PriceCount.ProductCount,
                                            Price = p.Price                                            
                                        })

                                        //Prices = uof.PriceProducts.GetAll()
                                        //                .Where(product => product.ProductId == x.ProductId)
                                        //                .Select(price => new PriceProductDTO
                                        //                {
                                        //                    ProductCount = price.PriceCount.ProductCount,
                                        //                    Price = price.Price
                                        //                })
                                    });
            return productList;
        }

        public void Remove(ProductDTO obj)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
