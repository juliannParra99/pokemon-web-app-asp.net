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
                        <%--  autopostback en true para que cuando tenga un evento, como el que permite capturar lo que se escribe
                    vaya al servidor directamente y ejecute la logica--%>
                        <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />
                    </div>
                </div>
            </div>

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
