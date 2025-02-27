using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationContext _context;
        public BooksController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel book)
        {
            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == book.Author.Name.Trim());

                if (author == null)
                {
                    ModelState.AddModelError("Name", "Такого автора нет в базе данных");
                    Console.WriteLine("Автора не существуе в базе данных");

                    return View(book);
                }

                var existsTitleTask = await _context.Books.AnyAsync(b => b.Title == book.Title.Trim());

                int existsAuthor = author.Id;
                bool existsTitleTaskResoult = existsTitleTask;

                if (existsTitleTaskResoult)
                {
                    ModelState.AddModelError("Title", "Такая книга уже есть в базе данных");
                    Console.WriteLine("Книга уже есть в базе данных");
                    return View(book);
                }
                BookModel newbook = new BookModel
                {
                    AuthorId = author.Id,
                    Title = book.Title,
                };

                _context.Books.Add(newbook);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Authors");
            }
            catch
            {
                Console.WriteLine("Модель невалидна");
                return View(book);
            }
        }
    }
}
