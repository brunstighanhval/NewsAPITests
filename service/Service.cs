using infrastructure;
using infrastructure.DataModels;

namespace service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        try
        {
            return _repository.GetAllBooks();
        }
        catch (Exception)
        {
            throw new Exception("Could not get books");
        }
    }

    public Book CreateBook(string title, string publisher, string coverImgUrl)
    {
        try
        {
            return _repository.CreateBook(title, publisher, coverImgUrl);
        }
        catch (Exception )
        {
            throw new Exception("Could not create new book");
        }
    }
    public Book UpdateBook(int id, string title, string publisher, string coverImgUrl)
    {
        try
        {
            return _repository.UpdateBook(id, title, publisher, coverImgUrl);
        }
        catch (Exception)
        {
            throw new Exception("could not update the book");
        }
    }
    public bool DeleteBook(int id)
    {
        try
        {
            return _repository.DeleteBook(id);
        }
        catch (Exception e)
        {
            throw new Exception("could not delete the book");
        }
    }

}