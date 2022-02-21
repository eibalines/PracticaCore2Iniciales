using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2Iniciales.Providers
{
    public class PathProviders
    {
        private IWebHostEnvironment hostEnvironment;
        public PathProviders(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public enum Folders
        {
            Images = 0, Documents = 1
        }
        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
           
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Documents)
            {
                carpeta = "documents";
            }
            string path = Path.Combine(this.hostEnvironment.WebRootPath, carpeta, fileName);
           
            return path;

        }
    }
}
