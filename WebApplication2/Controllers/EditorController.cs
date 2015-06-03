//
// EditController.cs in Project/Controllers
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class EditorController : Controller
    {
        public string test()
        {
            return "ready";
        }
        [HttpPost]
        public string ConvertImageToBase64()
        {
            string base64String = "";
            HttpResponseMessage response = new HttpResponseMessage();

            if (HttpContext.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Request.Files["file"];

                if (httpPostedFile != null)
                {
                    // Validate the uploaded image(optional)

                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(httpPostedFile.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(httpPostedFile.ContentLength);
                        base64String = Convert.ToBase64String(fileData);
                    }
                }
            }

            //response.Content = new StringContent(base64String);
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return base64String;
        }
    }
}