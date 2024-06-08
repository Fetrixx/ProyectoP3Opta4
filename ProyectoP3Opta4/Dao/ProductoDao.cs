using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using ProyectoP3Opta4.Controllers;
using ProyectoP3Opta4.Models;
using System.Data;

namespace ProyectoP3Opta4.Dao
{
    public class ProductoDao: Conexion
    {
        public string respGral = "En proceso";
        Producto prod = new Producto();


        public void Insertar(Producto obj) // guardar_ca
        {
            string sql = "INSERT INTO Productos (CodigoProducto, Nombre, Cantidad, categoria, marca, almacen) VALUES (@p1,@p2,@p3,@p4,@p5,@p6);";
            try
            {
                AbrirConexion();
                MySqlCommand cmd = new MySqlCommand(sql, DBconexion);
                cmd.CommandType = CommandType.Text;
                //cmd.Prepare();
                cmd.Parameters.AddWithValue("@p1", obj.CodigoProducto);
                cmd.Parameters.AddWithValue("@p2", obj.Nombre);
                cmd.Parameters.AddWithValue("@p3", obj.Cantidad);
                cmd.Parameters.AddWithValue("@p4", obj.Categoria);
                cmd.Parameters.AddWithValue("@p5", obj.Marca);
                cmd.Parameters.AddWithValue("@p6", obj.Almacen);
                cmd.ExecuteNonQuery();

                Console.Write("grabo con exito");

                respGral = "ok";


            }
            catch (Exception ex)
            {
                new Exception("Error al grabar la tabla...!!!" + ex.Message);
            }

            finally // una vez cerrada la conexion anterior, se hace:
            {
                CerrarConexion();
                //respGral = "En proceso";
                try                         // AGREGAR += A LAS TABLAS
                {
                    // MenuProducto mnuProd = new MenuProducto();
                    sumarATabla("Categorias", "CantidadProductos", obj.Cantidad, "Nombre", obj.Categoria);                       // SUMAR TABLAAAAAAAAAAAAA
                    sumarATabla("Marcas", "Items", obj.Cantidad, "Nombre", obj.Marca);
                    sumarATabla("Almacenes", "CantidadProductos", obj.Cantidad, "Nombre", obj.Almacen);
                }
                catch (Exception)
                {
                    // MessageBox.Show("Error al sumar productos a de las tablas", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }

            try
            {


            }
            catch (Exception)
            {

                throw;
            }
        }


        public static DataTable Listado_Productos(string nombreProdListado) // regresar solo el dato en el input
        {
            // Conectarse a la base de datos
            string cadena = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection conexionDB;
            DataTable datatable = new DataTable();
            MySqlDataReader resultado;


            try
            {
                conexionDB = new MySqlConnection(cadena);
                string query = "SELECT * FROM Productos WHERE upper(trim(CodigoProducto)) like upper(trim(@nombreProdListado));";
                MySqlCommand cmd = new MySqlCommand(query, conexionDB);
                cmd.Parameters.AddWithValue("@nombreProdListado", "%" + nombreProdListado + "%");
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

        

        public int getDatoModif(string nombreTabla, string nombreValor, int CodigoProducto)
        {

            int val;

            string cadenaConex = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection connection = new MySqlConnection(cadenaConex);

            MySqlCommand command = new MySqlCommand($"SELECT {nombreValor} FROM {nombreTabla} WHERE CodigoProducto = {CodigoProducto} ;", connection);      // cargar Categorias
            command.CommandType = CommandType.Text;
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            val = Convert.ToInt32(reader[$"{nombreValor}"].ToString());
            reader.Close(); // Cierra el lector
            return val;
        }

        public Producto duplicarProd(int codigoProd)
        {
            string cadena = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection conexionDB;
            Producto prod = new Producto();
            try
            {
                conexionDB = new MySqlConnection(cadena);

                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Productos WHERE CodigoProducto = {codigoProd};", conexionDB);
                cmd.CommandType = CommandType.Text;
                conexionDB.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                prod.Cantidad = Convert.ToInt32(reader[$"Cantidad"].ToString());
                prod.Categoria = reader["categoria"].ToString();
                prod.Marca = reader["marca"].ToString();
                prod.Almacen = reader["almacen"].ToString();

                reader.Close(); // Cierra el lector
                return prod;
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al duplicar producto", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.Message);
            }


            return prod;
        }


        public void modificar(Producto obj)
        {
            Producto prodAnterior = duplicarProd(obj.CodigoProducto);
            // MessageBox.Show($"Producto anteriormente tenia: {prodAnterior.Cantidad}");
            // MessageBox.Show($"Producto actualmente tiene: {obj.Cantidad}");
            try
            {
                if (prodAnterior.Cantidad != obj.Cantidad) // si se cambio la cantidad
                {
                    // MessageBox.Show("Se modifico la cantidad del producto");
                    if (prodAnterior.Cantidad < obj.Cantidad) // ahora hay mas prod.
                    {
                        int cantNueva = obj.Cantidad - prodAnterior.Cantidad;
                        sumarATabla("Categorias", "CantidadProductos", cantNueva, "Nombre", obj.Categoria);
                        sumarATabla("Marcas", "Items", cantNueva, "Nombre", obj.Marca);
                        sumarATabla("Almacenes", "CantidadProductos", cantNueva, "Nombre", obj.Almacen);
                    }
                    else // hay menos productos
                    {
                        int cantNueva = prodAnterior.Cantidad - obj.Cantidad;
                        restarATabla("Categorias", "CantidadProductos", cantNueva, "Nombre", obj.Categoria);
                        restarATabla("Marcas", "Items", cantNueva, "Nombre", obj.Marca);
                        restarATabla("Almacenes", "CantidadProductos", cantNueva, "Nombre", obj.Almacen);
                    }
                }
            }
            catch (Exception)
            {
                // MessageBox.Show("Error al modificar cantidad de prod", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (prodAnterior.Categoria != obj.Categoria)   // cambio en categoria, se mueve la cantidad de cat a cat
                {
                    // MessageBox.Show("Se modifico la categoria del producto");
                    restarATabla("Categorias", "CantidadProductos", prodAnterior.Cantidad, "Nombre", prodAnterior.Categoria); // restar de categoria anterior
                    sumarATabla("Categorias", "CantidadProductos", obj.Cantidad, "Nombre", obj.Categoria);      // agregar a nueva cat                                              
                }
            }
            catch (Exception)
            {
                // MessageBox.Show("Error al modificar categoria de prod", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (prodAnterior.Marca != obj.Marca)   // cambio en marca
                {
                    // MessageBox.Show("Se modifico la marca del producto");
                    restarATabla("Marcas", "Items", prodAnterior.Cantidad, "Nombre", prodAnterior.Marca);
                    sumarATabla("Marcas", "Items", obj.Cantidad, "Nombre", obj.Marca);
                }
            }
            catch (Exception)
            {
                // MessageBox.Show("Error al modificar marca de prod", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (prodAnterior.Almacen != obj.Almacen)   // cambio en almacen
                {
                    // MessageBox.Show("Se modifico el almacen del producto");
                    restarATabla("Almacenes", "CantidadProductos", prodAnterior.Cantidad, "Nombre", prodAnterior.Almacen);
                    sumarATabla("Almacenes", "CantidadProductos", obj.Cantidad, "Nombre", obj.Almacen);
                }
            }
            catch (Exception)
            {
                // MessageBox.Show("Error al modificar marca de prod", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            string query = "UPDATE Productos SET Nombre = @p2, Cantidad = @p3, categoria = @p4, marca = @p5, almacen = @p6 WHERE CodigoProducto = @p1;";
            try
            {
                AbrirConexion();
                MySqlCommand Cmd = new MySqlCommand(query, DBconexion);
                Cmd.Parameters.AddWithValue("@p1", obj.CodigoProducto);
                Cmd.Parameters.AddWithValue("@p2", obj.Nombre);
                Cmd.Parameters.AddWithValue("@p3", obj.Cantidad);
                Cmd.Parameters.AddWithValue("@p4", obj.Categoria);
                Cmd.Parameters.AddWithValue("@p5", obj.Marca);
                Cmd.Parameters.AddWithValue("@p6", obj.Almacen);

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
            }
        }

        public void Eliminar_Prod(Producto obj) // guardar_ca
        {
            try                         // eliminar -= a tablas
            {
                // MenuProducto mnuProd = new MenuProducto();


                restarATabla("Categorias", "CantidadProductos", obj.Cantidad, "Nombre", obj.Categoria);
                restarATabla("Marcas", "Items", obj.Cantidad, "Nombre", obj.Marca);
                restarATabla("Almacenes", "CantidadProductos", obj.Cantidad, "Nombre", obj.Almacen);
            }
            catch (Exception)
            {
                // MessageBox.Show("Error al restar productos a de las tablas", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            string sql = "DELETE FROM Productos WHERE CodigoProducto = @p1;";
            try
            {
                AbrirConexion();
                MySqlCommand cmd = new MySqlCommand(sql, DBconexion);
                cmd.CommandType = CommandType.Text;
                //cmd.Prepare();
                cmd.Parameters.AddWithValue("@p1", obj.CodigoProducto);
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

            }
        }


        public static DataTable getListaProductos() // regresar todos los datos
        {
            // Conectarse a la base de datos
            string cadena = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection conexionDB;
            DataTable datatable = new DataTable();
            MySqlDataReader resultado;

            try
            {
                conexionDB = new MySqlConnection(cadena);

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Productos;", conexionDB);
                cmd.CommandType = CommandType.Text;
                conexionDB.Open();
                resultado = cmd.ExecuteReader();
                datatable.Load(resultado);
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error en ProductoDao getListaProductos()", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.Message);
            }
            return datatable;
        }

        /*
        public static void cargarBoxes(ComboBox cboxCat, ComboBox cboxMar, ComboBox cboxAlm)
        {

            string cadenaConex = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection connection = new MySqlConnection(cadenaConex);

            MySqlCommand command = new MySqlCommand("SELECT Nombre FROM Categorias;", connection);      // cargar Categorias
            MySqlCommand command2 = new MySqlCommand("SELECT Nombre FROM Marcas;", connection);
            MySqlCommand command3 = new MySqlCommand("SELECT Nombre FROM Almacenes;", connection);

            command.CommandType = CommandType.Text;// Ejecuta la consulta
            command2.CommandType = CommandType.Text;
            command3.CommandType = CommandType.Text;
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();// Obtiene el resultado de la consulta

            while (reader.Read())
            {
                // Agrega los resultados de la consulta al combobox
                cboxCat.Items.Add(reader["Nombre"].ToString());
            }
            reader.Close(); // Cierra el lector




            MySqlDataReader reader2 = command2.ExecuteReader();// Obtiene el resultado de la consulta       Cargar Marcas

            while (reader2.Read())
            {
                // Agrega los resultados de la consulta al combobox
                cboxMar.Items.Add(reader2["Nombre"].ToString());
            }
            reader2.Close();

            MySqlDataReader reader3 = command3.ExecuteReader();// Obtiene el resultado de la consulta       Cargar Almacenes

            while (reader3.Read())
            {
                // Agrega los resultados de la consulta al combobox
                cboxAlm.Items.Add(reader3["Nombre"].ToString());
            }
            reader3.Close();

        }
        */

        public static List<string> cargarCategorias()
        {
            List<string> categorias = new List<string>();

            string cadenaConex = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection connection = new MySqlConnection(cadenaConex);

            MySqlCommand command = new MySqlCommand("SELECT Nombre FROM Categorias;", connection); // Consulta para obtener todas las categorías
            command.CommandType = CommandType.Text;

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Agrega cada nombre de categoría a la lista
                    categorias.Add(reader["Nombre"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si ocurre algún error
                // Por ejemplo, puedes registrar el error o lanzar una excepción
                Console.WriteLine("Error al cargar las categorías: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Cierra la conexión
            }

            return categorias;
        }

        public static List<string> cargarMarcas()
        {
            List<string> marcas = new List<string>();

            string cadenaConex = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection connection = new MySqlConnection(cadenaConex);

            MySqlCommand command = new MySqlCommand("SELECT Nombre FROM Marcas;", connection); // Consulta para obtener todas las marcas
            command.CommandType = CommandType.Text;

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Agrega cada nombre de marca a la lista
                    marcas.Add(reader["Nombre"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si ocurre algún error
                Console.WriteLine("Error al cargar las marcas: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Cierra la conexión
            }

            return marcas;
        }

        public static List<string> cargarAlmacenes()
        {
            List<string> almacenes = new List<string>();

            string cadenaConex = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection connection = new MySqlConnection(cadenaConex);

            MySqlCommand command = new MySqlCommand("SELECT Nombre FROM Almacenes;", connection); // Consulta para obtener todos los almacenes
            command.CommandType = CommandType.Text;

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Agrega cada nombre de almacén a la lista
                    almacenes.Add(reader["Nombre"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si ocurre algún error
                Console.WriteLine("Error al cargar los almacenes: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Cierra la conexión
            }

            return almacenes;
        }



        public void sumarATabla(string nombreTabla, string nombreValor, int valorASumar, string nombreID, string nombreItem) // reutilizar cargarBoxes
        {
            int val = getDato(nombreTabla, nombreValor, nombreItem); // agarra el valor para hacer la sumatoria

            val += valorASumar;
            // where nombre = nombreDeCategoria
            string query = $"UPDATE {nombreTabla} SET {nombreValor} = @p2 WHERE {nombreID} = @p1;";
            try
            {
                AbrirConexion();
                MySqlCommand Cmd = new MySqlCommand(query, DBconexion);
                Cmd.Parameters.AddWithValue("@p1", nombreItem);
                Cmd.Parameters.AddWithValue("@p2", val);

                Cmd.ExecuteNonQuery();
                Console.Write("grabo con exito");
                respGral = "ok";

            }
            catch (Exception ex)
            {
                throw new Exception("Error al += en la la tabla...!!!"
                + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

        }

        public void restarATabla(string nombreTabla, string nombreValor, int valorARestar, string nombreID, string nombreItem) // reutilizar cargarBoxes

        {
            int val = getDatoElim(nombreTabla, nombreValor, nombreItem); // agarra el valor para hacer la sumatoria



            val -= valorARestar;

            string query = $"UPDATE {nombreTabla} SET {nombreValor} = @p2 WHERE {nombreID} = @p1;";
            try
            {
                AbrirConexion();
                MySqlCommand Cmd = new MySqlCommand(query, DBconexion);
                Cmd.Parameters.AddWithValue("@p1", nombreItem);
                Cmd.Parameters.AddWithValue("@p2", val);

                Cmd.ExecuteNonQuery();
                Console.Write("grabo con exito");
                respGral = "ok";

            }
            catch (Exception ex)
            {
                throw new Exception("Error al += en la la tabla...!!!"
                + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        public int getDato(string nombreTabla, string nombreValor, string nombreItem)
        {
            int val;
            string cadenaConex = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection connection = new MySqlConnection(cadenaConex);

            MySqlCommand command = new MySqlCommand($"SELECT {nombreValor} FROM {nombreTabla} WHERE Nombre = '{nombreItem}' ;", connection);      // cargar Categorias
            command.CommandType = CommandType.Text;
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            val = Convert.ToInt32(reader[$"{nombreValor}"].ToString());
            reader.Close(); // Cierra el lector
            return val;
        }


        public int getDatoElim(string nombreTabla, string nombreValor, string nombreItem)
        {
            int val;

            string cadenaConex = Conexion.getInstancia().getCadenaConexion();
            MySqlConnection connection = new MySqlConnection(cadenaConex);

            MySqlCommand command = new MySqlCommand($"SELECT {nombreValor} FROM {nombreTabla} WHERE Nombre = '{nombreItem}' ;", connection);      // cargar Categorias
            command.CommandType = CommandType.Text;
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            val = Convert.ToInt32(reader[$"{nombreValor}"].ToString());
            reader.Close(); // Cierra el lector
            return val;

        }


        public Producto ObtenerProductoPorCodigo(int codigoProducto)
        {
            Producto producto = null;

            try
            {
                AbrirConexion();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Productos WHERE CodigoProducto = @codigoProducto", DBconexion);
                cmd.Parameters.AddWithValue("@codigoProducto", codigoProducto);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    producto = new Producto
                    {
                        CodigoProducto = Convert.ToInt32(reader["CodigoProducto"]),
                        Nombre = reader["Nombre"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Categoria = reader["categoria"].ToString(),
                        Marca = reader["marca"].ToString(),
                        Almacen = reader["almacen"].ToString()
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto por código: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return producto;
        }

    }
}