using Library.Application.Abstractions;
using Library.Application.Features.Clients;

namespace Library.Application.Features.Clients.Commands
{
    public class UpdateClientValidator : IValidator<UpdateClientCommand>
    {
        public Task<Result> IsValidAsync(
            UpdateClientCommand tCommand,
            CancellationToken cancellationToken)
        {
            if (tCommand.PhoneNumber.Any(char.IsLetter))
            {
                return Task.FromResult<Result>(ClientErrors.PhoneNumberContainLetters);
            }

            return Task.FromResult(Result.Success());
        }
    }
}
