using RadioStore.BusinessAccessLayer.AbstractsDTO;
using RadioStore.BusinessAccessLayer.ModelsDTO;
using RadioStore.DataAccessLayer.Abstracts;
using RadioStore.DataAccessLayer.Entities;
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

        public ProductDTO Get(int? id)
        {
            return GetAll().FirstOrDefault(x => x.ProductId == id);
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
                                        IsPublished = x.IsProductPublished,
                                        Prices = x.PriceProducts.Select(p => new PriceProductDTO
                                        {
                                            ProductCount = p.PriceCount.ProductCount,
                                            Price = p.Price,
                                            IsNewPrice = false,
                                            IsRemove = false
                                        }).ToList(),
                                        Specifications = x.ProductSpecifications.Select(s => new ProductSpecificationDTO
                                        {
                                            SpecificationId = s.SpecificationId,
                                            SpecificationTypeId = s.SpecificationTypeId,
                                            SpecificationValue = s.SpecificationValue,
                                            SpecificationType = s.SpecificationType.SpecificationName,
                                            IsNewSpec = false,
                                            IsRemove = false
                                        }).ToList()
                                    });
            return productList;
        }

        public void Add(ProductDTO obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException();
            }
            var newProduct = new Product
            {
                CategoryId = obj.CategoryId,
                ProductName = obj.ProductName,
                ProductImage = obj.ProductImage
            };
            uof.Products.Create(newProduct);
            uof.SaveChanges();
        }

        public void Update(ProductDTO obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }
                var productForUpdate = uof.Products.Get(obj.ProductId.Value);
                productForUpdate.IsProductPublished = obj.IsPublished;
                productForUpdate.CategoryId = obj.CategoryId;
                productForUpdate.ProductImage = obj.ProductImage;
                productForUpdate.ProductName = obj.ProductName;
                foreach (var item in obj.Prices.Where(x => !(x.IsNewPrice || x.IsRemove)))
                {
                    var priceProduct = uof.PriceProducts.GetAll()
                                                    .ToList()
                                                    .Find(x => x.ProductId == obj.ProductId 
                                                        && x.PriceCount.ProductCount == item.ProductCount);
                    if (priceProduct != null)
                    {
                        priceProduct.Price = item.Price;
                        uof.PriceProducts.Update(priceProduct);
                    }
                }
                foreach (var item in obj.Prices.Where(x => x.IsRemove))
                {
                    var priceProduct = uof.PriceProducts.GetAll()
                                                   .ToList()
                                                   .Find(x => x.ProductId == obj.ProductId
                                                       && x.PriceCount.ProductCount == item.ProductCount);
                    if (priceProduct != null)
                    {
                        uof.PriceProducts.Remove(priceProduct);
                    }
                }
                foreach (var item in obj.Prices.Where(x => x.IsNewPrice && !x.IsRemove))
                {
                    int priceCountId = -1;
                    var priceCount = uof.PriceCounts.GetAll()
                                                    .ToList()
                                                    .Find(x => x.ProductCount == item.ProductCount);

                    if (priceCount == null)
                    {
                        uof.PriceCounts.Create(new PriceCount { ProductCount = item.ProductCount });
                        uof.SaveChanges();
                        priceCount = uof.PriceCounts.GetAll()
                                                    .ToList()
                                                    .Find(x => x.ProductCount == item.ProductCount);
                    }
                    priceCountId = priceCount.PriceCountId;
                    uof.PriceProducts.Create(new PriceProduct
                    {
                        ProductId = obj.ProductId.Value,
                        PriceCountId = priceCountId,
                        Price = item.Price
                    });
                    uof.SaveChanges();
                }

                foreach (var item in obj.Specifications)
                {
                    if (item.IsNewSpec && !item.IsRemove)
                    {
                        uof.ProductSpecifications.Create(new ProductSpecification
                        {
                            ProductId = obj.ProductId.Value,
                            SpecificationTypeId = item.SpecificationTypeId
                        });
                    }
                    else if (item.IsRemove)
                    {
                        var productSpecificationForRemove = uof.ProductSpecifications.Get(item.SpecificationId);
                        if (productSpecificationForRemove != null)
                        {
                            uof.ProductSpecifications.Remove(productSpecificationForRemove);
                        }
                    }
                    else if (!item.IsNewSpec)
                    {
                        var productSpecificationForRemove = uof.ProductSpecifications.Get(item.SpecificationId);
                        if (productSpecificationForRemove != null)
                        {
                            productSpecificationForRemove.SpecificationValue = item.SpecificationValue;
                            uof.ProductSpecifications.Update(productSpecificationForRemove);
                        }
                    }
                }

                uof.Products.Update(productForUpdate);
                uof.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Remove(ProductDTO obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
            var productForUpdate = uof.Products.Get(obj.ProductId.Value);
        }
    }
}
