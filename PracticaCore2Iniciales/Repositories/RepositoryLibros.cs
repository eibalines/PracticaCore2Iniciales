using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PracticaCore2Iniciales.Data;
using PracticaCore2Iniciales.Models;

namespace PracticaCore2Iniciales.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }

        public List<Libro> GetLibros()
        {
            var consulta = from datos in this.context.Libros
                           select datos;
            return consulta.ToList();
        }

        public List<Genero> GetGeneros()
        {
            var consulta = from datos in this.context.Generos
                           select datos;
            return consulta.ToList();
        }

        //Detalles Libro
       public Libro DetallesLibro(int idlibro)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdLibro == idlibro
                           select datos;
            return consulta.FirstOrDefault();
        }

        // Libros por genero
        public List<Libro> LibrosGenero(int idgenero)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdGenero == idgenero
                           select datos;
            return consulta.ToList();
        }

        public List<Libro> GetLibroSession(List<int> idlibros)
        {
            var consulta = from datos in this.context.Libros
                           where idlibros.Contains(datos.IdLibro)
                           select datos;
            if(consulta.Count() == 0)
            {
                return null;
            }
            return consulta.ToList();
        }

        public Usuario ExisteUsuario(string email, string password)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Email == email &&
                            datos.Pass == password
                           select datos;
            return consulta.FirstOrDefault();
        }

        public Usuario GetUsuario(int idusuario)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.IdUsuario == idusuario
                           select datos;
            return consulta.FirstOrDefault();
        }


        private SqlDataAdapter addept;
        private String CadenaConexion;
        private SqlConnection cn;
        private SqlCommand com;

        public int FinalizarCompra(int idpedido, int idfactura, long fecha, int idusuario, int cantidad)
        {
        string sql = "insert into PEDIDOS values (@idpedido, @idfactura, @fecha, @idlibro, @idusuario, @cantidad)";
            this.com.Parameters.AddWithValue("@idpedido", idpedido);
            this.com.Parameters.AddWithValue("@idfactura", idfactura);
            this.com.Parameters.AddWithValue("@fecha", fecha);
            this.com.Parameters.AddWithValue("@idusuario", idusuario);
            this.com.Parameters.AddWithValue("@cantidad", cantidad);

            this.com.CommandText = sql;
            this.com.CommandType = CommandType.Text;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            int insertados = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            return insertados;
        }


    }
}
