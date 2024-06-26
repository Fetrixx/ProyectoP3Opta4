using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Data;
using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;



namespace ProyectoP3Opta4.Controllers
{
    public class ProveedoresController : Controller
    {

        ProveedoresDao dao = new ProveedoresDao();

        public ActionResult Index(string search)
        {
            List<Proveedores> proveedores = new List<Proveedores>();

            if (string.IsNullOrEmpty(search))
            {
                DataTable dataTable = ProveedoresDao.GetListaProveedores();
                proveedores = ConvertirDataTableAProveedores(dataTable);
            }
            else
            {
                DataTable dataTable = ProveedoresDao.ListadoProveedores(search);
                proveedores = ConvertirDataTableAProveedores(dataTable);
            }

            return View(proveedores);
        }

        private List<Proveedores> ConvertirDataTableAProveedores(DataTable dataTable)
        {
            List<Proveedores> proveedores = new List<Proveedores>();

            foreach (DataRow row in dataTable.Rows)
            {
                Proveedores proveedor = new Proveedores(
                    idProveedor: Convert.ToInt32(row["id_proveedor"]),
                    nombreEmpresa: row["nombre_empresa"].ToString(),
                    nombreContacto: row["nombre_contacto"].ToString(),
                    direccion: row["direccion"].ToString(),
                    telefono: row["telefono"].ToString(),
                    correoElectronico: row["correo_electronico"].ToString()
                );
                proveedores.Add(proveedor);
            }

            return proveedores;
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                dao.Insertar(proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        public ActionResult Editar(int id)
        {
            Proveedores proveedor = ObtenerProveedorPorId(id);

            if (proveedor != null)
            {
                return View(proveedor);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar(Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                dao.Modificar(proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        private Proveedores ObtenerProveedorPorId(int id)
        {
            DataTable dataTable = dao.GetProveedorPorId(id);
            List<Proveedores> proveedores = ConvertirDataTableAProveedores(dataTable);

            if (proveedores.Count > 0)
            {
                return proveedores[0];
            }
            return null;
        }

        public ActionResult ConfirmarEliminar(int id)
        {
            Proveedores proveedor = ObtenerProveedorPorId(id);

            if (proveedor != null)
            {
                return View(proveedor);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EliminarConfirmado(int idProveedor)
        {
            try
            {
                dao.Eliminar(idProveedor);  // Método para eliminar en el DAO
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el proveedor: " + ex.Message);
                return RedirectToAction("ConfirmarEliminar", new { id = idProveedor });
            }
        }

    }
}