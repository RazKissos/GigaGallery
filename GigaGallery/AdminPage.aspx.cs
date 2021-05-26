using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;
using System.Data;
using System.Data.OleDb;
public partial class AdminPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(! IsPostBack)
        {
            if(Session["pUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Session is active!
                User u = (User)Session["pUser"];
                if(!u.IsAdmin)
                {
                    Response.Redirect("HomePage.aspx");
                }
                // User is admin.

                this.updateDropDownParams(sender, e);
            }
        }
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
            return;
        }

        Response.Redirect(Page.Request.Url.ToString(), true);
    }
    protected void updateDropDownParams(object sender, EventArgs e)
    {
        string tableName = this.TableSelector.Text;
        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();

        DataTable schema = ws.GetTableSchema(tableName);
        if(schema == null)
        {
            this.SerchBySelector.Items.Insert(0, "Error");
            return;
        }

        this.SerchBySelector.Items.Clear();

        foreach(DataRow dr in schema.Rows)
        {
            this.SerchBySelector.Items.Insert(0, dr["COLUMN_NAME"].ToString());
        }
    }
    protected void searchDataBtn_Click(object sender, EventArgs e)
    {
        string tableName = this.TableSelector.Text;
        string searchParam = this.SerchBySelector.Text;
        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();
        DataTable dt = ws.SelectEntireTable(tableName);

        Dictionary<string, Type> colTypes = new Dictionary<string, Type>();
        foreach(DataColumn dc in dt.Columns)
        {
            string type = dc.DataType.Name;
            // Finish fetching of specific table with correct types.
        }
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