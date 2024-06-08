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
    public class ProductoController : Controller
    {
        private ProductoDao productoDao = new ProductoDao();

        // GET: Producto/Index
        public ActionResult Index(string search)
        {
            List<Producto> productos = new List<Producto>();

            if (string.IsNullOrEmpty(search))
            {
                DataTable dataTable = ProductoDao.getListaProductos();
                productos = ConvertirDataTableAProductos(dataTable);
            }
            else
            {
                DataTable dataTable = ProductoDao.Listado_Productos(search);
                productos = ConvertirDataTableAProductos(dataTable);
            }

            return View(productos);
        }

        // Método para convertir un DataTable a una lista de Productos
        private List<Producto> ConvertirDataTableAProductos(DataTable dataTable)
        {
            List<Producto> productos = new List<Producto>();

            foreach (DataRow row in dataTable.Rows)
            {
                Producto producto = new Producto(
                    codigoprod: Convert.ToInt32(row["CodigoProducto"]),
                    nombre: row["Nombre"].ToString(),
                    cantidad: Convert.ToInt32(row["Cantidad"]),
                    categoria: row["categoria"].ToString(),
                    marca: row["marca"].ToString(),
                    almacen: row["almacen"].ToString()
                );
                productos.Add(producto);
            }

            return productos;
        }

        // GET: Producto/Nuevo
        public ActionResult Nuevo()
        {
            return View();
        }

        // POST: Producto/Nuevo
        [HttpPost]
        public ActionResult Nuevo(Producto producto)
        {
            if (ModelState.IsValid)
            {
                productoDao.Insertar(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Editar/5
        public ActionResult Editar(int codigoProducto)
        {
            // Producto producto = productoDao.modificar(codigoProducto);
            Producto producto = productoDao.ObtenerProductoPorCodigo(codigoProducto);

            if (producto != null)
            {
                return View(producto);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Producto/Editar/5
        [HttpPost]
        public ActionResult Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                productoDao.modificar(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }
        /*
        // GET: Producto/Eliminar/5
        public ActionResult Eliminar(int codigoProducto)
        {
            ViewBag.CodigoProducto = codigoProducto;
            return View();
        }
        */
        // POST: Producto/Eliminar/5




        // GET: Producto/ConfirmarEliminar/5
        public ActionResult ConfirmarEliminar(int codigoProducto)
        {
            Producto producto = productoDao.ObtenerProductoPorCodigo(codigoProducto);
            if (producto == null)
            {
                return HttpNotFound(); // Manejar el caso en que el producto no se encuentra
            }
            return View(producto);
        }

        // POST: Producto/Eliminar/5
        [HttpPost]
        public ActionResult Eliminar(int codigoProducto)
        {
            // Eliminar el producto
            // Obtener el producto a eliminar utilizando el método del DAO
            Producto producto = productoDao.ObtenerProductoPorCodigo(codigoProducto);

            if (producto != null) // Verificar si se encontró el producto
            {
                productoDao.Eliminar_Prod(producto);
                return RedirectToAction("Index");
            }
            else
            {
                // Manejar el caso en que no se encontró el producto
                // Por ejemplo, podrías mostrar un mensaje de error, redirigir a una página de error, etc.
                return RedirectToAction("ProductoNoEncontrado");
            }
        }


        /*
        // POST: Producto/ConfirmarEliminar
        [HttpPost]
        public ActionResult ConfirmarEliminar(int codigoProducto)
        {
            // Obtener el producto a eliminar utilizando el método del DAO
            Producto producto = productoDao.ObtenerProductoPorCodigo(codigoProducto);

            if (producto != null) // Verificar si se encontró el producto
            {
                productoDao.Eliminar_Prod(producto);
                return RedirectToAction("Index");
            }
            else
            {
                // Manejar el caso en que no se encontró el producto
                // Por ejemplo, podrías mostrar un mensaje de error, redirigir a una página de error, etc.
                return RedirectToAction("ProductoNoEncontrado");
            }
        }
        */


        /*
        [HttpPost]
        public ActionResult ConfirmarEliminar(int codigoProducto)
        {
            // Obtener el producto a eliminar utilizando el método del DAO
            Producto producto = productoDao.ObtenerProductoPorCodigo(codigoProducto);

            if (producto != null) // Verificar si se encontró el producto
            {
                // Llamar al método Eliminar_Prod del DAO pasando el objeto del producto
                productoDao.Eliminar_Prod(producto);
                return RedirectToAction("Index");
            }
            else
            {
                // Manejar el caso en que no se encontró el producto
                // Por ejemplo, podrías mostrar un mensaje de error, redirigir a una página de error, etc.
                return RedirectToAction("ProductoNoEncontrado");
            }
        }*/
    }
}



