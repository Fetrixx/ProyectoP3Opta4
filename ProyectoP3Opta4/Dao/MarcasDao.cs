using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using MySql.Data.MySqlClient;
using System.Data;
using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;

namespace ProyectoP3Opta4.Dao
{
    public class MarcasDao : Conexion
    {
        public static DataTable getListaMarcas() // regresar todos los datos
        {
            // Conectarse a la base de datos
            string cadena = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection conexionDB;
            DataTable datatable = new DataTable();
            MySqlDataReader resultado;

            try
            {
                conexionDB = new MySqlConnection(cadena);

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Marcas;", conexionDB);
                cmd.CommandType = CommandType.Text;
                conexionDB.Open();
                resultado = cmd.ExecuteReader();
                datatable.Load(resultado);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return datatable;
        }

        public static DataTable Listado_Marcas(string nombreMarc) // regresar solo el dato en el input
        {
            // Conectarse a la base de datos
            string cadena = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection conexionDB;
            DataTable datatable = new DataTable();
            MySqlDataReader resultado;


            try
            {
                conexionDB = new MySqlConnection(cadena);
                string query = "SELECT * FROM Marcas WHERE upper(trim(Nombre)) like upper(trim(@nombreMarc));";
                MySqlCommand cmd = new MySqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@nombreMarc", "%" + nombreMarc + "%");
                cmd.CommandType = CommandType.Text;
                conexionDB.Open();
                resultado = cmd.ExecuteReader();
                datatable.Load(resultado);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return datatable;
        }
    }
}