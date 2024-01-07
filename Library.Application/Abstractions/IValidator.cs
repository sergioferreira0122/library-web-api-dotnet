namespace Library.Application.Abstractions
{
    public interface IValidator<TCommand>
        where TCommand : ICommand
    {
        Task<Result> IsValidAsync(TCommand tCommand, CancellationToken cancellationToken);
    }
}