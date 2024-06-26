using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// using MySql.Data.MySqlClient;

using Npgsql;
using ProyectoP3Opta4.Controllers;
using ProyectoP3Opta4.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace ProyectoP3Opta4.Dao
{
    public class ProductoDao: Conexion
    {
        public string respGral = "En proceso";

        public void Insertar(Producto obj)
        {
            string sql = "INSERT INTO Productos (nombre, descripcion, precio_unitario, id_categoria, id_marca, id_proveedor, cantidad, id_almacen) " +
                         "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8);";

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@p1", obj.nombre);
                        cmd.Parameters.AddWithValue("@p2", obj.descripcion);
                        cmd.Parameters.AddWithValue("@p3", obj.precio_unitario);
                        cmd.Parameters.AddWithValue("@p4", obj.id_categoria);
                        cmd.Parameters.AddWithValue("@p5", obj.id_marca);
                        cmd.Parameters.AddWithValue("@p6", obj.id_proveedor);
                        cmd.Parameters.AddWithValue("@p7", obj.cantidad);
                        cmd.Parameters.AddWithValue("@p8", obj.id_almacen);
                        cmd.ExecuteNonQuery();
                    }
                }
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el producto: " + ex.Message);
            }
        }

        public void Modificar(Producto obj)
        {
            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE Productos SET nombre = @p2, descripcion = @p3, precio_unitario = @p4, " +
                                                                "id_categoria = @p5, id_marca = @p6, id_proveedor = @p7, cantidad = @p8, id_almacen = @p9 " +
                                                                "WHERE id_producto = @p1;", conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@p1", obj.id_producto);
                        cmd.Parameters.AddWithValue("@p2", obj.nombre);
                        cmd.Parameters.AddWithValue("@p3", obj.descripcion);
                        cmd.Parameters.AddWithValue("@p4", obj.precio_unitario);
                        cmd.Parameters.AddWithValue("@p5", obj.id_categoria);
                        cmd.Parameters.AddWithValue("@p6", obj.id_marca);
                        cmd.Parameters.AddWithValue("@p7", obj.id_proveedor);
                        cmd.Parameters.AddWithValue("@p8", obj.cantidad);
                        cmd.Parameters.AddWithValue("@p9", obj.id_almacen);
                        cmd.ExecuteNonQuery();
                    }
                }
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el producto: " + ex.Message);
            }
        }


        public void Eliminar(int id_producto)
        {
            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Productos WHERE id_producto = @id_producto;", conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@id_producto", id_producto);
                        cmd.ExecuteNonQuery();
                    }
                }
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message);
            }
        }
        public List<Producto> ListarProductos(string nombre)
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    string query = "SELECT * FROM Productos WHERE upper(trim(nombre)) LIKE upper(trim(@nombre));";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto
                                {
                                    id_producto = Convert.ToInt32(reader["id_producto"]),
                                    nombre = reader["nombre"].ToString(),
                                    descripcion = reader["descripcion"].ToString(),
                                    precio_unitario = Convert.ToDecimal(reader["precio_unitario"]),
                                    id_categoria = Convert.ToInt32(reader["id_categoria"]),
                                    id_marca = Convert.ToInt32(reader["id_marca"]),
                                    id_proveedor = Convert.ToInt32(reader["id_proveedor"]),
                                    cantidad = Convert.ToInt32(reader["cantidad"]),
                                    id_almacen = Convert.ToInt32(reader["id_almacen"]),
                                    categoria_nombre = ObtenerNombrePorId("categorias", Convert.ToInt32(reader["id_categoria"])),
                                    marca_nombre = ObtenerNombrePorId("marcas", Convert.ToInt32(reader["id_marca"])),
                                    proveedor_nombre = ObtenerNombrePorId("proveedores", Convert.ToInt32(reader["id_proveedor"])),
                                    almacen_nombre = ObtenerNombrePorId("almacenes", Convert.ToInt32(reader["id_almacen"]))
                                };
                                productos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el listado de productos: " + ex.Message);
            }

            return productos;
        }

        // Método para obtener todas las categorías desde la base de datos
        public List<Categoria> CargarCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    string query = "SELECT id_categoria, nombre FROM Categorias;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categoria categoria = new Categoria
                                {
                                    id_categoria = Convert.ToInt32(reader["id_categoria"]),
                                    nombre = reader["nombre"].ToString()
                                };
                                categorias.Add(categoria);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las categorías: " + ex.Message);
            }

            return categorias;
        }

        // Método para obtener todas las marcas desde la base de datos
        public List<Marcas> CargarMarcas()
        {
            List<Marcas> marcas = new List<Marcas>();

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    string query = "SELECT id_marca, nombre FROM Marcas;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Marcas marca = new Marcas
                                {
                                    id_marca = Convert.ToInt32(reader["id_marca"]),
                                    nombre = reader["nombre"].ToString()
                                };
                                marcas.Add(marca);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las marcas: " + ex.Message);
            }

            return marcas;
        }

        // Método para obtener todos los almacenes desde la base de datos
        public List<Almacen> CargarAlmacenes()
        {
            List<Almacen> almacenes = new List<Almacen>();

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    string query = "SELECT id_almacen, nombre FROM Almacenes;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Almacen almacen = new Almacen
                                {
                                    id_almacen = Convert.ToInt32(reader["id_almacen"]),
                                    nombre = reader["nombre"].ToString()
                                };
                                almacenes.Add(almacen);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los almacenes: " + ex.Message);
            }

            return almacenes;
        }

        public List<Proveedores> CargarProveedores()
        {
            List<Proveedores> proveedores = new List<Proveedores>();

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    string query = "SELECT id_proveedor, nombre_empresa FROM Proveedores;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Proveedores proveedor = new Proveedores
                                {
                                    IdProveedor = Convert.ToInt32(reader["id_proveedor"]),
                                    NombreEmpresa = reader["nombre_empresa"].ToString()
                                };
                                proveedores.Add(proveedor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los proveedores: " + ex.Message);
            }

            return proveedores;
        }


        private string ObtenerNombrePorId(string tabla, int id)
        {
            string nombre = string.Empty;
            
                try
                {
                NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion());
                if (tabla.ToLower().Trim() == "almacenes")
                {
                    conexionDB.Open();
                    string query = $"SELECT nombre FROM almacenes WHERE id_almacen = @id;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombre = reader["nombre"].ToString();
                            }
                        }
                    }
                }
                if (tabla.ToLower().Trim() == "categorias")
                {
                    conexionDB.Open();
                    string query = $"SELECT nombre FROM categorias WHERE id_categoria = @id;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombre = reader["nombre"].ToString();
                            }
                        }
                    }
                }
                if (tabla.ToLower().Trim() == "marcas")
                {
                    conexionDB.Open();
                    string query = $"SELECT nombre FROM marcas WHERE id_marca = @id;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombre = reader["nombre"].ToString();
                            }
                        }
                    }
                }
                if (tabla.ToLower().Trim() == "proveedores")
                {
                    conexionDB.Open();
                    string query = $"SELECT nombre_empresa FROM proveedores WHERE id_proveedor = @id;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombre = reader["nombre_empresa"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el nombre de la tabla {tabla}: " + ex.Message);
            }

            return nombre;
        }


        /*
        public Producto ObtenerProductoPorId(int id_producto)
        {
            Producto producto = null;

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Productos WHERE id_producto = @id_producto;", conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@id_producto", id_producto);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                producto = new Producto
                                {
                                    id_producto = Convert.ToInt32(reader["id_producto"]),
                                    nombre = reader["nombre"].ToString(),
                                    descripcion = reader["descripcion"].ToString(),
                                    precio_unitario = Convert.ToDecimal(reader["precio_unitario"]),
                                    id_categoria = Convert.ToInt32(reader["id_categoria"]),
                                    id_marca = Convert.ToInt32(reader["id_marca"]),
                                    id_proveedor = Convert.ToInt32(reader["id_proveedor"]),
                                    cantidad = Convert.ToInt32(reader["cantidad"]),
                                    id_almacen = Convert.ToInt32(reader["id_almacen"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto por ID: " + ex.Message);
            }

            return producto;
        }
        */

        public Producto ObtenerProductoPorId(int id_producto)
        {
            Producto producto = null;

            try
            {
                using (NpgsqlConnection conexionDB = new NpgsqlConnection(getCadenaConexion()))
                {
                    conexionDB.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Productos WHERE id_producto = @id_producto;", conexionDB))
                    {
                        cmd.Parameters.AddWithValue("@id_producto", id_producto);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                producto = new Producto
                                {
                                    id_producto = Convert.ToInt32(reader["id_producto"]),
                                    nombre = reader["nombre"].ToString(),
                                    descripcion = reader["descripcion"].ToString(),
                                    precio_unitario = Convert.ToDecimal(reader["precio_unitario"]),
                                    id_categoria = Convert.ToInt32(reader["id_categoria"]),
                                    id_marca = Convert.ToInt32(reader["id_marca"]),
                                    id_proveedor = Convert.ToInt32(reader["id_proveedor"]),
                                    cantidad = Convert.ToInt32(reader["cantidad"]),
                                    id_almacen = Convert.ToInt32(reader["id_almacen"])
                                };
                            }
                        }
                    }

                    // Cargar nombre de la categoría
                    if (producto != null)
                    {
                        producto.categoria_nombre = ObtenerNombrePorId("Categorias", producto.id_categoria);

                        // Cargar nombre de la marca
                        producto.marca_nombre = ObtenerNombrePorId("Marcas", producto.id_marca);

                        // Cargar nombre del proveedor
                        producto.proveedor_nombre = ObtenerNombrePorId("Proveedores", producto.id_proveedor);

                        // Cargar nombre del almacén
                        producto.almacen_nombre = ObtenerNombrePorId("Almacenes", producto.id_almacen);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto por ID: " + ex.Message);
            }

            return producto;
        }

    }
}