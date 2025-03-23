using MeSoftCase.Application.Dtos.BlockedIpDtos;

namespace MeSoftCase.Infrastructure.Persistance.Repositories.Abstract
{
    public interface IBlockedIpRepository
    {
        /// <summary>
        /// Asynchronously adds the specified IP address to the blacklist.
        /// </summary>
        /// <param name="ipAddress">The IP address to be added to the blacklist.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddToBlackList(string ipAddress);

        /// <summary>
        /// Asynchronously remove the specified IP address from the blacklist.
        /// </summary>
        /// <param name="ipAddress">The IP address to be added to the blacklist.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RemoveFromBlackList(string ipAddress);

        /// <summary>
        /// Asynchronously retrieves a list of blocked IP addresses.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of blocked IP addresses.</returns>
        Task<List<BlockedIpListResponseDto>> List();
    }
}