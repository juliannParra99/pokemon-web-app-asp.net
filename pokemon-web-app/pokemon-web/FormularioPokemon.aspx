<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="FormularioPokemon.aspx.cs" Inherits="pokemon_web.FormularioPokemon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- Este es el formulario para dar de alta/modificar distintos pokemons. --%>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id:</label>
                <asp:TextBox id="txtId" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox id="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNumero" class="form-label"> Numero:</label>
                <asp:TextBox id="txtNumero" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion:</label>
                <asp:TextBox id="txtDescripcion" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlTipo" class="form-label">Tipo:</label>
                <asp:DropDownList ID="ddlTipo" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlDebilidad" class="form-label">Debilidad:</label>
                <asp:DropDownList ID="ddlDebilidad" CssClass="form-select" runat="server"></asp:DropDownList>

            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" CssClass="btn btn-primary" ID="btnAceptar" OnClick="btnAceptar_Click" runat="server" />

                <a href="pokemonLista.aspx" Class="btn btn-danger">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
