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
    //En este ejemplo se va ejjemplificar la diferencia entre listar con repeater  y utilizar un foreach
    //para poder repetir codigo.

    //cual usemos dependera de lo qu queramos construir: por ejemplo, no siempre vamos a querer usar un repetear
    //por que no siempre vmos a querer  almaacenar un valor de un objeto y ejecutar el el evento load
    //para manipularlo en la misma pestaña. El for each no permite esto, pero al manjerar directamente el objeto
    //podemos trabajar con distintas propiedades al mismo tiempo

    //el repeater permite repetir codigo, pero ademas tiene otras funcionalidades, que permite capturar
    //los valores de un objeto y guardarlo en una variable y utilizarlo en la misma pagina para crear
    //una nueva funcionalidad dentro de la misma pagina. Como cuando mandamos una variable por url para
    //relizar una funcionalidad. Pero con el repeater lo que pasa es que cuando se ejecuta el evento y se 
    //guarda la variable se recarga el evento load, pero tenemos que validarlo para que guarde el valor
    //utilizando el !IsPostBack, por que se recaga la pagina pero con el valor que se guardo
    public partial class _default : System.Web.UI.Page
    {
        public List<Pokemon> ListaPokemon { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            
            PokemonNegocio negocio = new PokemonNegocio();

            ListaPokemon = negocio.listaConSP();
            //con esto le asigno la lista al repetidor para despues acceder a sus atributos y metodos.
            //Similar a como es en el DGV

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
            //yo se que el sender es un button asique le hago casteo explicito, por que es un object. Ademas, lo envuelvo entre otros parentesis para 
            //indicar que es un boton, asique accedo a la propiedad de ese button.Con la propiedad CommandArgument guardamos
            //el valor que conseguimos como argumento, lo guarda ahi.

            //importante:cuando se ejecute el evento, se va a traer el dato y se va a generar un postaback, por lo que 
            //se va  avolver a recargaar todo y  el repetidor a volver a recargarse en postback se v a arompar, por lo
            //que solo cargamos los valores del repetidos si no es postback,  por que con que lo hagan la primera vez ya esta
            //sino se duplican los valores: esa validacion se hace en el load
            string valor = ((Button)sender).CommandArgument;
            //valor capturaa el valor del id del pokemon, y lo podriamos usar para manejar esa variable en la misma pestaña
            //y manejar logica como hacer un filtro. pero en este caso no lo usamos
        }
    }
}