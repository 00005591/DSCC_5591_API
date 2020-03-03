using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSCC_CW1_5591.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC_CW1_5591.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _dbContext;

        public ProductRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteProduct(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public Product GetProductById(int productId)
        {
            var product = _dbContext.Products.Find(productId);
            _dbContext.Entry(product).Reference(p => p.CategoryName);
            return product;

        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.Include(p => p.CategoryName).ToList();
        }

        public void InsertProduct(Product product)
        {
            _dbContext.Add(product);
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State =
           Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
