using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;
using System.Web.Services.Description;
using static ProWeb.Default;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
//using Library;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using library;
namespace ProWeb

{


    public partial class Default : Page
    {
        protected TextBox txtCode;
        protected TextBox txtName;
        protected TextBox txtAmount;
        protected TextBox txtPrice;
        protected TextBox txtCreationDate;
        protected Label lblMessage;
        protected DropDownList ddlCategory; // Cambiado de


        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                lblMessage.Text = ""; // Inicializarlo, por ejemplo, en blanco al cargar la página por primera vez
            }
        }

        // Establecemos una segunda validacion en la parte del servidor , es redundante ya uqe se comprueba en la pare del cliente en el formulario aspx 
        private bool ValidateProduct(ref string mensaje)
        {
            // Validar Code
            if (txtCode.Text.Length < 1 || txtCode.Text.Length > 16)
            {
                mensaje = "El código debe tener entre 1 y 16 caracteres.";
                return false;
            }

            // Validar Name
            if (txtName.Text.Length < 1 || txtName.Text.Length > 32)
            {
                mensaje = "El nombre del producto debe tener entre 1 y 32 caracteres.";
                return false;
            }

            // Validar Amount
            if (!int.TryParse(txtAmount.Text, out int amount) || amount < 0 || amount > 9999)
            {
                mensaje = "La cantidad debe ser un número entre 0 y 9999.";
                return false;
            }

            // Validar Price
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0 || price > 9999.99m)
            {
                mensaje = "El precio debe ser un valor real entre 0 y 9999.99.";
                return false;
            }

            ENProduct product = new ENProduct();
            product.Category = int.Parse(ddlCategory.SelectedValue);
            //aquicomprobamos en la base de datos que la categoria introducida es valida 
            if (!product.validCategory())
            {
                mensaje = "La categoría no es válida.";
                return false;
            }

            return true;
        }

        // crea el producto y aplica las validaciones
        protected void CreateProduct(object sender, EventArgs e)
        {
            string mensaje = "";
            if (ValidateProduct(ref mensaje))
            {

                ENProduct product = new ENProduct
                (
                    txtCode.Text,
                    txtName.Text,
                    int.Parse(txtAmount.Text),
                    float.Parse(txtPrice.Text),
                    int.Parse(ddlCategory.SelectedValue),
                    DateTime.ParseExact(txtCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );
                // si se puede crear
                if (product.Create())
                {
                    lblMessage.Text = "Product created succesfully";
                }
                // en caso de error
                else
                {
                    lblMessage.Text = "Product Operation failed. Error to create, ";
                }
            }
            // en caso de error
            else
            {
                string m = "Product Operation failed. Error to create ";
                m += mensaje;
                lblMessage.Text = m;
            }
        }

        // lee el producto especificado
        protected void ReadProduct(object sender, EventArgs e)
        {

            ENProduct product = new ENProduct
            (
                txtCode.Text,
                txtName.Text,
                int.Parse(txtAmount.Text),
                float.Parse(txtPrice.Text),
                int.Parse(ddlCategory.SelectedValue),
                DateTime.ParseExact(txtCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            );
            if (product.Read())
            {
                txtCode.Text = product.Code;
                txtName.Text = product.Name;
                txtAmount.Text = product.Amount.ToString();
                txtPrice.Text = product.Price.ToString();
                ddlCategory.SelectedValue = product.Category.ToString();
                txtCreationDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                lblMessage.Text = "Product loaded successfully";
            }
            // en caso de error
            else
            {
                lblMessage.Text = "Product not found";
            }
        }

        // actualiza el producto cargado
        protected void UpdateProduct(object sender, EventArgs e)
        {
            string mensaje = "";
            if (ValidateProduct(ref mensaje))
            {
                ENProduct product = new ENProduct
                (
                    txtCode.Text,
                    txtName.Text,
                    int.Parse(txtAmount.Text),
                    float.Parse(txtPrice.Text),
                    int.Parse(ddlCategory.SelectedValue),
                    DateTime.ParseExact(txtCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );
                // si se puede actualizar
                if (product.Update())
                {
                    lblMessage.Text = "Product updated succesfully";
                }
                // en caso de error
                else
                {
                    string m = "Product Operation failed: Error to update ";
                    m += mensaje;
                    lblMessage.Text = m;
                }
            }
            // en caso de error
            else
            {
                string m = "Product Operation failed: Error to update ";
                m += mensaje;
                lblMessage.Text = m;
            }
        }

        // borra el producto
        protected void DeleteProduct(object sender, EventArgs e)
        {
            ENProduct product = new ENProduct
            (

                txtCode.Text,
                txtName.Text,
                int.Parse(txtAmount.Text),
                float.Parse(txtPrice.Text),
                int.Parse(ddlCategory.SelectedValue),
                DateTime.ParseExact(txtCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            );
            // si se puede borrar
            if (product.Delete())
            {
                lblMessage.Text = "Product deleted";
            }
            // en caso de error
            else
            {
                lblMessage.Text = "Product not found";
            }
        }

        // lee el producto siguiente al cargado
        protected void ReadNextProduct(object sender, EventArgs e)
        {
            ENProduct product = new ENProduct
            (
                txtCode.Text,
                txtName.Text,
                int.Parse(txtAmount.Text),
                float.Parse(txtPrice.Text),
                int.Parse(ddlCategory.SelectedValue),

                DateTime.ParseExact(txtCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            );
            // si se puede leer
            if (product.ReadNext())
            {
                txtCode.Text = product.Code;
                txtName.Text = product.Name;
                txtAmount.Text = product.Amount.ToString();
                txtPrice.Text = product.Price.ToString();
                ddlCategory.SelectedValue = product.Category.ToString();

                txtCreationDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                lblMessage.Text = "Product loaded successfully";
            }
            // en caso de error
            else
            {
                lblMessage.Text = "Product not found, It is the last element";
            }
        }

        // lee el producto anterior al cargado
        protected void ReadPrevProduct(object sender, EventArgs e)
        {
            ENProduct product = new ENProduct
            (
                txtCode.Text,
                txtName.Text,
                int.Parse(txtAmount.Text),
                float.Parse(txtPrice.Text),
                int.Parse(ddlCategory.SelectedValue),
                DateTime.ParseExact(txtCreationDate.Text, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
            );
            // si se puede leer
            if (product.ReadPrev())
            {
                txtCode.Text = product.Code;
                txtName.Text = product.Name;
                txtAmount.Text = product.Amount.ToString();
                txtPrice.Text = product.Price.ToString();
                ddlCategory.SelectedValue = product.Category.ToString();

                txtCreationDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                lblMessage.Text = "Product loaded successfully";
            }
            // en caso de error
            else
            {
                lblMessage.Text = "Error  {It is the first element, there is no previous one}";
            }
        }

        // lee el primer producto
        protected void ReadFirstProduct(object sender, EventArgs e)
        {
            ENProduct product = new ENProduct();
            // si se puede leer
            if (product.ReadFirst())
            {
                txtCode.Text = product.Code;
                txtName.Text = product.Name;
                txtAmount.Text = product.Amount.ToString();
                txtPrice.Text = product.Price.ToString();
                ddlCategory.SelectedValue = product.Category.ToString();

                txtCreationDate.Text = product.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                lblMessage.Text = "Product loaded succesfully";
            }
            // en caso de error
            else
            {
                lblMessage.Text = "Data base is empty ";
            }
        }
    }
}