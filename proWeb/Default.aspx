<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h2>Products management</h2>
    <label>Code </label> <asp:TextBox ID="textCode" runat="server"> </asp:TextBox>

<br />
<br />
    <label>Name </label>
    <asp:TextBox ID="textName" runat="server"> </asp:TextBox>

<br />
<br />
    <label>Amount </label> 
    <asp:TextBox ID="textAmount" runat="server"> </asp:TextBox>

<br />
<br />
    <label>Categorys </label> 
        <asp:DropDownList ID="ddlCategory" runat="server"> 
        <asp:ListItem>Computing</asp:ListItem> 
        <asp:ListItem>Telephony</asp:ListItem> 
        <asp:ListItem>Gaming</asp:ListItem> 
        <asp:ListItem>Home appliances</asp:ListItem> 
        </asp:DropDownList>

<br />
<br />
    <label>Price </label> 
    <asp:TextBox ID="textPrice" runat="server"> </asp:TextBox>

<br />
<br />
    <label>Creation Date </label> 
    <asp:TextBox ID="textDate" runat="server"> </asp:TextBox>

<br/>
<br/>

    <asp:Button Text="Leer" OnClick="onLeer" ID="buttom_Leer" runat="server" />
    <asp:Button Text="Leer Primero" OnClick="onPrimero" ID="buttom_Primero" runat="server" />
    <asp:Button Text="Leer Anterior" OnClick="onAnterior" ID="buttom_Anterior" runat="server" />
    <asp:Button Text="Leer Siguiente" OnClick="onSiguiente" ID="buttom_Siguiente" runat="server" />
    <asp:Button Text="Crear" OnClick="onCrear" ID="buttom_Crear" runat="server" />
    <asp:Button Text="Actualizar" OnClick="onActualizar" ID="buttom_Actualizar" runat="server" />
    <asp:Button Text="Borrar" OnClick="onBorrar" ID="buttom_Borrar" runat="server" />
    
    <asp:Label ID="mensaje" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>
