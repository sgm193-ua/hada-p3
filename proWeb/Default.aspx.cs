using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using library;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {

        public class ProductException : Exception
        {
            public ProductException(string message) : base(message)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CADCategory cadCategory = new CADCategory();

                List<ENCategory> categorias = cadCategory.ReadAll();

                ddlCategory.DataSource = categorias;

                ddlCategory.DataTextField = "name";
                ddlCategory.DataValueField = "id";

                ddlCategory.DataBind();
            }
        }


        private bool CheckeoDatos(out string errorMsg)
        {
            errorMsg = null;
            //checkeo para code
            if (string.IsNullOrWhiteSpace(textCode.Text) || textCode.Text.Length > 16)
            {
                errorMsg = "Code debe tener entre 1 y 16 caracteres.";
                return false;
            }
            //checkeo para name
            if (textName.Text.Length > 32)
            {
                errorMsg = "Name no puede superar 32 caracteres.";
                return false;
            }
            //checkeo para amount
            int amount;
            if (!int.TryParse(textAmount.Text, out amount) || amount < 0 || amount > 9999)
            {
                errorMsg = "Amount debe ser un entero entre 0 y 9999.";
                return false;
            }
            //checkeo para price
            float price;
            if (!float.TryParse(textPrice.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out price)
                || price < 0 || price > 9999.99f)
            {
                errorMsg = "Price debe ser un valor real entre 0 y 9999,99.";
                return false;
            }
            //checkeo para datetime
            DateTime creationDate;
            if (!DateTime.TryParseExact(textDate.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out creationDate))
            {
                errorMsg = "Creation Date debe seguir el formato dd/MM/yyyy HH:mm:ss.";
                return false;
            }
            //checkeo category
            int category;
            if (!int.TryParse(ddlCategory.SelectedValue, out category) || category < 0 || category > 3)
            {
                errorMsg = "Category inválida (debe ser 0,1,2 o 3).";
                return false;
            }

            return true;
        }

        private ENProduct CrearObjetoProducto()
        {
            // Tras validar, convertimos a los tipos correctos
            int amount = int.Parse(textAmount.Text);
            float price = float.Parse(textPrice.Text, CultureInfo.InvariantCulture);
            DateTime creationDate = DateTime.ParseExact(textDate.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            int category = int.Parse(ddlCategory.SelectedValue);

            // Construimos el ENProduct
            ENProduct product = new ENProduct(
                code: textCode.Text,
                name: textName.Text,
                amount: amount,
                price: price,
                category: category,
                creationDate: creationDate
            );
            return product;
        }

        private void MostrarError(Exception ex)
        {
            mensaje.ForeColor = System.Drawing.Color.Red; // para errores
            mensaje.Text = "Error: " + ex.Message;
            Console.WriteLine("La operación con el producto ha fallado. Error: {0}", ex.Message);
        }


        // CREATE
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar campos
                if (!CheckeoDatos(out string errorMsg))
                {
                    throw new ProductException(errorMsg);  // Lanza una excepción si los datos no son válidos
                }

                // 2. Verificar que NO existe ya un producto con ese Code usando un objeto auxiliar
                ENProduct checker = new ENProduct();
                checker.Code = textCode.Text;

                if (checker.existe())
                {
                    throw new ProductException($"Ya existe un producto con el Code {textCode.Text}.");
                }

                // 3. Crear objeto con los datos del formulario
                ENProduct product = CrearObjetoProducto();

                // 4. Llamar a Create   
                bool success = product.Create();
                if (success)
                {
                    mensaje.ForeColor = System.Drawing.Color.Green; // Añadido
                    mensaje.Text = "Producto creado con éxito.";
                }
                else
                {
                    throw new ProductException("No se pudo crear el producto.");
                }
            }
            catch (ProductException ex)
            {
                // Llamamos a MostrarError para manejar el error
                MostrarError(ex);
            }
            catch (Exception ex)
            {
                // Si ocurre otro tipo de error, lo manejamos también
                MostrarError(ex);
            }
        }

        // UPDATE 
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar campos
                if (!CheckeoDatos(out string errorMsg))
                {
                    throw new ProductException(errorMsg); // Lanza una excepción si los datos no son válidos
                }

                // 2. Comprobar existencia del producto con un objeto auxiliar
                ENProduct checker = new ENProduct();
                checker.Code = textCode.Text;

                if (!checker.existe())
                {
                    throw new ProductException($"No existe un producto con el Code {textCode.Text} para actualizar.");
                }

                // 3. Crear objeto con los datos actualizados
                ENProduct product = CrearObjetoProducto();

                // 4. Llamar a Update
                bool success = product.Update();
                if (success)
                {
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto actualizado con éxito.";
                }

                else
                {
                    throw new ProductException("No se pudo actualizar el producto.");
                }

            }
            catch (ProductException ex)
            {
                MostrarError(ex);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // DELETE
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Debes indicar un Code para borrar.");
                }

                ENProduct checker = new ENProduct();
                checker.Code = textCode.Text;

                if (!checker.existe())
                {
                    throw new ProductException($"No existe un producto con el Code {textCode.Text} para borrar.");
                }

                ENProduct product = new ENProduct();
                product.Code = textCode.Text;

                bool success = product.Delete();
                if (success)
                {
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto borrado con éxito.";
                }
                else
                {
                    throw new ProductException("No se pudo borrar el producto.");
                }
            }
            catch (ProductException ex)
            {
                MostrarError(ex);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // READ 
        protected void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Debes indicar un Code para leer.");
                }

                ENProduct product = new ENProduct();
                product.Code = textCode.Text;
                bool success = product.Read();

                if (success)
                {
                    textName.Text = product.Name;
                    textAmount.Text = product.Amount.ToString();
                    textPrice.Text = product.Price.ToString(CultureInfo.InvariantCulture);
                    textDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    ddlCategory.SelectedValue = product.Category.ToString();
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto leído correctamente.";
                }
                else
                {
                    throw new ProductException($"No se encontró un producto con el Code {textCode.Text}.");
                }
            }
            catch (ProductException ex)
            {
                MostrarError(ex);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // READ FIRST
        protected void btnReadFirst_Click(object sender, EventArgs e)
        {
            try
            {
                ENProduct product = new ENProduct();
                bool success = product.ReadFirst();

                if (success)
                {
                    textCode.Text = product.Code;
                    textName.Text = product.Name;
                    textAmount.Text = product.Amount.ToString();
                    textPrice.Text = product.Price.ToString(CultureInfo.InvariantCulture);
                    textDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    ddlCategory.SelectedValue = product.Category.ToString();
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Primer producto leído correctamente.";
                }
                else
                {
                    throw new ProductException("No hay productos en la BD.");
                }
            }
            catch (ProductException ex)
            {
                MostrarError(ex);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // READ PREVIOUS
        protected void btnReadPrev_Click(object sender, EventArgs e)
        {
            try
            {
                // Necesitamos saber el Code actual para buscar el anterior.
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Debes indicar el Code actual para leer el anterior.");
                }

                ENProduct product = new ENProduct();
                product.Code = textCode.Text;
                bool success = product.ReadPrev();

                if (success)
                {
                    textCode.Text = product.Code;
                    textName.Text = product.Name;
                    textAmount.Text = product.Amount.ToString();
                    textPrice.Text = product.Price.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    textDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    ddlCategory.SelectedValue = product.Category.ToString();
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto anterior leído correctamente.";
                }
                else
                {
                    mensaje.Text = "No hay producto anterior al indicado.";
                    throw new ProductException("No existe producto anterior al Code");
                }
            }
            catch (ProductException ex)
            {
                MostrarError(ex);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // READ NEXT
        protected void btnReadNext_Click(object sender, EventArgs e)
        {
            try
            {
                // Necesitamos saber el Code actual para buscar el siguiente.
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Debes indicar el Code actual para leer el siguiente.");
                }

                ENProduct product = new ENProduct();
                product.Code = textCode.Text;
                bool success = product.ReadNext();

                if (success)
                {
                    textCode.Text = product.Code;
                    textName.Text = product.Name;
                    textAmount.Text = product.Amount.ToString();
                    textPrice.Text = product.Price.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    textDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    ddlCategory.SelectedValue = product.Category.ToString();
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto siguiente leído correctamente.";
                }
                else
                {
                    mensaje.Text = "No hay producto siguiente al indicado.";
                    throw new ProductException("No existe producto siguiente al Code");
                }
            }
            catch (ProductException ex)
            {
                MostrarError(ex);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }
    }
}