using BookStore.Domain;
using BookStore.Domain.Contracts;
using BookStore.Utils.Attributes;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.OutputCache.V2;

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
        [DeflateCompression]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public List<Book> Get()
        {
            /*DeflateCompression - Criado para comprimir os dados na transferência. Atributo existente na web*/
            return repository.Get(0, 30);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
        }


    }
}
