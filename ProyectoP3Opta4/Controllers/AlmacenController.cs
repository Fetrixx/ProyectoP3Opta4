using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;



namespace ProyectoP3Opta4.Controllers
{
    public class AlmacenController : Controller
    {
        // GET: Almacen
        /*
        public ActionResult Index()
        {
            return View();
        }
        */

        public ActionResult Index(string search)
        {
            List<Almacen> almacenes = new List<Almacen>();

            if (string.IsNullOrEmpty(search))
            {
                DataTable dataTable = AlmacenDao.getListaAlmacenes();
                almacenes = ConvertirDataTableAProductos(dataTable);
            }
            else
            {
                DataTable dataTable = AlmacenDao.Listado_Almacenes(search); // nombre almacen
                almacenes = ConvertirDataTableAProductos(dataTable);
            }

            return View(almacenes);
        }

        // Método para convertir un DataTable a una lista de Productos
        private List<Almacen> ConvertirDataTableAProductos(DataTable dataTable)
        {
            List<Almacen> almacenes = new List<Almacen>();

            foreach (DataRow row in dataTable.Rows)
            {
                Almacen almacen= new Almacen(
                    nombre: row["Nombre"].ToString(),
                    cantidadProductos: Convert.ToInt32(row["CantidadProductos"])
                );
                almacenes.Add(almacen);
            }

            return almacenes;
        }
    }
}