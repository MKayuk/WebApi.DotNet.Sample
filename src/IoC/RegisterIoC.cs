using WebApi.DotNet.Sample.Data.Repository;
using WebApi.DotNet.Sample.Data.Repository.Interface;
using WebApi.DotNet.Sample.Services.Interface;

namespace WebApi.DotNet.Sample.IoC
{
    public static class RegisterIoC
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, Services.CustomerService>();

            return services;
        }
    }
}
