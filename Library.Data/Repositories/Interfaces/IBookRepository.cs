﻿
using Library.Model.Model;

namespace Library.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookInfo>> GetAllBooksAsync();
        public Task<int> InsertBook(BookInfo book);
        public Task InsertMassiveBooks();
    }
}
