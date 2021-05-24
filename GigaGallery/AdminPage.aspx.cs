using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class AdminPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void addUserBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;
        string username = this.usernameTB.Text;
        string email = this.emailTB.Text;
        string password = this.passwordTB.Text;
        bool isAdmin = this.isAdminCB.Checked;
        DateTime birthday = default(DateTime);

        try
        {
            birthday = DateTime.Parse(this.birthdayTB.Text);
        }
        catch
        {
            this.errorLabel.Text = "Error!";
            this.errorLabel.Visible = true;
            return;
        }

        // Validate Data:
        localhost.UserValidationWS validationWS = new localhost.UserValidationWS();
        if (!validationWS.isUsernameValid(username))
        {
            this.errorLabel.Text = validationWS.usernameLengthInvalidMessage();
            this.errorLabel.Visible = true;
            return;
        }
        if (!validationWS.isEmailLengthValid(email))
        {
            this.errorLabel.Text = validationWS.emailLengthInvalidMessage();
            this.errorLabel.Visible = true;
            return;
        }
        if (!validationWS.isPasswordValid(password))
        {
            this.errorLabel.Text = validationWS.passwordLengthInvalidMessage();
            this.errorLabel.Visible = true;
            return;
        }

        localhost.GigaGalleryWS dbWS = new localhost.GigaGalleryWS();
        try
        {
            dbWS.AdminAddUser(username, email, password, birthday, isAdmin);
        }
        catch
        {
            this.errorLabel.Text = "Error!";
            this.errorLabel.Visible = true;
        }

        Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected void searchDataBtn_Click(object sender, EventArgs e)
    {

    }

    protected void UsersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void UsersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void Users_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}