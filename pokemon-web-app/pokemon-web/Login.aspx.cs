using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;


//lo que se va hacer  en esta seccion: 

//Home: todos
//Listado pokemon: SOLO ADMINT
//Mi perfil: logeado
//Favoritos:logeado

//Clase seguridad:
//Metodos:
//seccionActiva(User u)
//EsAdmin(user U)
namespace pokemon_web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Método que se ejecuta cuando se hace clic en el botón de inicio de sesión.

        //con este metodo y el metodo Login() de trainee negocio ya funciona el login

        //Este código maneja el evento de clic en el botón de inicio de sesión en una página web.Toma los valores del
        //    correo electrónico y la contraseña desde los campos de texto en la página, y luego llama al método Login 
        //    en la instancia de TraineeNegocio para verificar las credenciales.Si las credenciales son correctas, se almacena 
        //    el objeto Trainee en la sesión y se redirige al usuario a la página "MiPerfil.aspx". Si las credenciales son 
        //    incorrectas, se almacena un mensaje de error en la sesión y se redirige al usuario a la página "Error.aspx". En 
        //    caso de cualquier excepción, se almacena el mensaje de error en la sesión y se redirige al usuario a la página "Error.aspx".
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Creamos una instancia de la clase Trainee para almacenar los datos del usuario.
            Trainee trainee = new Trainee();

            // Creamos una instancia de la clase TraineeNegocio para manejar la lógica de negocios relacionada con los Trainees.
            TraineeNegocio negocio = new TraineeNegocio();
            try
            {
                // Asignamos los valores del correo electrónico y la contraseña desde los campos de texto en la página web.
                trainee.Email = txtEmail.Text;
                trainee.Pass = txtPassword.Text;

                // Llamamos al método Login en la instancia de TraineeNegocio para verificar las credenciales.
                //si devuelve true, encontro los datos con la consulta.
                if (negocio.Login(trainee))
                {
                    // Si las credenciales son válidas, almacenamos el objeto Trainee en la sesión.
                    Session.Add("trainee", trainee);

                    // Redirigimos al usuario a la página "MiPerfil.aspx" sin interrumpir el flujo.
                    Response.Redirect("MiPerfil.aspx", false);
                }
                else
                {
                    // Si las credenciales son incorrectas, almacenamos un mensaje de error en la sesión.
                    Session.Add("error", "User o Pass incorrectos");

                    // Redirigimos al usuario a la página "Error.aspx".
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                // En caso de error, almacenamos el mensaje de error en la sesión.
                Session.Add("error", ex.ToString());

                // Redirigimos al usuario a la página "Error.aspx".
                Response.Redirect("Error.aspx");
            }
        }
    }
}