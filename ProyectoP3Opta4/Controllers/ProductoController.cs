using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoP3Opta4.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoDao productoDao = new ProductoDao();

        // GET: Producto/Index
        public ActionResult Index(string search)
        {
            List<Producto> productos = string.IsNullOrEmpty(search) ? productoDao.ListarProductos("") : productoDao.ListarProductos(search);
            return View(productos);
        }

        // GET: Producto/Nuevo
        public ActionResult Nuevo()
        {
            // Cargar listas de categorías, marcas, almacenes y proveedores desde la base de datos
            List<Categoria> categorias = productoDao.CargarCategorias();
            List<Marcas> marcas = productoDao.CargarMarcas();
            List<Almacen> almacenes = productoDao.CargarAlmacenes();
            List<Proveedores> proveedores = productoDao.CargarProveedores();

            // Convertir listas a SelectListItems para los dropdowns en la vista
            ViewBag.Categorias = categorias.Select(c => new SelectListItem { Value = c.id_categoria.ToString(), Text = c.nombre }).ToList();
            ViewBag.Marcas = marcas.Select(m => new SelectListItem { Value = m.id_marca.ToString(), Text = m.nombre }).ToList();
            ViewBag.Almacenes = almacenes.Select(a => new SelectListItem { Value = a.id_almacen.ToString(), Text = a.nombre }).ToList();
            ViewBag.Proveedores = proveedores.Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreEmpresa }).ToList();

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
        public ActionResult Editar(int id_producto)
        {
            Producto producto = productoDao.ObtenerProductoPorId(id_producto);
            if (producto == null)
            {
                return HttpNotFound();
            }

            // Cargar listas de categorías, marcas, almacenes y proveedores desde la base de datos
            List<Categoria> categorias = productoDao.CargarCategorias();
            List<Marcas> marcas = productoDao.CargarMarcas();
            List<Almacen> almacenes = productoDao.CargarAlmacenes();
            List<Proveedores> proveedores = productoDao.CargarProveedores();

            // Convertir listas a SelectListItems para los dropdowns en la vista
            ViewBag.Categorias = categorias.Select(c => new SelectListItem { Value = c.id_categoria.ToString(), Text = c.nombre }).ToList();
            ViewBag.Marcas = marcas.Select(m => new SelectListItem { Value = m.id_marca.ToString(), Text = m.nombre }).ToList();
            ViewBag.Almacenes = almacenes.Select(a => new SelectListItem { Value = a.id_almacen.ToString(), Text = a.nombre }).ToList();
            ViewBag.Proveedores = proveedores.Select(p => new SelectListItem { Value = p.IdProveedor.ToString(), Text = p.NombreEmpresa }).ToList();

            return View(producto);
        }

        // POST: Producto/Editar/5
        [HttpPost]
        public ActionResult Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                productoDao.Modificar(producto);
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Detalles/5
        public ActionResult Detalles(int id_producto)
        {
            Producto producto = productoDao.ObtenerProductoPorId(id_producto);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/ConfirmarEliminar/5
        public ActionResult ConfirmarEliminar(int id_producto)
        {
            Producto producto = productoDao.ObtenerProductoPorId(id_producto);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Eliminar/5
        [HttpPost]
        public ActionResult Eliminar(int id_producto)
        {
            productoDao.Eliminar(id_producto);
            return RedirectToAction("Index");
        }
    }
}
