using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoP3Opta4.Models
{
    public class Producto
    {
        private string nombre;
        private int codigoprod;
        private int cantidad;
        private string categoria;
        private string marca;
        private string almacen;



        public string Nombre { get => nombre; set => nombre = value; }  // Pk
        public int CodigoProducto { get => codigoprod; set => codigoprod = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Almacen { get => almacen; set => almacen = value; }


        public Producto() { }
        public Producto(string nombre, int codigoprod, int cantidad, string categoria, string marca, string almacen)
        {
            this.nombre = nombre;
            this.codigoprod = codigoprod;
            this.cantidad = cantidad;
            this.categoria = categoria;
            this.marca = marca;
            this.almacen = almacen;

        }
    }
}