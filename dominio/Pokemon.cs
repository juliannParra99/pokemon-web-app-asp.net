using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    //en este ejemplo vamo a utilizar un procedimiento almacenado(STORE PROCEDURE) que es una especie de funcion
    //que va a estar prearmada en la base de datos y se puede llamar: se puede hacer muy compleja, pero vamos
    //a hacer una sencilla que nos permita listar los elementos; vamos a tener un procedimiento almacenado, osea
    //una pequeña funcion que nos permita listar los datos, las columnas que necesitamos. ESTO NOS EVITA
    //TENER QUE USAR LA CONSULTA ENVEVIDA, aunque no es muy acosnejable tener mucha logica en la base de datos. 
    public class Pokemon
    {
        public int Id { get; set; }
        [DisplayName("Número")]
        public int Numero { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public Elemento Tipo { get; set; }
        public Elemento Debilidad { get; set; }
        public bool Activo { get; set; }

    }
}
