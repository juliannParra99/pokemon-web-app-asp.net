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
    public partial class _default : System.Web.UI.Page
    {
        //creo la propiedad listaPokemon.  A la que le voy a asignar la lista que devuelve un metodo listar de pokemonNeogicio()
        public List<Pokemon> ListaPokemon { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            //para dibujar los datos en la pagina traigo los datos de la db: 
            //ese codigo se podria centralizar para ahorrar recursos y acceder  a los datos de manera mas directa y centralizada
            PokemonNegocio negocio = new PokemonNegocio();

            //le cargo los datos de la base de datos y ya los puedo usar en el front
            ListaPokemon = negocio.listaConSP();
        }
    }
}