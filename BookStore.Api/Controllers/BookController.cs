using BookStore.Data.Repositories;
using BookStore.Domain;
using BookStore.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Api.Controllers
{
    [RoutePrefix("api/public/v1")]
    public class BookController : ApiController
    {
        private readonly IBookRepository repository;

        public BookController(IBookRepository pRepository)
        {
            repository = pRepository;
        }

        [Route("livros")]
        public List<Book> Get()
        {
            return repository.Get(0, 30);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
        }


    }
}
