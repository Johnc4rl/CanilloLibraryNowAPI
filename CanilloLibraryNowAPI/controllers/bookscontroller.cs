using CanilloLibraryNowAPI.models;
using Microsoft.AspNetCore.Mvc;

namespace CanilloLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "The Time I Got Reincarnated as a Slime", Author = "FUSE", Genre = "FICTION, ISEKAI", Available = true, PublishedYear = 2013 },
            new Book { Id = 2, Title = "Alice's Adventures in Wonderland", Author = "Lewis Carroll", Genre = "FICTION", Available = true, PublishedYear = 1865 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Books retrieved" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });

            return Ok(new { status = "success", data = book, message = "Book retrieved" });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            if (newBook == null)
                return BadRequest(new { status = "error", message = "Invalid book data" });

            newBook.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;

            books.Add(newBook);

            return CreatedAtAction(nameof(GetById), new { id = newBook.Id },
                new { status = "success", data = newBook, message = "Book created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new { status = "success", data = book, message = "Book updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });

            books.Remove(book);

            return Ok(new { status = "success", data = (object?)null, message = "Book deleted" });
        }
    }
}