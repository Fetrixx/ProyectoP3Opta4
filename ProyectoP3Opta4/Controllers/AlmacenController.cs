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
        public ActionResult Index(string search)
        {
            List<Almacen> almacenes = new List<Almacen>();

            if (string.IsNullOrEmpty(search))
            {
                DataTable dataTable = AlmacenDao.getListaAlmacenes();
                almacenes = ConvertirDataTableAAlmacenes(dataTable);
            }
            else
            {
                DataTable dataTable = AlmacenDao.Listado_Almacenes(search);
                almacenes = ConvertirDataTableAAlmacenes(dataTable);
            }

            return View(almacenes);
        }

        private List<Almacen> ConvertirDataTableAAlmacenes(DataTable dataTable)
        {
            List<Almacen> almacenes = new List<Almacen>();

            foreach (DataRow row in dataTable.Rows)
            {
                Almacen almacen = new Almacen(
                    id_almacen: Convert.ToInt32(row["id_almacen"]),
                    nombre: row["nombre"].ToString(),
                    direccion: row["direccion"].ToString(),
                    ciudad: row["ciudad"].ToString(),
                    telefono: row["telefono"].ToString(),
                    cantidadProductos: Convert.IsDBNull(row["cantidadProductos"]) ? 0 : Convert.ToInt32(row["cantidadProductos"])
                );
                almacenes.Add(almacen);
            }

            return almacenes;
        }
    }
}