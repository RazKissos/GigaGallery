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

                this.errorLabel.Visible = false;
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
        this.errorLabel.Visible = false;
        string tableName = this.TableSelector.Text;
        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();

        this.SerchBySelector.Items.Clear();

        DataTable schema = ws.GetTableSchema(tableName);
        if(schema != null)
        {
            foreach (DataRow dr in schema.Rows)
            {
                this.SerchBySelector.Items.Insert(0, dr["COLUMN_NAME"].ToString());
            }
        }
        else
        {
            this.errorLabel.Text = "Error!";
            this.errorLabel.Visible = true;
        }
    }
    protected void searchDataBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;
        string tableName = this.TableSelector.Text;
        string searchParam = this.SerchBySelector.Text;
        string valueToSearch = this.SearchBarTB.Text;
        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();
        DataTable dt = ws.SelectEntireTable(tableName);
        DataTable searchResults = new DataTable("searchResultsTable");

        if (dt.Columns.Contains(searchParam))
        {
            foreach(DataRow dr in dt.Rows)
            {
                if (dr[searchParam].ToString().ToLower() == valueToSearch.ToLower().Trim())
                    searchResults.ImportRow(dr);
            }

            // Update grid view and make visible.
            if (searchResults.Rows.Count > 0)
            {
                this.searchResultsDL.DataSource = searchResults;
                this.searchResultsDL.DataBind();
                // Debug gridview not displaying data.
            }
            else
            {
                this.errorLabel.Text = "Search did not yield any results!";
                this.errorLabel.Visible = true;
            }
        }
    }
}