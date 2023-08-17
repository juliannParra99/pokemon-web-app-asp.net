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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Método que se ejecuta cuando se hace clic en el botón de inicio de sesión.
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Creamos una instancia de la clase Trainee para almacenar los datos del usuario.
            Trainee trainee = new Trainee();

            TraineeNegocio negocio = new TraineeNegocio();
            try
            {
                trainee.Email = txtEmail.Text;
                trainee.Pass = txtPassword.Text;

                if (negocio.Login(trainee))
                {
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