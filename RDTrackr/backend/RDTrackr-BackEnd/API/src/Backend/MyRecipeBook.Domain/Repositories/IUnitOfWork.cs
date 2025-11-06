namespace RDTrackR.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}