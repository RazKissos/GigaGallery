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
        
    }
    protected void submitBtn_Click(object sender, EventArgs e)
    {
        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();
        try
        {
            if (ws.Login(emailTB.Text, passwordTB.Text))
                ErrorLabel.Text = "Success";
            else
                ErrorLabel.Text = "Fail";
        }
        catch(Exception ex)
        {
            string outputStr = "";
            // TODO: Show user the error.
            ErrorLabel.Text = outputStr;
        }
       
    }
    protected void forgotpasswordBtn_Click(object sender, EventArgs e)
    {
        
    }
}