using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADProduct
    {
        private string constring;

        public CADProduct()
        {
            constring = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ToString();
        }

        public bool CategoryExists(int catId)
        {
            ENCategory category = new ENCategory();
            category.id = catId;

            CADCategory dataCategory = new CADCategory();
            return dataCategory.Read(category); // Retorna true si la categoría existe
        }

        public bool Create(ENProduct en)
        {
            bool success = false;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(constring);

                using (connection)
                {
                    connection.Open();

                    // Instrucción SQL para insertar un producto
                    string insertQuery = "INSERT INTO [dbo].[Products] (name, code, amount, price, category, creationDate) " +
                                         "VALUES (@name, @code, @amount, @price, @category, @creationDate)";

                    if (!CategoryExists(en.category))
                    {
                        Console.WriteLine("Operación fallida. Error: {Categoría Inválida}");
                    }
                    else
                    {
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            // Agregamos los valores de forma segura
                            command.Parameters.AddWithValue("@name", en.name);
                            command.Parameters.AddWithValue("@code", en.code);
                            command.Parameters.AddWithValue("@amount", en.amount);
                            command.Parameters.AddWithValue("@price", en.price);
                            command.Parameters.AddWithValue("@category", en.category);

                            // Asegurar formato correcto para datetime
                            command.Parameters.AddWithValue("@creationDate", en.creationDate);

                            command.ExecuteNonQuery();
                            success = true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al insertar producto: " + ex.Message);
            }

            return success;
        }

        public bool Update(ENProduct en)
        {
            bool actualizado = false;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(constring);

                using (conexion)
                {
                    conexion.Open();

                    // Instrucción SQL a realizar por la base de datos
                    string actualizar = "UPDATE [dbo].[Products] SET name = @name, amount = @amount, price = @price, " +
                                        "category = @category, creationDate = @creationDate WHERE code = @code";

                    using (SqlCommand comando = new SqlCommand(actualizar, conexion))
                    {
                        // Agregar los valores de forma segura usando parámetros
                        comando.Parameters.AddWithValue("@name", en.name);
                        comando.Parameters.AddWithValue("@code", en.code);
                        comando.Parameters.AddWithValue("@amount", en.amount);
                        comando.Parameters.AddWithValue("@price", en.price);
                        comando.Parameters.AddWithValue("@category", en.category);
                        comando.Parameters.AddWithValue("@creationDate", en.creationDate);

                        comando.ExecuteNonQuery();
                        actualizado = true;
                    }
                }
            }
            // Control de excepciones
            catch (SqlException ex)
            {
                Console.WriteLine("La operación del usuario ha fallado. Error: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("La operación del usuario ha fallado. Error: {0}", ex.Message);
            }
            // Cierra la conexión sin importar lo que realice el método
            finally
            {
                if (conexion != null) conexion.Close();
            }

            return actualizado;
        }

        public bool Delete(ENProduct producto)
        {
            bool eliminado = false;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(constring);
                conexion.Open();

                // Instrucción SQL a realizar por la base de datos
                string eliminar = "DELETE FROM [dbo].[Products] WHERE code = @code";

                using (SqlCommand comando = new SqlCommand(eliminar, conexion))
                {
                    // Usando parámetros para evitar SQL Injection
                    comando.Parameters.AddWithValue("@code", producto.code);

                    comando.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            // Control de excepciones
            catch (SqlException ex)
            {
                Console.WriteLine("La operación del usuario ha fallado. Error: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("La operación del usuario ha fallado. Error: {0}", ex.Message);
            }
            // Cierra la conexión sin importar lo que realice el método
            finally
            {
                if (conexion != null) conexion.Close();
            }

            return eliminado;
        }

        public bool Read(ENProduct en)
        {
            bool leidoExitoso = false;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(constring);
                conexion.Open();

                // Instrucción SQL a realizar por la base de datos
                string leerQuery = "SELECT * FROM [dbo].[Products] WHERE code = @code";

                using (SqlCommand comando = new SqlCommand(leerQuery, conexion))
                {
                    // Usando parámetros para evitar SQL Injection
                    comando.Parameters.AddWithValue("@code", en.code);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            if (lector["code"].ToString() == en.code)
                            {
                                en.code = lector["code"].ToString();
                                en.name = lector["name"].ToString();
                                en.amount = int.Parse(lector["amount"].ToString());
                                en.price = float.Parse(lector["price"].ToString());
                                en.category = int.Parse(lector["category"].ToString());
                                en.creationDate = DateTime.Parse(lector["creationDate"].ToString());

                                leidoExitoso = true;
                            }
                        }
                    }
                }
            }
            // Control de excepciones
            catch (SqlException ex)
            {
                Console.WriteLine("La operación del usuario ha fallado. Error: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("La operación del usuario ha fallado. Error: {0}", ex.Message);
            }
            // Cierra la conexión sin importar lo que realice el método
            finally
            {
                if (conexion != null) conexion.Close();
            }

            return leidoExitoso;
        }

        public bool ReadFirst(ENProduct en)
        {
            bool encontradoProducto = false;
            using (SqlConnection conexionBD = new SqlConnection(constring))
            {
                string query = "SELECT TOP 1 name, code, amount, price, category, creationDate FROM Products ORDER BY id ASC";
                SqlCommand sqlComando = new SqlCommand(query, conexionBD);

                try
                {
                    conexionBD.Open();
                    SqlDataReader dataReader = sqlComando.ExecuteReader();
                    if (dataReader.Read())
                    {
                        en.code = dataReader["code"].ToString();
                        en.name = dataReader["name"].ToString();
                        en.amount = Convert.ToInt32(dataReader["amount"]);
                        en.price = Convert.ToSingle(dataReader["price"]);
                        en.category = Convert.ToInt32(dataReader["category"]);
                        en.creationDate = Convert.ToDateTime(dataReader["creationDate"]);
                        encontradoProducto = true;
                    }
                    dataReader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer el primer producto: {ex.Message}");
                }
            }
            return encontradoProducto;
        }

        public bool ReadNext(ENProduct en)
        {
            bool productoEncontrado = false;
            using (SqlConnection conexionBD = new SqlConnection(constring))
            {
                string query = "SELECT TOP 1 name, code, amount, price, category, creationDate FROM Products WHERE id > (SELECT id FROM Products WHERE code = @code) ORDER BY id ASC";
                SqlCommand sqlComando = new SqlCommand(query, conexionBD);
                sqlComando.Parameters.AddWithValue("@code", en.code);

                try
                {
                    conexionBD.Open();
                    SqlDataReader dataReader = sqlComando.ExecuteReader();
                    if (dataReader.Read())
                    {
                        en.code = dataReader["code"].ToString();
                        en.name = dataReader["name"].ToString();
                        en.amount = Convert.ToInt32(dataReader["amount"]);
                        en.price = Convert.ToSingle(dataReader["price"]);
                        en.category = Convert.ToInt32(dataReader["category"]);
                        en.creationDate = Convert.ToDateTime(dataReader["creationDate"]);
                        productoEncontrado = true;
                    }
                    dataReader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al leer el siguiente producto: {ex.Message}");
                }
            }
            return productoEncontrado;
        }

        public bool ReadPrev(ENProduct en)
        {
            bool productoLeido = false;
            SqlConnection conexion = null;

            try
            {
                conexion = new SqlConnection(constring);
                conexion.Open();

                // Instrucción SQL a realizar por la base de datos
                string consultaSQL = "SELECT code, name, amount, price, category, creationDate FROM [dbo].[Products] ORDER BY Code";

                using (SqlCommand comando = new SqlCommand(consultaSQL, conexion))
                using (SqlDataReader lectorDatos = comando.ExecuteReader())
                {
                    ENProduct productoAnterior = null; // Inicializamos como null para evitar datos vacíos

                    // Usamos un bucle que no depende directamente de `Read()`
                    while (lectorDatos.Read())
                    {
                        string codigoActual = lectorDatos["code"].ToString();

                        if (en.code == codigoActual && productoAnterior != null)  // Comprobamos que productoAnterior tiene datos
                        {
                            en.code = productoAnterior.code;
                            en.name = productoAnterior.name;
                            en.amount = productoAnterior.amount;
                            en.price = productoAnterior.price;
                            en.category = productoAnterior.category;
                            en.creationDate = productoAnterior.creationDate;
                            productoLeido = true;
                            break;  // Salimos del bucle si encontramos el producto anterior
                        }

                        // Guardamos la información del producto actual en `productoAnterior`
                        productoAnterior = new ENProduct
                        {
                            code = codigoActual,
                            name = lectorDatos["name"].ToString(),
                            amount = Convert.ToInt32(lectorDatos["amount"]),
                            price = Convert.ToSingle(lectorDatos["price"]),
                            category = lectorDatos["category"] != DBNull.Value ? Convert.ToInt32(lectorDatos["category"]) : 0,
                            creationDate = Convert.ToDateTime(lectorDatos["creationDate"])
                        };
                    }
                }
            }
            // Control de excepciones
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }
            // Cierra la conexión sin importar lo que realice el método
            finally
            {
                conexion?.Close();
            }

            return productoLeido;
        }
    }
}