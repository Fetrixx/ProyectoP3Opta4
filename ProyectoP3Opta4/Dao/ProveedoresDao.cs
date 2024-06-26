using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
// using Npgsql.Data.NpgsqlClient;
using Npgsql;

namespace ProyectoP3Opta4.Dao
{
    public class ProveedoresDao: Conexion
    {
        public string respGral = "En proceso";

        public void Insertar(Proveedores obj)
        {
            string sql = "INSERT INTO Proveedores (nombre_empresa, nombre_contacto, direccion, telefono, correo_electronico) VALUES (@p1, @p2, @p3, @p4, @p5);";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, DBconexion);
                cmd.Parameters.AddWithValue("@p1", obj.NombreEmpresa);
                cmd.Parameters.AddWithValue("@p2", obj.NombreContacto);
                cmd.Parameters.AddWithValue("@p3", obj.Direccion);
                cmd.Parameters.AddWithValue("@p4", obj.Telefono);
                cmd.Parameters.AddWithValue("@p5", obj.CorreoElectronico);
                cmd.ExecuteNonQuery();
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al grabar en la tabla...!!!" + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void Modificar(Proveedores obj)
        {
            string query = "UPDATE Proveedores SET nombre_empresa = @p1, nombre_contacto = @p2, direccion = @p3, telefono = @p4, correo_electronico = @p5 WHERE id_proveedor = @p6;";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(query, DBconexion);
                cmd.Parameters.AddWithValue("@p1", obj.NombreEmpresa);
                cmd.Parameters.AddWithValue("@p2", obj.NombreContacto);
                cmd.Parameters.AddWithValue("@p3", obj.Direccion);
                cmd.Parameters.AddWithValue("@p4", obj.Telefono);
                cmd.Parameters.AddWithValue("@p5", obj.CorreoElectronico);
                cmd.Parameters.AddWithValue("@p6", obj.IdProveedor);
                cmd.ExecuteNonQuery();
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al grabar en la tabla...!!!" + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void Eliminar(int idProveedor)
        {
            string sql = "DELETE FROM Proveedores WHERE id_proveedor = @p1;";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, DBconexion);
                cmd.Parameters.AddWithValue("@p1", idProveedor);
                cmd.ExecuteNonQuery();
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar en la tabla...!!!" + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public static DataTable GetListaProveedores()
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB = new NpgsqlConnection(cadena);
            DataTable dataTable = new DataTable();

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Proveedores;", conexionDB);
                conexionDB.Open();
                NpgsqlDataReader resultado = cmd.ExecuteReader();
                dataTable.Load(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de proveedores...!!!" + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }

            return dataTable;
        }

        public static DataTable ListadoProveedores(string nombreEmpresa)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB = new NpgsqlConnection(cadena);
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM Proveedores WHERE upper(trim(nombre_empresa)) like upper(trim(@nombreEmpresa));";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@nombreEmpresa", "%" + nombreEmpresa + "%");
                conexionDB.Open();
                NpgsqlDataReader resultado = cmd.ExecuteReader();
                dataTable.Load(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar proveedores...!!!" + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }

            return dataTable;
        }

        // Método para obtener un proveedor por su ID
        public DataTable GetProveedorPorId(int id)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB = new NpgsqlConnection(cadena);
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM Proveedores WHERE id_proveedor = @Id;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@Id", id);
        
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                conexionDB.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el proveedor por ID...!!! " + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }

            return dataTable;
        }

    }
}