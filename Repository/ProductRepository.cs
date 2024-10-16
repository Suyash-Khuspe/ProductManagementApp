using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagementApp.Models;
using System.Data;

namespace ProductManagementApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var query = "SELECT * FROM Products";
            return await _dbConnection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var query = "SELECT * FROM Products WHERE ProductId = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task AddProductAsync(Product product)
        {
            var query = "INSERT INTO Products (Name, Description, CreatedAt) VALUES (@Name, @Description, GETDATE())";
            await _dbConnection.ExecuteAsync(query, product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var query = "UPDATE Products SET Name = @Name, Description = @Description WHERE ProductId = @ProductId";
            await _dbConnection.ExecuteAsync(query, new { product.Name, product.Description, product.ProductId });
        }


        public async Task DeleteProductAsync(int id)
        {
            var query = "DELETE FROM Products WHERE ProductId = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });

            
        }

    }
}
