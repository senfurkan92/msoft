using MeSoftCase.Application.Dtos.AppUserDtos;

namespace MeSoftCase.Infrastructure.Persistance.Repositories.Abstract
{
    public interface IAppUserRepository
    {
        /// <summary>
        /// asynchronously retrieves the role distribution of the application users
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the role distribution data of the users.</returns>
        Task<AppUserGetRoleDistributionResponseDto> GetRoleDistribution();

        /// <summary>
        /// Aasynchronously retrieves a list of application users with relevant details
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of application users with the necessary response details.</returns>
        Task<List<AppUserListResponseDto>> List();
    }
}