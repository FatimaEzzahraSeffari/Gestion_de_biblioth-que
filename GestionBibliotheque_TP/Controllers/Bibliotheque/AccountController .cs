using GestionBibliotheque_TP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }
    

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (ModelState.IsValid)
        {
            var user = userRegister.ToUser();
            _context.UserRegisters.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("RedirectAfterRegistration", new { email = userRegister.Email });
        }

        return View(userRegister);
    }

    public IActionResult RedirectAfterRegistration(string email)
    {
        if (email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToAction("Home", "Home");
        }
        else if (email.EndsWith("@admin.com", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("DefaultAction", "Home");
        }
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.UserRegisters
                .SingleOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

            if (user != null)
            {
                
                return RedirectToAction("RedirectAfterLogin", new { email = user.Email });
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }

        return View(loginModel);
    }

    public IActionResult RedirectAfterLogin(string email)
    {
        if (email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToAction("Home", "Home");
        }
        else if (email.EndsWith("@admin.com", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToAction("Index", "Home");
            
        }
        else
        {
            return View("loginModel");
        }
    }


}