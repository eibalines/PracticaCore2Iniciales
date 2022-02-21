using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticaCore2Iniciales.Models;
using PracticaCore2Iniciales.Repositories;
using PracticaCore2Iniciales.Extensions;
using Microsoft.AspNetCore.Http;

namespace PracticaCore2Iniciales.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;

        //Mostrar todos los libros
        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        //Mostrar los generos
        public IActionResult Index()
        {

            List<Libro> libros = this.repo.GetLibros();


            return View(libros);
        }


        public IActionResult DetallesLibro(int idlibro)
        {
            Libro libro = this.repo.DetallesLibro(idlibro);
            return View(libro);
        }


        public IActionResult LibrosGenero(int idgenero)
        {
            List<Libro> libros = this.repo.LibrosGenero(idgenero);
            return View(libros);
        }

        public IActionResult LibrosAlmacenados(int? idlibro)
        {
            List<int> lstIdLibros =
            HttpContext.Session.GetObject<List<int>>("IdLibro");
            if (lstIdLibros == null)
            {
                ViewData["MENSAJE"] =
                "No existen libros en el carrito";
                return View();
            }
            else
            {
                if (idlibro != null)
                {
                    lstIdLibros.Remove(idlibro.Value);
                    if (lstIdLibros.Count == 0)
                    {

                        HttpContext.Session.Remove("IdLibro");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IdLibro", lstIdLibros);
                    }
                }

                List<Libro> libros =
                           this.repo.GetLibroSession(lstIdLibros);
                return View(libros);
            }


        }
    }
}
