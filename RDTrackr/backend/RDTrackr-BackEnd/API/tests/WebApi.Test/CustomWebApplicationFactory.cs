using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Entities.Products;
using CommonTestUtilities.Entities.Warehouses;
using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.PurchaseOrders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Enums;
using RDTrackR.Infrastructure.DataAccess;


namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private RDTrackR.Domain.Entities.User _user = default!;
        private RDTrackR.Domain.Entities.Warehouse _warehouse = default!;
        private RDTrackR.Domain.Entities.Product _product = default!;
        private RDTrackR.Domain.Entities.PurchaseOrder _purchaseOrder = default!;
        private string _password = string.Empty;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services => 
                {
                    var descriptor = services.SingleOrDefault(
                        descriptor => descriptor.ServiceType == typeof(DbContextOptions<RDTrackRDbContext>)
                        );
                    if(descriptor is not null)
                        services.Remove(descriptor);

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    var blobStorage = new BlobStorageServiceBuilder().Build();
                    services.AddScoped(option =>blobStorage);

                    services.AddDbContext<RDTrackRDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });
                    using var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<RDTrackRDbContext>();
                    dbContext.Database.EnsureDeleted();

                    StartDatabase(dbContext);

                } ) ;
        }

        public string GetEmail() => _user.Email;
        public Guid GetUserIdentifier() => _user.UserIdentifier;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;
        public RDTrackR.Domain.Entities.User GetUser() => _user;
        public long GetProductId() => _product.Id;
        public long GetWarehouseId() => _warehouse.Id;
        public long GetPurchaseOrderId() => _purchaseOrder.Id;
        public string GetProductName() => _product.Name;


        private void StartDatabase(RDTrackRDbContext dbContext)
        {
            (_user, _password) = UserBuilder.Build();

            _warehouse = WarehouseBuilder.Build(_user);
            _product = ProductBuilder.Build(createdBy: _user);
            _purchaseOrder = PurchaseOrderBuilder.Build(createdByUserId:_user.Id,productId:_product.Id);

            dbContext.Users.Add(_user);
            dbContext.Warehouses.Add(_warehouse);
            dbContext.Products.Add(_product);
            dbContext.PurchaseOrders.Add(_purchaseOrder);

            dbContext.SaveChanges();
        }

    }


}
