using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
using System;
using System.Web.Mvc;

namespace ProyectoP3Opta4.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            RolesDao dao = new RolesDao();
            var roles = dao.GetListaRoles();
            return View(roles);
        }

        // GET: Roles/Nuevo
        public ActionResult Nuevo()
        {
            return View();
        }

        // POST: Roles/Nuevo
        [HttpPost]
        public ActionResult Nuevo(Roles model)
        {
            try
            {
                RolesDao dao = new RolesDao();
                dao.Insertar(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                return View(model);
            }
        }

        // GET: Roles/Editar/5
        public ActionResult Editar(int id)
        {
            RolesDao dao = new RolesDao();
            var rol = dao.GetRolPorId(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Roles/Editar/5
        [HttpPost]
        public ActionResult Editar(Roles model)
        {
            try
            {
                RolesDao dao = new RolesDao();
                dao.Modificar(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                return View(model);
            }
        }
       


        // POST: Roles/EliminarConfirmado
        [HttpPost]
        public ActionResult EliminarConfirmado(int id_rol)
        {
            try
            {
                RolesDao dao = new RolesDao();
                dao.Eliminar(id_rol);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                return RedirectToAction("ConfirmarEliminar", new { id = id_rol });
            }
        }



        // GET: Roles/ConfirmarEliminar/5
        public ActionResult ConfirmarEliminar(int id)
        {
            RolesDao dao = new RolesDao();
            var rol = dao.GetRolPorId(id);
            if (rol != null)
            {
                return View(rol);
            }
            return RedirectToAction("Index");
        }

   

       
    }
}
