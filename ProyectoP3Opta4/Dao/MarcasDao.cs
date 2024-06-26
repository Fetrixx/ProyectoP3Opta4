using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MySql.Data.MySqlClient;
using System.Data;
using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
using Npgsql;

namespace ProyectoP3Opta4.Dao
{
    public class MarcasDao : Conexion
    {
        public static DataTable getListaMarcas()
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB;
            DataTable datatable = new DataTable();
            NpgsqlDataReader resultado;

            try
            {
                conexionDB = new NpgsqlConnection(cadena);
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT m.id_marca, m.nombre, m.pais_origen, SUM(p.cantidad) AS items " +
                                                      "FROM marcas m " +
                                                      "LEFT JOIN productos p ON m.id_marca = p.id_marca " +
                                                      "GROUP BY m.id_marca, m.nombre, m.pais_origen;", conexionDB);
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


        public static DataTable Listado_Marcas(string nombreMarc)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB;
            DataTable datatable = new DataTable();
            NpgsqlDataReader resultado;

            try
            {
                conexionDB = new NpgsqlConnection(cadena);
                string query = "SELECT m.id_marca, m.nombre, m.pais_origen, COUNT(p.id_producto) AS items " +
                               "FROM marcas m " +
                               "LEFT JOIN productos p ON m.id_marca = p.id_marca " +
                               "WHERE upper(trim(m.nombre)) LIKE upper(trim(@nombreMarc)) " +
                               "GROUP BY m.id_marca, m.nombre, m.pais_origen;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@nombreMarc", "%" + nombreMarc + "%");
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