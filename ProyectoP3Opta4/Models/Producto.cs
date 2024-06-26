using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Producto
    {
        public int id_producto { get; set; }  // PK autoincremental en la base de datos
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio_unitario { get; set; }
        public int id_categoria { get; set; }
        public string categoria_nombre { get; set; }
        public int id_marca { get; set; }
        public string marca_nombre { get; set; }
        public int id_proveedor { get; set; }
        public string proveedor_nombre { get; set; }
        public int cantidad { get; set; }
        public int id_almacen { get; set; }
        public string almacen_nombre { get; set; }

        public Producto() { }

        public Producto(int id_producto, string nombre, string descripcion, decimal precio_unitario, int id_categoria, int id_marca, int id_proveedor, int cantidad, int id_almacen,string categoria_nombre, string marca_nombre, string proveedor_nombre, string almacen_nombre)
        {
            this.id_producto = id_producto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio_unitario = precio_unitario;
            this.id_categoria = id_categoria;
            this.id_marca = id_marca;
            this.id_proveedor = id_proveedor;
            this.cantidad = cantidad;
            this.id_almacen = id_almacen;
            this.categoria_nombre = categoria_nombre;
            this.marca_nombre = marca_nombre;
            this.proveedor_nombre = proveedor_nombre;
            this.almacen_nombre = almacen_nombre;
        }
    }
}