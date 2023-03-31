<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="pokemonLista.aspx.cs" Inherits="pokemon_web.pokemonLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Esta es la lista de pokemons</h1>
    <%--aca voy a mostrar la lista de pokemons--%>
    <asp:GridView ID="dgvPokemons" CssClass="table" AutoGenerateColumns="false" runat="server">
        <%-- El grid view de web no es tan inteligente y puede no traer algunos datos por defecto por lo que podemos hacerlo manualmetne
            ; propiedades que a la vez son objetos puede no traerlos. por lo que tenemos que hacer referencia a la propiedad del objeto
            principal y a la del objeto que referencia. Si hago esto y no pongo el autoGenerateColumns=false se agregan las que faltarian--%>
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
            <asp:BoundField HeaderText="Debilidad" DataField="Debilidad.Descripcion" />
        </Columns>
    </asp:GridView>
</asp:Content>
