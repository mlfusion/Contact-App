using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact.UI
{
    /// <summary>
    /// Summary description for ProfilePhotoHandler
    /// </summary>
    public class ProfilePhotoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                // Set 3 sec timeout
               // System.Threading.Thread.Sleep(3000);

                var count = context.Request.Files.Count;
                var filename = context.Request.Files[0].FileName;
                var filesize = context.Request.Files[0].ContentLength;
                var filetype = context.Request.Files[0].ContentType;
                var fileStream = context.Request.Files[0].InputStream;

                var ext = filename.Split('.')[1];
                var newfilename = "profile_" + DateTime.Now.ToString("MMddyyyyhhmmss.") + ext;

                HttpPostedFile postedFile = context.Request.Files[0];
                var savepath = "";
                var tempPath = "";
                tempPath = System.Configuration.ConfigurationManager.AppSettings["upload-photo"];
                savepath = context.Server.MapPath(tempPath);
                postedFile.SaveAs(savepath + newfilename);

                context.Response.StatusCode = 200;
                context.Response.Write(newfilename);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}