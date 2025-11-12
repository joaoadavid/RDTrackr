using AutoMapper;
using RDTrackR.Domain.Repositories;
using RDTrackR.Communication.Requests.Product;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using RDTrackR.Domain.Repositories.Products;
using RDTrackR.Application.UseCases.Products;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Services.LoggedUser;

namespace RDTrackR.Application.UseCases.Product.Update
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductReadOnlyRepository _readRepository;
        private readonly IProductWriteOnlyRepository _writeRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductUseCase(
            IProductReadOnlyRepository readRepository,
            IProductWriteOnlyRepository writeRepository,
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(long id, RequestRegisterProductJson request)
        {
            var loggedUser = await _loggedUser.User();

            await Validate(request);

            var product = await _readRepository.GetByIdAsync(id,loggedUser);

            if (product == null)
                throw new NotFoundException(ResourceMessagesException.PRODUCT_NOT_FOUND);

            _mapper.Map(request, product);
            product.UpdatedAt = DateTime.UtcNow;

            await _writeRepository.UpdateAsync(product);
            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestRegisterProductJson request)
        {
            var validator = new ProductValidator();
            var result = await validator.ValidateAsync(request);

            if (result.IsValid.IsFalse())
            {
                throw new ErrorOnValidationException(
                    result.Errors.Select(e => e.ErrorMessage).Distinct().ToList()
                );
            }
        }
    }
}
