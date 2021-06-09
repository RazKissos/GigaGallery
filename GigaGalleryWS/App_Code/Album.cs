using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConstantsNS;
using UserNS;
using ImgNS;

/// <summary>
/// Summary description for Album
/// </summary>
namespace AlbumNS
{
    public class Album
    {
        private int album_id;
        public int AlbumId {
            get
            {
                if (this.album_id == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.album_id;
            }
            set 
            {
                if (value > 0)
                {
                    this.album_id = value;
                }
                else
                    throw new Exception("Album Id must be positive!");
            }
        }

        private int album_owner_id;
        public int AlbumOwnerId {
            get
            {
                if (this.album_owner_id == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.album_owner_id;
            }
            set
            {
                if (value > 0)
                {
                    this.album_owner_id = value;
                }
                else
                    throw new Exception("Album Owner Id must be positive!");
            }
        }

        private string album_name;
        public string AlbumName 
        {
            get
            {
                if (this.album_name == "")
                    throw new Exception("Data is uninitialized!");
                else
                    return this.album_name;
            }
            set
            {
                if (value is string)
                {
                    if (value.Length > Constants.MIN_ALBUM_NAME_LENGTH)
                    {
                        if (value.Length <= Constants.MAX_ALBUM_NAME_LENGTH)
                        {
                            this.album_name = value;
                        }
                        else
                            throw new Exception(string.Format("Album Name length cannot exceed the max length of {0}!", Constants.MAX_ALBUM_NAME_LENGTH));
                    }
                    else
                        throw new Exception(string.Format("Albu, Name length cannot be below minimum required username length({0})!", Constants.MIN_ALBUM_NAME_LENGTH));
                }
                else
                    throw new ArgumentException("Album Name must be a string!");
            }
        }

        private DateTime album_creation_time;
        public DateTime AlbumCreationTime 
        {
            get
            {
                if (this.album_creation_time == default(DateTime))
                    throw new Exception("Data is uninitialized!");
                else
                    return this.album_creation_time;
            }
            set { this.album_creation_time = value; }
        }
        private int album_size = 0;
        public int AlbumSize {
            get 
            {
                if (this.album_size == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.album_size;
            }
            set
            {
                if (value >= 0)
                {
                    this.album_size = value;
                }
                else
                    throw new Exception("Album Size must be positive or zero!");
            }
        }

        // Default Constructor.
        public Album()
        {
            this.wipe();
        }
        // Constructor that sets all necessary values.
        public Album(int id, int owner_id, string name, DateTime creation_time, int album_size)
        {
            this.wipe();
            try
            {
                this.AlbumId = id;
                this.AlbumName = name;
                this.AlbumOwnerId = owner_id;
                this.AlbumCreationTime = creation_time;
                this.AlbumSize = album_size;
            }
            catch (Exception e)
            {
                this.wipe();
                throw e;
            }
        }
        // Constructor that sets the creation time to be DateTime.Now
        public Album(int id, int owner_id, string name) : this(id, owner_id, name, DateTime.Now, 0) { }
        // Copy Constructor.
        public Album(Album other) : this(other.AlbumId, other.AlbumOwnerId, other.AlbumName, other.AlbumCreationTime, other.AlbumSize) { }
        
        /// <summary>
        /// This function resets all the values of the class to known default values.
        /// </summary>
        private void wipe()
        {
            this.album_id = -1;
            this.album_size = -1;
            this.album_name = "";
            this.album_owner_id = -1;
            this.album_creation_time = default(DateTime);
        }
    }
}