namespace Library.Application.Features.Books
{
    public class BookErrors
    {
        public static readonly Error BookNotFound = new(
            "Books.NotFound",
            "Book not found");

        public static readonly Error UpdateBookNotSameIdFromBodyAndParameter = new(
            "Books.Update",
            "Book Id from body is not equals to parameter Book Id");
    }
}
