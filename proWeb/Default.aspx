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

    <asp:Button ID="botonCrear" runat="server" Text="Create" OnClick="onCreate"  />
    <asp:Button ID="botonActualizar" runat="server" Text="Update" OnClick="onUpdate" />
    <asp:Button ID="botonBorrar" runat="server" Text="Delete" OnClick="onDelete"  />
    <asp:Button ID="botonLeer" runat="server" Text="Read" OnClick="onRead" />
    <asp:Button ID="botonLeer_Primero" runat="server" Text="Read First" OnClick="onReadfirst"  />
    <asp:Button ID="botonLeer_Anterior" runat="server" Text="Read Prev" OnClick="onReadPrev" />
    <asp:Button ID="botonLeer_Siguiente" runat="server" Text="Read Next" OnClick="onReadNext" />
    
    <asp:Label ID="mensaje" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>
