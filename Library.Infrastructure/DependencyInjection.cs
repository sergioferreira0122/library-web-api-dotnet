using Library.Domain.Abstractions;
using Library.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ConnectionContext>();

            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IBookRepository, BookRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
