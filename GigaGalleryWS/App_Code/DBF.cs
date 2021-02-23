using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using ConstantsNS;
using UserNS;
using AlbumNS;
using ImageNS;

/// <summary>
/// Summary description for DBF
/// </summary>
namespace DBF
{
    public class DBF
    {
        #region object functions
        public User GetUserById(int user_id)
        {
            string sql = string.Format("select * from users where user_id='{0}' ", user_id);

            DataRow dr;
            try
            {
                dr = selectFromTable(sql).Rows[0];
            }
            catch
            {
                return null;
            }

            return new User(int.Parse(dr["user_id"].ToString()), dr["user_name"].ToString(), dr["user_email"].ToString(), dr["user_password"].ToString(), dr["user_birthday"].ToString(), int.Parse(dr["user_album_count"].ToString()), bool.Parse(dr["is_admin"].ToString()));
        }
        public static List<Album> GetUserAlbumsById(int owner_id)
        {
            string sql = string.Format("select * from albums where album_owner_id='{0}' ", owner_id);

            DataTable dt;
            try
            {
                dt = selectFromTable(sql);
            }
            catch
            {
                return null;
            }

            if (dt.Rows.Count == 0)
                return null;

            List<Album> toRet = new List<Album>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                toRet.Add(new Album(int.Parse(dr["album_id"].ToString()), int.Parse(dr["album_owner_id"].ToString()), dr["album_name"].ToString(), DateTime.Parse(dr["album_creation_time"].ToString())));
            }

            return toRet;
        }
        public static List<Image> GetAlbumImagesById(int album_id)
        {
            string sql = string.Format("select * from images where album_id='{0}' ", album_id);

            DataTable dt;
            try
            {
                dt = selectFromTable(sql);
            }
            catch
            {
                return null;
            }

            if (dt.Rows.Count == 0)
                return null;

            List<Image> toRet = new List<Image>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                toRet.Add(new Image(int.Parse(dr["image_id"].ToString()), int.Parse(dr["owner_id"].ToString()), int.Parse(dr["album_id"].ToString()), dr["image_name"].ToString(), DateTime.Parse(dr["image_creation_time"].ToString()), dr["image_file"].ToString()));
            }

            return toRet;
        }
        public static List<User> GetUsersByName(string user_name)
        {
            string sql = string.Format("select * from users where user_name='{0}' ", user_name);

            DataTable dt = selectFromTable(sql);
            List<User> listToRet = new List<User>();

            if (dt.Rows.Count == 0)
                return null;

            foreach (DataRow dr in dt.Rows)
            {
                listToRet.Add(new User(int.Parse(dr["user_id"].ToString()), dr["user_name"].ToString(), dr["user_email"].ToString(), dr["user_password"].ToString(), dr["user_birthday"].ToString(), int.Parse(dr["user_album_count"].ToString()), bool.Parse(dr["is_admin"].ToString())));
            }

            return listToRet;
        }
        public static List<Album> GetAlbumsByName(string album_name)
        {
            string sql = string.Format("select * from albums where album_name='{0}' ", album_name);
            DataTable dt = selectFromTable(sql);
            List<Album> listToRet = new List<Album>();

            if (dt.Rows.Count == 0)
                return null;

            foreach (DataRow dr in dt.Rows)
            {
                listToRet.Add(new Album(int.Parse(dr["album_id"].ToString()), int.Parse(dr["album_owner_id"].ToString()), dr["album_name"].ToString(), DateTime.Parse(dr["album_creation_time"].ToString()), int.Parse(dr["album_size"].ToString())));
            }

            return listToRet;
        }
        public static List<Image> GetImagesByName(string image_name)
        {
            string sql = string.Format("select * from images where image_name='{0}' ", image_name);
            DataTable dt = selectFromTable(sql);
            List<Image> listToRet = new List<Image>();

            if (dt.Rows.Count == 0)
                return null;

            foreach (DataRow dr in dt.Rows)
            {
                listToRet.Add(new Image(int.Parse(dr["image_id"].ToString()), int.Parse(dr["image_a;bum_id"].ToString()), int.Parse(dr["image_owner_id"].ToString()), dr["image_name"].ToString(), DateTime.Parse(dr["image_creation_time"].ToString()), dr["image_file_path"].ToString()));
            }
            return listToRet;
        }
        public static bool AddUser(User u)
        {
            if (GetUsersByName(u.UserName) != null)
                // Code will stop if it reaces this if becuase we throw exception.
                throw new Exception("Username is already in use!");

            string sql = string.Format("insert into users (user_name, user_password, user_email, user_birthday, user_album_count, is_admin) values('{0}', '{1}', '{2}', #{3}#, '{4}' ,'{5}')", u.UserName, u.UserPassword, u.UserEmail, u.UserBirthday, u.UserAlbumCount, u.IsAdmin);
            return DBF.ChangeTable(sql) == 1;
        }
        public static bool AddAlbum(Album a)
        {
            // Get all albums with the same name and check if their owner is identical.
            List<Album> albums_to_check = GetAlbumsByName(a.AlbumName);
            if (albums_to_check != null)
            {
                foreach (Album al in albums_to_check)
                {
                    if (al.AlbumOwnerId == a.AlbumOwnerId)
                    {
                        // Code will stop if it reaces this if becuase we throw exception.
                        throw new Exception("Album Name is already used by this user!");
                    }
                }
            }

            string sql = string.Format("insert into albums (album_owner_id, album_name, album_creation_time, album_size) values('{0}', '{1}', #{2}#, '{3}')", a.AlbumOwnerId, a.AlbumName, a.AlbumCreationTime, a.AlbumSize);
            return DBF.ChangeTable(sql) == 1;
        }
        public static bool AddImage(Image i)
        {
            // Get all images with the same name and check if their album is identical.
            List<Image> images_to_check = GetImagesByName(i.ImageName);
            if (images_to_check != null)
            {
                foreach (Image img in images_to_check)
                {
                    if (img.ImageAlbumId == i.ImageAlbumId)
                    {
                        // Code will stop if it reaces this if becuase we throw exception.
                        throw new Exception("Image Name is already in use in this album!");
                    }
                }
            }
            string sql = string.Format("insert into images (image_album_id, image_owner_id, image_name, image_creation_time, image_file_path) values('{0}', '{1}', '{2}', #{3}#, '{4}')", i.ImageAlbumId, i.ImageOwnerId, i.ImageName, i.ImageCreationTime, i.ImageFilePath);
            return DBF.ChangeTable(sql) == 1;
        }
        #endregion

        #region general operations
        public static OleDbConnection GenerateConnection()
        {
            // פעולה מקבלת שם קובץ של מסד נתונים ובונה אובייקט התחברות ופותחת ערוץ תקשורת 
            OleDbConnection obj = new OleDbConnection();
            // Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\GigaGallery\GigaGalleryWS\App_Data\GigaGallery.accdb
            obj.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + HttpContext.Current.Server.MapPath("App_Data/GigaGallery.accdb");
            obj.Open();
            return obj;
        }
        public static int ChangeTable(string sqlstring)
        {
            OleDbConnection conobj = GenerateConnection();
            OleDbCommand com = new OleDbCommand(sqlstring, conobj);
            int result = 0;
            try
            {
                result = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conobj.Close();
            }
            return result;
        }
        public static DataTable selectFromTable(string sqlstring)
        {
            OleDbConnection obj = GenerateConnection();
            OleDbDataAdapter da = new OleDbDataAdapter(sqlstring, obj);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                obj.Close();
            }
            return dt;
        }
        #endregion

    }
}