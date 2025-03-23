namespace MeSoftCase.Domain.Entities
{
    public class BlockedIp
    {
        public int Id { get; set; }
        public string IpAddress { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
