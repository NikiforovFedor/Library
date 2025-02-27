using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationContext _context;
        public AuthorsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors.Include(a => a.Books).ToList();// Сохранение авторов в список 

            return View(authors);
        }

        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorModel author)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                using (var context = new ApplicationContext(optionsBuilder.Options))
                {
                    bool exists = context.Authors.Any(a => a.Name == author.Name.Trim());
                    if (exists)
                    {
                        Console.WriteLine("Такая модель уже есть в бд");
                        return View();
                    }
                    else 
                    {
                        _context.Authors.Add(author);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Authors");
                    }
                }
            }
            catch
            {
                return View(author);
            }
        }
    }
}
