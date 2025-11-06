using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Application.UseCases.Recipe.Delete;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Application.UseCases.Recipe.GetById;
using MyRecipeBook.Application.UseCases.Recipe.Image;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Application.UseCases.Recipe.Update;
using MyRecipeBook.Application.UseCases.Token;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Delete.Delete;
using MyRecipeBook.Application.UseCases.User.Delete.Request;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;
using RDTrackR.Application.Services.AutoMapper;
using RDTrackR.Application.UseCases.Dashboard;
using RDTrackR.Application.UseCases.Login.DoLogin;
using RDTrackR.Application.UseCases.Login.Logout;
using RDTrackR.Application.UseCases.Movements.GetAll;
using RDTrackR.Application.UseCases.Movements.Register;
using RDTrackR.Application.UseCases.Overview.Get;
using RDTrackR.Application.UseCases.Product.Delete;
using RDTrackR.Application.UseCases.Product.GetAll;
using RDTrackR.Application.UseCases.Product.Update;
using RDTrackR.Application.UseCases.Products.Register;
using RDTrackR.Application.UseCases.PurchaseOrders.Delete;
using RDTrackR.Application.UseCases.PurchaseOrders.GetAll;
using RDTrackR.Application.UseCases.PurchaseOrders.Register;
using RDTrackR.Application.UseCases.PurchaseOrders.Update;
using RDTrackR.Application.UseCases.StockItems.GetAll;
using RDTrackR.Application.UseCases.StockItems.Register;
using RDTrackR.Application.UseCases.Suppliers.Delete;
using RDTrackR.Application.UseCases.Suppliers.GetAll;
using RDTrackR.Application.UseCases.Suppliers.Register;
using RDTrackR.Application.UseCases.Suppliers.Update;
using RDTrackR.Application.UseCases.User.ChangePassword;
using RDTrackR.Application.UseCases.User.Register;
using RDTrackR.Application.UseCases.User.Update;
using RDTrackR.Application.UseCases.Warehouses.Delete;
using RDTrackR.Application.UseCases.Warehouses.GetAll;
using RDTrackR.Application.UseCases.Warehouses.Register;
using RDTrackR.Application.UseCases.Warehouses.Update;
using Sqids;

namespace RDTrackR.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddUseCases(services);
            AddIdEncoder(services, configuration);
            AddProductUseCases(services);
            AddMovementsUseCases(services);
            AddWarehousesUseCases(services);
            AddStockItemUseCases(services);
            AddSuppliersesUseCases(services);
            AddPurchaseOrderUseCase(services);
            services.AddAutoMapper();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {

            services.AddScoped(option => new AutoMapper.MapperConfiguration(automapperOption =>
            {
                var sqids = option.GetService<SqidsEncoder<long>>()!;

                automapperOption.AddProfile(new AutoMapping(sqids));
            }).CreateMapper());
        }

        private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
        {
            var sqids = new SqidsEncoder<long>(new()
            {
                MinLength = 3,
                Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
            });
            services.AddSingleton(sqids);
        }
        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
            services.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
            services.AddScoped<IGetRecipeByIdUseCase, GetRecipeByIdUseCase>();
            services.AddScoped<IDeleteRecipeUseCase, DeleteRecipeUseCase>();
            services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IGetDashboardUseCase, GetDashboardUseCase>();
            services.AddScoped<IAddUpdateImageCoverUseCase, AddUpdateImageCoverUseCase>();
            services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
            services.AddScoped<IRequestDeleteUserUseCase, RequestDeleteUserUseCase>();
            services.AddScoped<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();
            services.AddScoped<IGetOverviewUseCase, GetOverviewUseCase>();
            services.AddScoped<ILogoutUseCase, LogoutUseCase>();
        }

        private static void AddProductUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();
            services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
            services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
            services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
        }

        private static void AddWarehousesUseCases(IServiceCollection services)
        {
            services.AddScoped<IGetAllWarehousesUseCase, GetAllWarehousesUseCase>();
            services.AddScoped<IRegisterWarehouseUseCase, RegisterWarehouseUseCase>();
            services.AddScoped<IUpdateWarehouseUseCase, UpdateWarehouseUseCase>();
            services.AddScoped<IDeleteWarehouseUseCase, DeleteWarehouseUseCase>();
        }

        private static void AddSuppliersesUseCases(IServiceCollection services)
        {
            services.AddScoped<IDeleteSupplierUseCase, DeleteSupplierUseCase>();
            services.AddScoped<IRegisterSupplierUseCase, RegisterSupplierUseCase>();
            services.AddScoped<IGetAllSuppliersUseCase, GetAllSuppliersUseCase>();
            services.AddScoped<IUpdateSupplierUseCase, UpdateSupplierUseCase>();
        }
        private static void AddMovementsUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterMovementUseCase, RegisterMovementUseCase>();
            services.AddScoped<IGetAllMovementsUseCase, GetAllMovementsUseCase>();
        }

        private static void AddStockItemUseCases(IServiceCollection services)
        {
            services.AddScoped<IGetAllStockItemsUseCase, GetAllStockItemsUseCase>();
            services.AddScoped<IRegisterStockItemUseCase, RegisterStockItemUseCase>();
        }

        private static void AddPurchaseOrderUseCase(IServiceCollection services)
        {
            services.AddScoped<IDeletePurchaseOrderUseCase, DeletePurchaseOrderUseCase>();
            services.AddScoped<IGetPurchaseOrdersUseCase, GetPurchaseOrdersUseCase>();
            services.AddScoped<IRegisterPurchaseOrderUseCase, RegisterPurchaseOrderUseCase>();
            services.AddScoped<IUpdatePurchaseOrderItemsUseCase, UpdatePurchaseOrderItemsUseCase>();
            services.AddScoped<IUpdatePurchaseOrderStatusUseCase, UpdatePurchaseOrderStatusUseCase>();
        }
    }
}
