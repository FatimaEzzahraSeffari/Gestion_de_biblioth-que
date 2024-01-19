using GestionBibliotheque_TP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace GestionBibliotheque_TP.Controllers.Bibliotheque
{
    public class LivreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivreController(ApplicationDbContext context)
        {
            _context = context;
        }

        






        public IActionResult ListeLivres()
        {
            var livres = _context.Livre.ToList();
            return View(livres);
        }
        [HttpGet]
        public IActionResult CreateLivre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLivre(Livre livre)
        {
            if (ModelState.IsValid)
            {
                if (livre.ImageFile != null && livre.ImageFile.Length > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(livre.ImageFile.FileName);
                    var imagePath = Path.Combine("wwwroot/img", newFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await livre.ImageFile.CopyToAsync(stream);
                    }

                    livre.ImagePath = "/img/" + newFileName;
                }

                _context.Livre.Add(livre);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Le livre a été ajouté avec succès.";
                return RedirectToAction("ListeLivres");
            }

            return View(livre);
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

                    return RedirectToAction("ListeLivres");
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
