using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using DBFNS;
using UserNS;
using ImageNS;
using AlbumNS;

/// <summary>
/// Summary description for GigaGalleryWS
/// </summary>
[WebService(Namespace = "http://gigagallery.net/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GigaGalleryWS : System.Web.Services.WebService
{
    [WebMethod]
    public bool Login(string email, string password)
    {
        return DBF.Login(email, password);
    }
    [WebMethod]
    public bool Signup(string name, string password, string email, DateTime birthday)
    {
        try
        {
            // id doesn't matter in the DBF function AddUser because database gives id automatically.
            User u = new User(1, name, password, email, birthday);
            return DBF.AddUser(u);
        }
        catch (Exception e) { throw e; }
    }
    [WebMethod]
    public bool AdminAddUser(string name, string password, string email, DateTime birthday, bool isadmin)
    {
        try
        {
            // id doesn't matter in the DBF function AddUser because database gives id automatically.
            User u = new User(1, name, password, email, birthday, 0, isadmin);
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
    public bool AddImage(int owner_id, int album_id, string name, byte[] bytes)
    {
        try
        {
            // TODO: Compute file path and save bytes as image.
            // Image id doesn't matter since it is not considered in the AddImage function in DBF.
            //string file_path = ""
            //Image img = new Image(1, owner_id, album_id, name);
            //return DBF.AddAlbum(al);
            return false;
        }
        catch (Exception e) { throw e; }
    }
    public GigaGalleryWS()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
}