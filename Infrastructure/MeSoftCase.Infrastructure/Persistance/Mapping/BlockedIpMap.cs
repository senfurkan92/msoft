using MeSoftCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeSoftCase.Infrastructure.Persistance.Mapping
{
    public class BlockedIpMap : IEntityTypeConfiguration<BlockedIp>
    {
        public void Configure(EntityTypeBuilder<BlockedIp> builder)
        {
            builder.ToTable("BlockedIps");
            builder.HasKey(x => x.Id);
        }
    }
}
