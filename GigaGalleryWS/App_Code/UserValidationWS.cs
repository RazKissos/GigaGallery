using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ConstantsNS;

/// <summary>
/// Summary description for UserValidationWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class UserValidationWS : System.Web.Services.WebService
{
    [WebMethod]
    public bool isPasswordValid(string password)
    {
        return password.Length >= Constants.MIN_PASSWORD_LENGTH && password.Length <= Constants.MAX_PASSWORD_LENGTH;
    }
    [WebMethod]
    public string passwordLengthInvalidMessage()
    {
        return string.Format("Password Length Invalid! needs to be between {0} and {1}!", Constants.MIN_PASSWORD_LENGTH, Constants.MAX_PASSWORD_LENGTH);
    }
    [WebMethod]
    public bool isEmailLengthValid(string email)
    {
        return email.Length <= Constants.MAX_EMAIL_LENGTH;
    }
    [WebMethod]
    public string emailLengthInvalidMessage()
    {
        return string.Format("Email Length invalid! needs to be equal to or lower than {0}!", Constants.MAX_EMAIL_LENGTH);
    }
    [WebMethod]
    public bool isUsernameValid(string username)
    {
        return username.Length >= Constants.MIN_USERNAME_LENGTH && username.Length <= Constants.MAX_USERNAME_LENGTH;
    }

    [WebMethod]
    public string usernameLengthInvalidMessage()
    {
        return string.Format("Username Length invalid! needs to be between {0} and {1}!", Constants.MIN_USERNAME_LENGTH, Constants.MAX_USERNAME_LENGTH);
    }
    [WebMethod]
    public bool isAlbumNameValid(string albumName)
    {
        return albumName.Length >= Constants.MIN_ALBUM_NAME_LENGTH && albumName.Length <= Constants.MAX_ALBUM_NAME_LENGTH;
    }
    [WebMethod]
    public bool isImageNameValid(string imageName)
    {
        return imageName.Length >= Constants.MIN_IMAGE_NAME_LENGTH && imageName.Length <= Constants.MAX_IMAGE_NAME_LENGTH;
    }
    public UserValidationWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
}
