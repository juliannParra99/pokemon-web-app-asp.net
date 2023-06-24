<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="pokemonLista.aspx.cs" Inherits="pokemon_web.pokemonLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Esta es la lista de pokemons</h1>
    <%-- LOS BOTONES DE AGREGAR Y SELECCIOANR VAN A REDIRIJIR A LA MISMA PANTALLA POR QUE LA VAMOS A REUTILIZAR
        
        Se agrega pagininacion. El daKeyName para poder manipular el ID de los elemenos seleccionados y trabajar con ellos,
        se agrega un  campo de seleccion de la fila para poder redirijir a otra pagina y modificar, etc,e ntre otros
        
        DataKeyNames="Id": Este atributo define la(s) clave(s) de datos del GridView. En este caso, se establece en "Id", lo que significa que el
        GridView utilizará la columna "Id" como clave de datos. La clave de datos se utiliza generalmente para identificar de manera única cada
        fila en el GridView.
        
        OnPageIndexChanging="dgvPokemons_PageIndexChanging": Este atributo especifica el nombre del método de evento que se ejecutará
        cuando se produzca un cambio en el índice de la página del GridView. En este caso, se establece en "dgvPokemons_PageIndexChanging", 
        lo que indica que se llamará al método dgvPokemons_PageIndexChanging en el código detrás cuando se produzca un cambio de página en el
        GridView.

AllowPaging="True": Este atributo se establece en "True" para habilitar la paginación en el GridView. Permite dividir los resultados de la 
        tabla en varias páginas para facilitar la navegación.

PageSize="5": Este atributo especifica el número de filas que se mostrarán en cada página del GridView. En este caso, se establece en "5",
        lo que significa que se mostrarán como máximo 5 filas por página. --%>
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

            <%--esta línea de código agrega un campo de comando al control GridView. El campo de comando muestra un botón
                de selección en cada fila del GridView, con el texto "Editar". Al hacer clic en este botón, se activará el
                evento dgvPokemons_SelectedIndexChanged que se mencionó anteriormente en el código del servidor. Esto permite
                realizar alguna acción, como redirigir a una página de edición de datos para la fila seleccionada.--%>
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar"  />
        </Columns>
    </asp:GridView>

    <a href="FormularioPokemon.aspx" Class="btn btn-primary">Agregar</a>
</asp:Content>
