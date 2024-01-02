namespace Library.Application.Abstractions
{
    public interface IMapper<TSource, TResult>
    {
        TResult Map(TSource data, TResult target);
    }
}
