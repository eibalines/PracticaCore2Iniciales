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

        public IActionResult CarritoLibros(int? idlibro)
        {
            if (idlibro != null)
            {
                List<int> lstLibrosId;
                if (HttpContext.Session.GetString("Idlibro") == null)
                {
                   
                    lstLibrosId = new List<int>();
                }
                else
                {
                    
                    lstLibrosId =
             HttpContext.Session.GetObject<List<int>>("Idlibro");
                }
                
                lstLibrosId.Add(idlibro.Value);
           
                HttpContext.Session.SetObject("Idlibro", lstLibrosId);
            }
            return View(this.repo.GetLibros());

        }


    }
    }

