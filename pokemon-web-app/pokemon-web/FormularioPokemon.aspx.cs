using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//para traer los datos de los desplegables
using dominio;
using negocio;

//en este ejemplo se va a m,odifica el formulario para que cuando queremos ingresar un nuevo pokemon podamos hacerlo
//pero que cuando pulsemos en el boton 'modificar' se cargue el formulario con el id del pokemon seleccionado, para
//asi precargar los valores y modificar aquello que queremos modificar. 

namespace pokemon_web
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
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

                //configuracion si estamos modificando. 

                //pregunto si cuando se accede al formulario se lo hizo trayendo un id de  un pokemon en la url
                //osea, si el query string tiene esa variable. Si la tiene es modificar.
                //aca necesito el pokemon que coincide con el id para modificarlo; podria tener el objeto pokemon
                //en Session pero no seria conveniente si manejo una gran cantidad de datos, asique lo manejo directamente 
                //desde la db; traigo la lista de  datos de la db directamente

                //si tiene ID voy a ir a la db a traer el listado de pokemons
                //podria crear un metodo que me trajera el pokemon filtrando por id pero no viene al caso ahora.]
                
                //sigo en pokemonNegocio


                //operador ternario: si viene con id lo guardo, sino lo dejo vacio
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "" ;
                //y tengo que agregarle que ejecuto la logica cuando no es postback, de lo contrario, cuando
                //toque en un pokemon para modificar, y rellene con los datos actualizados, antes de que
                //se ejecute el boton aceptar la pantalla va a ejecutar el postaback de nuevo con los datos
                //originales, por lo que los datos seguirian siendo los mismos, cosa que no quiero.
                if (id != "" && !IsPostBack)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    //obtengo la lista que va a tener un solo elemento y que viene desde el metodo listar()
                    List<Pokemon> lista = negocio.listar(id);
                    //obtengo el pokemon de la lista, y como solo hya uno siempre va a estar en el indice 0
                    Pokemon seleccionado =lista[0];

                    //OTRA MANERA DE HACER LO QUE HACEN LAS ULTIMAS DOS LINEAS Y HACERLO EN 1 SOLA SERIA 
                    //por que se que negocio listar da una lista, entonces le digo directo el indice que quiero y lo asigno.
                    //Pokemon seleccionado = (negocio.listar(id))[0]

                    //precargar todos los campos del formulario de modificacion.

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
    }
}