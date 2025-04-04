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
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            CADCategory cadCategory = new CADCategory();
            List<ENCategory> listaCategorias = cadCategory.ReadAll();
            textCategory.DataSource = listaCategorias;
            textCategory.DataTextField = "name";
            textCategory.DataValueField = "id";
            textCategory.DataBind();
        }


        private bool Validate(out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(textCode.Text) || textCode.Text.Length > 16)
            {
                error = "Code debe tener entre 1 y 16 caracteres.";
                return false;
            }

            if (textName.Text.Length > 32)
            {
                error = "El nombre no puede exceder los 32 caracteres.";
                return false;
            }

            if (!int.TryParse(textAmount.Text, out int amount) || amount < 0 || amount > 9999)
            {
                error = "Amount debe ser un número entero entre 0 y 9999";
                return false;
            }

            if (!float.TryParse(textPrice.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float price) || price < 0 || price > 9999.99f)
            {
                error = "Price debe ser un valor real entre 0 y 9999,99.";
                return false;
            }

            if (!DateTime.TryParseExact(textCreationDate.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime creationDate))
            {
                error = "Creation Date debe seguir el formato dd/MM/yyyy HH:mm:ss.";
                return false;
            }

            if (!int.TryParse(textCategory.SelectedValue, out int category) || category < 0 || category > 3)
            {
                error = "La categoría seleccionada no es válida (debe ser 0, 1, 2 o 3).";
                return false;
            }

            return true;
        }

        private ENProduct CrearObjeto()
        {
            return new ENProduct
            (
                textCode.Text,
                textName.Text,
                int.Parse(textAmount.Text),
                float.Parse(textPrice.Text, CultureInfo.InvariantCulture),
                int.Parse(textCategory.SelectedValue),
                DateTime.ParseExact(textCreationDate.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            );
        }



        protected void CreateProduct(object sender, EventArgs e)
        {
            try
            {
                string mensajeError = "";
                if (!Validate(out mensajeError))
                {
                    throw new ProductException("Error de validación: " + mensajeError);
                }

                ENProduct checker = new ENProduct();
                checker.code = textCode.Text;

                if (checker.existe())
                {
                    throw new ProductException($"Ya existe un producto con el Code {textCode.Text}.");
                }

                ENProduct product = new ENProduct
                (
                    textCode.Text,
                    textName.Text,
                    int.Parse(textAmount.Text),
                    float.Parse(textPrice.Text),
                    int.Parse(textCategory.SelectedValue),
                    DateTime.ParseExact(textCreationDate.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                );


                if (product.Create())
                {
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto creado con éxito.";
                }
                else
                {
                    throw new ProductException("No se pudo crear el producto.");
                }
            }
            catch (ProductException ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Error: " + ex.Message;
            }
   
        }


        protected void Update(object sender, EventArgs e)
        {
            try
            {
                string mens = "";

                if (!Validate(out mens))
                {
                    throw new ProductException("Error de validación: " + mensaje);
                }

                ENProduct check = new ENProduct();
                check.code = textCode.Text;

                if (!check.existe())
                {
                    throw new ProductException($"No existe un producto con el Code {textCode.Text} para actualizar.");
                }

                ENProduct product = new ENProduct
                (
                    textCode.Text,
                    textName.Text,
                    int.Parse(textAmount.Text),
                    float.Parse(textPrice.Text, CultureInfo.InvariantCulture),
                    int.Parse(textCategory.SelectedValue),
                    DateTime.ParseExact(textCreationDate.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                );

                if (product.Update())
                {
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Product updated successfully";
                }
                else
                {
                    throw new ProductException("Error al actualizar el producto.");
                }
            }
            catch (ProductException ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Error inesperado: " + ex.Message;
            }
        }


        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Por favor, introduce un código para proceder con la eliminación.");
                }

                ENProduct comprobar = new ENProduct();
                comprobar.code = textCode.Text;

                if (!comprobar.existe())
                {
                    throw new ProductException($"No hay ningún producto registrado con el código '{textCode.Text}'.");
                }

                ENProduct product = new ENProduct();
                product.code = textCode.Text;

                if (product.Delete())
                {
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "El producto se eliminó satisfactoriamente.";
                }
                else
                {
                    throw new ProductException("No se pudo completar la eliminación. Intenta de nuevo.");
                }
            }
            catch (ProductException ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Atención: " + ex.Message;
            }
            
        }

        protected void Read(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Por favor, ingrese un Code para leer el producto.");
                }

                ENProduct product = new ENProduct
                (
                    textCode.Text,
                    textName.Text,
                    int.Parse(textAmount.Text),
                    float.Parse(textPrice.Text),
                    int.Parse(textCategory.SelectedValue),
                    DateTime.ParseExact(textCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );

                if (product.Read())
                {
                    textCode.Text = product.code;
                    textName.Text = product.name;
                    textAmount.Text = product.amount.ToString();
                    textPrice.Text = product.price.ToString();
                    textCategory.SelectedValue = product.category.ToString();
                    textCreationDate.Text = product.creationDate.ToString("dd/MM/yyyy HH:mm:ss");

                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto cargado exitosamente.";
                }
                else
                {
                    throw new ProductException("No se encontró un producto con el Code ingresado.");
                }
            }
            catch (ProductException ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Error: " + ex.Message;
            }
    
        }


        protected void ReadFirst(object sender, EventArgs e)
        {
            try
            {
                ENProduct prod = new ENProduct();

                if (prod.ReadFirst())
                {
                    textCode.Text = prod.code;
                    textName.Text = prod.name;
                    textAmount.Text = prod.amount.ToString();
                    textPrice.Text = prod.price.ToString();
                    textCategory.SelectedValue = prod.category.ToString();
                    textCreationDate.Text = prod.creationDate.ToString("dd/MM/yyyy HH:mm:ss");

                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto leído exitosamente. Primer producto cargado correctamente.";
                }
                else
                {
                    throw new ProductException("La base de datos está vacía. No se encontró ningún producto.");
                }
            }
            catch (ProductException ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Error: " + ex.Message;
            }
        }


        protected void ReadPrev(object sender, EventArgs e)
        {
            try
            {
 
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Es necesario proporcionar un Code actual para leer el producto anterior.");
                }


                ENProduct prod = new ENProduct
                (
                    textCode.Text,
                    textName.Text,
                    int.Parse(textAmount.Text),
                    float.Parse(textPrice.Text),
                    int.Parse(textCategory.SelectedValue),
                    DateTime.ParseExact(textCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );


                if (prod.ReadPrev())
                {
                    textCode.Text = prod.code;
                    textName.Text = prod.name;
                    textAmount.Text = prod.amount.ToString();
                    textPrice.Text = prod.price.ToString();
                    textCategory.SelectedValue = prod.category.ToString();
                    textCreationDate.Text = prod.creationDate.ToString("dd/MM/yyyy HH:mm:ss");

                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto anterior leído correctamente.";
                }
                else
                {

                    throw new ProductException("No existe un producto anterior al indicado.");
                }
            }
            catch (ProductException ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void ReadNextProduct(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textCode.Text))
                {
                    throw new ProductException("Es necesario proporcionar un Code actual para leer el siguiente producto.");
                }

                ENProduct prod = new ENProduct
                (
                    textCode.Text,
                    textName.Text,
                    int.Parse(textAmount.Text),
                    float.Parse(textPrice.Text),
                    int.Parse(textCategory.SelectedValue),
                    DateTime.ParseExact(textCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );

                if (prod.ReadNext())
                {
                    textCode.Text = prod.code;
                    textName.Text = prod.name;
                    textAmount.Text = prod.amount.ToString();
                    textPrice.Text = prod.price.ToString();
                    textCategory.SelectedValue = prod.category.ToString();
                    textCreationDate.Text = prod.creationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    mensaje.ForeColor = System.Drawing.Color.Green;
                    mensaje.Text = "Producto siguiente leído correctamente.";
                }
                else
                {
                    throw new ProductException("No existe un producto siguiente al indicado.");
                }
            }
            catch (ProductException ex)
            {
                mensaje.ForeColor = System.Drawing.Color.Red;
                mensaje.Text = "Error: " + ex.Message;
            }
        }

    }
}
