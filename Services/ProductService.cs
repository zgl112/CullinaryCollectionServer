using baseAPI.Contracts;
using baseAPI.Data;
using baseAPI.Models;
using baseAPI.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace baseAPI.Services
{
    //middleware class to keep all CRUD logic outside of the endpoints, inheriting from main Interface

    public class ProductService : IProductService
    {
        private readonly MongoDbContext _context;

        public ProductService(MongoDbContext context)
        {
            _context = context;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Find(u=>true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _context.Products.Find(u => u.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<Product>> SearchAllProducts(string id, string searchName)
        {
            return await _context.Products.Find(u => u.Name == searchName).ToListAsync();
        }

        public async Task<Product> UpdateAsync(string id, Product product)
        {
            var dbProduct = await _context.Products.Find(u => u.Id == id).SingleOrDefaultAsync();

            var updatedProduct = new Product
            {
                Id = string.IsNullOrWhiteSpace(product.Id) ? dbProduct.Id : product.Id,
                Name = string.IsNullOrWhiteSpace(product.Name) ? dbProduct.Name : product.Name,
                Caption = string.IsNullOrWhiteSpace(product.Caption) ? dbProduct.Caption : product.Caption,
                Description = string.IsNullOrWhiteSpace(product.Description) ? dbProduct.Description : product.Description,
                Image = string.IsNullOrWhiteSpace(product.Image) ? dbProduct.Image : product.Image,
                History = string.IsNullOrWhiteSpace(product.History) ? dbProduct.History : product.History,
                HistoryImage = string.IsNullOrWhiteSpace(product.HistoryImage) ? dbProduct.HistoryImage : product.HistoryImage,
                Price = product.Price == 0 ? dbProduct.Price : product.Price,
                Stock = product.Stock == 0 ? dbProduct.Stock : product.Stock,
                Tags = string.IsNullOrWhiteSpace(product.Tags) ? dbProduct.Tags : product.Tags,
                Country = string.IsNullOrWhiteSpace(product.Country) ? dbProduct.Country : product.Country,

            };

            await _context.Products.ReplaceOneAsync(u => u.Id == id, updatedProduct);

            return await _context.Products.Find(u => u.Id == id).SingleOrDefaultAsync();
        }

        public async Task DeleteAsync(string id)
        {
             await _context.Products.DeleteOneAsync(u => u.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return await _context.Products.Find(u => u.Id == product.Id).SingleOrDefaultAsync();
        }
    }
}
