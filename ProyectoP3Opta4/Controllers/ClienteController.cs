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
    public class ClienteController : Controller
    {
        // GET: Cliente
        /*
        public ActionResult Index()
        {
            return View();
        }
        */

        Cliente bean = new Cliente();
        ClienteDao dao = new ClienteDao();
        public string resp;


        // GET: Proveedores
        // GET: Proveedores/Index
        public ActionResult Index(string search)
        {
            List<Cliente> clientes = new List<Cliente>();

            // Verificar si hay un término de búsqueda
            if (string.IsNullOrEmpty(search))
            {
                // Si no hay búsqueda, obtener todos los proveedores
                DataTable dataTable = ClienteDao.getListaClientes();
                clientes = ConvertirDataTableAClientes(dataTable);
            }
            else
            {
                // Si hay búsqueda, obtener los proveedores que coincidan con el término
                DataTable dataTable = ClienteDao.Listado_Clientes(search);
                clientes = ConvertirDataTableAClientes(dataTable);
            }

            return View(clientes);
        }


        // Método para convertir un DataTable a una lista de Proveedores
        private List<Cliente> ConvertirDataTableAClientes(DataTable dataTable)
        {
            List<Cliente> clientes= new List<Cliente>();

            foreach (DataRow row in dataTable.Rows)
            {
                Cliente cliente = new Cliente(
                    /*
                     private string nombre;
                    private string apellido;
                    private string cedula;
                    private string mail;
                    private string telefono;
                    private string direccion;*/


                    nombre: row["nombre"].ToString(),
                    apellido: row["apellido"].ToString(),
                    mail: row["gmail"].ToString(),
                    cedula: row["cedula"].ToString(),
                    telefono: row["telefono"].ToString(),
                    direccion: row["direccion"].ToString()
                    
                );
                clientes.Add(cliente);
            }

            return clientes;
        }

        // GET: Proveedores/Nuevo
        public ActionResult Nuevo()
        {
            return View();
        }

        // POST: Proveedores/Nuevo
        [HttpPost]
        public ActionResult Nuevo(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // Agregar el nuevo proveedor a la base de datos
                ClienteDao cliDao = new ClienteDao();
                cliDao.Insertar(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
            /*
            ProveedoresController provCtrl = new ProveedoresController();

            provCtrl.agregar(proveedor.Nombre, proveedor.Direccion, proveedor.Telefono, proveedor.Mail);
            return RedirectToAction("Index");
            */
        }







        // Acción para editar un proveedor
        public ActionResult Editar(string cedula)
        {
            // Obtener el proveedor con el nombre proporcionado
            Cliente cliente = ObtenerClientePorCI(cedula);

            // Verificar si el proveedor existe
            if (cliente != null)
            {
                // Si existe, mostrar la vista de edición con el proveedor
                return View(cliente);
            }
            else
            {
                // Si no existe, redirigir al usuario a la página de índice
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ClienteDao cliDao = new ClienteDao();
                // Lógica para guardar los cambios en el proveedor en la base de datos utilizando ProveedoresDao
                cliDao.modificar(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // Método para obtener un proveedor por su nombre
        private Cliente ObtenerClientePorCI(string cedula)
        {
            // Lógica para obtener el proveedor de la base de datos utilizando ProveedoresDao
            ClienteDao provDao = new ClienteDao();
            DataTable dataTable = ClienteDao.Listado_Clientes(cedula);
            List<Cliente> clientes = ConvertirDataTableAClientes(dataTable);

            // Verificar si se encontró algún proveedor con el nombre proporcionado
            if (clientes.Count > 0)
            {
                // Si se encontró, devolver el primer proveedor de la lista
                return clientes[0];
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
            Cliente cliente = new Cliente(); // Asegúrate de tener la clase Proveedor definida
                                                       // Lógica para cargar los datos del proveedor con el ID proporcionado
            return View(cliente);
        }

        public ActionResult ConfirmarEliminar(string cedula)
        {
            // Aquí podrías cargar cualquier información adicional del proveedor que desees mostrar en la vista de confirmación
            ViewBag.Cedula = cedula;
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmarEliminar(string cedula, FormCollection collection)
        {
            ClienteDao cliDao = new ClienteDao();
            cliDao.Eliminar_Cli(cedula);
            return RedirectToAction("Index");
        }



    }
}