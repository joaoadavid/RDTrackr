namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistsActiveUserWithEmail(string email);
        public Task<bool> ExistActiveUserWithIdenfier(Guid userIdentifier);

        Task<RDTrackR.Domain.Entities.User> GetByEmail(string email);
    }
}
