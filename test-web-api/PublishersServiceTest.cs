using Book_Store.Data;
using Book_Store.Data.Models;
using Book_Store.Data.Services;
using Book_Store.Data.ViewsModel;
using Microsoft.EntityFrameworkCore;
using my_books.Data.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace test_web_api
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "WebAPIi-9")
            .Options;

        AppDbContext _context;
        PublishersService _publishersService;
        BooksService _booksService;
        AuthorService _authorsService;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _publishersService = new PublishersService(_context);
        }
        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Name="Publisher 1"
                },
                new Publisher()
                {
                    Name="Publisher 2"
                },
                new Publisher()
                {
                    Name="Publisher 3"
                },
                new Publisher()
                {
                    Name="Publisher 4"
                },
                new Publisher()
                {
                    Name="Publisher 5"
                },
                new Publisher()
                {
                    Name="Publisher 6"
                },
                new Publisher()
                {
                    Name="Publisher 7"
                },
                new Publisher()
                {
                    Name="Publisher 8"
                },
                new Publisher()
                {
                    Name="Publisher 9"
                }

            };
            _context.Publishers.AddRange(publishers);
            _context.SaveChanges();
            var books = new List<Book>
            {
                new Book()
                {
                    Title = "Book 1",
                    PublisherId = 1
                },
                new Book()
                {
                    Title = "Book 2",
                    PublisherId = 2
                },
                new Book()
                {
                    Title = "Book 3",
                    PublisherId = 3
                },
                new Book()
                {
                    Title = "Book 4",
                    PublisherId = 4
                },
                new Book()
                {
                    Title = "Book 5",
                    PublisherId = 5
                },
                new Book()
                {
                    Title = "Book 6",
                    PublisherId = 6
                },
                new Book()
                {
                    Title = "Book 7",
                    PublisherId = 7
                },
                new Book()
                {
                    Title = "Book 8",
                    PublisherId = 8
                },
                new Book()
                {
                    Title = "Book 9",
                    PublisherId = 9
                }
            };
            _context.Books.AddRange(books);
            _context.SaveChanges();
            var authors = new List<Author>
            {
                new Author()
                {
                    FullName = "Author 1",
                },
                new Author()
                {
                    FullName = "Author 2",
                },
                new Author()
                {
                    FullName = "Author 3",
                },
                new Author()
                {
                    FullName = "Author 4",
                },
                new Author()
                {
                    FullName = "Author 5",
                },
                new Author()
                {
                    FullName = "Author 6",
                },
                new Author()
                {
                    FullName = "Author 7",
                },
                new Author()
                {
                    FullName = "Author 8",
                },
                new Author()
                {
                    FullName = "Author 9",
                }
            };
            _context.Authors.AddRange(authors);
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void ClenUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSort_WithNoSerch_WithNoPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers("as", "", null);

            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithSortDesc_WithNoSerch_WithNoPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers("desc", "", null);

            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test, Order(3)]
        public void GetPublisherById_Test()
        {
            var result = _publishersService.GetPublisherById(4);

            Assert.That(result.Name, Is.EqualTo("Publisher 4"));
        }

        [Test, Order(4)]
        public void AddPublisher_Test()
        {
            var result = _publishersService.AddPublisher(new PublisherVM() { Name = "NEwPublisher" });

            Assert.That(result.Id, Is.EqualTo(26));
        }

        [Test, Order(5)]
        public void GetAllAuthor1_Test()
        {
            var result = _authorsService.GetAllAuthor("as", "", null);
            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test, Order(6)]
        public void GetAllAuthor2_Test()
        {
            var result = _authorsService.GetAllAuthor("desc", "", null);

            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test, Order(7)]
        public void GetPublisherData()
        {
            var result = _publishersService.GetPublisherData(3);

            Assert.That(result.BookAuthors[0].BookName, Is.EqualTo("Book 3"));
        }

        [Test, Order(8)]
        public void EditPublisher()
        {
            _publishersService.UpdatePublisherById(9, new PublisherVM() { Name = "Publisher9" });
            var result = _publishersService.GetPublisherById(9);
            Assert.That(result.Name, Is.EqualTo("Publisher9"));
        }

        [Test, Order(9)]
        public void DeletePublisher()
        {
            _publishersService.DeletePublisher(9);
            var result = _publishersService.GetPublisherById(9);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test, Order(10)]
        public void GetBooks()
        {
            var result = _booksService.GetBooks();
            Assert.That(result.Count(), Is.EqualTo(8));
        }

        [Test, Order(11)]
        public void GetBookById()
        {
            var result = _booksService.GetBookById(1);
            Assert.That(result.Title, Is.EqualTo("Book 1"));
        }

        [Test, Order(12)]
        public void EditBook()
        {
            _booksService.UpdateBookById(8, new BookVM() { Title = "Book8" });
            var result = _booksService.GetBookById(8);
            Assert.That(result.Title, Is.EqualTo("Book8"));
        }

        [Test, Order(13)]
        public void AddBook()
        {
            _booksService.AddBookWithAuthors(new BookVM() { Title = "Book 10", PublisherId = 1, AuthorIds = new List<int>() { 9 } });

            var result = _booksService.GetBookById(10);
            Assert.That(result.Title, Is.EqualTo("Book 10"));
            Assert.That(result.AuthorNames[0], Is.EqualTo("Author 9"));
        }

        [Test, Order(14)]
        public void DeleteBook()
        {
            _booksService.DeleteBook(8);
            var result = _booksService.GetBookById(8);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test, Order(15)]
        public void GetAuthors()
        {
            var result = _authorsService.GetAuthors();

            Assert.That(result.Count(), Is.EqualTo(9));
        }

        [Test, Order(16)]
        public void GetAuthorById()
        {
            var result = _authorsService.GetAuthorById(9);

            Assert.That(result.FullName, Is.EqualTo("Author 9"));
        }

        [Test, Order(17)]
        public void AddAuthor()
        {
            var result = _authorsService.AddAuthor(new AuthorVM() { FullName = "Author 10" });

            Assert.That(result.FullName, Is.EqualTo("Author 10"));
        }

        [Test, Order(18)]
        public void EditAuthor()
        {
            _authorsService.UpdateAuthorById(9, new AuthorVM() { FullName = "Author9" });
            var result = _authorsService.GetAuthorById(9);
            Assert.That(result.FullName, Is.EqualTo("Author9"));
        }

        [Test, Order(19)]
        public void DeleteAuthor()
        {
            _authorsService.DeleteAuthor(9);
            var result = _authorsService.GetAuthorById(9);
            Assert.That(result, Is.EqualTo(null));
        }

        

        

    }
}