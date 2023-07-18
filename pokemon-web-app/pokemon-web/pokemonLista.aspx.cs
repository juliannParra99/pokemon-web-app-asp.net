using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

//Se agrega filtro rapido para la lista de pokemons; vamos a hacer que filtre por nombre, para lo que vamos a modificar
//el evento load. Vamos a guardar el objeto de la lista que estamos obteniendo y lo vamos a guardar en la session
//le agrego el update panel para que no se recarguen todos los recursos de nuevo

namespace pokemon_web
{
    public partial class pokemonLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            //guardo la lista que obtengo en session para despues filtrar: los valores de esa lista en session lo guardo en otra
            //lista exclusiva para filtrar, en el evento del filtro

            Session.Add("listaPokemons", negocio.listaConSP());
            dgvPokemons.DataSource = Session["listaPokemons"];
            //crea la estructura de la tabla con los datos pasados al data source y carga los pokemons.
            dgvPokemons.DataBind();

            
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
            //capturo la lsita completa que tengo ahsta el momento
            //lisa para filtrar: le guardo el contenido de la session. Le hago casteo explicito
            List<Pokemon> lista = (List<Pokemon>)Session["listaPokemons"];
            //filtro en una nueva lista, aunque se podria hacer todo en una line; el toUpper() para que pase todo a Upper y no sea caseSensitive
            //la lambda expression es una funcionalidad de las colleciones que se encuentra en el find all
            List<Pokemon> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            //le paso la lsita filtrada a la grilla

            dgvPokemons.DataSource = listaFiltrada;
            dgvPokemons.DataBind();
        }
    }
}