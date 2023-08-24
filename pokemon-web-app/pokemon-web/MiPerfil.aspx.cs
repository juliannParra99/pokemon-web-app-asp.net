using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;



//En esta seccion se va a agregar de que el usuaio agrege una foto de perfil desde su propia computadora. Para esto lo mas comun es 
//disponer de una carpeta de archivos dentro de nuestra aplicacion que va a contener la imagen que el usuario cargue y que va a 
//va a ser referenciada por ubicacion en la base de datos para despues poder mostrarla 

//-Usualmente cuando cargamos una imagen desde nuestro disco local, no se guarda directamente en la base de datos, sino que se realiza una
//    copia de ese archivo en una carpeta(IMPORTANTE) dentro de nuesto sistema de archivos de nuestra app y desde ahi lo guardamos en la db
//    y referenciando a la ubicacion de ese sistema de archivos
//- Hacer que se cargue la imagen segun quien este logeado
//- Validar DB null
//- tarea: que el metodo actualizar actualice los datos ( nombre apellido, imagen); y que se levanten los datos precargados; el mail solo readOnly 


//necesitamos un lugar donde guardar los archivos y tiene  que ser una capeta fisica, no puede ser directamente en la db.

//En este caso se crea una carpeta en pokemon-Web que va a contener nuestros archivos para imagenes de perfil etc. 

//en ambito web, esa carpeta no tiene nuestra ruta de pc local, sino que hay que referenciarla a partir del proyecto de nuestra web, 
// dado que esta todo en un ambito virtual.

//En web vamos a necesitar dos rustas distinas: 1 para guardar los archivos y otra para leer los archivos

//se crea la carpeta imagenes, vamos a recuperar esa ruta por que es donde vamos a escribir y guardar nuestro archivos; es una carpeta fisica dentro
//de nuestra pc, pero cuando lo corramos dentro de un servidor la ruta va a cambiar de acuerdo al servidor por lo que para resolver esto lo que
//hay que hacer hacer que la ruta se recupere apartir de este proyecto/solucion , es decir que el proyecto en que esta trabajando sea
// referenciado por cualquier pc

//Tarea: algunos usuarios no tienen imagen, por lo que si se logean no se vera la imagen; en ese caso hay que validar que si no tiene foto
//se muestre la foto de que no hay foto, y no el elemento de imagen roto


namespace pokemon_web
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //atualizar los datos
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TraineeNegocio negocio = new TraineeNegocio();
                //Escribir img
                string ruta = Server.MapPath("./Images/");
                Trainee user = (Trainee)Session["trainee"];
                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg");

                //guardar datos perfil
                user.ImagenPerfil = "perfil-" + user.Id + ".jpg";
                negocio.actualizar(user);

                //leer img
                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Images/" + user.ImagenPerfil;

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}