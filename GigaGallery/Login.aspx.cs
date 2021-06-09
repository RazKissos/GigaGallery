using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;
using System.Collections;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            emailTB.Text = "";
            passwordTB.Text = "";
            ErrorLabel.Text = "";
        }
        RequiredEmailLabel.Visible = false;
        RequiredPasswordLabel.Visible = false;
    }
    protected void submitBtn_Click(object sender, EventArgs e)
    {
        if (emailTB.Text == "")
        {
            RequiredEmailLabel.Visible = true;
            return;
        }
        else
            RequiredEmailLabel.Visible = false;

        if (passwordTB.Text == "")
        {
            RequiredPasswordLabel.Visible = true;
            return;
        }
        else
            RequiredPasswordLabel.Visible = false;

        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();
        try
        {
            if (ws.Login(emailTB.Text, passwordTB.Text))
            {
                localhost.User userObj = ws.GetUserObj(emailTB.Text);
                Session["pUser"] = userObj;
                Session["pUserId"] = userObj.UserId;
                if (userObj.IsAdmin)
                    Response.Redirect("AdminPage.aspx");
                else
                    Response.Redirect("HomePage.aspx");
            }
            else
                ErrorLabel.Text = "Login Failed! Email or Password are incorrect.";
        }
        catch
        {
            ErrorLabel.Text = "Login Failed! Email or Password are incorrect.";
        }
    }
    protected void forgotpasswordBtn_Click(object sender, EventArgs e)
    {
        
    }
}