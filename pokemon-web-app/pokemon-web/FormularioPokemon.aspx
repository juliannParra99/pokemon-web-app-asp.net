<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="FormularioPokemon.aspx.cs" Inherits="pokemon_web.FormularioPokemon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- cuando utilizas el control <asp:UpdatePanel> en una página ASP.NET, 
        también necesitas tener un <asp:ScriptManager> en la misma página para habilitar las actualizaciones parciales
        y la funcionalidad de AJAX dentro del UpdatePanel. --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <%-- Este es el formulario para dar de alta/modificar distintos pokemons. --%>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id:</label>
                <asp:TextBox ID="txtId" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNumero" class="form-label">Numero:</label>
                <asp:TextBox ID="txtNumero" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlTipo" class="form-label">Tipo:</label>
                <asp:DropDownList ID="ddlTipo" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlDebilidad" class="form-label">Debilidad:</label>
                <asp:DropDownList ID="ddlDebilidad" CssClass="form-select" runat="server"></asp:DropDownList>

            </div>
        </div>
        <%-- Se agrega al formulario la opcion para cargar y ver la imagen --%>
        <div class="col-6">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion:</label>
                <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" CssClass="form-control" runat="server" />
            </div>
            <%--  El control UpdatePanel utiliza tecnología AJAX (Asynchronous JavaScript and XML) para permitir la comunicación
                asíncrona entre el cliente y el servidor. Cuando ocurre un evento dentro del UpdatePanel, en lugar de realizar un 
                postback completo y recargar toda la página, solo se envía al servidor la información necesaria para procesar ese 
                evento específico. El servidor responde enviando de vuelta solo el contenido actualizado del UpdatePanel, y ese contenido 
                se actualiza en el cliente de forma asincrónica sin afectar el resto de la página.--%>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <%-- Al combinar estos dos atributos, AutoPostBack="true" y OnTextChanged="txtImagenUrl_TextChanged",
                            se logra que, cuando el usuario escriba o modifique el contenido en el control de entrada de texto txtImagenUrl,
                            se realice automáticamente un postback al servidor y se ejecute el evento txtImagenUrl_TextChanged en el servidor.
                            Esto permite que se realice algún tipo de procesamiento adicional o actualización de la página en respuesta al cambio 
                            en el texto de la URL de la imagen. --%>
                        <label for="txtImagenUrl" class="form-label">Url Imagen</label>
                        <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                    </div>
                    <asp:Image ID="imgPokemon" ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png" Width="60%"
                        runat="server" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="mb-3">
            <asp:Button Text="Aceptar" CssClass="btn btn-primary" ID="btnAceptar" OnClick="btnAceptar_Click" runat="server" />

            <a href="pokemonLista.aspx" class="btn btn-danger">Cancelar</a>
        </div>
    </div>
</asp:Content>
