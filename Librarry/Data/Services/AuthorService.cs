using Book_Store.Data.Models;
using Book_Store.Data.Paging;
using Book_Store.Data.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book_Store.Data.Services
{
    public class AuthorService
    {
        private AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAuthors()
        {
            var authors = _context.Authors;
            return authors;
        }
        /*
        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitles = n.Books.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
        */
        public Author AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();

            return _author;
        }

        public void DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(b => b.Id == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Author with id: {id} not found");
            }
        }

        public Author UpdateAuthorById(int authorId, AuthorVM author)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == authorId);

            if (_author != null)
            {
                _author.FullName = author.FullName;

                _context.SaveChanges();
            }

            return _author;
        }

        public Author GetAuthorById(int id)
        {
            var author = _context.Authors.FirstOrDefault(p => p.Id == id);
            if (author != null)
                return author;
            else
                return null;
        }



        public List<Author> GetAllAuthor(string sortBy, string searchString, int? pageNumber)
        {
            var allAuthors= _context.Authors.OrderBy(n => n.FullName).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "as":
                        allAuthors = allAuthors.OrderBy(p => p.FullName).ToList();
                        break;
                    case "desc":
                        allAuthors = allAuthors.OrderByDescending(n => n.FullName).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allAuthors = allAuthors.Where(n => n.FullName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            int pageSize = 6;
            allAuthors = PaginationList<Author>.Create(allAuthors.AsQueryable(), pageNumber ?? 1, pageSize);

            return allAuthors;
        }

    }
}
