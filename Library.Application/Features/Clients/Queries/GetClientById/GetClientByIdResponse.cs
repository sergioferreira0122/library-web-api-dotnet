namespace Library.Application.Features.Clients.Queries
{
    public class GetClientByIdResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}