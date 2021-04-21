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

    protected void CheckedShowPassword(object sender, EventArgs e)
    {
    }

    protected void submitBtnClick(object sender, EventArgs e)
    {
        bool validEmail = this.emailTB.Text != "";
        bool validUsername = this.usernameTB.Text != "";
        bool validPassword =  this.passwordTB.Text != "";
        bool validPasswordConfirm = this.passwordConfirmTB.Text != "";
        bool validBirthday = this.birthdayTB.Text != "";
        if (!(validEmail && validUsername && validPassword && validPasswordConfirm && validBirthday))
        {
            modifyRequiredLabels(validEmail, validUsername, validPassword, validPasswordConfirm, validBirthday);
            return;
        }

        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();
        bool trySignup = ws.Signup(usernameTB.Text, emailTB.Text, passwordTB.Text, DateTime.Parse(birthdayTB.Text));
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
}