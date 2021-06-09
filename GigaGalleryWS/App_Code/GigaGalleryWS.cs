using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using DBFNS;
using UserNS;
using AlbumNS;
using ImgNS;
using ConstantsNS;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for GigaGalleryWS
/// </summary>
[WebService(Namespace = "http://gigagallery.net/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class GigaGalleryWS : System.Web.Services.WebService
{
    public bool CheckPasswordLength(string password)
    {
        return password.Length >= Constants.MIN_PASSWORD_LENGTH && password.Length <= Constants.MAX_PASSWORD_LENGTH;
    }
    public bool CheckEmailLength(string email)
    {
        return email.Length <= Constants.MAX_EMAIL_LENGTH;
    }
    [WebMethod]
    public bool Login(string email, string password)
    {
        // This function should only be called after we verified the password and email length!
        if (!CheckPasswordLength(password))
        {
            Exception e = new Exception("Password Error");
            e.Data.Add("stringInfo", string.Format("Password Length must be longer than {0} and shorter than {1}!", Constants.MIN_PASSWORD_LENGTH, Constants.MAX_PASSWORD_LENGTH));
            throw e;
        }   
        else if (!CheckEmailLength(email))
        {
            Exception e = new Exception("Email Error");
            e.Data.Add("stringInfo", string.Format("Email Length must be shorter than {0}!", Constants.MAX_EMAIL_LENGTH));
            throw e;
        }

        return DBF.Login(email, password);
    }
    [WebMethod]
    public DataTable GetTableSchema(string tableName)
    {
        DataTable schema = DBF.GetTableSchema(tableName);

        if(schema != null)
            schema.TableName = "dbSchema";

        return schema;
    }
    [WebMethod]
    public DataTable GenerateSelectQueryAndFetchData(string tableName, string paramField, string searchParam, ParamAttrs attrs)
    {
        string paramWithMods = "";

        if (attrs.isBool)
            paramWithMods = searchParam;
        else if (attrs.isDate)
            paramWithMods = string.Format("#{0}#", searchParam);
        else if (attrs.isString)
            paramWithMods = string.Format("\"{0}\"", searchParam);

        string query = string.Format("select * from [{0}] where {1}={2}", tableName, paramField, paramWithMods);
        DataTable res = DBF.selectFromTable(query);
        res.TableName = "selectionResults";
        return res;
    }
    [WebMethod]
    public string GetConnectionString()
    {
        return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + HttpContext.Current.Server.MapPath("App_Data/GigaGallery.accdb");
    }
    
    [WebMethod]
    public bool Signup(string name, string email, string password, DateTime birthday)
    {
        try
        {
            // `id` doesn't matter in the DBF function AddUser because database gives id automatically.
            User u = new User(1, name, email, password, birthday);
            bool dpfRes = DBF.AddUser(u);
        }
        catch
        {
            return false;
        }
        return true;
    }
    [WebMethod]
    public User GetUserObj(string email)
    {
        return DBF.GetUserByEmail(email);
    }
    [WebMethod]
    public bool AdminAddUser(string name, string email, string password, DateTime birthday, bool isadmin)
    {
        try
        {
            // `id` doesn't matter in the DBF function AddUser because database gives id automatically.
            User u = new User(1, name, email, password, birthday, 0, isadmin);
            return DBF.AddUser(u);
        }
        catch (Exception e){ throw e; }
    }
    [WebMethod]
    public bool AddAlbum(int owner_id, string name)
    {
        try
        {
            // Album id doesn't matter since it is not considered in the AddAlbum function in DBF.
            Album al = new Album(1, owner_id, name);
            return DBF.AddAlbum(al);
        }
        catch(Exception e) { throw e; }
    }
    [WebMethod]
    public bool AddImage(int owner_id, int album_id, string name, string image_file_path)
    {
        try
        {
            Img img = new Img(1, owner_id, album_id, name, image_file_path);
            return DBF.AddImage(img);
        }
        catch (Exception e) { throw e; }
    }
    [WebMethod]
    public DataTable SelectEntireTable(string tableName)
    {
        DataTable dt = new DataTable();
        string sql = string.Format("select * from [{0}]", tableName.ToLower());
        dt = DBF.selectFromTable(sql);
        dt.TableName = string.Format("{0}DataTable", tableName);
        return dt;
    }
    [WebMethod]
    public int GetAlbumIdByUserIdAndAlbumName(int ownerId, string albumName)
    {
        List<Album> albumList = DBF.GetAlbumsByName(albumName);
        if (albumList == null)
            return -1;
        foreach(Album al in albumList)
        {
            if (al.AlbumOwnerId == ownerId)
                return al.AlbumId;
        }
        return -1;
    }
    [WebMethod]
    public List<Album> GetUserAlbums(int userId)
    {
        return DBF.GetUserAlbumsById(userId);
    }
    [WebMethod]
    public List<Img> GetAlbumImages(int albumId)
    {
        return DBF.GetAlbumImagesById(albumId);
    }
    [WebMethod]
    public bool UpdateAlbum(int id, int owner_id, string name, DateTime creation_time, int album_size)
    {
        //int id, int owner_id, string name, DateTime creation_time, int album_size
        Album album = new Album(id, owner_id, name, creation_time, album_size);
        return DBF.UpdateAlbum(album);
    }
    [WebMethod]
    public bool UpdateImage(Img updatedImage)
    {
        // int id, int owner_id, int album_id, string name, DateTime creation_time, string file_path // new Img(id, owner_id, album_id, name, creation_time, file_path
        return DBF.UpdateImage(updatedImage);
    }
    [WebMethod]
    public bool DeleteAlbum(Album album)
    {
        return DBF.DeleteAlbum(album);
    }
    [WebMethod]
    public bool DeleteImage(Img image)
    {
        return DBF.DeleteImage(image);
    }
    [WebMethod]
    public bool DeleteAlbumImages(Album album)
    {
        return DBF.DeleteImagesFromAlbum(album.AlbumId);
    }
    [WebMethod]
    public Album GetAlbumById(int albumId)
    {
        return DBF.GetAlbumById(albumId);
    }
    public GigaGalleryWS()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
}