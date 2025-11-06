namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Add(RDTrackR.Domain.Entities.User user);
    }
}
