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
                DataTable dataTable = CategoriaDao.Listado_Categorias(search);
                categorias = ConvertirDataTableACategorias(dataTable);
            }

            return View(categorias);
        }

        private List<Categoria> ConvertirDataTableACategorias(DataTable dataTable)
        {
            List<Categoria> categorias = new List<Categoria>();

            foreach (DataRow row in dataTable.Rows)
            {
                Categoria categoria = new Categoria(
                    id_categoria: Convert.ToInt32(row["id_categoria"]),
                    nombre: row["nombre"].ToString(),
                    descripcion: row["descripcion"].ToString(),
                    cantidadProductos: Convert.ToInt32(row["cantidadProductos"])
                );
                categorias.Add(categoria);
            }

            return categorias;
        }
    }
}