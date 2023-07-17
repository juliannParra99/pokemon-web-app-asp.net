<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="pokemonLista.aspx.cs" Inherits="pokemon_web.pokemonLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Esta es la lista de pokemons</h1>
   
    <asp:GridView ID="dgvPokemons"  runat="server" DataKeyNames="Id"
        CssClass="table" AutoGenerateColumns="false" 
        OnSelectedIndexChanged="dgvPokemons_SelectedIndexChanged" 
        OnPageIndexChanging="dgvPokemons_PageIndexChanging"
        AllowPaging="True" PageSize="5">
        
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Numero" DataField="Numero" />
            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
            <asp:BoundField HeaderText="Debilidad" DataField="Debilidad.Descripcion" />
            <%-- para ver el estado de activo o inactivo --%>
            <asp:CheckBoxField  HeaderText="Activo" DataField="Activo"/>

            
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar"  />
        </Columns>
    </asp:GridView>

    <a href="FormularioPokemon.aspx" Class="btn btn-primary">Agregar</a>
</asp:Content>
