namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        public Task <RDTrackR.Domain.Entities.User> GetById(long id);

        public void Update(RDTrackR.Domain.Entities.User user);
    }
}
