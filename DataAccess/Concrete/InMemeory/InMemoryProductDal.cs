using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemeory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            //Oracle, Sql Server, Postgres, MongoDb ... 
            _products = new List<Product>()
            {
                new Product
                {
                    ProductId = 1, CategoryId = 1, ProductName = "dsad", QuantityPerUnit = "sdakdj", UnitPrice = 3432, UnitsInStock = 12
                },
                new Product
                {
                    ProductId = 2, CategoryId = 1, ProductName = "dsDASad", QuantityPerUnit = "sASDAdakdj", UnitPrice = 32, UnitsInStock = 13
                },
                new Product
                {
                    ProductId = 3, CategoryId = 2, ProductName = "dsDFDSASad", QuantityPerUnit = "sD3253kdj", UnitPrice = 3254, UnitsInStock = 8
                }
            };
        }
        public void Add(Product entity)
        {
            _products.Add(entity);
        }

        public void Delete(Product entity)
        {
            //Product productToDelete = null;
            //foreach (var pro in _products)
            //{
            //    if (entity.Id == pro.Id)
            //    {
            //        productToDelete = pro;
            //    }
            //}
            var productToDelete = _products.SingleOrDefault(p => p.ProductId == entity.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return _products;
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            var productToUpdate = _products.SingleOrDefault(p => p.ProductId == entity.ProductId);
            productToUpdate.ProductName = entity.ProductName;
            productToUpdate.CategoryId = entity.CategoryId;
            productToUpdate.UnitPrice = entity.UnitPrice;
            productToUpdate.QuantityPerUnit = entity.QuantityPerUnit;
            productToUpdate.UnitsInStock = entity.UnitsInStock;
        }
    }
}
