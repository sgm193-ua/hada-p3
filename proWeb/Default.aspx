<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Products management</h1>
    <div>
        <label>Code</label>&nbsp&nbsp
        <asp:TextBox ID="textCode" runat="server" Width="225px" />
    </div>
    <br />

    <div>
         <label>Name</label>&nbsp&nbsp
         <asp:TextBox ID="textName" runat="server" Width="225px" />
    </div>

    <br />

    <div>
         <label>Amount</label>&nbsp&nbsp
         <asp:TextBox ID="textAmount" runat="server" Width="95px" />
    </div>

    <br />

    <div>
         <label>Category</label>&nbsp&nbsp
         <asp:DropDownList ID="textCategory" runat="server">
         </asp:DropDownList>
    </div>

    <br />

    <div>
         <label>Price</label>&nbsp&nbsp
         <asp:TextBox ID="textPrice" runat="server" Width="95px" />
    </div>

    <br />

    <div>
         <label>Creation Date</label>&nbsp&nbsp
         <asp:TextBox ID="textCreationDate" runat="server" Width="165px" />
    </div>

    <br />

    <div>
        <asp:Button ID="boton_crear" runat="server" Text="Create" OnClick="CreateProduct" />
        <asp:Button ID="boton_actualizar" runat="server" Text="Update" OnClick="Update" />
        <asp:Button ID="boton_borrar" runat="server" Text="Delete" OnClick="Delete" />
        <asp:Button ID="boton_leer" runat="server" Text="Read" OnClick="Read" />
        <asp:Button ID="boton_leer_prim" runat="server" Text="Read First" OnClick="ReadFirst" />
        <asp:Button ID="boton_leer_ant" runat="server" Text="Read Prev" OnClick="ReadPrev" />
        <asp:Button ID="boton_leer_siguiente" runat="server" Text="Read Next" OnClick="ReadNextProduct" />

        <br /><br />
        <asp:Label ID="mensaje" runat="server" ForeColor="Red"></asp:Label>
       
    </div>

</asp:Content>
