using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pokemon_web
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //va a dibujar el error: esto es solo temporal eventualmente lo voy a mejorar
            lblError.Text = Session["error"].ToString();
        }
    }
}