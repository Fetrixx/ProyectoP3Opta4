using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Marcas
    {
        public int id_marca { get; set; }
        public string nombre { get; set; }
        public string pais_origen { get; set; }
        public int items { get; set; }

        public Marcas() { }

        public Marcas(int id_marca, string nombre, string pais_origen, int items)
        {
            this.id_marca = id_marca;
            this.nombre = nombre;
            this.pais_origen = pais_origen;
            this.items = items;
        }
    }
}