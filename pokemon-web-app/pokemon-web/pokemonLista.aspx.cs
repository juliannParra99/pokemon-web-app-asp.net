using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace pokemon_web
{
    public partial class pokemonLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            dgvPokemons.DataSource = negocio.listaConSP();
            //crea la estructura de la tabla con los datos pasados al data source y carga los pokemons.
            dgvPokemons.DataBind();

            
        }

        // Se ocupa d ela paginacion

        //dgvPokemons_PageIndexChanging, se ejecuta cuando se produce un cambio en el índice de la página del
        //control GridView llamado dgvPokemons.Este método se utiliza generalmente para controlar la paginación
        //de los datos mostrados en el GridView.

        //Primero, se actualiza el índice de la página del GridView dgvPokemons con el valor del nuevo índice de página
        //proporcionado en el evento e.NewPageIndex.
        //Luego, se llama al método DataBind() para volver a enlazar los datos al GridView y mostrar la nueva página seleccionada.
        protected void dgvPokemons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPokemons.PageIndex = e.NewPageIndex;
            dgvPokemons.DataBind();
        }

        //maneja la selección de filas para redirigir a una página de formulario detallada del Pokémon seleccionado:
        //dgvPokemons_SelectedIndexChanged, se ejecuta cuando se selecciona una fila en el GridView dgvPokemons.

        //En este método, se obtiene el valor de la clave de datos(DataKey) de la fila seleccionada del GridView,
        //que se almacena en la variable id.El valor de la clave de datos suele ser un identificador único asociado a la fila seleccionada.
        //Luego, se redirige al usuario a la página "FormularioPokemon.aspx" pasando el parámetro id en la URL mediante Response.Redirect().
        //Esto permitiría cargar una página de formulario específica para el Pokémon seleccionado, utilizando el identificador obtenido.
        protected void dgvPokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvPokemons.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioPokemon.aspx?id=" + id);
        }
    }
}