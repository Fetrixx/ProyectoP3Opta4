using Npgsql;
using ProyectoP3Opta4.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ProyectoP3Opta4.Dao;
using ProyectoP3Opta4.Models;
// using Npgsql.Data.NpgsqlClient;
using Npgsql;

namespace ProyectoP3Opta4.Dao
{
    public class EmpleadoDao : Conexion
    {
        public string respGral = "En proceso";

        public void Insertar(Empleado obj)
        {
            string sql = "INSERT INTO Empleados (nombre_completo, usuario, contrasena, id_rol) VALUES (@p1, @p2, @p3, @p4);";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, DBconexion);
                cmd.Parameters.AddWithValue("@p1", obj.nombre_completo);
                cmd.Parameters.AddWithValue("@p2", obj.usuario);
                cmd.Parameters.AddWithValue("@p3", obj.contrasena);
                cmd.Parameters.AddWithValue("@p4", obj.id_rol);
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

        public void Modificar(Empleado obj)
        {
            // string query = "UPDATE Proveedores SET nombre_empresa = @p1, nombre_contacto = @p2, direccion = @p3, telefono = @p4, correo_electronico = @p5 WHERE id_proveedor = @p6;";
            string query = "UPDATE Empleados SET nombre_completo = @p2, usuario = @p3, contrasena = @p4, id_rol = @p5 WHERE id_empleado = @p6;";

            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(query, DBconexion);
                cmd.Parameters.AddWithValue("@p2", obj.nombre_completo);
                cmd.Parameters.AddWithValue("@p3", obj.usuario);
                cmd.Parameters.AddWithValue("@p4", obj.contrasena);
                cmd.Parameters.AddWithValue("@p5", obj.id_rol);
                cmd.Parameters.AddWithValue("@p6", obj.id_empleado);
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

        public void Eliminar(int id_empleado)
        {
            string sql = "DELETE FROM Empleados WHERE id_empleado = @p1;";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, DBconexion);
                cmd.Parameters.AddWithValue("@p1", id_empleado);
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
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Empleados;", conexionDB);
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

        public static DataTable ListadoProveedores(string nombre_completo)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB = new NpgsqlConnection(cadena);
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM Empleados WHERE upper(trim(nombre_completo)) like upper(trim(@nombre_completo));";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@nombre_completo", "%" + nombre_completo + "%");
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
        public DataTable GetEmpleadoPorId(int id)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB = new NpgsqlConnection(cadena);
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM Empleados WHERE id_empleado = @Id;";
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
