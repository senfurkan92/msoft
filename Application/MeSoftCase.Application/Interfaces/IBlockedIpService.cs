using MeSoftCase.Application.Dtos.BlockedIpDtos;

namespace MeSoftCase.Application.Interfaces
{
    public interface IBlockedIpService
    {
        /// <summary>
        /// check whether ip is blocked
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<bool> CheckIsBlocked(string ipAddress);

        /// <summary>
        /// add ip on blacklist
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task AddToBlackList(string ipAddress);

        /// <summary>
        /// remove ip on blacklist
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task RemoveFromBlackList(string ipAddress);

        /// <summary>
        /// list of blockedIps
        /// </summary>
        /// <returns></returns>
        Task<List<BlockedIpListResponseDto>> List();
    }
}
