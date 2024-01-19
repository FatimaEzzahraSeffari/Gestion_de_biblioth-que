using GestionBibliotheque_TP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Xml.Linq;

namespace GestionBibliotheque_TP.Controllers
{
    public class HomeController : Controller
    {
        
       
        private readonly ApplicationDbContext _context; 
        public HomeController( ApplicationDbContext context)
        {
           
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            try
            {
                var livres = _context.Livre.ToList();
                var comments = _context.Comment.ToList();

                var combinedData = Tuple.Create(comments, livres);

                Console.WriteLine($"Number of books: {livres.Count}");

                ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
                return View(combinedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving books: {ex.Message}");
                throw; 
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public IActionResult RestituerLivre(int id)
        {
            var livre = _context.Livre.FirstOrDefault(l => l.Id == id);

            if (livre == null)
            {
                return NotFound();
            }

            if (livre.EstEmprunte)
            {
                return View("RestituerLivre", livre);
            }

            return BadRequest("Le livre n'est pas actuellement emprunté.");
        }

        [HttpPost]
        public IActionResult RestituerLivreConfirmation(int livreId)
        {

            var livre = _context.Livre.Find(livreId);

            if (livre != null && livre.EstEmprunte)
            {
                var emprunt = _context.Emprunt.FirstOrDefault(e => e.LivreId == livre.Id);

                if (emprunt != null)
                {
                    livre.EstEmprunte = false;

                    _context.Emprunt.Remove(emprunt);

                    _context.SaveChanges();

                    return RedirectToAction("Home", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Aucun emprunt en cours pour ce livre.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Le livre n'est pas emprunté ou n'est pas valide.");
            }

            return View("RestituerLivre", livre);
        }

    }
}
