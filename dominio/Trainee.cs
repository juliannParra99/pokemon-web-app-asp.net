using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    //clase para manejar los valores del objeto trainee que se van a mandar a la base de datos. 
    //son los usuarios; van a ser admin o no, pero eso lo vamos a configurar por otro lado 

    //Imp: los datos de usuario, se van a guardar en la nueva tabla que se llama Users; quedaria mas intuitivo si esta clase se correspondiera
    //en nombre con el de la tabla (users), pero en el contexto de la aplicacion se intuye que el trainee es un usuario. 

    //MAS INFO EN: Registro.aspx.cs
    public class Trainee
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string ImagenPerfil { get; set; }
        public bool Admin { get; set; }
    }
}
