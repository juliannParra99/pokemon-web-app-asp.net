using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

//En este ejemplo se va crear la funcionalidad de hacer que cuando un usuario se registrar quede la session guardada directamente y lo
//redirija al home directamente .

//Ademas, en esta seccion se terminan ciertas funcionalidades que quedaron pendientes de la seccion anterior, la 
//tarea era:  no se debe de restringir la vista del home y la pantalla de registrarse aunque no este logead. Si hay una
//session activa o no habilitar o no los botones de login y registrarse dependiendo de si estoy logueeado o no, osea,
//y si hay una seccion activa no voy a tener disponible el boton registrarse y loguearse, y que se muestre un boton salir,
//que permitiria desloguearse.

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


                //Obtiene el id de usuario que se acaba de generar para disponer de sus datos, y lo asigna al usuario que va a estar vivio
                //en la sesion despues. si el usuario tuviera mas datos tambien habria que asignarlos, pero con esto estamos bien por ahora
                //por que solo usamos email y password. Lo que pdoria ahcer tambiene s llamar al metodo login y traer los datos de db para 
                //iniciar session, pero seria unn poco redundante
                user.Id = traineeNegocio.insertarNuevo(user);
                //agrega el usuario que se acaba de generar a la session para que interactue con la aplicacion. 
                Session.Add("trainee", user);


                //IMP: TENGO QUE CONFIGURAR CORRECTAMENTE EL ENVIO DE MAILS, SINO DA UNA EXCEPCION Y NO SE REDIRIJE A LA PAGINA DEFAULT
                //UNA VEZ EL USUARIO SE REGISTRA
                //emailService.armarCorreo(user.Email, "Bienvenida Trainee", "Hola te damos la bienvenida a la aplicación...");

                //emailService.enviarEmail();

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