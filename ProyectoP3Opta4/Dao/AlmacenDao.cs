using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using MySql.Data.MySqlClient;
using Npgsql;

namespace ProyectoP3Opta4.Dao
{
    public class AlmacenDao : Conexion
    {
        public static DataTable getListaAlmacenes()
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB;
            DataTable datatable = new DataTable();
            NpgsqlDataReader resultado;

            try
            {
                conexionDB = new NpgsqlConnection(cadena);
                NpgsqlCommand cmd = new NpgsqlCommand(
                    "SELECT a.id_almacen, a.nombre, a.direccion, a.ciudad, a.telefono, " +
                    "COALESCE(SUM(p.cantidad), 0) AS cantidadProductos " +
                    "FROM almacenes a " +
                    "LEFT JOIN productos p ON a.id_almacen = p.id_almacen " +
                    "GROUP BY a.id_almacen, a.nombre, a.direccion, a.ciudad, a.telefono;", conexionDB);
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

        public static DataTable Listado_Almacenes(string nombreCat)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB;
            DataTable datatable = new DataTable();
            NpgsqlDataReader resultado;

            try
            {
                conexionDB = new NpgsqlConnection(cadena);
                string query =
                    "SELECT a.id_almacen, a.nombre, a.direccion, a.ciudad, a.telefono, " +
                    "COALESCE(SUM(p.cantidad), 0) AS cantidadProductos " +
                    "FROM almacenes a " +
                    "LEFT JOIN productos p ON a.id_almacen = p.id_almacen " +
                    "WHERE upper(trim(a.nombre)) LIKE upper(trim(@nombreCat)) " +
                    "GROUP BY a.id_almacen, a.nombre, a.direccion, a.ciudad, a.telefono;";
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