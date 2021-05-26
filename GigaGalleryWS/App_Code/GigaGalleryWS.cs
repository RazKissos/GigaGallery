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
    [WebMethod ]
    public DataTable SelectEntireTable(string tableName)
    {
        DataTable dt = new DataTable();
        string sql = string.Format("select * from [{0}]", tableName.ToLower());
        dt = DBF.selectFromTable(sql);
        dt.TableName = string.Format("{0}DataTable", tableName);
        return dt;
    }
    public GigaGalleryWS()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
}