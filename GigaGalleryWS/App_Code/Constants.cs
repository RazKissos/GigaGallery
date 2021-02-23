using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Constants
/// </summary>
namespace ConstantsNS
{
    public static class Constants
    {
        #region PASSWORD DATA
        public const int MIN_PASSWORD_LENGTH = 8;
        public const int MAX_PASSWORD_LENGTH = 25;
        #endregion
        #region USERNAME DATA
        public const int MIN_USERNAME_LENGTH = 6;
        public const int MAX_USERNAME_LENGTH = 25;
        #endregion
        #region EMAIL DATA
        public const int MAX_EMAIL_LENGTH = 50;
        #endregion
        #region DATE DATA
        public const string DATE_FORMAT = "dd.MM.yyyy";
        #endregion
        #region ALBUM DATA
        public const int MIN_ALBUM_NAME_LENGTH = 6;
        public const int MAX_ALBUM_NAME_LENGTH = 40;
        #endregion
        #region IMAGE DATA
        public const int MIN_IMAGE_NAME_LENGTH = 6;
        public const int MAX_IMAGE_NAME_LENGTH = 30;
        // We will hash the username, albumname and imagename to imrove security, we will use sha256 which will output 256 bits or 32 bytes.
        public const int MAX_IMAGE_FILE_PATH_LENGTH = (32 * 3) + 10; // 96 + 10 = 106 (Adding 10 characters of overhead just in case).
        #endregion
    }
}