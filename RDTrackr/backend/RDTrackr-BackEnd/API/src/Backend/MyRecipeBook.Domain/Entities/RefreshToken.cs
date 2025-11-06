namespace RDTrackR.Domain.Entities
{
    public class RefreshToken : EntityBase
    {
        public required string Value { get; set; }
        public required long UserId { get; set; }
        public required string TokenId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsRevoked { get; set; } = false;

        public User User { get; set; } = default!;
    }
}
