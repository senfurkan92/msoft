using Dapper;
using MeSoftCase.Application.Dtos.BlockedIpDtos;
using MeSoftCase.Infrastructure.Persistance.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MeSoftCase.Infrastructure.Persistance.Repositories.Concrete
{
    public class BlockedIpRepository(IConfiguration configuration) : IBlockedIpRepository
    {
        /// <summary>
        /// Asynchronously adds the specified IP address to the blacklist.
        /// </summary>
        /// <param name="ipAddress">The IP address to be added to the blacklist.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddToBlackList(string ipAddress)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CreatedAt", DateTimeOffset.UtcNow);
            parameters.Add("@IpAddress", ipAddress);

            var sql = @"insert into ""BlockedIps"" (""IpAddress"", ""CreatedAt"") values (@IpAddress, @CreatedAt);";

            await using var con = new SqlConnection(configuration.GetConnectionString("MsSql")!);
            await con.ExecuteAsync(sql, parameters);
        }

        /// <summary>
        /// Asynchronously remove the specified IP address from the blacklist.
        /// </summary>
        /// <param name="ipAddress">The IP address to be added to the blacklist.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task RemoveFromBlackList(string ipAddress)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IpAddress", ipAddress);

            var sql = @"delete from ""BlockedIps"" where ""IpAddress"" = @IpAddress;";

            await using var con = new SqlConnection(configuration.GetConnectionString("MsSql")!);
            await con.ExecuteAsync(sql, parameters);
        }

        /// <summary>
        /// Asynchronously retrieves a list of blocked IP addresses.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of blocked IP addresses.</returns>
        public async Task<List<BlockedIpListResponseDto>> List()
        {
            var sql = @"select * from ""BlockedIps"";";

            await using var con = new SqlConnection(configuration.GetConnectionString("MsSql")!);
            var result = await con.QueryAsync<BlockedIpListResponseDto>(sql);

            return result.ToList();
        }
    }
}
