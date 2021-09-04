using Book_Store.Data.Services;
using Book_Store.Data.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("get-books")]
        public IActionResult GetBooks()
        {
            var books = _booksService.GetAllBooks();
            return Ok(books);
        }
        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var _book = _booksService.GetBookById(id);
            if (_book != null)
                return Ok(_book);
            else
                return NotFound();
        }
        [HttpPost("add-book")]
        public IActionResult AddBookWithAuthors([FromBody] BookVM book)
        {
            _booksService.AddBookWithAuthors(book);
            return Ok();
        }
        [HttpDelete("delete-book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _booksService.DeleteBook(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {

            var _book = _booksService.UpdateBookById(id, book);
            return Ok(_book);
        }

    }
}
