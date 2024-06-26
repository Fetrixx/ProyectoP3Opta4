using Npgsql;
using ProyectoP3Opta4.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProyectoP3Opta4.Dao
{
    public class RolesDao : Conexion
    {
        public string respGral = "En proceso";

        public void Insertar(Roles obj)
        {
            string sql = "INSERT INTO Roles (nombre, descripcion) VALUES (@nombre, @descripcion);";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, DBconexion);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                cmd.ExecuteNonQuery();
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el rol...!!! " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void Modificar(Roles obj)
        {
            string sql = "UPDATE Roles SET nombre = @nombre, descripcion = @descripcion WHERE id_rol = @id_rol;";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, DBconexion);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                cmd.Parameters.AddWithValue("@id_rol", obj.id_rol);
                cmd.ExecuteNonQuery();
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el rol...!!! " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public void Eliminar(int id_rol)
        {
            string sql = "DELETE FROM Roles WHERE id_rol = @id_rol;";
            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, DBconexion);
                cmd.Parameters.AddWithValue("@id_rol", id_rol);
                cmd.ExecuteNonQuery();
                respGral = "ok";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el rol...!!! " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public List<Roles> GetListaRoles()
        {
            List<Roles> rolesList = new List<Roles>();
            string query = "SELECT id_rol, nombre, descripcion FROM Roles;";

            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(query, DBconexion);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Roles rol = new Roles
                    {
                        id_rol = Convert.ToInt32(reader["id_rol"]),
                        nombre = reader["nombre"].ToString(),
                        descripcion = reader["descripcion"].ToString()
                    };
                    rolesList.Add(rol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de roles...!!! " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return rolesList;
        }

        public List<Roles> BuscarPorNombre(string nombreRol)
        {
            List<Roles> rolesList = new List<Roles>();
            string query =
                "SELECT id_rol, nombre, descripcion FROM Roles " +
                "WHERE upper(trim(nombre)) LIKE upper(trim(@nombreRol));";

            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(query, DBconexion);
                cmd.Parameters.AddWithValue("@nombreRol", "%" + nombreRol + "%");
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Roles rol = new Roles
                    {
                        id_rol = Convert.ToInt32(reader["id_rol"]),
                        nombre = reader["nombre"].ToString(),
                        descripcion = reader["descripcion"].ToString()
                    };
                    rolesList.Add(rol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar roles por nombre...!!! " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return rolesList;
        }

        public Roles GetRolPorId(int idRol)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            NpgsqlConnection conexionDB;
            Roles rol = null;

            try
            {
                conexionDB = new NpgsqlConnection(cadena);
                string query =
                    "SELECT id_rol, nombre, descripcion " +
                    "FROM Roles " +
                    "WHERE id_rol = @idRol;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@idRol", idRol);
                cmd.CommandType = CommandType.Text;
                conexionDB.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    rol = new Roles
                    {
                        id_rol = Convert.ToInt32(reader["id_rol"]),
                        nombre = reader["nombre"].ToString(),
                        descripcion = reader["descripcion"].ToString()
                    };
                }

                conexionDB.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el rol por ID...!!! " + ex.Message);
            }

            return rol;
        }


        public List<Roles> GetRoles()
        {
            List<Roles> rolesList = new List<Roles>();
            string query = "SELECT id_rol, nombre, descripcion FROM Roles;";

            try
            {
                AbrirConexion();
                NpgsqlCommand cmd = new NpgsqlCommand(query, DBconexion);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Roles rol = new Roles
                    {
                        id_rol = Convert.ToInt32(reader["id_rol"]),
                        nombre = reader["nombre"].ToString(),
                        descripcion = reader["descripcion"].ToString()
                    };
                    rolesList.Add(rol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de roles...!!! " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return rolesList;
        }
    }
}
