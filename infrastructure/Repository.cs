using System.Runtime.CompilerServices;
using Dapper;
using Npgsql;
using Tests;
using Book = infrastructure.DataModels.Book;

namespace infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        var sql = $@"select * from library.books;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Book>(sql);
        }
    }


    public Book CreateBook(string title, string publisher, string coverImgUrl)
    {
        var sql = $@"INSERT INTO library.books (title, publisher, coverimgurl)
                VALUES (@title, @publisher, @coverImgUrl)
                RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Book>(sql, new{title, publisher, coverImgUrl});
        }
    }
    public Book UpdateBook(int id, string title, string publisher, string coverImgUrl)
    {
        var sql = @"UPDATE library.books 
        SET title =@title,
            publisher = @publisher,
            coverimgurl = @coverImgUrl
            WHERE bookid = @id
            RETURNING *;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Book>(sql, new{id, title, publisher, coverImgUrl});
        }
    }
    public bool DeleteBook(int id)
    {
        var sql = $@"
        DELETE FROM library.books WHERE bookId = @id";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id }) == 1;
        }
        
    }


}