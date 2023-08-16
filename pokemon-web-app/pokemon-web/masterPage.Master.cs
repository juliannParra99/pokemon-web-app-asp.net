using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;


namespace pokemon_web
{
    public partial class masterPage : System.Web.UI.MasterPage
    {

        //utilizamos la pagina maestra que es heradada a las otras para que, cuando se carge una pagina, se verifique si el usuario tiene
        //Permitido por la, seguridad de la aplicacion, acceso a las pestañas.

        // Evento Page_Load que se ejecuta cuando la página se carga.

        protected void Page_Load(object sender, EventArgs e)
        {
            //CON ESTO TNEMOS CENTRALIZADA LA SEGURIDAD
            //El objeto 'Page' se refiere a la pagina(que es un objeto) a la que se esta dirigiendo ene ste momento el objeto MasterPage
            // Verificamos si la página actual no es la página de inicio de sesión (Login).
            //seria algo como :  si la pagina que va a contener el masterPage es la page Login, entonces...
            //Si la pagina que estoy por cargar no es login, entonces que chequee la seguridad

            //tarea: con esto todas las paginas que no sean login no las voy a poder ver y me va a redirigir a login. Por lo que en esta condicion
            //tengo que  colocar para que se puedan ver El home, el registrarse y el login
            if (!(Page is Login))
            {
                // Verificamos si no hay una sesión activa para el usuario en la variable de sesión "trainee".
                if (!Seguridad.sesionActiva(Session["trainee"]))
                {
                    // Si no hay una sesión activa, redirigimos al usuario a la página de inicio de sesión (Login.aspx).
                    Response.Redirect("Login.aspx", false);
                }
            }


        }
    }
}


//En este código, se define una clase llamada masterPage que hereda de System.Web.UI.MasterPage. Esta clase está relacionada con la lógica
//    de una página maestra en ASP.NET. En el evento Page_Load, se ejecuta cuando la página se carga.

//if (!(Page is Login)): Esta línea verifica si la página actual no es la página de inicio de sesión (Login).
//    El operador !(Page is Login) verifica si la página no es una instancia de la clase Login.

//if (!Seguridad.sesionActiva(Session["trainee"])): Si la página no es la página de inicio de sesión y no
//    hay una sesión activa para el usuario (verificado utilizando el método sesionActiva de la clase Seguridad y la variable de sesión "trainee"),
//    se ejecuta el bloque de código dentro de este if.

//Response.Redirect("Login.aspx", false);: Si no hay una sesión activa, se redirige al usuario a la página de inicio de sesión (Login.aspx).
//    El segundo parámetro false indica que no se debe detener el flujo de ejecución después de la redirección, lo que permite que el código continúe ejecutándose después de la redirección.

//En resumen, este código se utiliza en una página maestra para verificar si el usuario tiene una sesión activa. Si el usuario no tiene una 
//sesión activa y la página actual no es la página de inicio de sesión, se redirige al usuario a la página de inicio de sesión.