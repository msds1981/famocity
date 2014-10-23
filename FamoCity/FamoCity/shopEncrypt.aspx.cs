using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FamoLibrary;
using mUtilities;

namespace famocity
{
    public partial class shopEncrypt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EncryptionThread t = new EncryptionThread ();

            string encrpt =t.EncryptString(TextBox1.Text,FamoBase.const_Encryption_Password);
            Response.Redirect("shopMain.aspx?p="+encrpt);
        }
    }
}