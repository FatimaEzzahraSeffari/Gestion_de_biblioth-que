using GestionBibliotheque_TP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionBibliotheque_TP.Controllers.Bibliotheque
{
    public class AbonneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbonneController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult ListeAbonnes()
        {
            var abonnes = _context.Abonne.ToList();

            return View(abonnes);
        }
        [HttpGet]
        public IActionResult CreateAbonne()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbonne(Abonne abonne)
        {
            if (ModelState.IsValid)
            {
                _context.Abonne.Add(abonne);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "L'abonner a été ajouté avec succès.";
                return RedirectToAction("ListeAbonnes");
            }

            return View(abonne);
        }

        public IActionResult LivresEmpruntes(int abonneId)
        {
            var abonne = _context.Abonne.Include(a => a.Emprunts).ThenInclude(e => e.Livre).FirstOrDefault(a => a.Id == abonneId);

            if (abonne == null)
            {
                return NotFound();
            }

            var livresEmpruntes = abonne.Emprunts.Select(e => e.Livre).ToList();

           livresEmpruntes.RemoveAll(livre => livre.EstEmprunte==false);
            ViewBag.AbonneNom = abonne.Nom;
            return View(livresEmpruntes);
        }
        [HttpGet]
        public IActionResult EditAbonne(int abonneId)
        {
            var abonne = _context.Abonne.Find(abonneId);

            if (abonne == null)
            {
                return NotFound();
            }

            return View(abonne);
        }

        [HttpPost]
        public async Task<IActionResult> EditAbonne(Abonne abonne)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(abonne).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Les informations de l'abonné ont été modifiées avec succès.";
                return RedirectToAction("ListeAbonnes");
            }

            return View(abonne);
        }


        public IActionResult Delete(int id)
        {
            var abonne = _context.Abonne.Find(id);

            if (abonne == null)
            {
                return NotFound();
            }

            return View(abonne);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonne = await _context.Abonne.FindAsync(id);

            if (abonne == null)
            {
                return NotFound();
            }

            _context.Abonne.Remove(abonne);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListeAbonnes");
        }





    }

}