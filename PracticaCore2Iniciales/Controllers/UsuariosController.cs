using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticaCore2Iniciales.Filters;
using PracticaCore2Iniciales.Models;
using PracticaCore2Iniciales.Repositories;
using System.Security.Claims;

namespace PracticaCore2Iniciales.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryLibros repo;
        public UsuariosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }


        [AthorizeUser]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Perfil(int idusuario)
        {
            idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Usuario usuario = this.repo.GetUsuario(idusuario);

            return View(usuario);
        }

        [AthorizeUser]
        public IActionResult FinalizarCompra(int idpedido, int idfactura, long fecha, int idusuario, int cantidad)
        {
            int results = this.repo.FinalizarCompra( idpedido, idfactura, fecha, idusuario, cantidad);
          ViewData["MENSAJECONFIRMACION"] = "Compra realizada";
            

            return View();
        }


    }
}
