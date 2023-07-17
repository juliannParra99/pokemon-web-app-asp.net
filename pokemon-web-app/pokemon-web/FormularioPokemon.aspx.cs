using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//para traer los datos de los desplegables
using dominio;
using negocio;


//Elimiancion logica de un registro:  dar de baja del registro pero no borrarlo de la base de datos; permite mantener el registro, y actualizar
//un valor como inactivo, lo que va a hacer que aparezca o no.
//La eliminacion logica es mas util pero requiere mas trabajo. Mientras los registros sean mas SENSIBLES, es mas recomendable usar eliminacion
//logica
//En mi DB tengo una columna llamadaa 'activo', que es de tipo bit (bool), que puedo usar para esto; guarda un 1 o 0
//como la consulta que estamos usando solo muestra los que estan activos, los que no esten activos no se  mostraran

//tambien se ppuede crear una manera de ver que elementos estan activos o inactivos y ver esa info; Para eso voy a hacer algunos ligeros cambios
//en mi manejo de las consultas con la db


//IMP IMP

//Esto est util,por que por ejemplo en la vista de tarjetas quiero ver solo aquellos que esten activos, pero en el panel de administracion quiero
//ver activos e inactivos, por ejemplo para ver cuando un articulo esta disponible o no en un e-commerce y poder hacer una reactivacion de ser
//necesario

//Hasta antes de esta seccion se utiliza esta consulta para listar los valores activos, con el stored procudere. 

//ANTES: 
//ALTER procedure[dbo].[storedListar] as
//Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id 
//From POKEMONS P, ELEMENTOS E, ELEMENTOS D 
//Where E.Id = P.IdTipo And D.Id = P.IdDebilidad And P.Activo = 1

//Ahora: Le quito de la ultima linea que solo traiga losa ctivos, y traigo la columna de p.Activo; Ademas le agrego la propiedad Activo al Pokemon

//ALTER procedure[dbo].[storedListar] as
//Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo 
//From POKEMONS P, ELEMENTOS E, ELEMENTOS D 
//Where E.Id = P.IdTipo And D.Id = P.IdDebilidad 






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

                //uso el eliminar logico de mi negocio y le paso el id que esta en el formulario
                negocio.eliminarLogico(int.Parse(txtId.Text));
                //aca podria hacer una validacion como en el eliminar fisico, pero en este caso no lo voy a hacer.

                Response.Redirect("pokemonLista.aspx");
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }
        }
    }
}