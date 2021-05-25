using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submitBtnClick(object sender, EventArgs e)
    {
        string username = this.usernameTB.Text;
        string email = this.emailTB.Text;
        string password = this.passwordTB.Text;
        string confirmPassword = this.passwordConfirmTB.Text;
        DateTime birthday = default(DateTime);

        try
        {
            birthday = DateTime.Parse(this.birthdayTB.Text);
        }
        catch
        {
            this.birthdayErrorLabel.Text = "Invalid birthday!";
            this.birthdayErrorLabel.Visible = true;
            return;
        }

        // Validate Data:
        localhost.UserValidationWS validationWS = new localhost.UserValidationWS();
        if (!validationWS.isUsernameValid(username))
        {
            this.usernameErrorLabel.Text = validationWS.usernameLengthInvalidMessage();
            this.usernameErrorLabel.Visible = true;
            return;
        }
        if (!validationWS.isEmailLengthValid(email))
        {
            this.emailErrorLabel.Text = validationWS.emailLengthInvalidMessage();
            this.emailErrorLabel.Visible = true;
            return;
        }
        if (!validationWS.isPasswordValid(password))
        {
            this.passwordErrorLabel.Text = validationWS.passwordLengthInvalidMessage();
            this.passwordErrorLabel.Visible = true;
            return;
        }
        if (password != confirmPassword)
        {
            this.passwordErrorLabel.Text = "Passwords do not match!";
            this.passwordErrorLabel.Visible = true;
            return;
        }

        localhost.GigaGalleryWS dbWS = new localhost.GigaGalleryWS();
        bool result = false;
        try
        {
            result = dbWS.Signup(username, email, password, birthday);
        }
        catch
        {
            this.errorLabel.Text = "Error!";
            this.errorLabel.Visible = true;
            return;
        }

        if(result)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            this.errorLabel.Text = "Error!";
            this.errorLabel.Visible = true;
            return;
        }
    }

    void modifyRequiredLabels(bool email, bool username, bool password, bool confirmPassword, bool birthday)
    {
        if (!email)
            this.requiredEmailLabel.Visible = true;
        else
            this.requiredEmailLabel.Visible = false;
        if (!username)
            this.requiredUsernameLabel.Visible = true;
        else
            this.requiredUsernameLabel.Visible = false;
        if (!password)
            this.requiredPasswordLabel.Visible = true;
        else
            this.requiredPasswordLabel.Visible = false;
        if (!confirmPassword)
            this.requiredPasswordConfirmLabel.Visible = true;
        else
            this.requiredPasswordConfirmLabel.Visible = false;
        if (!birthday)
            this.requiredBirthdayLabel.Visible = true;
        else
            this.requiredBirthdayLabel.Visible = false;
    }

    protected void showPasswordCB_CheckedChanged(object sender, EventArgs e)
    {
        if (this.showPasswordCB.Checked)
            this.passwordTB.TextMode = TextBoxMode.SingleLine;
        else
        {
            this.passwordTB.TextMode = TextBoxMode.Password;
        }
    }
}