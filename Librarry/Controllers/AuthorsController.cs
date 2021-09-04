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
    public class AuthorsController : ControllerBase
    {

        private AuthorService _authorsService;
        public AuthorsController(AuthorService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("get-all-author")]
        public IActionResult GetAllAuthor(string sortBy, string searchString, int pageNumber)
        {
            try
            {
                var _result = _authorsService.GetAllAuthor(sortBy, searchString, pageNumber);
                return Ok(_result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, we could not load publoshers");
            }
        }

        [HttpPost("add-author")]
        public IActionResult AddBook([FromBody] AuthorVM author)
        {
            Console.WriteLine(author);
            _authorsService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("get-authors")]
        public IActionResult GetAuthors()
        {
            var authors = _authorsService.GetAuthors();
            return Ok(authors);
        }
        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorsService.GetAuthorById(id);
            if (author != null)
                return Ok(author);
            else
                return NotFound();
        }
        [HttpPost("add-author")]
        public IActionResult AddPublisher([FromBody] AuthorVM author)
        {
            var newAuthor = _authorsService.AddAuthor(author);
            return Created(nameof(AddPublisher), newAuthor);
        }
        [HttpDelete("delete-author/{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorsService.DeleteAuthor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-author/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorVM author)
        {
            var updatedAuthor = _authorsService.UpdateAuthorById(id, author);
            return Ok(updatedAuthor);
        }

    }
}
