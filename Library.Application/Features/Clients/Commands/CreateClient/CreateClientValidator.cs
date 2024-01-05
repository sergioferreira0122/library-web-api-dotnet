using Library.Application.Abstractions;

namespace Library.Application.Features.Clients.Commands
{
    public class CreateClientValidator : IValidator<CreateClientCommand>
    {
        public Task<Result> IsValidAsync(
            CreateClientCommand tCommand,
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