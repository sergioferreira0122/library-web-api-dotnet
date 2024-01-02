using Library.Application.Abstractions;
using Library.Application.Features.Clients;

namespace Library.Application.Features.Clients.Commands
{
    public class UpdateClientValidator : IValidator<UpdateClientCommand>
    {
        public async Task<Result> IsValidAsync(
            UpdateClientCommand tCommand,
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
