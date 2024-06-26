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
    public class EmpleadoController : Controller
    {
        EmpleadoDao dao = new EmpleadoDao();
        RolesDao rolesDao = new RolesDao();

        public ActionResult Index(string search)
        {
            List<Empleado> empleados = new List<Empleado>();

            if (string.IsNullOrEmpty(search))
            {
                DataTable dataTable = EmpleadoDao.GetListaProveedores();
                empleados = ConvertirDataTableAEmpleados(dataTable);
            }
            else
            {
                DataTable dataTable = EmpleadoDao.GetListaProveedores();
                empleados = ConvertirDataTableAEmpleados(dataTable);
            }

            // Obtener los roles para mapear el id_rol al nombre del rol
            var roles = rolesDao.GetRoles().ToDictionary(r => r.id_rol, r => r.nombre);

            // Asignar el nombre del rol correspondiente a cada empleado
            foreach (var empleado in empleados)
            {
                if (roles.ContainsKey(empleado.id_rol))
                {
                    empleado.id_nombre = roles[empleado.id_rol];
                }
            }

            return View(empleados);
        }

        private List<Empleado> ConvertirDataTableAEmpleados(DataTable dataTable)
        {
            List<Empleado> empleados = new List<Empleado>();

            foreach (DataRow row in dataTable.Rows)
            {
                Empleado empleado = new Empleado(
                    id_empleado: Convert.ToInt32(row["id_empleado"]),
                    nombre_completo: row["nombre_completo"].ToString(),
                    usuario: row["usuario"].ToString(),
                    contrasena: row["contrasena"].ToString(),
                    id_rol: Convert.ToInt32(row["id_rol"]),
                    id_nombre: string.Empty // Se asignará posteriormente
                );
                empleados.Add(empleado);
            }

            return empleados;
        }

        public ActionResult Nuevo()
        {
            ViewBag.Roles = new SelectList(rolesDao.GetRoles(), "id_rol", "nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                dao.Insertar(empleado);
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(rolesDao.GetRoles(), "id_rol", "nombre", empleado.id_rol);
            return View(empleado);
        }

        public ActionResult Editar(int id)
        {
            Empleado empleado = ObtenerEmpleadoPorId(id);

            if (empleado != null)
            {
                ViewBag.Roles = new SelectList(rolesDao.GetRoles(), "id_rol", "nombre", empleado.id_rol);
                return View(empleado);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                dao.Modificar(empleado);
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(rolesDao.GetRoles(), "id_rol", "nombre", empleado.id_rol);
            return View(empleado);
        }

        private Empleado ObtenerEmpleadoPorId(int id)
        {
            DataTable dataTable = dao.GetEmpleadoPorId(id);
            List<Empleado> empleados = ConvertirDataTableAEmpleados(dataTable);

            if (empleados.Count > 0)
            {
                return empleados[0];
            }
            return null;
        }

        public ActionResult ConfirmarEliminar(int id)
        {
            Empleado empleado = ObtenerEmpleadoPorId(id);

            if (empleado != null)
            {
                return View(empleado);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EliminarConfirmado(int id_empleado)
        {
            try
            {
                dao.Eliminar(id_empleado);  // Método para eliminar en el DAO
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el empleado: " + ex.Message);
                return RedirectToAction("ConfirmarEliminar", new { id = id_empleado });
            }
        }
    }
}
