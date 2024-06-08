using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
using System.Data;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoP3Opta4.Controllers
{
    public class MarcasController : Controller
    {
        // GET: Marcas
        /*
        public ActionResult Index()
        {
            return View();
        }
        */

        public ActionResult Index(string search)
        {
            List<Marcas> marcas = new List<Marcas>();

            if (string.IsNullOrEmpty(search))
            {
                DataTable dataTable = MarcasDao.getListaMarcas();
                marcas = ConvertirDataTableAProductos(dataTable);
            }
            else
            {
                DataTable dataTable = MarcasDao.Listado_Marcas(search); // nombre marca
                marcas = ConvertirDataTableAProductos(dataTable);
            }

            return View(marcas);
        }

        // Método para convertir un DataTable a una lista de Productos
        private List<Marcas> ConvertirDataTableAProductos(DataTable dataTable)
        {
            List<Marcas> marcas = new List<Marcas>();

            foreach (DataRow row in dataTable.Rows)
            {
                Marcas marca = new Marcas(
                    nombre: row["Nombre"].ToString(),
                    items: Convert.ToInt32(row["Items"])
                );
                marcas.Add(marca);
            }

            return marcas;
        }
    }
}