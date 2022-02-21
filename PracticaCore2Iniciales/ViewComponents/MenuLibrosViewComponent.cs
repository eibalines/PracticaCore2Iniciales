using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticaCore2Iniciales.Repositories;
using PracticaCore2Iniciales.Models;

namespace PracticaCore2Iniciales.ViewComponents
{
    public class MenuLibrosViewComponent : ViewComponent
    {
        private RepositoryLibros repo;
        public MenuLibrosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = this.repo.GetGeneros();
            return View(generos);
        }
    }
}
