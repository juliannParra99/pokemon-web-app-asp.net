<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="pokemon_web._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Esta es la pagina por defecto</h1>
    
    
    <div class="row row-cols-1 row-cols-md-3 g-4">

    <%--<%foreach ( dominio.Pokemon poke in ListaPokemon)
        {%>

        <div class="col">
            <div class="card h-100">
                <img src="<%:poke.UrlImagen %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%:poke.Nombre %></h5>
                    <p class="card-text"><%:poke.Descripcion %></p>
                    <a href="pokemonDetalles.aspx?id=<%:poke.Id %> " class="btn btn-primary">Ver Detalle</a>
                </div>
            </div>
        </div>
    <%
        } %>--%>

        <%--ejemplo utulizando repeater: comento el foreach--%>
        <asp:Repeater ID="repRepetidor" runat="server">
            <%-- itemTemplate: la parte del codigo que se va a repetir(equivalente a lo que esta dentro del foreach)
                
                Aa diferencia del forEach donde podemos trabajar con el objeto como tal. en el repeater
                tenemos que llamar a las propiedades de ese objeto, despues de asignarlo como fuente
                del repeater en el code behind: para usar esas propiedades tenemos que
                colocarlas dentro de un <%#Eval("propiedad")%> ver el #, es importante.--%>

            <%--con el repeater puedo conseguir una variable de esta pantalla y traerlo en esta pantalla de nuevo y hacer algo--%>
            <%-- ademas podemos llamar al evento de un boton para capaturar el valor por ejemplo --%>
            <ItemTemplate>
                <div class="col">
            <div class="card h-100">
                <img src="<%# Eval("UrlImagen") %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%#Eval("Nombre") %></h5>
                    <p class="card-text"><%#Eval("Descripcion ")%></p>
                    <a href="pokemonDetalles.aspx?id=<%#Eval("Id") %> " class="btn btn-primary">Ver Detalle</a>
                    <%--un bton para capturar un valor y reutilizarlo en la misma pantalla: el boton va a ir al evento
                        click del boton en esta pantalla
                        
                        Para capturar el valor: utilizo el commandArgument que va a capturar el valor, y es el nombre de la 
                        propiedad del objeto que va a capturar, en este caso el Id.Al argumento lo voy a guardar en una variable
                         al momento en que llegue al code behind mediante el commandName, para manipularo en el back
                        Capturar el valor ocurre mediante el evento onClick--%>
                    <%-- el COMMANDArgument se pasa entre comillas simples --%>
                    <asp:Button Text="Ejemplo" ID="btnEjemplo" CssClass="btn btn-danger" CommandArgument= '<%#Eval("Id") %>' CommandName="PokemonId" OnClick="btnEjemplo_Click" runat="server" />
                </div>
            </div>
        </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>
