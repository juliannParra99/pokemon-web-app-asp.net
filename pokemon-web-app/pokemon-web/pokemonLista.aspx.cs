using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;


//en esta seccion esta toda la administracion dura de la configuracion y datos de los pokemons, por lo que esta pantalla solo va a ser accesible
//por alguien con el perfil ADMIN.No queremos que un usuario normal pueda modificar ni accedder esto. Entonces lo vamos a validar aqui a eso
//Y preguntamos, tu sessiona activa, es Admin? 

//Es lo mismo que hicimos von la seguridad en el amster page, pero como en este caso queremos solo configurar 1, lo puedo
//hacer aca directamente

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

            // Verificamos si el usuario actual no es un administrador (utilizando el método esAdmin de la clase Seguridad).

            //este código se utiliza para controlar y restringir el acceso a una pantalla a los usuarios que tienen permisos de
            //administrador.Si un usuario que no es administrador intenta acceder a la pantalla, se le redirige a una página 
            //de error con un mensaje apropiado.

            if (!Seguridad.esAdmin(Session["trainee"]))
            {
                // Si el usuario no es un administrador, almacenamos un mensaje de error en la sesión.
                Session.Add("error", "Se requiere permisos de admin para acceder a esta pantalla");

                // Redirigimos al usuario a la página de error (Error.aspx).
                Response.Redirect("Error.aspx");
            }


            if (!IsPostBack)
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Session.Add("listaPokemons", negocio.listar());
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
            
            txtFiltro.Enabled = !FiltroAvanzado;
        }


        //filtro avanzado: logica de desplegables
        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //quita los elementos anteriores para que no se acumulen cada que se selecciona un item
            ddlCriterio.Items.Clear();
            //va a llenar el otro desplegable: 
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