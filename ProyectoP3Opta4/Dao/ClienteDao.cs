using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
using MySql.Data.MySqlClient;

namespace ProyectoP3Opta4.Dao
{
    public class ClienteDao : Conexion
    {
        public string respGral = "En proceso";
        public void Insertar(Cliente obj) // guardar_ca
        {
            string sql = "INSERT INTO Clientes (nombre,apellido,cedula,gmail,telefono,direccion) VALUES (@p1,@p2,@p3,@p4,@p5,@p6);";
            try
            {
                AbrirConexion();
                MySqlCommand cmd = new MySqlCommand(sql, DBconexion);
                cmd.CommandType = CommandType.Text;
                //cmd.Prepare();
                cmd.Parameters.AddWithValue("@p1", obj.Nombre);
                cmd.Parameters.AddWithValue("@p2", obj.Apellido);
                cmd.Parameters.AddWithValue("@p3", obj.Cedula);
                cmd.Parameters.AddWithValue("@p4", obj.Mail);
                cmd.Parameters.AddWithValue("@p5", obj.Telefono);
                cmd.Parameters.AddWithValue("@p6", obj.Direccion);
                cmd.ExecuteNonQuery();
                Console.Write("grabo con exito");
                respGral = "ok";
            }
            catch (Exception ex)
            {
                new Exception("Error al gravar la tabla...!!!" + ex.Message);
            }
            finally
            {
                CerrarConexion();
                //respGral = "En proceso";
            }
        }

        public void modificar(Cliente obj)
        {

            //string sql = "INSERT INTO Clientes (nombre,apellido,cedula,mail,telefono,direccion)) VALUES (@p1,@p2,@p3,@p4,@p5,@p6);";

            string query = "UPDATE Clientes SET nombre = @p2,apellido = @p3,gmail = @p4,telefono = @p5,direccion= @p6 WHERE cedula = @p1;";
            //string query = "UPDATE Proveedores SET direccion = @p2,telefono = @p3,gmail = @p4 WHERE nombre = @p1;";
            try
            {
                AbrirConexion();
                MySqlCommand Cmd = new MySqlCommand(query, DBconexion);
                Cmd.Parameters.AddWithValue("@p1", obj.Cedula);
                Cmd.Parameters.AddWithValue("@p2", obj.Nombre);
                Cmd.Parameters.AddWithValue("@p3", obj.Apellido);
                Cmd.Parameters.AddWithValue("@p4", obj.Mail);
                Cmd.Parameters.AddWithValue("@p5", obj.Telefono);
                Cmd.Parameters.AddWithValue("@p6", obj.Direccion);

                Cmd.ExecuteNonQuery();
                Console.Write("grabo con exito");
                respGral = "ok";

            }
            catch (Exception ex)
            {
                throw new Exception("Error al grabar la tabla...!!!"
                + ex.Message);
            }
            finally
            {
                CerrarConexion();
                //respGral = "En proceso";
            }
        }

        public void Eliminar_Cli(string cedulaCliente) // guardar_ca
        {
            //string sql = "INSERT INTO Clientes (nombre,apellido,cedula,mail,telefono,direccion)) VALUES (@p1,@p2,@p3,@p4,@p5,@p6);";
            string sql = "DELETE FROM Clientes WHERE cedula = @p1;";
            try
            {
                AbrirConexion();
                MySqlCommand cmd = new MySqlCommand(sql, DBconexion);
                cmd.CommandType = CommandType.Text;
                //cmd.Prepare();
                cmd.Parameters.AddWithValue("@p1", cedulaCliente);
                cmd.ExecuteNonQuery();
                Console.Write("elimino con exito");
                respGral = "ok";
            }
            catch (Exception ex)
            {
                new Exception("Error al eliminar la tabla...!!!" + ex.Message);
            }
            finally
            {
                CerrarConexion();
                //respGral = "En proceso";
            }
        }

        public static DataTable getListaClientes() // regresar todos los datos
        {
            // Conectarse a la base de datos
            string cadena = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection conexionDB;
            DataTable datatable = new DataTable();
            MySqlDataReader resultado;

            try
            {
                conexionDB = new MySqlConnection(cadena);

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Clientes;", conexionDB);
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

        public static DataTable Listado_Clientes(string nombreCliListado) // regresar solo el dato en el input
        {
            // Conectarse a la base de datos
            string cadena = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection conexionDB;
            DataTable datatable = new DataTable();
            MySqlDataReader resultado;


            try
            {
                conexionDB = new MySqlConnection(cadena);
                string query = "SELECT * FROM Clientes WHERE upper(trim(cedula)) like upper(trim(@nombreCliListado));";
                MySqlCommand cmd = new MySqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@nombreCliListado", "%" + nombreCliListado + "%");
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