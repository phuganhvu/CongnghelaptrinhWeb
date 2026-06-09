using Lesson3_CNLTWeb.Models;

namespace Lesson3_CNLTWeb.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll(string? search = null, string? sort = null);
        Book? GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
