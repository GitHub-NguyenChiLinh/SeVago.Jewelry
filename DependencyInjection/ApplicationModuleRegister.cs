using Repositories.Implementation;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using Mappers;

namespace DependencyInjection {
    public static class ApplicationModuleRegister { 
        public static IServiceCollection AddRepositories(this IServiceCollection services){
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            return services;
        }

        public static IServiceCollection AddServices (this IServiceCollection services) {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services) {
            services.AddAutoMapper(typeof(OrderMapperProfile));
            return services;
        }
    }
}