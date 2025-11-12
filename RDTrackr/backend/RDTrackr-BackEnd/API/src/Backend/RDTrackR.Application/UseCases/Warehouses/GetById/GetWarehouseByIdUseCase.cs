using AutoMapper;
using RDTrackR.Communication.Responses.Warehouse;
using RDTrackR.Domain.Repositories.Warehouses;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Warehouses.GetById
{
    public class GetWarehouseByIdUseCase : IGetWarehouseByIdUseCase
    {
        private readonly IWarehouseReadOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;

        public GetWarehouseByIdUseCase(IWarehouseReadOnlyRepository repository,ILoggedUser loggedUser , IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseWarehouseJson> Execute(long id)
        {
            var loggedUser = await _loggedUser.User();
            var warehouse = await _repository.GetByIdAsync(id,loggedUser)
                ?? throw new NotFoundException(ResourceMessagesException.WAREHOUSE_NOT_FOUND);

            return _mapper.Map<ResponseWarehouseJson>(warehouse);
        }
    }

}
