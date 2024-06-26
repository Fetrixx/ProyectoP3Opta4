using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Categoria
    {
        public int id_categoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int cantidadProductos { get; set; }

        public Categoria() { }

        public Categoria(int id_categoria, string nombre, string descripcion, int cantidadProductos)
        {
            this.id_categoria = id_categoria;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.cantidadProductos = cantidadProductos;
        }
    }
}