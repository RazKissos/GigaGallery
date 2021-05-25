using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class AdminHomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["pUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Session is active!
                User u = (User)Session["pUser"];
            }
        }
    }
}