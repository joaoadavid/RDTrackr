namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserDeleteOnlyRepository
    {
        Task DeleteAccoutn(Guid userIdentifier);
    }
}
