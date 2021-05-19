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
        localhost.GigaGalleryWS ws = new localhost.GigaGalleryWS();
        this.UsersGridView.DataSource = ws.GetAllUsers();
        this.UsersGridView.DataBind();
    }

    protected void addUserBtn_Click(object sender, EventArgs e)
    {
        string username = "";
        string email = "";
        string password = "";
        
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
}