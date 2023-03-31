<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="pokemon_web._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Esta es la pagina por defecto</h1>
    <%-- en este eejemplo lo que se va a hacer es listar los datos pero de otra manera; en ligar de hacerlo en formato lista, en este caso
        lo vamos a hacer con las cartas de bootstrap. lo que se hace es se pega el componente de Bootstrap y se ejecuta un FOREACH para 
        poder ejecutar logica en el html y que se traigan tantos componentes html y dibuje uno por pokemons hay en la base de datos
        se v a repetir tantas veces como elementos haya dentro de la grilla--%>
    
    <div class="row row-cols-1 row-cols-md-3 g-4">

    <%foreach ( dominio.Pokemon poke in ListaPokemon)
        {%>

        <div class="col">
            <div class="card h-100">
                <img src="<%:poke.UrlImagen %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%:poke.Nombre %></h5>
                    <p class="card-text"><%:poke.Descripcion %></p>
                    <%--En este caso genero un boton para ver todos los detalles de un pokemon en otra pantalla: lo que 
                    puedo hacer es caputrar el id del pokemon mandarlo a otra url y extraer todos los valores en otra screen
                        cargando en un formulario los detalles del pokemon por ejemplo--%>
                    <a href="pokemonDetalles.aspx?id=<%:poke.Id %> " class="btn btn-primary">Ver Detalle</a>
                </div>
            </div>
        </div>
    <%
        } %>
        
    </div>
</asp:Content>
