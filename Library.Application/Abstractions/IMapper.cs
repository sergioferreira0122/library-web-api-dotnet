namespace Library.Application.Abstractions
{
    public interface IMapper<in TSource, TResult>
    {
        TResult Map(TSource data, TResult target);
    }
}
