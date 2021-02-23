using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DBF;
using ConstantsNS;
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
    public GigaGalleryWS()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
}
