using Library.Application.Abstractions;

namespace Library.Application.Features.Clients.Commands
{
    public class CreateClientValidator : IValidator<CreateClientCommand>
    {
        public async Task<Result> IsValidAsync(
            CreateClientCommand tCommand,
            CancellationToken cancellationToken)
        {
            if (tCommand.PhoneNumber.Any(char.IsLetter))
            {
                return ClientErrors.PhoneNumberContainLetters;
            }

            return Result.Success();
        }
    }
}