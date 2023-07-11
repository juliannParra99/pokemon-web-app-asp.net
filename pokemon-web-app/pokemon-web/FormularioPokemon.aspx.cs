using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//para traer los datos de los desplegables
using dominio;
using negocio;

//En  este ejemplo se realizara la funcion de eliminacion  fisica: Se agrega un boton de eliminar, el cual al ser
//presionado habilitara una confirmacion que se dara mediante un checked (podria hacerse mediante una pantalla emergente
//que pida confirmacion), y cuando se toque otro boton se produzca la eliminacion. La confirmacion nos garantiza evitar errores.

//Recordar: La eliminacion fisica tiene que ser una situacion muy particular, donde no se manejen datos sensibles, por que puede ser un 
//problema borrar esos valores definitivamente. Si se trabajo con registros de personas, sueldos,Facturas de comercios etc, no es recomendado.

//A tener en cuenta: El elimanar tambien aparece en  la pantalla de agregar pokemo, por lo que no tiene mucho sentido dejarla en ese lugar.
//ademas el eliminar utiliza el ID tambien, por lo que la funcionalidad ocurre cuando se toca en la la propiedad 'Seleccionar'

namespace pokemon_web
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        //propiedad para manejar la confirmacion de la eliminacion de los pokemons en el formulario
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            //por defecto va a estar en false cuando cargue la pagina
            ConfirmaEliminacion = false;
            try
            {
                //Configuracion inicial de la pantalla
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    //quiero traer  los valores de tipo y debilidad, que ambos estan en el objeto elemento : 
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
                    //obtengo la lista que va a tener un solo elemento y que viene desde el metodo listar()
                    List<Pokemon> lista = negocio.listar(id);
                    //obtengo el pokemon de la lista, y como solo hya uno siempre va a estar en el indice 0
                    Pokemon seleccionado =lista[0];

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

                }


            }
            catch (Exception ex )
            {
                //puedo guardar el error en la session
                Session.Add("error", ex);
                throw ex;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //creo un pokemon con los datos del nuevo pokemon: el Id no por que lo va a generar cuando
                //hagamos el alta

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
                //si viene por url un id distinto de nulo modifico, sino agrego
                if (Request.QueryString["id"] != null)
                {
                    //tiene que tener precargado el id, de lo contrario no se acuatlizaran los datos.
                    //si tiene el id en el formulario, es por que se apreto seleccionar
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

        //confirma y ejecuta la eliminacion: En este caso voy a usar el metodo eliminar que ya tengo en PokemonNegocio, pero podria
        //haerlo con storedProcedure tambien
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
    }
}