using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Roles
    {
        public int id_rol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Roles() { }

        public Roles(int id_rol, string nombre, string descripcion)
        {
            this.id_rol = id_rol;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }
    }
}