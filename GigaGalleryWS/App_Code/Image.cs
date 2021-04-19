using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConstantsNS;
using UserNS;
using AlbumNS;

/// <summary>
/// Summary description for Image
/// </summary>
namespace ImageNS
{
    public class Image
    {
        private int image_id;
        public int ImageId
        {
            get
            {
                if (this.image_id == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.image_id;
            }
            set
            {
                if (value > 0)
                {
                    this.image_id = value;
                }
                else
                    throw new Exception("Image Id must be positive!");
            }
        }

        private int image_owner_id;
        public int ImageOwnerId
        {
            get
            {
                if (this.image_owner_id == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.image_owner_id;
            }
            set
            {
                if (value > 0)
                {
                    this.image_owner_id = value;
                }
                else
                    throw new Exception("Image Owner Id must be positive!");
            }
        }
        private int image_album_id;
        public int ImageAlbumId
        {
            get
            {
                if (this.image_album_id == -1)
                    throw new Exception("Data is uninitialized!");
                else
                    return this.image_album_id;
            }
            set
            {
                if (value > 0)
                {
                    this.image_album_id = value;
                }
                else
                    throw new Exception("Image Album Id must be positive!");
            }
        }

        private string image_name;
        public string ImageName
        {
            get
            {
                if (this.image_name == "")
                    throw new Exception("Data is uninitialized!");
                else
                    return this.image_name;
            }
            set
            {
                if (value is string)
                {
                    if (value.Length > Constants.MAX_IMAGE_NAME_LENGTH)
                    {
                        if (value.Length <= Constants.MAX_IMAGE_NAME_LENGTH)
                        {
                            this.image_name = value;
                        }
                        else
                            throw new Exception(string.Format("Image Name length cannot exceed the max length of {0}!", Constants.MAX_IMAGE_NAME_LENGTH));
                    }
                    else
                        throw new Exception(string.Format("Image Name length cannot be below minimum required username length({0})!", Constants.MIN_IMAGE_NAME_LENGTH));
                }
                else
                    throw new ArgumentException("Image Name must be a string!");
            }
        }

        private DateTime image_creation_time;
        public DateTime ImageCreationTime
        {
            get 
            { 
                if (this.image_creation_time == default(DateTime))
                    throw new Exception("Data is uninitialized!");
                else
                    return this.image_creation_time; 
            }
            set { this.image_creation_time = value; }
        }

        private string image_file_path;
        public string ImageFilePath
        {
            get
            {
                if (this.image_file_path == "")
                    throw new Exception("Data is uninitialized!");
                else
                    return this.image_file_path;
            }
            set
            {
                if (value is string)
                {
                    if (value.Length > 0)
                    {
                        if (value.Length <= Constants.MAX_IMAGE_FILE_PATH_LENGTH)
                        {
                            this.image_file_path = value;
                        }
                        else
                            throw new Exception(string.Format("Image File Path length cannot exceed the max length of {0}!", Constants.MAX_IMAGE_FILE_PATH_LENGTH));
                    }
                    else
                        throw new Exception(string.Format("Image File Path length cannot be empty!"));
                }
                else
                    throw new ArgumentException("Image File Path must be a string!");
            }
        }

        // Default Constructor.
        public Image()
        {
            this.wipe();
        }
        // Constructor that sets all necessary values.
        public Image(int id, int owner_id, int album_id, string name, DateTime creation_time, string file_path)
        {
            this.wipe();
            try
            {
                this.ImageId = id;
                this.ImageName = name;
                this.ImageOwnerId = owner_id;
                this.ImageAlbumId = album_id;
                this.ImageCreationTime = creation_time;
                this.ImageFilePath = file_path;
            }
            catch (Exception e)
            {
                this.wipe();
                throw e;
            }
        }
        // Constructor that sets the creation time to be DateTime.Now
        public Image(int id, int owner, int album, string name, string file_path) : this(id, owner, album, name, DateTime.Now, file_path) { }
        // Copy Constructor.
        public Image(Image other) : this(other.ImageId, other.ImageOwnerId, other.ImageAlbumId, other.ImageName, other.ImageCreationTime, other.ImageFilePath) { }
        
        /// <summary>
        /// This function resets all the values of the class to known default values.
        /// </summary>
        private void wipe()
        {
            this.image_id = -1;
            this.image_name = "";
            this.image_file_path = "";
            this.image_album_id = -1;
            this.image_owner_id= -1;
            this.image_creation_time = default(DateTime);
        }
    }
}