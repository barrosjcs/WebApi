﻿using BookStore.Domain;
using System.Collections.Generic;

namespace BookeStore.Domain.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> GetWithAuthors(int skip = 0, int take = 25);
        Book GetWithAuthors(int id);
    }
}