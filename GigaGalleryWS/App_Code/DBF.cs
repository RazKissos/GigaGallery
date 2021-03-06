﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using ConstantsNS;
using UserNS;
using AlbumNS;
using ImgNS;

/// <summary>
/// Summary description for DBF
/// </summary>
namespace DBFNS
{
    public class DBF
    {
        #region Update
        public static bool UpdateUser(User u)
        {
            if (u != null && GetUserById (u.UserId) != null)
            {
                string sql = string.Format("update [users] set user_name = '{0}', user_password = '{1}', user_email = '{2}', user_album_count = '{3}', user_birthday = #{4}#, is_admin = '{5}' where user_id = {6}", u.UserName, u.UserPassword, u.UserEmail, u.UserAlbumCount, u.UserAlbumCount, u.UserBirthday, u.IsAdmin, u.UserId);
                return ChangeTable(sql) == 1;
            }
            return false;
        }
        public static bool UpdateAlbum(Album al)
        {
            if (al != null && GetAlbumById (al.AlbumId) != null)
            {
                string sql = string.Format("update [albums] set album_name = '{0}', album_owner_id = {1}, album_creation_time = #{2}#, album_size = '{3}' where album_id = {4}", al.AlbumName, al.AlbumOwnerId, al.AlbumCreationTime, al.AlbumSize, al.AlbumId);
                return ChangeTable(sql) == 1;
            }
            return false;
        }
        public static bool UpdateImage(Img img)
        {
            if (img != null && GetImageById(img.ImageId) != null)
            {
                string sql = string.Format("update [images] set image_album_id = {0}, image_owner_id = {1}, image_name = '{2}', image_creation_time = #{3}#, image_file_path = '{4}' where image_id = {5}", img.ImageAlbumId, img.ImageOwnerId, img.ImageName, img.ImageCreationTime, img.ImageFilePath, img.ImageId);
                return ChangeTable(sql) == 1;
            }
            return false;
        }
        #endregion
        #region Delete
        public static bool DeleteUser(User u)
        {
            if (u != null && GetUserById(u.UserId) != null)
            {
                string sql = string.Format("delete * from [users] where user_id = {0}", u.UserId);
                return ChangeTable(sql) == 1;
            }
            return false;
        }
        public static bool DeleteAlbum(Album al)
        {
            if (al != null && GetAlbumById(al.AlbumId) != null)
            {
                string sql = string.Format("delete * from [albums] where album_id = {0}", al.AlbumId);
                return ChangeTable(sql) == 1;
            }
            return false;
        }
        public static bool DeleteImage(Img img)
        {
            if (img != null && GetImageById(img.ImageId) != null)
            {
                string sql = string.Format("delete * from [images] where image_id = {0}", img.ImageId);
                return ChangeTable(sql) == 1;
            }
            return false;
        }
        public static bool DeleteImagesFromAlbum(int AlbumId)
        {
            if (GetAlbumById(AlbumId) != null)
            {
                string sql = string.Format("delete * from [images] where image_album_id = {0}", AlbumId);
                return ChangeTable(sql) == 1;
            }
            return false;
        }
        #endregion
        #region Select
        public static User GetUserById(int user_id)
        {
            string sql = string.Format("select * from [users] where user_id={0}", user_id);

            DataRow dr;
            try
            {
                dr = selectFromTable(sql).Rows[0]; 
                return new User(int.Parse(dr["user_id"].ToString()), dr["user_name"].ToString(), dr["user_email"].ToString(), dr["user_password"].ToString(), DateTime.Parse(dr["user_birthday"].ToString()), int.Parse(dr["user_album_count"].ToString()), bool.Parse(dr["is_admin"].ToString()));
            }
            catch { return null; }
        }
        public static User GetUserByEmail(string email)
        {
            string sql = string.Format("select * from [users] where user_email='{0}'", email);

            DataRow dr;
            try
            {
                dr = selectFromTable(sql).Rows[0];
                return new User(int.Parse(dr["user_id"].ToString()), dr["user_name"].ToString(), dr["user_email"].ToString(), dr["user_password"].ToString(), DateTime.Parse(dr["user_birthday"].ToString()), int.Parse(dr["user_album_count"].ToString()), bool.Parse(dr["is_admin"].ToString()));
            }
            catch { return null; }
        }
        public static Album GetAlbumById(int album_id)
        {
            string sql = string.Format("select * from [albums] where album_id={0}", album_id);

            DataRow dr;
            try
            {
                dr = selectFromTable(sql).Rows[0];
                return new Album(int.Parse(dr["album_id"].ToString()), int.Parse(dr["album_owner_id"].ToString()), dr["album_name"].ToString(), DateTime.Parse(dr["album_creation_time"].ToString()), int.Parse(dr["album_size"].ToString()));
            }
            catch { return null; }
        }
        public static Img GetImageById(int image_id)
        {
            string sql = string.Format("select * from [images] where image_id={0}", image_id);

            DataRow dr;
            try
            {
                dr = selectFromTable(sql).Rows[0];
                return new Img(int.Parse(dr["image_id"].ToString()), int.Parse(dr["image_owner_id"].ToString()), int.Parse(dr["image_album_id"].ToString()), dr["image_name"].ToString(), DateTime.Parse(dr["image_creation_time"].ToString()), dr["image_file_path"].ToString());
            }
            catch { return null; }
        }
        public static List<Album> GetUserAlbumsById(int owner_id)
        {
            string sql = string.Format("select * from [albums] where album_owner_id={0}", owner_id);

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
                toRet.Add(new Album(int.Parse(dr["album_id"].ToString()), int.Parse(dr["album_owner_id"].ToString()), dr["album_name"].ToString(), DateTime.Parse(dr["album_creation_time"].ToString()), int.Parse(dr["album_size"].ToString())));
            }

