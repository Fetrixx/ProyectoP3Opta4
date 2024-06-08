using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Marcas
    {
        private string nombre;
        private int items;
        
        public string Nombre { get => nombre; set => nombre = value; }  // Pk
        public int Items { get => items; set => items = value; }

        public Marcas() { }
        public Marcas(string nombre, int items)
        {
            this.nombre = nombre;
            this.items = items;
        }
    }
}