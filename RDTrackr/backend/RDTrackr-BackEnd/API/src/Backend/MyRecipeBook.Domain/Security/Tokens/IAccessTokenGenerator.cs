namespace RDTrackR.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string Generate(Guid userIdentifier);
        string GenerateWithTokenId(Guid userIdentifier, string tokenId);
    }
}
