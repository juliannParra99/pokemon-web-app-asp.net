<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="pokemonLista.aspx.cs" Inherits="pokemon_web.pokemonLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- para que se recargue solo lo que se tiene que actualizar: el scriptManager siempre va con el UpadatedPanel--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Esta es la lista de pokemons</h1>

    <%-- con el update panel hago que se actualice lo que necesito sin recargar todo --%>
    <asp:UpdatePanel runat="server">

        <ContentTemplate>
            <div class="row">
                <div class="col-6">
                    <%-- Filtro --%>
                    <div class="mb-3">
                        <asp:Label Text="Filtrar por nombre:" runat="server" />
                        <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />
                    </div>
                </div>
                <%-- estilacion en linea para el boton checked --%>
                <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
                    <div class="mb-3">
                        <%-- autopostbakc true para que cada que chekeo cambie el valor de la proeperty FiltroAAvanzado y me muestre
                            el filtro avanzado--%>
                        <asp:CheckBox Text="Filtro avanzado" CssClass="" AutoPostBack="true" ID="chkAvanzado" OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />
                    </div>
                </div>
            </div>

            <%--  si filtro avanzado, que esta relacion con el chkAvanzado es verdadero muestra el filtro avanzado
                tambien podria haber puesto directamente chkAvanzado.Cheked--%>
            <%if (chkAvanzado.Checked)
                {%>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Campo:" runat="server" />
                        <%-- el evento para seleccionar el valor en ese indice, y autopostback, para que cuando se selleciona uno vaya al servidor
                           y  haga la logica correspondiente--%>
                        <asp:DropDownList runat="server" ID="ddlCampo" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Text="Nombre" />
                            <asp:ListItem Text="Tipo" />
                            <asp:ListItem Text="Número" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <%--  este  desplegable no esta cargado todavia voy a agregar un evento al desplegable anterior para cundo
                            se seleccione uno se carguen los correspondientes en este; no va  a tener items precargados; eso lo hago en el 
                            evento del ddlCampo--%>
                        <asp:Label Text="Criterio:" runat="server" />
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Filtro:" runat="server" />
                        <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Estado:" runat="server" />
                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                            <asp:ListItem Text="Todos" />
                            <asp:ListItem Text="Activo" />
                            <asp:ListItem Text="Inactivo" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Button Text="Buscar" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" />
                    </div>
                </div>
            </div>

            <% } %>

            <asp:GridView ID="dgvPokemons" runat="server" DataKeyNames="Id"
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
                    <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />


                    <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>


    <a href="FormularioPokemon.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
