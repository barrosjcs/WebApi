﻿using BookStore.Domain;
using BookStore.Domain.Contracts;
using BookStore.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        #region Read
        //[BasicAuthentication]
        [HttpGet]
        [Route("livros")]
        //[DeflateCompression]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public Task<HttpResponseMessage> Get()
        {
            /*DeflateCompression - Criado para comprimir os dados na transferência. Atributo existente na web*/
            /*Task - Representa uma operação assíncrona*/
            /*HttpResponseMessage - Operador que controla qual é a resposta que deseja enviar do seu serviço*/
            /*BasicAuthentication - Atributo de autenticação do usuário*/
            HttpResponseMessage response;

            try
            {
                var result = repository.GetWithAuthors();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar livros");
                throw;
            }

            var tsk = new TaskCompletionSource<HttpResponseMessage>();
            tsk.SetResult(response);
            return tsk.Task;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        //[DeflateCompression]
        [Route("livros/{id}")]
        public Task<HttpResponseMessage> GetById(int id)
        {
            HttpResponseMessage response;

            try
            {
                var result = repository.GetWithAuthors(id);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar livros");
                throw;
            }

            var tsk = new TaskCompletionSource<HttpResponseMessage>();
            tsk.SetResult(response);
            return tsk.Task;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        //[DeflateCompression]
        [Route("livros/{id}/autores")]
        public Task<HttpResponseMessage> GetAuthors(int id)
        {
            HttpResponseMessage response;

            try
            {
                var result = repository.GetWithAuthors(id).Authors.ToList();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao recuperar autores");
                throw;
            }

            var tsk = new TaskCompletionSource<HttpResponseMessage>();
            tsk.SetResult(response);
            return tsk.Task;
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("livros")]
        public Task<HttpResponseMessage> Put(Book book)
        {
            HttpResponseMessage response;

            try
            {
                repository.Update(book);
                response = Request.CreateResponse(HttpStatusCode.OK, book);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao atualizar os livros");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("livros/{id}")]
        public Task<HttpResponseMessage> Delete(int id)
        {
            HttpResponseMessage response;

            try
            {
                repository.Delete(id);
                response = Request.CreateResponse(HttpStatusCode.OK, "Livro removido com sucesso!");
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao remover o livro");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
        }
    }
}
