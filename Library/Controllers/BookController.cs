using Library.Context;
using Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public BookController(AppDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var produtos = _dbContext.Books.AsNoTracking().ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            return produtos;
        }
        [HttpGet("{id:int}", Name = "GetBook")]
        public  ActionResult<Book> Get(int id) {
            var produtoId = _dbContext.Books.Find(id);
            if(produtoId is null)
            {
                return BadRequest("Id não é valido");
            }
            return Ok(produtoId);
        }
        [HttpPost]
        public ActionResult Post(Book book)
        {
            if(book is null)
            {
                return BadRequest();
            }
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return Ok(book);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Book book) {
            if(book.ID != id)
            {
                return BadRequest("Id diferente");
            }
            var bookId = _dbContext.Books.Find(id);
            bookId.Title= book.Title;
            bookId.Description= book.Description;
            bookId.Price= book.Price;
            bookId.Pages= book.Pages;
            bookId.Author= book.Author;
            _dbContext.SaveChanges();
            return Ok(bookId);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) {
            var produtoId = _dbContext.Books.Find(id);
            if(produtoId is null){
                return BadRequest();
            }
            _dbContext.Remove(produtoId);
            _dbContext.SaveChanges();
            return NotFound();
        }
    }
}
