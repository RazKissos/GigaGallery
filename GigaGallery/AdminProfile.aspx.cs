using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class AdminProfile : System.Web.UI.Page
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
                if (!u.IsAdmin)
                {
                    Response.Redirect("HomePage.aspx");
                }
                this.UsernameLabel.Text = u.UserName.ToString();
                this.EmailLabel.Text = u.UserEmail.ToString();
            }
        }
    }

    protected void logoutBtn_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
}