namespace Library.Application.Features.Clients
{
    public static class ClientErrors
    {
        public static readonly Error ClientNotFound = new(
            "Clients.NotFound",
            "Client not found");

        public static readonly Error PhoneNumberContainLetters = new(
            "Clients.PhoneNumber",
            "Phone number cant have letters");

        public static readonly Error UpdateClientNotSameIdFromBodyAndParameter = new(
            "Clients.Update",
            "Client Id from body is not equals to parameter Client Id");
    }
}