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
        protected void Page_Load(object sender, EventArgs e)
        {
            //el usuario no va a tener que modificar el id ni colocarlo asique lo desactivamos
            txtId.Enabled = false;
            //siempre hacer manejo de excepciones
            try
            {
                
                //Vamos a cargar los desplegables de tipo y de debilidad

                //si no es postback: para que no se traigan los datos nuevamente
                if (!IsPostBack)
                {
                    ElementoNegocio negocio = new ElementoNegocio();
                    //quiero traer  los valores de tipo y debilidad, que ambos estan en el objeto elemento : entonces creo la lista y le guardo los elementos
                    List<Elemento> lista = negocio.listar();

                    //ahora cargo los desplegables 
                    ddlTipo.DataSource = lista;
                    //DataTextField se refiere al aspecto visual (frontend) y descriptivo de las opciones en el desplegable, mientras que el DataValueField se refiere al valor seleccionado
                    //y se utiliza para interactuar con la lógica del backend y realizar acciones adicionales basadas en esa selección.

                    //se les asigna las propiedades del objeto que es el origen de datos(Elemento): Id va  a devolver el
                    //elemento con ese ID, para poder trabajar con ese Id
                    ddlTipo.DataValueField = "Id";
                    //De esta manera, estás indicando que el texto descriptivo de cada opción en el DropDownList se obtendrá de la 
                    //    propiedad "Descripcion" en el origen de datos.
                    ddlTipo.DataTextField = "Descripcion";

                    ddlTipo.DataBind();

                    //Ahora para la debilidad

                    ddlDebilidad.DataSource = lista;
                    ddlDebilidad.DataValueField = "Id";
                    // estás indicando que el texto descriptivo de cada opción en el DropDownList se obtendrá de la propiedad "Descripcion"
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();

                    //ahora configuro el evento del click abajo: para que permita agregar pokemons
                }
            }
            catch (Exception ex )
            {
                //puedo guardar el error en la session
                Session.Add("error", ex);
                throw ex;
                //posteriormente puedo hacer una redireccion a otra pantalla.
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

                //tipo es un objeto de tipo elemento, por lo que tengo que crear ese objeto (instanciarlo).
                //esto por que cuando selecciono el valor del desplegable ya no es mas un objeto, sino texto 
                //plano. Por lo que creo el objeto, y al tipo.Id le asigno el valor de backend(id) que se
                //asigno al desplegable. 
                nuevo.Tipo = new Elemento();
                //le asigno el value del campo seleccionado en el desplegable.Y lo hago int, por que al 
                //renderizarse en web se vuelve texto plano. Va a guardar el Int.
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);

                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                //agrego el dato a la db: le paso el nuevo pokemon: Puedo usar el metodo agregar, o el que
                //hace uso del procedimiento almacenado. En este caso se va a usar el del procedimiento almacenado
                //y se va a comentar el agregar. Despues voy a dejar el de SP. Pero es mas bien para ejemplificar
                //cualqueira sirve, y en un proyecto elijo el que mas me convenga
                negocio.agregarConSP(nuevo);
                //y vamos a la pantalla de redirect. 
                Response.Redirect("pokemonLista.aspx",false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }

        //Para precargar la imagen en formulario
        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }
    }
}