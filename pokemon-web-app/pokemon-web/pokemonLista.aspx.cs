using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;


//filtro avanzado :  filtro que desabilita el filtor rapido y habilita mas opciones; de un desplegable de campo se selecciona una opcion
//y en base a eso  se habilitan otras opciones en los otros desplegables. Hay 4 campos para filtrar. Se utiliza el metodo que ya teniamos
// en otro proyecto, y se agrega el campo de busqueda para buscar por activo o inactivo.

//filtro busca por: nombre, tipo de pokemon y numero.

//IMPORTANTE A REALIZAR: el filtro no tiene un boton que limpie el filtro y traiga todos los datos de nuevo de ser necesario, por 
//lo que seria ideal crear esa funcionalidad: boton que traiga todos los registros de nuevo. Ademas arreglar la PAGINACION,
//cuando filtro y quiero ver otras paginas se pierde los datos del filtro
namespace pokemon_web
{
    public partial class pokemonLista : System.Web.UI.Page
    {
        //para manejar el filtro avanzado
        public bool FiltroAvanzado{ get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //segun el chkAvanzado cambia el valor para mostrarlo
            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Session.Add("listaPokemons", negocio.listaConSP());
                dgvPokemons.DataSource = Session["listaPokemons"];
                dgvPokemons.DataBind();
            }
            
        }

        
        protected void dgvPokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPokemons.PageIndex = e.NewPageIndex;
            dgvPokemons.DataBind();
        }

        protected void dgvPokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPokemons.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioPokemon.aspx?id=" + id);
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            //lisa para filtrar: le guardo el contenido de la session. 
            List<Pokemon> lista = (List<Pokemon>)Session["listaPokemons"];
            List<Pokemon> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            //le paso la lsita filtrada a la grilla

            dgvPokemons.DataSource = listaFiltrada;
            dgvPokemons.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            //va a guardarle lo correspondiente, si no esta chekeado es false, sino true. Si es true se ejecuta la condicion del aspx.
            FiltroAvanzado = chkAvanzado.Checked;
            //niego el filtro avanzado, es decir coloco su booleano opuesto. esto por que si el filtro avanzado es true, niego el valor
            //para que se desabilite el filtro avanzado.
            txtFiltro.Enabled = !FiltroAvanzado;
        }


        //filtro avanzado: logica de desplegables
        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //quita los elementos anteriores para que no se acumulen cada que se selecciona un item
            ddlCriterio.Items.Clear();
            //va a llenar el otro desplegable: cada que cambie el desplegable de criterio voy a querer que se cargue el criterio correspondiente
            //si es Numero, que es un valor de cadena precargado muestro algo, sino, muestro otra cosa
            if (ddlCampo.SelectedItem.ToString() == "Número")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();

                //muestro la lista filtrada: uso el metodo que ya tenia y que modifique acorde a este ejemplo: OJO CON LAS MODIFICACIONES
                //PRESTAR ATENCION.
                //le paso todos los parametros al metodo filtrar
                dgvPokemons.DataSource = negocio.filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text,
                    ddlEstado.SelectedItem.ToString());
                dgvPokemons.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
    }
}