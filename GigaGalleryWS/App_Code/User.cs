using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConstantsNS;
using AlbumNS;
/// <summary>
/// Summary description for User
/// </summary
namespace UserNS
{
    public class User
    {
        private int user_id;
        public int UserId
        {
            get
            {
                if (this.user_id == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.user_id;
            }
            set
            {
                if (value > 0)
                {
                    this.user_id = value;
                }
                else
                    throw new Exception("User Id must be positive!");
            }
        }

        private string user_name;
        public string UserName
        {
            get
            {
                if (this.user_name == "")
                    throw new Exception("Data is uninitialized!");
                else
                    return this.user_name;
            }
            set
            {
                if (value is string)
                {
                    if (value.Length > Constants.MIN_USERNAME_LENGTH)
                    {
                        if (value.Length <= Constants.MAX_USERNAME_LENGTH)
                        {
                            this.user_name = value;
                        }
                        else
                            throw new Exception(string.Format("Username length cannot exceed the max length of {0}!", Constants.MAX_USERNAME_LENGTH));
                    }
                    else
                        throw new Exception(string.Format("Username length cannot be below minimum required username length({0})!", Constants.MIN_USERNAME_LENGTH));
                }
                else
                    throw new ArgumentException("User Name must be a string!");
            }
        }

        private string user_email;
        public string UserEmail
        {
            get
            {
                if (this.user_email == "")
                    throw new Exception("Data is uninitialized!");
                else
                    return this.user_email;
            }
            set
            {
                if (value is string)
                    if (value.Length > 0)
                        if (value.Length <= Constants.MAX_EMAIL_LENGTH)
                            if (value.IndexOf("@") != -1)
                                this.user_email = value;
                            else
                                throw new Exception("Email string must contain a `@` sign!");
                        else
                            throw new Exception(string.Format("Email length cannot exceed the max length of {0}!", Constants.MAX_EMAIL_LENGTH));
                    else
                        throw new Exception("User Name cannot be empty string!");
                else
                    throw new ArgumentException("User Name must be a string!");
            }
        }

        private string user_password;
        public string UserPassword
        {
            get
            {
                if (this.user_password == "")
                    throw new Exception("Data is uninitialized!");
                else
                    return this.user_password;
            }
            set
            {
                if (value is string)
                {
                    if (value.Length >= Constants.MIN_PASSWORD_LENGTH)
                    {
                        if (value.Length <= Constants.MAX_PASSWORD_LENGTH)
                            this.user_email = value;
                        else
                            throw new Exception(string.Format("User Password cannot exceed the max password length of {0}!", Constants.MAX_PASSWORD_LENGTH));
                    }
                    else
                        throw new Exception(string.Format("User Password length cannot be below minimum required password length ({0})!", Constants.MIN_PASSWORD_LENGTH));
                }
                else
                    throw new ArgumentException("User Name must be a string!");
            }
        }
        private DateTime user_birthday;
        public DateTime UserBirthday
        {
            get
            {
                if (this.user_birthday == default(DateTime))
                    throw new Exception("Data is uninitialized!");
                else
                    return this.user_birthday;
            }
            set
            {
                if (value.Year >= 1900)
                    this.user_birthday = value;
                else
                    throw new Exception("User Birth Year is not valid!");
            }
        }

        private int user_album_count;
        public int UserAlbumCount
        {
            get
            {
                if (this.user_album_count == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.user_album_count;
            }
            set
            {
                if (value >= 0)
                {
                    this.user_album_count = value;
                }
                else
                    throw new Exception("User Album Count must be positive or zero!");
            }
        }
        protected bool is_admin = false;
        public bool IsAdmin { get { return this.is_admin; } }
        public User()
        {
            this.wipe();
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="name">user name</param>
        /// <param name="email">user email (must contain a `@`)</param>
        /// <param name="password">user password (must be minimum `minSizeOfPass`)</param>
        /// <param name="birthday">user birthday (in format `dateFormat`)</param>
        /// <param name="albumCount">the amounts of albums the user has.</param>
        /// <param name="isAdmin">is the user an admin.</param>
        public User(int id, string name, string email, string password, DateTime birthday, int albumCount, bool isAdmin)
        {
            this.wipe();
            try
            {
                this.UserId = id;
                this.UserName = name;
                this.UserEmail = email;
                this.UserPassword = password;
                this.UserBirthday = birthday;
                this.UserAlbumCount = albumCount;
                this.is_admin = isAdmin;
            }
            catch (Exception e)
            {
                this.wipe();
                throw e;
            }
        }
        public User(int id, string name, string email, string password, DateTime birthday) : this(id, name, email, password, birthday, 0, false) { }
        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="user">Other User to copy data from</param>
        /// 
        public User(User user) : this(user.UserId, user.UserName, user.UserEmail, user.UserPassword, user.UserBirthday, user.UserAlbumCount, user.is_admin) { }
        
        /// <summary>
        /// This function resets all the values of the class to known default values.
        /// </summary>
        private void wipe()
        {
            this.user_id = -1;
            this.user_name = "";
            this.user_email = "";
            this.user_password = "";
            this.user_birthday = default(DateTime);
            this.user_album_count = -1;
            this.is_admin = false;
        }
    }
}