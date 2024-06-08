using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Cliente
    {
        private string nombre;
        private string apellido;
        private string cedula;
        private string mail;
        private string telefono;
        private string direccion;



        public string Nombre { get => nombre; set => nombre = value; }  // PK
        public string Apellido { get => apellido; set => apellido = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        public Cliente() { }
        public Cliente(string nombre, string apellido, string cedula, string mail, string telefono, string direccion)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.cedula = cedula;
            this.mail = mail;
            this.telefono = telefono;
            this.direccion = direccion;
        }
    }
}