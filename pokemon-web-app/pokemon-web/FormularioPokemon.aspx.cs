using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//para traer los datos de los desplegables
using dominio;
using negocio;


//En este ejemplo se van A reactivar los valores que fueron dados de baja por ELIMINACION LOGICA.
//La funcionalidd de Reactivar va a variar dependiendo de lo que estemos connstruyendo y se le pueden dar distintas funcionalidades: MODELO DE ESTADO
// La reactivacion se va a hacer por un boton en el formulario.
//
// Ademas, voy  hacer que el nombre del boton inactivar cambie a 'activar' si el registro esta desactivado, y
// voy a hacer algunas modificaciones en el eliminarLogico para que reactive o inactive segun el caso: Para esto voy a guardar en la session
// el boton el pokemonSeleccionado para poder acceder rapidamente a el y manipularlo facilmente en este caso (como en otros futuros)
//  y lo voy a usar en btnInactivar_Click, para pasarle los parametros eliminarLogico y que reactive o inactive segun el caso.)





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

                    //En este punto me va a dar un error: Por que Listar filtra por los que estan ACTIVO, por que
                    //aun no he cambiado la consulta, entonces no lo encuentra. Entonces tengo que sacar de esa
                    //consulta en el metodo listar  esa parte por que esta filtrando por ACTIVO, y quiero 
                    //reactivar los inactivos, pero no los encuentra.
                    List<Pokemon> lista = negocio.listar(id);
                    //obtengo el pokemon de la lista, y como solo hya uno siempre va a estar en el indice 0
                    Pokemon seleccionado =lista[0];

                    //GUARDO POKEMON SELECCIONADO EN SESSION PARA DESPUES REFERENCIARLO DIRECTAMENTE CUANDO TENGA QUE HACER ALGUNA ACCION
                    //guardo el objeto 'seleccionado'
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

                    //cuando toque seleccionar un pokemon voy a estar realizando una modificacion asique voy a precargar los datos
                    //Si el seleccionado no esta activo voy a cambiarle el nombre
                    //si no esta activo, osea si esta en false, cambia el texto; sino lo dejo igual
                    //tengo que modificar la consulta para que traiga la columna activo, sino no funcionara
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

                //Recupero el pokemon de la session para pasarle los valroes al metoto eliminarLogico

                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];
                //otra alternativa seria evaluar si el  btnInactivar.Text del boton es Inactivar o Inactivar y  mandar un valor u otro
                //al eliminarLogico()

                //uso el eliminar logico de mi negocio y le paso el id que esta en el formulario
                //OJ0: le mando de la propiedad ACTIVO el opuesto, para que pueda modificar lo opuesto: es decir, si esta activo, va a 
                //poder desactivar, y si esta inactivo, va  apoder activar
                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
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