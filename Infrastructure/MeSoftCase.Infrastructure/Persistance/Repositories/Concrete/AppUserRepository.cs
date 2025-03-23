using Dapper;
using MeSoftCase.Application.Dtos.AppUserDtos;
using MeSoftCase.Infrastructure.Persistance.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MeSoftCase.Infrastructure.Persistance.Repositories.Concrete
{
    public class AppUserRepository(IConfiguration configuration) : IAppUserRepository
    {
        /// <summary>
        /// asynchronously retrieves the role distribution of the application users
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role distribution data of the users.</returns>
        public async Task<AppUserGetRoleDistributionResponseDto> GetRoleDistribution()
        {
            await using var con = new SqlConnection(configuration.GetConnectionString("MsSql")!);

            var totalSql = @"select count(*) from AspNetUserRoles";

            var totalCount = await con.QueryAsync<int>(totalSql);

            var distributionSql = @"select anr.Name, COUNT(*) as 'Count' from ""AspNetUserRoles"" as anur
                    inner join AspNetRoles as anr on anr.Id = anur.RoleId
                    group by anr.Name";

            var result = await con.QueryAsync(distributionSql);

            return new AppUserGetRoleDistributionResponseDto(
                    totalCount.FirstOrDefault(),
                    result.FirstOrDefault(x => x.Name == "Admin")?.Count ?? 0,
                    result.FirstOrDefault(x => x.Name == "Manager")?.Count ?? 0,
                    result.FirstOrDefault(x => x.Name == "Customer")?.Count ?? 0
                );
        }

        /// <summary>
        /// Asynchronously retrieves a list of blocked IP addresses.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of blocked IP addresses.</returns>
        public async Task<List<AppUserListResponseDto>> List()
        {
            var sql = @"SELECT 
                            u.Id,
                            u.UserName,
                            u.Email,
                            u.LockoutEnd,
                            STRING_AGG(r.Name, ', ') AS Roles
                        FROM AspNetUsers u
                        LEFT JOIN AspNetUserRoles ur ON u.Id = ur.UserId
                        LEFT JOIN AspNetRoles r ON ur.RoleId = r.Id
                        GROUP BY u.Id, u.UserName, u.Email, u.LockoutEnd";

            await using var con = new SqlConnection(configuration.GetConnectionString("MsSql")!);
            var result = await con.QueryAsync<AppUserListResponseDto>(sql);

            return result.ToList();
        }
    }
}
