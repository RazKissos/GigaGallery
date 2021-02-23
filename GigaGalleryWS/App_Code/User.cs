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

        private string user_birthday;
        public string UserBirthday
        {
            get
            {
                if (this.user_birthday == "")
                    throw new Exception("Data is uninitialized!");
                else
                    return this.user_birthday;
            }
            set
            {
                if (isDateFormatValid(value))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(value, Constants.DATE_FORMAT, null, System.Globalization.DateTimeStyles.None, out dt) == true) // Check if date exists.
                        this.user_birthday = value;
                    else
                        throw new Exception("User Birth Day is a non existant date!");
                }
                else
                    throw new Exception(string.Format("User Birth Day must be in this format: {0}!", Constants.DATE_FORMAT.ToUpper()));
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
        public User(int id, string name, string email, string password, string birthday, int albumCount, bool isAdmin)
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
        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="user">Other User to copy data from</param>
        public User(User user) : this(user.UserId, user.UserName, user.UserEmail, user.UserPassword, user.UserBirthday, user.UserAlbumCount, user.is_admin) { }

        /// <summary>
        /// Function that checks if date is in valid foramt `dateFormat`.
        /// </summary>
        /// <param name="dateStr">The string to check for matching the formay</param>
        /// <returns>boolean true if string matches or false if string is invalid</returns>
        bool isDateFormatValid(string dateStr)
        {
            // Birthday String Template should be dd.mm.yyyy: 
            // -    First period index: 2
            // -    Second period index: 5
            int tempIndex = 0;
            if ((tempIndex = dateStr.IndexOf(".")) != 2)
                return false;
            if ((tempIndex = dateStr.IndexOf(".", tempIndex + 1)) != 5)
                return false;
            if (dateStr.Length != Constants.DATE_FORMAT.Length)
                return false;
            if (!(Char.IsDigit(dateStr[0]) &&
                Char.IsDigit(dateStr[1]) &&
                Char.IsDigit(dateStr[3]) &&
                Char.IsDigit(dateStr[4]) &&
                Char.IsDigit(dateStr[6]) &&
                Char.IsDigit(dateStr[7]) &&
                Char.IsDigit(dateStr[8]) &&
                Char.IsDigit(dateStr[9]))) // Validate that all non periods are numbers.
                return false;

            return true;
        }
        /// <summary>
        /// Wipes all the worker data to default values that are invalid,
        /// in any case of access to uninitialized data there will be an exception thrown.
        /// </summary>
        private void wipe()
        {
            this.user_id = -1;
            this.user_name = "";
            this.user_email = "";
            this.user_password = "";
            this.user_birthday = "";
            this.user_album_count = -1;
            this.is_admin = false;
        }
    }
}