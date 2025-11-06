using RDTrackR.Communication.Requests.Warehouse;
using RDTrackR.Communication.Responses.Warehouse;
using RDTrackR.Domain.Repositories.Warehouses;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Exceptions.ExceptionBase;
using RDTrackR.Exceptions;
using AutoMapper;
using RDTrackR.Domain.Extensions;

namespace RDTrackR.Application.UseCases.Warehouses.Update
{
    public class UpdateWarehouseUseCase : IUpdateWarehouseUseCase
    {
        private readonly IWarehouseReadOnlyRepository _readOnlyRepository;
        private readonly IWarehouseWriteOnlyRepository _writeOnlyRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWarehouseUseCase(
            IWarehouseReadOnlyRepository readOnlyRepository,
            IWarehouseWriteOnlyRepository writeOnlyRepository,
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseWarehouseJson> Execute(long id, RequestUpdateWarehouseJson request)
        {
            var warehouse = await _readOnlyRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ResourceMessagesException.WAREHOUSE_NOT_FOUND);

            var validator = new UpdateWarehouseValidator();
            var result = await validator.ValidateAsync(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());

            var loggedUser = await _loggedUser.User();

            warehouse.Name = request.Name;
            warehouse.Location = request.Location;
            warehouse.Capacity = request.Capacity;
            warehouse.Items = request.Items;
            warehouse.Utilization = request.Capacity == 0 ? 0 : (decimal)request.Items / request.Capacity * 100;

            warehouse.UpdatedByUserId = loggedUser.Id;
            warehouse.UpdatedAt = DateTime.UtcNow;

            await _writeOnlyRepository.UpdateAsync(warehouse);
            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseWarehouseJson>(warehouse);

            response.UpdatedByName = loggedUser.Name;
            response.UpdatedByUserId = loggedUser.Id;
            response.UpdatedAt = DateTime.UtcNow;
            return response;
        }
    }
}
