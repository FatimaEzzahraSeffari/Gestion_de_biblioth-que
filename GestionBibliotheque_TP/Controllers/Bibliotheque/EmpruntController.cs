using GestionBibliotheque_TP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionBibliotheque_TP.Controllers.Bibliotheque
{
    public class EmpruntController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpruntController(ApplicationDbContext context)
        {
            _context = context;

        }
        public ActionResult ListeEmprunt()
        {
            var emprunts = _context.Emprunt.ToList();
            return View(emprunts);
        }


        [HttpGet]
        public IActionResult CreateEmprunt(int livreId)
        {
            var livre = _context.Livre.Find(livreId);

            if (livre == null || livre.EstEmprunte)
            {
                return RedirectToAction("ListeLivres");
            }

            var abonnes = _context.Abonne
                .Select(a => new { a.Id, NomPrenom = $"{a.Nom} {a.Prenom}" })
                .ToList();

            ViewBag.LivreId = livreId;
            ViewBag.Abonnes = new SelectList(abonnes, "Id", "NomPrenom"); 

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmprunt(Emprunt emprunt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var nbEmpruntsAbonne = _context.Emprunt
                        .Where(e => e.AbonneId == emprunt.AbonneId)
                        .Count();

                    Console.WriteLine($"Nombre d'emprunts par abonné : {nbEmpruntsAbonne}");

                    if (nbEmpruntsAbonne >= 2)
                    {
                        ModelState.AddModelError(nameof(emprunt.AbonneId), "L'abonné a atteint la limite d'emprunts (2 emprunts maximum).");
                    }
                    else
                    {
                        var livre = await _context.Livre.FindAsync(emprunt.LivreId);

                        if (livre != null && livre.EstEmprunte == false)
                        {
                            var dateFinEmprunt = emprunt.DateEmprunt.AddDays(14); 
                            Console.WriteLine($"Date de fin de l'emprunt : {dateFinEmprunt}");

                            if (dateFinEmprunt >= emprunt.DateRetour) 
                            {
                                _context.Emprunt.Add(emprunt);
                                livre.EstEmprunte = true; 
                                await _context.SaveChangesAsync();
                                TempData["SuccessMessage"] = "L'emprunt a été ajouté avec succès.";
                                return RedirectToAction("ListeEmprunt");
                            }
                            else
                            {
                                ModelState.AddModelError(nameof(emprunt.DateRetour), "La date de retour doit être dans les 2 semaines suivant la date d'emprunt.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(emprunt.LivreId), "Le livre sélectionné n'est pas disponible.");
                        }
                    }
                }

                var abonnes = _context.Abonne
                    .Select(a => new { a.Id, NomPrenom = $"{a.Nom} {a.Prenom}" })
                    .ToList();

                ViewBag.Abonnes = new SelectList(abonnes, "Id", "NomPrenom"); 

                return View(emprunt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création d'emprunt : {ex.Message}");
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditEmprunt(int empruntId)
        {
            var emprunt = await _context.Emprunt.FindAsync(empruntId);

            if (emprunt == null)
            {
                return NotFound();
            }

            var abonnes = await _context.Abonne
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Nom} {a.Prenom}" })
                .ToListAsync();

            ViewBag.Abonnes = abonnes;

            return View(emprunt);
        }





        [HttpPost]
        public async Task<IActionResult> EditEmprunt(Emprunt emprunt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingEmprunt = await _context.Emprunt.FindAsync(emprunt.Id);

                    if (existingEmprunt == null)
                    {
                        return NotFound();
                    }

                    existingEmprunt.AbonneId = emprunt.AbonneId;
                    existingEmprunt.DateEmprunt = emprunt.DateEmprunt;
                    existingEmprunt.DateRetour = emprunt.DateRetour;

                    

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Les informations de l'emprunt ont été modifiées avec succès.";
                    return RedirectToAction("ListeEmprunt");
                }

                
                var abonnes = await _context.Abonne
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Nom} {a.Prenom}" })
                    .ToListAsync();

                ViewBag.Abonnes = abonnes;

                return View(emprunt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la modification de l'emprunt : {ex.Message}");
                throw;
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var emprunt = _context.Emprunt
                .Include(e => e.Abonne) 
                .FirstOrDefault(e => e.Id == id);

            if (emprunt == null)
            {
                return NotFound();
            }

            var abonnes = _context.Abonne
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Nom} {a.Prenom}" })
                .ToList();
            return View(emprunt);
        }


        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprunt = await _context.Emprunt.FindAsync(id);

            if (emprunt == null)
            {
                return NotFound();
            }

            var livre = await _context.Livre.FindAsync(emprunt.LivreId);

            if (livre != null)
            {
                livre.EstEmprunte = false;
            }

            _context.Emprunt.Remove(emprunt);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListeEmprunt");
        }



    }
}