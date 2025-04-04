using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADCategory
    {
        private string constring { get; set; }

        public CADCategory()
        {
            constring = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ToString();
        }

        public bool Read(ENCategory en)
        {
            bool resultadoLectura;
            resultadoLectura = false;
            SqlConnection conexionBaseDatos = null;

            try
            {
                conexionBaseDatos = new SqlConnection(constring);
                conexionBaseDatos.Open();

                string consultaSql = "SELECT * FROM [dbo].[Categories] WHERE id = '" + en.Id + "'";

                SqlCommand comandoSql = new SqlCommand(consultaSql, conexionBaseDatos);
                SqlDataReader lectorDatos = comandoSql.ExecuteReader();

                lectorDatos.Read();

                if (int.Parse(lectorDatos["id"].ToString()) == en.Id)
                {
                    en.Id = int.Parse(lectorDatos["id"].ToString());
                    en.Name = lectorDatos["name"].ToString();
                    resultadoLectura = true;
                }

                lectorDatos.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("User operation has failed. Error: {0}", ex.Message);
            }
            finally
            {
                if (conexionBaseDatos != null) conexionBaseDatos.Close();
            }

            return resultadoLectura;
        }

        public List<ENCategory> ReadAll()
        {
            List<ENCategory> listaCategorias = new List<ENCategory>();
            SqlConnection conexionDb = null;

            try
            {
                conexionDb = new SqlConnection(constring);
                conexionDb.Open();

                // instrucción SQL a realizar por la DB
                string consultaSql = "SELECT * FROM [dbo].[Categories] ";

                SqlCommand comandoSql = new SqlCommand(consultaSql, conexionDb);
                SqlDataReader lectorDatos = comandoSql.ExecuteReader();

                while (lectorDatos.Read())
                {
                    ENCategory categoria = new ENCategory();
                    categoria.Id = int.Parse(lectorDatos["id"].ToString());
                    categoria.Name = lectorDatos["name"].ToString();

                    listaCategorias.Add(categoria);
                }

                lectorDatos.Close();
            }
            // control de excepciones
            catch (SqlException ex)
            {
                Console.WriteLine("Error al realizar la operación en la base de datos.Detalles del error: { 0}", ex.Message);
            }
            
            finally
            {
                if (conexionDb != null) conexionDb.Close();
            }

            return listaCategorias;
        }


    }
}
