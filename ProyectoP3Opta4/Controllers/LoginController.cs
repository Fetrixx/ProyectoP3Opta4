using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// using MySql.Data.MySqlClient;
using Npgsql;
using ProyectoP3Opta4.Models;
using ProyectoP3Opta4.Dao;

namespace ProyectoP3Opta4.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session["loggedUser"] = null;
            Session["userType"] = null;
            Session["fullName"] = null;

            return View(new Empleado());
        }

        public ActionResult CerrarSesion()
        {
            Session["loggedUser"] = null;
            Session["userType"] = null;
            Session["fullName"] = null;
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Index(Empleado model)
        {
            if (ModelState.IsValid)
            {
                string cadenaConexion = Conexion.getInstancia().getCadenaConexion();
                string Usuario_datos = model.usuario;
                string Contrasena_datos = model.contrasena;

                try
                {
                    using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
                    {
                        conexion.Open();

                        string query = "SELECT id_rol, nombre_completo FROM Empleados WHERE usuario = @Usuario AND contrasena = @Contrasena";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, conexion))
                        {
                            command.Parameters.AddWithValue("@Usuario", Usuario_datos);
                            command.Parameters.AddWithValue("@Contrasena", Contrasena_datos);

                            // object id_rol = command.ExecuteScalar(); // Obtiene el valor de id_rol

                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Session["loggedUser"] = Usuario_datos;
                                    Session["userType"] = reader["id_rol"].ToString();
                                    Session["fullName"] = reader["nombre_completo"].ToString();

                                    // Los datos coinciden, usuario autenticado
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    // Los datos no coinciden, mostrar error
                                    ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                                    return View(model);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.Message);
                    return View(model);
                }
            }

            // El modelo no es válido, vuelve a mostrar el formulario con los errores
            return View(model);
        }




        private bool ____OLD___CheckEmpleado(string usuario, string contraseña)
        {
            string cadenaConexion = Conexion.getInstancia().getCadenaConexion();
            /*string Usuario_datos = textUsuario.Text;
            string Contrasena_datos = textUsuario.Text;*/


            /*string cadenaConexion = Conexion.getInstancia().getCadenaConexion();*/
            try
            {
                // Realizar la verificación del usuario en la base de datos
                // Usando la cadena de conexión y los datos del usuario
                // Retorna verdadero si el usuario es válido, falso de lo contrario

                using (NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    string query = "SELECT * FROM Empleados WHERE Usuario = @Usuario AND Contraseña = @Contraseña";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);
                        command.Parameters.AddWithValue("@Contraseña", contraseña);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Los datos coinciden, usuario autenticado
                                //MessageBox.Show();
                                Console.WriteLine("Inicio de sesión exitoso");
                                // Debug.WriteLine("Inicio de sesión exitoso");
                                System.Diagnostics.Debug.WriteLine("Inicio de sesión exitoso");

                                return true;
                                //abrirMenuPrincipal();
                            }
                            else
                            {
                                // Los datos no coinciden, mostrar error
                                //MessageBox.Show("Error: Usuario o contraseña incorrectos");
                                Console.WriteLine("Error de inicio");
                                System.Diagnostics.Debug.WriteLine("ERROOOOOOOOOOR");
                            }
                        }
                    }
                }

              
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                TempData["ErrorMessage"] = "Error de conexión: " + ex.Message;

            }
            //return false; // Cambiar esto según el resultado de la verificación


            // Si no se encontró un usuario válido o hubo un error, retorna falso
            TempData["ErrorMessage"] = "Usuario o contraseña incorrectos";
            return false;

        }
    }
}