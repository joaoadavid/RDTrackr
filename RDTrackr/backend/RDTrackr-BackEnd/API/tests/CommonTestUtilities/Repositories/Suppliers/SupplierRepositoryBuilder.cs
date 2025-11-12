using Moq;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Suppliers;

namespace CommonTestUtilities.Repositories.Suppliers
{
    public class SupplierRepositoryBuilder
    {
        private readonly Mock<ISupplierReadOnlyRepository> _read = new();
        private readonly Mock<ISupplierWriteOnlyRepository> _write = new();

        public SupplierRepositoryBuilder WithList(List<Supplier> suppliers)
        {
            _read.Setup(r => r.GetAllAsync()).ReturnsAsync(suppliers);
            return this;
        }

        public SupplierRepositoryBuilder GetById(User user, Supplier? supplier)
        {
            if (supplier != null)
            {
                _read.Setup(r => r.GetByIdAsync(supplier.Id, user))
                     .ReturnsAsync(supplier);
            }
            else
            {
                _read.Setup(r => r.GetByIdAsync(It.IsAny<long>(), user))
                     .ReturnsAsync((Supplier?)null);
            }

            return this;
        }


        public SupplierRepositoryBuilder ExistsForUser(Supplier supplier)
        {
            _read.Setup(r => r.GetByIdAsync(
                    supplier.Id,
                    It.Is<User>(u => u.Id == supplier.CreatedByUserId)))
                .ReturnsAsync(supplier);

            return this;
        }


        public SupplierRepositoryBuilder NotExistsForUser(long id, User user)
        {
            _read.Setup(r => r.GetByIdAsync(id, user)).ReturnsAsync((Supplier?)null);
            return this;
        }

        public SupplierRepositoryBuilder ExistsWithEmail(string email)
        {
            _read.Setup(r => r.ExistsWithEmail(email)).ReturnsAsync(true);
            return this;
        }

        public SupplierRepositoryBuilder ExistsSupplierWithEmail(string email, long id)
        {
            _read.Setup(r => r.ExistsSupplierWithEmail(email, id)).ReturnsAsync(true);
            return this;
        }

        public SupplierRepositoryBuilder Add()
        {
            _write.Setup(r => r.AddAsync(It.IsAny<Supplier>()))
                  .Returns(Task.CompletedTask);
            return this;
        }

        public SupplierRepositoryBuilder Update()
        {
            _write.Setup(r => r.UpdateAsync(It.IsAny<Supplier>()))
                  .Returns(Task.CompletedTask);
            return this;
        }

        public SupplierRepositoryBuilder Delete()
        {
            _write.Setup(r => r.DeleteAsync(It.IsAny<Supplier>()))
                  .Returns(Task.CompletedTask);
            return this;
        }

        public ISupplierReadOnlyRepository BuildRead() => _read.Object;
        public ISupplierWriteOnlyRepository BuildWrite() => _write.Object;
    }
}
