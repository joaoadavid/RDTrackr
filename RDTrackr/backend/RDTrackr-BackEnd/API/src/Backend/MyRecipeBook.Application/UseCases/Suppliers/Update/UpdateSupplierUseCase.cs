using AutoMapper;
using FluentValidation;
using RDTrackR.Application.UseCases.Warehouses;
using RDTrackR.Communication.Requests.Supplier;
using RDTrackR.Communication.Requests.Warehouse;
using RDTrackR.Communication.Responses.Supplier;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Suppliers;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Suppliers.Update
{
    public class UpdateSupplierUseCase : IUpdateSupplierUseCase
    {
        private readonly ISupplierReadOnlyRepository _readRepository;
        private readonly ISupplierWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSupplierUseCase(
            ISupplierReadOnlyRepository readRepository,
            ISupplierWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseSupplierJson> Execute(long id, RequestUpdateSupplierJson request)
        {
            await Validate(id, request);

            var supplier = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ResourceMessagesException.SUPPLIER_NOT_FOUND);

            supplier.Name = request.Name;
            supplier.Contact = request.Contact;
            supplier.Email = request.Email;
            supplier.Phone = request.Phone;
            supplier.Address = request.Address;

            await _writeRepository.UpdateAsync(supplier);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseSupplierJson>(supplier);
        }

        private async Task Validate(long id, RequestUpdateSupplierJson request)
        {
            var validator = new UpdateSupplierValidator();
            var result = await validator.ValidateAsync(request);

            if (await _readRepository.ExistsSupplierWithEmail(request.Email, id))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.Email),
                    ResourceMessagesException.SUPPLIER_EMAIL_DUPLICATE));

            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }


    }
}