            return toRet;
        }
        public static List<Img> GetAlbumImagesById(int album_id)
        {
            string sql = string.Format("select * from [images] where image_album_id={0}", album_id);

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

            List<Img> toRet = new List<Img>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                toRet.Add(new Img(int.Parse(dr["image_id"].ToString()), int.Parse(dr["image_owner_id"].ToString()), int.Parse(dr["image_album_id"].ToString()), dr["image_name"].ToString(), DateTime.Parse(dr["image_creation_time"].ToString()), dr["image_file_path"].ToString()));
            }

            return toRet;
        }
        public static List<User> GetUsersByName(string user_name)
        {
            string sql = string.Format("select * from [users] where user_name='{0}'", user_name);

            DataTable dt = selectFromTable(sql);
            List<User> listToRet = new List<User>();

            if (dt.Rows.Count == 0)
                return null;

            foreach (DataRow dr in dt.Rows)
            {
                listToRet.Add(new User(int.Parse(dr["user_id"].ToString()), dr["user_name"].ToString(), dr["user_email"].ToString(), dr["user_password"].ToString(), DateTime.Parse(dr["user_birthday"].ToString()), int.Parse(dr["user_album_count"].ToString()), bool.Parse(dr["is_admin"].ToString())));
            }

            return listToRet;
        }
        public static List<Album> GetAlbumsByName(string album_name)
        {
            string sql = string.Format("select * from [albums] where album_name='{0}'", album_name);
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
        public static List<Img> GetImagesByName(string image_name)
        {
            string sql = string.Format("select * from [images] where image_name='{0}'", image_name);
            DataTable dt = selectFromTable(sql);
            List<Img> listToRet = new List<Img>();

            if (dt.Rows.Count == 0)
                return null;

            foreach (DataRow dr in dt.Rows)
            {
                listToRet.Add(new Img(int.Parse(dr["image_id"].ToString()), int.Parse(dr["image_album_id"].ToString()), int.Parse(dr["image_owner_id"].ToString()), dr["image_name"].ToString(), DateTime.Parse(dr["image_creation_time"].ToString()), dr["image_file_path"].ToString()));
            }
            return listToRet;
        }
        public static bool Login(string email, string password)
        {
            User u = GetUserByEmail(email);
            if (u != null)
            {
                if (password == u.UserPassword)
                    return true;
            }
            return false;
        }
        #endregion
        #region Add
        public static bool AddUser(User u)
        {
            User users = GetUserByEmail(u.UserEmail);
            if (users != null)
            {
                throw new Exception("Email is already in use!");
            }

            string sql = string.Format("insert into users (user_name, user_password, user_email, user_birthday, user_album_count, is_admin) values('{0}', '{1}', '{2}', #{3}#, {4} ,{5})", u.UserName, u.UserPassword, u.UserEmail, u.UserBirthday, u.UserAlbumCount, u.IsAdmin);
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
                        // Code will stop if it reaces this becuase we throw exception.
                        throw new Exception("An Album with the same name already belongs to this user!");
                    }
                }
            }

            string sql = string.Format("insert into albums (album_owner_id, album_name, album_creation_time, album_size) values({0}, '{1}', #{2}#, {3})", a.AlbumOwnerId, a.AlbumName, a.AlbumCreationTime, a.AlbumSize);
            return DBF.ChangeTable(sql) == 1;
        }
        public static bool AddImage(Img i)
        {
            // Get all images with the same name and check if their album is identical.
            List<Img> images_to_check = GetImagesByName(i.ImageName);
            if (images_to_check != null)
            {
                foreach (Img img in images_to_check)
                {
                    if (img.ImageAlbumId == i.ImageAlbumId)
                    {
                        // Code will stop if it reaces this becuase we throw exception.
                        throw new Exception("An Image with the same name already exists in this album!");
                    }
                }
            }
            string sql = string.Format("insert into images (image_album_id, image_owner_id, image_name, image_creation_time, image_file_path) values({0}, {1}, '{2}', #{3}#, '{4}')", i.ImageAlbumId, i.ImageOwnerId, i.ImageName, i.ImageCreationTime, i.ImageFilePath);
            return DBF.ChangeTable(sql) == 1;
        }
        #endregion

        #region general operations
        public static string GetConnectionString()
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + HttpContext.Current.Server.MapPath("App_Data/GigaGallery.accdb");
        }
        public static OleDbConnection GenerateConnection()
        {
            // פעולה מקבלת שם קובץ של מסד נתונים ובונה אובייקט התחברות ופותחת ערוץ תקשורת 
            OleDbConnection obj = new OleDbConnection();
            // Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\GigaGallery\GigaGalleryWS\App_Data\GigaGallery.accdb
            obj.ConnectionString = GetConnectionString();
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
            }
            finally
            {
                obj.Close();
            }
            return dt;
        }

        public static DataTable GetTableSchema(string tableName)
        {
            OleDbConnection conobj = GenerateConnection();
            DataTable tables = conobj.GetSchema("Tables");
            DataTable schema = null;
            bool success = false;
            foreach (DataRow tableRow in tables.Rows)
            {
                string name = tableRow["TABLE_NAME"].ToString().ToLower();
                if (!success && tableName.ToLower() == tableRow["TABLE_NAME"].ToString().ToLower())
                {
                    // Table exists!
                    schema = conobj.GetSchema("Columns", new[] { null, null, tableName.ToLower() });
                    conobj.Close();
                    success = true;
                }
                if(success)
                    return schema; 
            }
            conobj.Close();
            return null;
        }
        #endregion
    }
}