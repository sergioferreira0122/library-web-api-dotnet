using Library.Application.Abstractions;
using Library.Application.Features.Bookings.Queries;
using Library.Application.Features.Books.Commands;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Clients.Commands;
using Library.Application.Features.Clients.Queries;
using Library.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Clients
            services.AddTransient<IValidator<CreateClientCommand>, CreateClientValidator>();
            services.AddTransient<IValidator<UpdateClientCommand>, UpdateClientValidator>();

            services.AddTransient<IMapper<CreateClientCommand, Client>, CreateClientMapper>();
            services.AddTransient<IMapper<UpdateClientCommand, Client>, UpdateClientMapper>();
            services.AddTransient<IMapper<ICollection<Client>, ICollection<GetAllClientsResponse>>, GetAllClientsMapper>();
            services.AddTransient<IMapper<Client, GetClientByIdResponse>, GetClientByIdMapper>();
            //Clients

            //Books
            services.AddTransient<IMapper<CreateBookCommand, Book>, CreateBookMapper>();
            services.AddTransient<IMapper<UpdateBookCommand, Book>, UpdateBookMapper>();
            services.AddTransient<IMapper<ICollection<Book>, ICollection<GetAllBooksResponse>>, GetAllBooksMapper>();
            services.AddTransient<IMapper<Book, GetBookByIdResponse>, GetBookByIdMapper>();
            //Books

            //Bookings
            services.AddTransient<IMapper<ICollection<Booking>, ICollection<GetAllBookingsResponse>>, GetAllBookingsMapper>();
            services.AddTransient<IMapper<ICollection<Booking>, ICollection<GetAllBookingsFromClientResponse>>, GetAllBookingsFromClientMapper>();
            services.AddTransient<IMapper<Booking, GetBookingByIdResponse>, GetBookingByIdMapper>();
            //Bookings

            return services;
        }
    }
}