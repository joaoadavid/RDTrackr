using AutoMapper;
using RDTrackR.Communication.Responses.PurchaseOrders;
using RDTrackR.Domain.Repositories.PurchaseOrders;
using RDTrackR.Domain.Services.LoggedUser;

namespace RDTrackR.Application.UseCases.PurchaseOrders.GetAll
{
    public class GetPurchaseOrdersUseCase : IGetPurchaseOrdersUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;

        public GetPurchaseOrdersUseCase(
            IPurchaseOrderReadOnlyRepository repository,
            ILoggedUser loggedUser,
            IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<List<ResponsePurchaseOrderJson>> Execute()
        {
            var loggedUser = await _loggedUser.User();
            var orders = await _repository.Get(loggedUser);
            return _mapper.Map<List<ResponsePurchaseOrderJson>>(orders);
        }
    }
}
