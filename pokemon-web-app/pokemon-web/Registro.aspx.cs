using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


//Primero: Le voy a dar funcionalidad a mi boton de registrarse. Es decir, cuando lo aprieto quiero que me genere mi usuario en la db
//par el registro solo vamos a pedir un mail y contraseña, el resto de atributos del objeto  se van a poder configurar desde una pestaña de la app
//a posteriori


//Ademas el id de cada usuario registrado se va a almacenar en una variable para que se sepa quien es y se mantenga en la session cuando se logee,si su seccion
//a activa o no

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
                // Se crea una instancia de la clase Trainee y se guarda en la variable 'user'.A la que se le va a guardar los valores
                Trainee user = new Trainee();

                TraineeNegocio traineeNegocio = new TraineeNegocio();
                EmailService emailService = new EmailService();

                // Se asigna al objeto los valores del campo de texto 
                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;

                // Se llama al método 'insertarNuevo' de la clase 'TraineeNegocio', pasando el objeto 'user' como parámetro y se guarda el ID devuelto en la variable 'id'.
                //ESTO ES IMPORTANTE PARA DESPUES POR QUE CON ESTO DESPUES: LO VAMOS A USAR PARA SABER TENER DISPONIBLE LA SESSION DISPONIBLE Y VER QUE USUARIO
                //SE HA LOGUADO, Y USARLA PARA QUE PUEDA NAVAR  POR DIVERSAS PARTES DE LAS PAGINAS; o para hacer un autologin si el usuario se acaba de
                //de registrar

                //Obtiene el id de usuario que se acaba de generar para disponer de sus datos
                int id = traineeNegocio.insertarNuevo(user);

                //HASTA AQUI YA SE HA GENERADO EL NUEVO USUARIO POR LO QUE PODRIA MANDARLE UN MENSAJE DE BIENVENIDA AL MAIL

                // Se llama al método 'armarCorreo' de la clase 'EmailService', pasando la dirección de correo electrónico, un asunto y un cuerpo para el correo.
                //le paso el objeto user.

                //con los parametros le paso lo necesario de  la clase  del mail y que va a a nacesitar
                //el segundo paraemtro  se puede remplazar por alguna plantilla html o algo ams elaborado que tengamos guardado
                emailService.armarCorreo(user.Email, "Bienvenida Trainee", "Hola te damos la bienvenida a la aplicación...");

                // Se llama al método 'enviarEmail' de la clase 'EmailService' para enviar el correo electrónico.
                emailService.enviarEmail();

                // Se redirige al usuario a la página "Default.aspx" y se establece el parámetro 'false' para que no se realice un envío inmediato de la respuesta al cliente.
                Response.Redirect("default.aspx", false);
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción en el bloque de código anterior, se agrega la descripción de la excepción a la sesión con la clave "error".
                Session.Add("error", ex.ToString());
            }
        }
    }
}