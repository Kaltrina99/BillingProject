using System.Collections.Generic;
using System.Linq;
using BillingSystem.API.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IProductService
    {
        IEnumerable<ProductResponse> GetAllProducts();
        ProductResponse GetProductById(int productId);
        ProductResponse CreateProduct(ProductRequest productDto);
        ProductResponse UpdateProduct(int productId, ProductRequest productDto);
        void DeleteProduct(int productId);
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductResponse> GetAllProducts()
        {
            return _context.Products
                .Select(product => new ProductResponse
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.Description
                })
                .ToList();
        }

        public ProductResponse GetProductById(int productId)
        {
            var product = _context.Products.Find(productId);

            return product != null
                ? new ProductResponse
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.Description
                }
                : null;
        }

        public ProductResponse CreateProduct(ProductRequest productDto)
        {
            var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price,
                Description = productDto.Description
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description
            };
        }

        public ProductResponse UpdateProduct(int productId, ProductRequest productDto)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
                return null;

            product.ProductName = productDto.ProductName;
            product.Price = productDto.Price;
            product.Description = productDto.Description;

            _context.SaveChanges();

            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description
            };
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
