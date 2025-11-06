using RDTrackR.Domain.Entities;

namespace RDTrackR.Domain.Repositories.Password
{
    public interface ICodeToPerformActionRepository
    {
        void DeleteAllUserCodes(User user);
        Task<CodeToPerformAction?> GetByCode(string code);
        Task Add(CodeToPerformAction code);


    }
}