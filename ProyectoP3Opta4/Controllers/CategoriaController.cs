using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoP3Opta4.Controllers
{
    public class CategoriaController : Controller
    {
        public ActionResult Index(string search)
        {
            List<Categoria> categorias = new List<Categoria>();

            if (string.IsNullOrEmpty(search))
            {
                DataTable dataTable = CategoriaDao.getListaCategorias();
                categorias = ConvertirDataTableACategorias(dataTable);
            }
            else
            {
                DataTable dataTable = CategoriaDao.Listado_Categorias(search); // nombre almacen
                categorias = ConvertirDataTableACategorias(dataTable);
            }

            return View(categorias);
        }

        // Método para convertir un DataTable a una lista de Productos
        private List<Categoria> ConvertirDataTableACategorias(DataTable dataTable)
        {
            List<Categoria> categorias = new List<Categoria>();

            foreach (DataRow row in dataTable.Rows)
            {
                Categoria categoria= new Categoria(
                    nombre: row["Nombre"].ToString(),
                    cantidadProductos: Convert.ToInt32(row["CantidadProductos"])
                );
                categorias.Add(categoria);
            }

            return categorias;
        }
    }
}