using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;



namespace pokemon_web
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Trainee user = new Trainee();

                TraineeNegocio traineeNegocio = new TraineeNegocio();
                EmailService emailService = new EmailService();

                // Se asigna al objeto los valores del campo de texto 
                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;


                //Obtiene el id de usuario que se acaba de generar para disponer de sus datos
                int id = traineeNegocio.insertarNuevo(user);

                emailService.armarCorreo(user.Email, "Bienvenida Trainee", "Hola te damos la bienvenida a la aplicación...");

                emailService.enviarEmail();

                // Se redirige al usuario a la página "Default.aspx" y se establece el parámetro 'false' para que no se realice un envío inmediato de la respuesta al cliente.
                Response.Redirect("default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}