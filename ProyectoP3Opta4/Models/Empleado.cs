using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Empleado
    {
        public int id_empleado { get; set; }
        public string nombre_completo { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public int id_rol { get; set; }
        public string id_nombre { get; set; }

        public Empleado() { }

        public Empleado(int id_empleado, string nombre_completo, string usuario, string contrasena, int id_rol, string id_nombre)
        {
            this.id_empleado = id_empleado ;
            this.nombre_completo = nombre_completo;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.id_rol = id_rol;
            this.id_nombre = id_nombre;
        }
    }
}