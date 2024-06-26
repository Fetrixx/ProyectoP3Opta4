using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Proveedores
    {
        private int id_proveedor;
        private string nombre_empresa;
        private string nombre_contacto;
        private string direccion;
        private string telefono;
        private string correo_electronico;

        public int IdProveedor { get => id_proveedor; set => id_proveedor = value; }

        public string NombreEmpresa { get => nombre_empresa; set => nombre_empresa = value; }

        public string NombreContacto { get => nombre_contacto; set => nombre_contacto = value; }

        public string Direccion { get => direccion; set => direccion = value; }

        public string Telefono { get => telefono; set => telefono = value; }

        public string CorreoElectronico { get => correo_electronico; set => correo_electronico = value; }

        public Proveedores() { }

        public Proveedores(int idProveedor, string nombreEmpresa, string nombreContacto, string direccion, string telefono, string correoElectronico)
        {
            this.id_proveedor = idProveedor;
            this.nombre_empresa = nombreEmpresa;
            this.nombre_contacto = nombreContacto;
            this.direccion = direccion;
            this.telefono = telefono;
            this.correo_electronico = correoElectronico;
        }
    }
}