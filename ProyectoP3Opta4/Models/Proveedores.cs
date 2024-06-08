using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Proveedores
    {

        private string nombre;
        private string direccion;
        private string telefono;
        private string mail;

        [Required(ErrorMessage = "El nombre es requerido ya que es un identificador primario")]
        public string Nombre { get => nombre; set => nombre = value; }  // PK
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Mail { get => mail; set => mail = value; }

        public Proveedores() { }
        public Proveedores(string nombre, string direccion, string telefono, string mail)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.mail = mail;
        }
    }
}