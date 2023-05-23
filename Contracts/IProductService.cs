using baseAPI.Models;
using baseAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace baseAPI.Contracts
{
    //Clasic interface to set contracts for what the class should return

    public interface IProductService
    {

        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> SearchAllProducts(string id, string searchName);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(string id, Product product);
        Task DeleteAsync(string id);
    }
}
