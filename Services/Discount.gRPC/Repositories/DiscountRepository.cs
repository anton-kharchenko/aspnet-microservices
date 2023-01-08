using Dapper;
using Discount.gRPC.Entities;
using Npgsql;

namespace Discount.gRPC.Repositories;

/// <inheritdoc />
public class DiscountRepository : IDiscountRepository
{
        private readonly IConfiguration _configuration;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="configuration">Configuration of application</param>
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public async Task<Coupon> GetDiscountAsync(string productName)
        {
            await using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (coupon == null)
                return new Coupon 
                { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            return coupon;
        }

        /// <inheritdoc />
        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                            new { coupon.ProductName, coupon.Description, coupon.Amount });

            return affected != 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });

            return affected != 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteDiscountAsync(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            return affected != 0;
        }
}