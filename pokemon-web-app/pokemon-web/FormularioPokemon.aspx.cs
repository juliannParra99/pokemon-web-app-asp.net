using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//para traer los datos de los desplegables
using dominio;
using negocio;


namespace pokemon_web
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        //propiedad para manejar la confirmacion de la eliminacion de los pokemons en el formulario
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            try
            {
                //Configuracion inicial de la pantalla
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    List<Elemento> lista = negocio.listar();

                    //cargo los desplegables 
                    ddlTipo.DataSource = lista;
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataTextField = "Descripcion";

                    ddlTipo.DataBind();

                    //Ahora para la debilidad
                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();
                }


                //operador ternario: si viene con id lo guardo, sino lo dejo vacio
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "" ;
                if (id != "" && !IsPostBack)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    List<Pokemon> lista = negocio.listar(id);
                    //obtengo el pokemon de la lista, y como solo hya uno siempre va a estar en el indice 0
                    Pokemon seleccionado =lista[0];

                    
                    Session.Add("pokeSeleccionado", seleccionado);   

                    txtId.Text = id;
                    //le asigno los valrores del pokemon seleccionado.
                    txtNombre.Text = seleccionado.Nombre;
                    txtNumero.Text = seleccionado.Numero.ToString();
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.UrlImagen;

                    //le cargo los desplegables con los valores precargados
                    ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();

                    //evento para que se precargue la iamgen: tambien en lugar de forzar el metodo podria ponerlo en un nuevo metodo
                    txtImagenUrl_TextChanged(sender, e);

                    //CONFIGURAR ACCIONES.

                    if (!seleccionado.Activo)
                    {
                        btnInactivar.Text = "Reactivar";
                    }
                }

            }

            catch (Exception ex )
            {
                //puedo guardar el error en la session
                Session.Add("error", ex);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                Pokemon nuevo = new Pokemon();
                //para enlazar los datos con la db
                PokemonNegocio negocio = new PokemonNegocio();

                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtImagenUrl.Text;

                 
                nuevo.Tipo = new Elemento();
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);

                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                //Le doy la funcionalidad al boton de agregar o modificar en la DB segun corresponda
                if (Request.QueryString["id"] != null)
                {
                    
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificarConSP(nuevo);
                }
                else
                {
                    //agrego el dato a la db: 
                    negocio.agregarConSP(nuevo);
                }

               
                Response.Redirect("pokemonLista.aspx",false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }

        //precargar la imagen en formulario
        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        //Eliminacion fisica
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        //confirma y ejecuta la eliminacion:
        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //si esta checkeado
                if (chkConfirmaEliminacion.Checked)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    //el id que esta en el formulario del pokemon seleccionado
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("pokemonLista.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
            
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();

                //Recupero el pokemon de la session 
                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];
                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);

                Response.Redirect("pokemonLista.aspx");
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }
        }
    }
}