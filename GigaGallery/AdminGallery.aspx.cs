using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class AdminGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.errorLabel.Text = "";
        this.errorLabel.Visible = false;
        this.selectedImagePreview.Visible = false;

        if (!IsPostBack)
        {
            if (Session["pUser"] == null || Session["pUserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Session is active!
                User u = (User)Session["pUser"];
                if (!u.IsAdmin)
                {
                    this.updateDropDownParams(sender, e);
                    this.imagesGridView.DataBind();
                    if (this.albumDropDown.Items.Count == 0)
                    {
                        this.errorLabel.Text = "You have no albums, please create some!";
                        this.errorLabel.Visible = true;
                    }
                    else if (this.imagesGridView.Rows.Count == 0)
                    {
                        this.errorLabel.Text = "Album is empty, please insert some images!";
                        this.errorLabel.Visible = true;
                    }
                    this.imagesGridView.SelectedIndex = -1;
                }
            }
        }
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
            }
        }
    }

    protected void updateDropDownParams(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;
        GigaGalleryWS ws = new GigaGalleryWS();
        Album[] albumList = ws.GetUserAlbums((int)Session["pUserId"]);
        this.albumDropDown.Items.Clear();
        if (albumList == null)
        {
            this.errorLabel.Text = "You have no albums! please create one!";
            this.errorLabel.Visible = true;
            return;
        }
        foreach (Album al in albumList)
        {
            this.albumDropDown.Items.Insert(0, al.AlbumName);
        }
    }
    protected void selectAlbumBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;
        GigaGalleryWS ws = new GigaGalleryWS();
        int selectedAlbumId = 0;
        try
        {
            selectedAlbumId = ws.GetAlbumIdByUserIdAndAlbumName((int)Session["pUserId"], this.albumDropDown.Text);
        }
        catch
        {
            this.errorLabel.Text = "Error!";
            this.errorLabel.Visible = true;
        }
        if (selectedAlbumId < 0)
        {
            this.errorLabel.Text = string.Format("User has no album called {0}!", this.albumDropDown.Text);
            this.errorLabel.Visible = true;
            this.updateDropDownParams(sender, e);
            return;
        }
        Session["selectedAlbumId"] = selectedAlbumId;
        this.imagesGridView.SelectedIndex = -1;
        this.imagesGridView.DataBind();
        this.updateDropDownParams(sender, e);
    }
    protected void addImageBtn_Click(object sender, EventArgs e)
    {
        if (Session["selectedAlbumId"] == null)
        {
            // No album was selected.
            this.errorLabel.Text = "No album was selected! Please select an album!";
            this.errorLabel.Visible = true;
            return;
        }
        GigaGalleryWS ws = new GigaGalleryWS();
        Album selectedAlbum = ws.GetAlbumById((int)Session["selectedAlbumId"]);

        string beginPath = Request.PhysicalApplicationPath + "UserImages";
        string userPath = string.Format("\\{0}\\{1}", CreateMD5(((User)Session["pUser"]).UserName), CreateMD5(selectedAlbum.AlbumName));

        if (this.imageFileUpload.HasFile)
        {
            if (!System.IO.Directory.Exists(beginPath + userPath))
            {
                // Need to create a directory for the album.
                System.IO.Directory.CreateDirectory(beginPath + userPath);
            }
            UserValidationWS validationWS = new UserValidationWS();
            string newName = this.newImageNameTB.Text.Trim();
            if (!validationWS.isImageNameValid(newName))
            {
                this.errorLabel.Text = validationWS.imageNameLengthInvalidMessage();
                this.errorLabel.Visible = true;
                return;
            }
            string fileExtention = System.IO.Path.GetExtension(this.imageFileUpload.FileName);
            if (fileExtention != ".png" && fileExtention != ".jpg" && fileExtention != ".jpeg")
            {
                this.errorLabel.Text = "File extention must be either of these types (.png, .jpg, .jpeg)!";
                this.errorLabel.Visible = true;
                return;
            }
            if (this.imageFileUpload.PostedFile.ContentLength < 10000000)
            {
                if (doesUserImageExist(beginPath, ((User)Session["pUser"]).UserName, selectedAlbum.AlbumName, newName, fileExtention))
                {
                    this.errorLabel.Text = "Image with the same name alrady exists in this album!";
                    return;
                }
                else
                {
                    // Successfully uploaded image.
                    // Update sql database and save image.
                    string fullPath = getHashedFilePath(beginPath, ((User)Session["pUser"]).UserName, selectedAlbum.AlbumName, newName, fileExtention);

                    if (!ws.UpdateAlbum(selectedAlbum.AlbumId, selectedAlbum.AlbumOwnerId, selectedAlbum.AlbumName, selectedAlbum.AlbumCreationTime, selectedAlbum.AlbumSize + 1))
                    {
                        // error! could not update album sql.
                        return;
                    }
                    string imagePath = getHashedFilePath(".", ((User)Session["pUser"]).UserName, selectedAlbum.AlbumName, newName, fileExtention);
                    bool res = false;
                    try
                    {
                        res = ws.AddImage((int)Session["pUserId"], selectedAlbum.AlbumId, newName, imagePath);
                    }
                    catch
                    {
                        this.errorLabel.Text = "Invalid Action!";
                        this.errorLabel.Visible = true;
                        this.imagesGridView.DataBind();
                        this.imagesGridView.SelectedIndex = -1;
                        return;
                    }
                    if (!res)
                    {
                        // error! could not update image sql.
                        return;
                    }
                    try
                    {
                        this.imageFileUpload.SaveAs(fullPath);
                    }
                    catch
                    {
                        this.errorLabel.Text = "Error!";
                        this.errorLabel.Visible = true;
                    }
                    this.updateDropDownParams(sender, e);
                    this.imagesGridView.DataBind();
                    this.imagesGridView.SelectedIndex = -1;
                }
            }
        }
        else
        {
            this.errorLabel.Text = "Please make sure to select an image file!";
            this.errorLabel.Visible = true;
            this.imagesGridView.SelectedIndex = -1;
            return;
        }
    }
    protected void createAlbumBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;

        GigaGalleryWS ws = new GigaGalleryWS();
        UserValidationWS validationWS = new UserValidationWS();

        string newName = this.newAlbumName.Text.Trim();

        if (validationWS.isAlbumNameValid(newName))
        {
            bool res = false;
            try
            {
                res = ws.AddAlbum((int)Session["pUserId"], newName);
            }
            catch
            {
                this.errorLabel.Text = "Invalid Action!";
                this.errorLabel.Visible = true;
                this.imagesGridView.DataBind();
                this.imagesGridView.SelectedIndex = -1;
                return;
            }
            if (res)
            {
                this.updateDropDownParams(sender, e);
                this.imagesGridView.DataBind();
                this.imagesGridView.SelectedIndex = -1;
            }
            else
            {
                this.errorLabel.Text = "Could not create new album, try again later!";
                this.errorLabel.Visible = true;
                this.imagesGridView.DataBind();
                this.imagesGridView.SelectedIndex = -1;
            }
        }
        else
        {
            this.errorLabel.Text = validationWS.albumNameLengthInvalidMessage();
            this.errorLabel.Visible = true;
            this.imagesGridView.SelectedIndex = -1;
            this.imagesGridView.DataBind();
            return;
        }
    }
    protected void changeAlbumNameBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;

        GigaGalleryWS ws = new GigaGalleryWS();
        UserValidationWS validationWS = new UserValidationWS();

        string newName = this.newAlbumName.Text.Trim();

        if (Session["selectedAlbumId"] == null)
        {
            // No album was selected.
            this.errorLabel.Text = "No album was selected! Please select an album!";
            this.errorLabel.Visible = true;
            return;
        }
        if (validationWS.isAlbumNameValid(newName))
        {
            Album albmObj = ws.GetAlbumById((int)Session["selectedAlbumId"]);
            albmObj.AlbumName = newName;
            // Annoying but only works like this.
            bool res = false;
            try
            {
                res = ws.UpdateAlbum(albmObj.AlbumId, albmObj.AlbumOwnerId, albmObj.AlbumName, albmObj.AlbumCreationTime, albmObj.AlbumSize);
            }
            catch
            {
                this.errorLabel.Text = "Invalid Action!";
                this.errorLabel.Visible = true;
                this.imagesGridView.DataBind();
                this.imagesGridView.SelectedIndex = -1;
                return;
            }
            if (res)
            {
                this.updateDropDownParams(sender, e);
                this.imagesGridView.DataBind();
                Session["selectedAlbumId"] = null;
            }
            else
            {
                this.errorLabel.Text = "Could not update album, try again later!";
                this.errorLabel.Visible = true;
            }
            this.imagesGridView.SelectedIndex = -1;
        }
        else
        {
            this.errorLabel.Text = validationWS.albumNameLengthInvalidMessage();
            this.errorLabel.Visible = true;
            this.imagesGridView.SelectedIndex = -1;
            this.imagesGridView.DataBind();
            return;
        }

    }
    protected void changeImageNameBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;

        GigaGalleryWS ws = new GigaGalleryWS();
        if (Session["selectedAlbumId"] == null)
        {
            // No album was selected.
            this.errorLabel.Text = "No album was selected! Please select an album!";
            this.errorLabel.Visible = true;
            return;
        }

        Img[] images = ws.GetAlbumImages((int)Session["selectedAlbumId"]);
        if (images == null)
        {
            this.errorLabel.Text = "Album is empty, please insert some images!";
            this.errorLabel.Visible = true;
            return;
        }
        Img selectedImg = null;

        foreach (Img img in images)
        {
            if (img.ImageName == Context.Server.HtmlDecode(this.imagesGridView.SelectedRow.Cells[2].Text))
                selectedImg = img;
        }

        if (selectedImg == null)
        {
            this.errorLabel.Text = "The selected image does not exist in this album!";
            this.errorLabel.Visible = true;
            return;
        }
        else
        {
            UserValidationWS validationWS = new UserValidationWS();
            string newName = this.changeImageNameTB.Text.Trim();
            if (validationWS.isImageNameValid(newName))
            {
                selectedImg.ImageName = newName;
                bool res = false;
                try
                {
                    res = ws.UpdateImage(selectedImg);
                }
                catch
                {

                    this.errorLabel.Text = "Invalid Action!";
                    this.errorLabel.Visible = true;
                    this.imagesGridView.DataBind();
                    this.imagesGridView.SelectedIndex = -1;
                    return;
                }
                if (res)
                {
                    this.imagesGridView.DataBind();
                    this.imagesGridView.SelectedIndex = -1;
                    this.updateDropDownParams(sender, e);
                }
                else
                {
                    this.errorLabel.Text = "Could not update the image name, try again later!";
                    this.errorLabel.Visible = true;
                    this.imagesGridView.SelectedIndex = -1;
                }
            }
            else
            {
                this.errorLabel.Text = validationWS.imageNameLengthInvalidMessage();
                this.errorLabel.Visible = true;
                this.imagesGridView.SelectedIndex = -1;
                return;
            }
        }
    }
    protected void deleteImageBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;

        GigaGalleryWS ws = new GigaGalleryWS();
        if (Session["selectedAlbumId"] == null)
            return;

        Img[] images = ws.GetAlbumImages((int)Session["selectedAlbumId"]);
        if (images == null)
        {
            this.errorLabel.Text = "Album is empty, please insert some images!";
            this.errorLabel.Visible = true;
            return;
        }
        Img selectedImg = null;

        foreach (Img img in images)
        {
            if (img.ImageName == Context.Server.HtmlDecode(this.imagesGridView.SelectedRow.Cells[2].Text))
                selectedImg = img;
        }

        if (selectedImg == null)
        {
            this.errorLabel.Text = "The selected image does not exist in this album!";
            this.errorLabel.Visible = true;
            return;
        }
        else
        {
            if (ws.DeleteImage(selectedImg))
            {
                this.imagesGridView.DataBind();
            }
            else
            {
                this.errorLabel.Text = "Could not delete the selected image, try again later!";
                this.errorLabel.Visible = true;
            }
            string beginPath = Request.PhysicalApplicationPath + "UserImages";
            string imagePath = selectedImg.ImageFilePath;

            // Delete physical file.
            if (System.IO.File.Exists(beginPath + imagePath))
            {
                System.IO.File.Delete(beginPath + imagePath);
                this.imagesGridView.SelectedIndex = -1;
            }
            else
            {
                this.errorLabel.Text = "Could not delete physical file instance!";
                this.errorLabel.Visible = true;
                this.imagesGridView.SelectedIndex = -1;
                this.imagesGridView.DataBind();
                this.updateDropDownParams(sender, e);
                return;
            }
        }
    }
    protected void deleteAlbumBtn_Click(object sender, EventArgs e)
    {
        this.errorLabel.Visible = false;

        GigaGalleryWS ws = new GigaGalleryWS();
        if (Session["selectedAlbumId"] == null)
        {
            // No album was selected.
            this.errorLabel.Text = "No album was selected! Please select an album!";
            this.errorLabel.Visible = true;
            return;
        }
        int selectedAlbumId = (int)Session["selectedAlbumId"];
        Album selectedAlbum = ws.GetAlbumById(selectedAlbumId);
        bool res = false;
        try
        {
            res = ws.DeleteAlbum(selectedAlbum) && ws.DeleteAlbumImages(selectedAlbum);
        }
        catch
        {
            this.errorLabel.Text = "Invalid Action!";
            this.errorLabel.Visible = true;
            this.imagesGridView.DataBind();
            this.imagesGridView.SelectedIndex = -1;
            return;
        }
        if (res)
        {
            this.updateDropDownParams(sender, e);
            this.imagesGridView.DataBind();
            Session["selectedAlbumId"] = null;
        }
        else
        {
            this.errorLabel.Text = "Could not delete the selected album, try again later!";
            this.errorLabel.Visible = true;
        }
        this.imagesGridView.SelectedIndex = -1;
        this.imagesGridView.DataBind();
    }
    protected void imagesGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        GigaGalleryWS ws = new GigaGalleryWS();
        if (Session["selectedAlbumId"] == null)
            return;

        Img[] images = ws.GetAlbumImages((int)Session["selectedAlbumId"]);
        if (images == null)
        {
            this.errorLabel.Text = "Album is empty, please insert some images!";
            this.errorLabel.Visible = true;
            return;
        }
        Img selectedImg = null;

        foreach (Img img in images)
        {
            if (img.ImageName == Context.Server.HtmlDecode(this.imagesGridView.SelectedRow.Cells[2].Text))
                selectedImg = img;
        }

        if (selectedImg == null)
        {
            this.errorLabel.Text = "The selected image does not exist in this album!";
            this.errorLabel.Visible = true;
            return;
        }
        else
        {
            string beginPath = "~\\UserImages\\";
            string imagePath = selectedImg.ImageFilePath;
            this.selectedImagePreview.ImageUrl = beginPath + imagePath;
            this.selectedImagePreview.DataBind();
            this.selectedImagePreview.Visible = true;
            this.updateDropDownParams(sender, e);
        }
    }
    private static string CreateMD5(string input)
    {
        // given, a password in a string

        // byte array representation of that string
        byte[] encodedInput = new System.Text.UTF8Encoding().GetBytes(input);

        // need MD5 to calculate the hash
        byte[] hash = ((System.Security.Cryptography.HashAlgorithm)System.Security.Cryptography.CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedInput);

        // string representation (similar to UNIX format)
        string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        return encoded;
    }
    private static string getHashedFilePath(string begin_path, string owner_name, string album_name, string file_name, string extention)
    {

        return begin_path + "\\" + CreateMD5(owner_name) + "\\" + CreateMD5(album_name) + "\\" + CreateMD5(file_name) + extention;
    }
    private static bool doesUserImageExist(string begin_path, string owner_name, string album_name, string image_name, string file_extention)
    {
        string fullPath = getHashedFilePath(begin_path, owner_name, album_name, image_name, album_name);
        return System.IO.File.Exists(fullPath);
    }
}