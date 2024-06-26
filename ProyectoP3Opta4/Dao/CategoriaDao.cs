using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

// using MySql.Data.MySqlClient;
using Npgsql;

namespace ProyectoP3Opta4.Dao
{
    public class CategoriaDao : Conexion
    {
        public static DataTable getListaCategorias()
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB;
            DataTable datatable = new DataTable();
            NpgsqlDataReader resultado;

            try
            {
                conexionDB = new NpgsqlConnection(cadena);
                NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT c.id_categoria, c.nombre, c.descripcion, " +
                    "COALESCE(SUM(p.cantidad), 0) AS cantidadProductos " +
                    "FROM categorias c " +
                    "LEFT JOIN productos p ON c.id_categoria = p.id_categoria " +
                    "GROUP BY c.id_categoria, c.nombre, c.descripcion;", conexionDB);
                cmd.CommandType = CommandType.Text;
                conexionDB.Open();
                resultado = cmd.ExecuteReader();
                datatable.Load(resultado);
            }
            catch (Exception ex)
            {
                // Manejo de errores
            }
            return datatable;
        }

        public static DataTable Listado_Categorias(string nombreCat)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB;
            DataTable datatable = new DataTable();
            NpgsqlDataReader resultado;

            try
            {
                conexionDB = new NpgsqlConnection(cadena);
                string query =
                    "SELECT c.id_categoria, c.nombre, c.descripcion, " +
                    "COALESCE(SUM(p.cantidad), 0) AS cantidadProductos " +
                    "FROM categorias c " +
                    "LEFT JOIN productos p ON c.id_categoria = p.id_categoria " +
                    "WHERE upper(trim(c.nombre)) LIKE upper(trim(@nombreCat)) " +
                    "GROUP BY c.id_categoria, c.nombre, c.descripcion;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@nombreCat", "%" + nombreCat + "%");
                cmd.CommandType = CommandType.Text;
                conexionDB.Open();
                resultado = cmd.ExecuteReader();
                datatable.Load(resultado);
            }
            catch (Exception ex)
            {
                // Manejo de errores
            }
            return datatable;
        }
    }
}