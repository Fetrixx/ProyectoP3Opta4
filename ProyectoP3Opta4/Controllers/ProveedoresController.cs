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

        Proveedores bean = new Proveedores();
        ProveedoresDao dao = new ProveedoresDao();
        public string resp;


        // GET: Proveedores
        // GET: Proveedores/Index
        public ActionResult Index(string search)
        {
            List<Proveedores> proveedores = new List<Proveedores>();

            // Verificar si hay un término de búsqueda
            if (string.IsNullOrEmpty(search))
            {
                // Si no hay búsqueda, obtener todos los proveedores
                DataTable dataTable = ProveedoresDao.getListaProveedores();
                proveedores = ConvertirDataTableAProveedores(dataTable);
            }
            else
            {
                // Si hay búsqueda, obtener los proveedores que coincidan con el término
                DataTable dataTable = ProveedoresDao.Listado_Proveedores(search);
                proveedores = ConvertirDataTableAProveedores(dataTable);
            }

            return View(proveedores);
        }


        // Método para convertir un DataTable a una lista de Proveedores
        private List<Proveedores> ConvertirDataTableAProveedores(DataTable dataTable)
        {
            List<Proveedores> proveedores = new List<Proveedores>();

            foreach (DataRow row in dataTable.Rows)
            {
                Proveedores proveedor = new Proveedores(
                    nombre: row["nombre"].ToString(),
                    direccion: row["direccion"].ToString(),
                    telefono: row["telefono"].ToString(),
                    mail: row["gmail"].ToString()
                );
                proveedores.Add(proveedor);
            }

            return proveedores;
        }

        // GET: Proveedores/Nuevo
        public ActionResult Nuevo()
        {
            return View();
        }

        // POST: Proveedores/Nuevo
        [HttpPost]
        public ActionResult Nuevo(Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                // Agregar el nuevo proveedor a la base de datos
                ProveedoresDao provDao = new ProveedoresDao();
                provDao.Insertar(proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);
            /*
            ProveedoresController provCtrl = new ProveedoresController();

            provCtrl.agregar(proveedor.Nombre, proveedor.Direccion, proveedor.Telefono, proveedor.Mail);
            return RedirectToAction("Index");
            */
        }







        // Acción para editar un proveedor
        public ActionResult Editar(string nombre)
        {
            // Obtener el proveedor con el nombre proporcionado
            Proveedores proveedor = ObtenerProveedorPorNombre(nombre);

            // Verificar si el proveedor existe
            if (proveedor != null)
            {
                // Si existe, mostrar la vista de edición con el proveedor
                return View(proveedor);
            }
            else
            {
                // Si no existe, redirigir al usuario a la página de índice
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar(Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                ProveedoresDao provDao = new ProveedoresDao();
                // Lógica para guardar los cambios en el proveedor en la base de datos utilizando ProveedoresDao
                provDao.modificar(proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // Método para obtener un proveedor por su nombre
        private Proveedores ObtenerProveedorPorNombre(string nombre)
        {
            // Lógica para obtener el proveedor de la base de datos utilizando ProveedoresDao
            ProveedoresDao provDao = new ProveedoresDao();
            DataTable dataTable = ProveedoresDao.Listado_Proveedores(nombre);
            List<Proveedores> proveedores = ConvertirDataTableAProveedores(dataTable);

            // Verificar si se encontró algún proveedor con el nombre proporcionado
            if (proveedores.Count > 0)
            {
                // Si se encontró, devolver el primer proveedor de la lista
                return proveedores[0];
            }
            else
            {
                // Si no se encontró, devolver null
                return null;
            }
        }

        // Acción para eliminar un proveedor
        // Cambia el nombre del método que recibe un int como parámetro a EliminarPorId
        public ActionResult EliminarPorId(int id)
        {
            // Lógica para cargar los datos del proveedor con el ID proporcionado
            // y pasarlos a la vista de confirmación de eliminación
            Proveedores proveedor = new Proveedores(); // Asegúrate de tener la clase Proveedor definida
                                                       // Lógica para cargar los datos del proveedor con el ID proporcionado
            return View(proveedor);
        }

        public ActionResult ConfirmarEliminar(string nombre)
        {
            // Aquí podrías cargar cualquier información adicional del proveedor que desees mostrar en la vista de confirmación
            ViewBag.NombreProveedor = nombre;
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmarEliminar(string nombre, FormCollection collection)
        {
            ProveedoresDao provDao = new ProveedoresDao();
            provDao.Eliminar_Prov(nombre);
            return RedirectToAction("Index");
        }



        /*
        public void agregar(string nom, string dir, string tel, string mail)
        {
            resp = "aguardando dao";
            bean.Nombre = nom;
            bean.Direccion = dir;
            bean.Telefono = tel;
            bean.Mail = mail;
            dao.Insertar(bean);
            if (dao.respGral == "En proceso")
            {
                resp = dao.respGral;
                //MessageBox.Show("El proceso no se realizo con exito");
            }
            else if (dao.respGral == "ok")
            {
                resp = dao.respGral;
                //MessageBox.Show("Cargado correctamente en base de datos");
            }
            else if (resp == "aguardando dao")
            {
                //MessageBox.Show("Aguardando dao...");
            }
        }
        public void modificar(string nom, string dir, string tel, string mail)
        {
            resp = "aguardando dao";
            bean.Nombre = nom;
            bean.Direccion = dir;
            bean.Telefono = tel;
            bean.Mail = mail;
            dao.modificar(bean);
            if (dao.respGral == "En proceso")
            {
                resp = dao.respGral;
                //MessageBox.Show("El proceso no se realizo con exito");
            }
            else if (dao.respGral == "ok")
            {
                resp = dao.respGral;
                //MessageBox.Show("Cargado correctamente en base de datos");
            }
            else if (resp == "aguardando dao")
            {
                //MessageBox.Show("Aguardando dao...");
            }
        }
        */
        /*
        public void eliminar(string nom)
        {
            resp = "aguardando dao";
            bean.Nombre = nom;
            dao.Eliminar_Prov(bean);
            if (dao.respGral == "En proceso")
            {
                resp = dao.respGral;
                //MessageBox.Show("El proceso no se realizo con exito");
            }
            else if (dao.respGral == "ok")
            {
                resp = dao.respGral;
                //MessageBox.Show("Cargado correctamente en base de datos");
            }
            else if (resp == "aguardando dao")
            {
                //MessageBox.Show("Aguardando dao...");
            }
        }*/

        /*
        public static DataTable listarDatos(string nombreProvListado)
        {
            return ProveedoresDao.Listado_Proveedores(nombreProvListado);

        }
        */



        /*
        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(string nombre)
        {
            ProveedoresDao provDao = new ProveedoresDao();
            // Lógica para eliminar el proveedor de la base de datos utilizando ProveedoresDao
            provDao.Eliminar_Prov(nombre);
            return RedirectToAction("Index");
        }*/









        /*
        public ActionResult Index(string search)
        {
            var proveedores = string.IsNullOrEmpty(search) ? ProveedoresDao.getListaProveedores() : ProveedoresDao.Listado_Proveedores(search);
            return View(proveedores);
        }
        */


        /*
        [HttpPost]
        public ActionResult Editar(Proveedores proveedor)
        {
            if (ModelState.IsValid)
            {
                ProveedoresDao provDao = new ProveedoresDao();
                // Lógica para guardar los cambios en el proveedor en la base de datos utilizando ProveedoresDao
                provDao.modificar(proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // Acción para eliminar un proveedor
        public ActionResult Eliminar(int id)
        {
            // Aquí puedes implementar la lógica para cargar los datos del proveedor con el ID proporcionado
            // y pasarlos a la vista de confirmación de eliminación
            return View();
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {
            // Lógica para eliminar el proveedor de la base de datos utilizando ProveedoresDao
            ProveedoresDao provDao = new ProveedoresDao();
            provDao.Eliminar_Prov(id);
            return RedirectToAction("Index");
        }*/


    }
}