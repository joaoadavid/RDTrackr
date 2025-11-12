using Moq;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.PurchaseOrders;

namespace CommonTestUtilities.Repositories.PurchaseOrders
{
    public class PurchaseOrderRepositoryBuilder
    {
        private readonly Mock<IPurchaseOrderReadOnlyRepository> _readMock = new();
        private readonly Mock<IPurchaseOrderWriteOnlyRepository> _writeMock = new();

        public PurchaseOrderRepositoryBuilder GetById(PurchaseOrder order, User user)
        {
            _readMock.Setup(r => r.GetByIdAsync(order.Id,user))
                     .ReturnsAsync(order);
            return this;
        }

        public PurchaseOrderRepositoryBuilder GetAll(PurchaseOrder[] orders, User user)
        {
            _readMock.Setup(r => r.Get(user))
                     .ReturnsAsync(orders.ToList());
            return this;
        }

        public PurchaseOrderRepositoryBuilder Add()
        {
            _writeMock.Setup(r => r.AddAsync(It.IsAny<PurchaseOrder>()))
                      .Returns(Task.CompletedTask);
            return this;
        }

        public PurchaseOrderRepositoryBuilder Update()
        {
            _writeMock.Setup(r => r.UpdateAsync(It.IsAny<PurchaseOrder>()))
                      .Returns(Task.CompletedTask);
            return this;
        }

        public PurchaseOrderRepositoryBuilder Delete()
        {
            _writeMock.Setup(r => r.DeleteAsync(It.IsAny<PurchaseOrder>()))
                      .Returns(Task.CompletedTask);
            return this;
        }

        public IPurchaseOrderReadOnlyRepository BuildRead() => _readMock.Object;
        public IPurchaseOrderWriteOnlyRepository BuildWrite() => _writeMock.Object;
    }
}
