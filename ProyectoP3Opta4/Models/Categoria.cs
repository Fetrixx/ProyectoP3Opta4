using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Categoria
    {
        private string nombre;
        private int cantidadProductos;

        public string Nombre { get => nombre; set => nombre = value; }  // PK
        public int CantidadProductos { get => cantidadProductos; set => cantidadProductos = value; }

        public Categoria() { }
        public Categoria(string nombre, int cantidadProductos)
        {
            this.Nombre = nombre;
            this.CantidadProductos = cantidadProductos;


        }
    }
}