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

    <asp:Button Text="Create" OnClick="onCreate" ID="botonCrear" runat="server" />
    <asp:Button Text="Update" OnClick="onUpdate" ID="botonActualizar" runat="server" />
    <asp:Button Text="Delete" OnClick="onDelete" ID="botonBorrar" runat="server" />
    <asp:Button Text="Read" OnClick="onRead" ID="botonLeer" runat="server" />
    <asp:Button Text="Read First" OnClick="onReadfirst" ID="botonLeer_Primero" runat="server" />
    <asp:Button Text="Read Prev" OnClick="onReadPrev" ID="botonLeer_Anterior" runat="server" />
    <asp:Button Text="Read Next" OnClick="onReadNext" ID="botonLeer_Siguiente" runat="server" />
    
    <asp:Label ID="mensaje" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>
