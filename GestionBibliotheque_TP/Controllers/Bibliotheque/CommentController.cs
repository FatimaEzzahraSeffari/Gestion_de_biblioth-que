using GestionBibliotheque_TP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionBibliotheque_TP.Controllers.Bibliotheque
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                if (comment.ImageFile != null && comment.ImageFile.Length > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(comment.ImageFile.FileName);
                    var imagePath = Path.Combine("wwwroot/img", newFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await comment.ImageFile.CopyToAsync(stream);
                    }

                    comment.ImagePath = "/img/" + newFileName;
                }

                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Home", "Home");

            }

            return View(comment);
        }


        public IActionResult Home()
        {
            try
            {
                var comments = _context.Comment.ToList();
                Console.WriteLine($"Number of books: {comments.Count}");

                return View(comments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving books: {ex.Message}");
                throw;
            }
        }
    }
}
