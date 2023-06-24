<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="pokemon_web._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Esta es la pagina por defecto</h1>
    
    
    <div class="row row-cols-1 row-cols-md-3 g-4">


        <%--ejemplo utulizando repeater: --%>
        <asp:Repeater ID="repRepetidor" runat="server">
            
            <ItemTemplate>
                <div class="col">
            <div class="card h-100">
                <img src="<%# Eval("UrlImagen") %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%#Eval("Nombre") %></h5>
                    <p class="card-text"><%#Eval("Descripcion ")%></p>
                    <a href="pokemonDetalles.aspx?id=<%#Eval("Id") %> " class="btn btn-primary">Ver Detalle</a>
                    
                    <asp:Button Text="Ejemplo" ID="btnEjemplo" CssClass="btn btn-danger" CommandArgument= '<%#Eval("Id") %>' CommandName="PokemonId" OnClick="btnEjemplo_Click" runat="server" />
                </div>
            </div>
        </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
