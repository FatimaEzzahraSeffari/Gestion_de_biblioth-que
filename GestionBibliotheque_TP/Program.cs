using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GestionBibliotheque_TP.Models;


var builder = WebApplication.CreateBuilder(args);

// Ajouter le service DbContext avec SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Livre_ListeLivres",
    pattern: "Livre/ListeLivres",
    defaults: new { controller = "Livre", action = "ListeLivres" });

app.MapControllerRoute(
    name: "Livre_CreateLivre",
    pattern: "Livre/CreateLivre",
    defaults: new { controller = "Livre", action = "CreateLivre" });

app.MapControllerRoute(
    name: "Abonne_ListeAbonnes",
    pattern: "Abonne/ListeAbonnes",
    defaults: new { controller = "Abonne", action = "ListeAbonnes" });

app.MapControllerRoute(
    name: "Abonne_CreateAbonne",
    pattern: "Abonne/CreateAbonne",
    defaults: new { controller = "Abonne", action = "CreateAbonne" });

app.MapControllerRoute(
    name: "Abonne_LivresEmpruntes",
    pattern: "Abonne/LivresEmpruntes",
    defaults: new { controller = "Abonne", action = "LivresEmpruntes" });

app.MapControllerRoute(
    name: "Emprunt_ListeEmprunt",
    pattern: "Emprunt/ListeEmprunt",
    defaults: new { controller = "Emprunt", action = "ListeEmprunt" });

app.MapControllerRoute(
    name: "Emprunt_CreateEmprunt",
    pattern: "Emprunt/CreateEmprunt",
    defaults: new { controller = "Emprunt", action = "CreateEmprunt" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.MapControllerRoute(
    name: "Account_AccountRegister",
    pattern: "Account/AccountRegister",
    defaults: new { controller = "Account", action = "Register" });

app.MapControllerRoute(
    name: "Account_AccountLogin",
    pattern: "Account/AccountLogin",
    defaults: new { controller = "Account", action = "Login" });


app.MapControllerRoute(
    name: "Home_Home",
    pattern: "Home/Home",
    defaults: new { controller = "Home", action = "Home" });

app.MapControllerRoute(
   name: "Comment_CreateComment",
        pattern: "Comment/CreateComment",
        defaults: new { controller = "Comment", action = "CreateComment" });


app.Run();
