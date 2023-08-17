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

        //utilizamos la pagina maestra que es heradada a las otras 
        // Evento Page_Load que se ejecuta cuando la página se carga.

        protected void Page_Load(object sender, EventArgs e)
        {
            //CON ESTO TNEMOS CENTRALIZADA LA SEGURIDAD
            //El objeto 'Page' se refiere a la pagina(que es un objeto) a la que se esta dirigiendo ene ste momento el objeto MasterPage
            // Verificamos si la página actual no es la página de inicio de sesión (Login).
            //seria algo como :  si la pagina que va a contener el masterPage es la page Login, entonces...
            //Si la pagina que estoy por cargar no es login, entonces que chequee la seguridad

             //para que se puedan ver El home, el registrarse y el login
             //si no es ninguna de esas paginas va a redirigir al login para iniciar sesion
            if (!(Page is Login || Page is Registro || Page is _default))
            {
                // Verificamos si no hay una sesión activa para el usuario en la variable de sesión "trainee".
                if (!Seguridad.sesionActiva(Session["trainee"]))
                {
                    // Si no hay una sesión activa, redirigimos al usuario a la página de inicio de sesión (Login.aspx).
                    Response.Redirect("Login.aspx", false);
                }
            }


        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            //con esto borro todos los datos del usuario de la session.. Podria solo buscar los datos del objeto trainee
            //pero como no quiero guardar nada del usuario borro todo
            //Session.Clear() borra todos los datos almacenados en la sesión actual.La "sesión" es una forma de mantener datos
            //entre diferentes solicitudes de un mismo usuario en una aplicación web.Borrar la sesión implica que se eliminarán
            //todos los datos almacenados en ella, como información de inicio de sesión, preferencias del usuario, etc.
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}


