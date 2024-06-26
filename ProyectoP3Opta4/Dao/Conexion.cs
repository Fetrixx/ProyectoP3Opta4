﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using ProyectoP3Opta4.Dao;
// using MySql.Data.MySqlClient;
using Npgsql;

namespace ProyectoP3Opta4.Dao
{
    public class Conexion
    {

        protected NpgsqlConnection DBconexion;
        protected NpgsqlCommand DB_Comando;
        protected NpgsqlDataReader DB_dataReader;
        private string server = "localhost";
        private string database = "proyectoOptaP3";
        private string user = "postgres";
        private string password = "root";
        private string cadenaConexion;
        private static Conexion Con = null;

        public Conexion()
        {
            /*
            this.cadenaConexion = "database=" + database +
            "; datasource=" + server +
            "; User ID= " + user +
            "; Password=" + password; */


            this.cadenaConexion = "Host=" + server +
                                 ";Database=" + database +
                                 ";Username=" + user +
                                 ";Password=" + password;

        }
        public string getCadenaConexion()
        {
            /*
            this.cadenaConexion = "database=" + database +
            "; datasource=" + server +
            "; User ID= " + user +
            "; Password=" + password;
            */
            this.cadenaConexion = "Host=" + server +
                                ";Database=" + database +
                                ";Username=" + user +
                                ";Password=" + password;

            return cadenaConexion;
        }

        public void AbrirConexion()
        {
            try
            {
                /*
                string cadena = "Server=localhost;"
                + "Port = 3306;"
                + "User Id = root;"
                + "Password= root;"
                + "Database = Facturacion;";*/
                DBconexion = new NpgsqlConnection(this.cadenaConexion);
                DBconexion.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CerrarConexion()
        {
            try
            {
                DBconexion.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public NpgsqlConnection getCadenaConexionDB() // crear conexion tuto
        {
            NpgsqlConnection cad = new NpgsqlConnection();
            try
            {
                string cadena = "database=" + database +
                    "; datasource=" + server +
                    "; User ID= " + user +
                    "; Password=" + password;

                /*string cadena = "Server=localhost;"
                + "Port = 3306;"
                + "User Id = root;"
                + "Password= root;"
                + "Database = Facturacion;";*/
                cad.ConnectionString = cadena;
                //DBconexion = new MySqlConnection(cadena);
                //DBconexion.Open();
            }
            catch (Exception ex)
            {
                cad = null;
                throw new Exception(ex.Message);
            }
            return cad;
        }


        public static Conexion getInstancia()
        {
            if (Con == null)
            {
                Con = new Conexion();
            }
            return Con;
        }

        public NpgsqlConnection getPruebaDeConexion()
        {

            if (DBconexion == null)
            {
                DBconexion = new NpgsqlConnection(cadenaConexion);
                DBconexion.Open();
            }


            return DBconexion;
        }
    }
}