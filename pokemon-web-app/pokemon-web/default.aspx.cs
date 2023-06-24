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
        public List<Pokemon> ListaPokemon { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            
            PokemonNegocio negocio = new PokemonNegocio();

            ListaPokemon = negocio.listaConSP();
            

            //queremos que se cargue el repetidor si no es posback, osea solamente la primera vez que se ejecuta. 
            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaPokemon;
                repRepetidor.DataBind();
            }
        }

        //el boton es el que lanzo el evento, y el boton tiene un argumento; para capturar el  argumento puedo usar el sender
        //que va en el parametro.El sender es el elemento que envia el evento, en este caso un button
        protected void btnEjemplo_Click(object sender, EventArgs e)
        {
            
            string valor = ((Button)sender).CommandArgument;
            
        }
    }
}