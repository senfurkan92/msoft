using MeSoftCase.Application.Dtos.BlockedIpDtos;
using MeSoftCase.Application.Interfaces;
using MeSoftCase.Infrastructure.Persistance.Repositories.Abstract;
using Microsoft.Extensions.Caching.Memory;

namespace MeSoftCase.Infrastructure.Services
{
    public class BlockedIpService(IBlockedIpRepository blockedIpRepository, IMemoryCache cache) : IBlockedIpService
    {
        /// <summary>
        /// add ip on blacklist
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task AddToBlackList(string ipAddress)
        {
            await blockedIpRepository.AddToBlackList(ipAddress);

            cache.Remove("BlockedIp_BlackList");
        }

        /// <summary>
        /// remove ip from blacklist
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task RemoveFromBlackList(string ipAddress)
        {
            await blockedIpRepository.RemoveFromBlackList(ipAddress);

            cache.Remove("BlockedIp_BlackList");
        }

        /// <summary>
        /// check whether ip on blacklist
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<bool> CheckIsBlocked(string ipAddress)
        {
            var blockedIps = cache.Get<List<BlockedIpListResponseDto>>("BlockedIp_BlackList");

            if (blockedIps == null)
            {
                blockedIps = await blockedIpRepository.List() ?? new ();

                cache.Set("BlockedIp_BlackList", blockedIps);
            }

            return blockedIps.Select(x => x.IpAddress).Contains(ipAddress);
        }

        /// <summary>
        /// check whether ip on blacklist
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<List<BlockedIpListResponseDto>> List()
        {
            var blockedIps = cache.Get<List<BlockedIpListResponseDto>>("BlockedIp_BlackList");

            if (blockedIps == null)
            {
                blockedIps = await blockedIpRepository.List() ?? new();

                cache.Set("BlockedIp_BlackList", blockedIps);
            }

            return blockedIps;
        }
    }
}
