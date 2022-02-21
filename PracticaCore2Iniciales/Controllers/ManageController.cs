using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PracticaCore2Iniciales.Models;
using PracticaCore2Iniciales.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace PracticaCore2Iniciales.Controllers
{
    public class ManageController : Controller
    {
        private RepositoryLibros repo;
        public ManageController(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario usuario =
                 this.repo.ExisteUsuario(email, password);
            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, usuario.Nombre);
                Claim claimPass = new Claim("Pass", usuario.Pass);
                Claim claimIdUsuario = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                Claim claimApellidos = new Claim("Apellidos", usuario.Apellidos);
                Claim claimFoto = new Claim("Foto", usuario.Foto);
                Claim claimmail = new Claim("Email", usuario.Email);
               

                identity.AddClaim(claimName);
                identity.AddClaim(claimPass);
                identity.AddClaim(claimmail);
                identity.AddClaim(claimIdUsuario);
                identity.AddClaim(claimApellidos);
                identity.AddClaim(claimFoto);
              

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                ViewData["idusuario"] = claimIdUsuario.ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJEACCESODENEGADO"] = "Mail/Contraseña incorrectos";
            }
            return View();
        }

        
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libros");
        }

       
    }
}
