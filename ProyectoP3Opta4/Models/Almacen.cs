using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Almacen
    {
         public int id_almacen { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string telefono { get; set; }
        public int cantidadProductos { get; set; }

        public Almacen() { }

        public Almacen(int id_almacen, string nombre, string direccion, string ciudad, string telefono, int cantidadProductos)
        {
            this.id_almacen = id_almacen;
            this.nombre = nombre;
            this.direccion = direccion;
            this.ciudad = ciudad;
            this.telefono = telefono;
            this.cantidadProductos = cantidadProductos;
        }
    }
}