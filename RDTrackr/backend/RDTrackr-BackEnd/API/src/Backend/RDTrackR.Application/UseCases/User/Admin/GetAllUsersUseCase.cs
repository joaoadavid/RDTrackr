using AutoMapper;
using RDTrackR.Communication.Responses.User.Admin;
using RDTrackR.Domain.Repositories.Users;

namespace RDTrackR.Application.UseCases.User.Admin
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUsersUseCase(IUserReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseUserListItemJson>> Execute()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<List<ResponseUserListItemJson>>(users);
        }
    }

}
