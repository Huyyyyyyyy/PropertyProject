using PropertyProject.Models.mapAvatar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        //update avatar of user
        public ActionResult updateAvatar(String email, HttpPostedFileBase fileImage)
        {
            if(fileImage == null)
            {
                ViewBag.error = "You need to choose a file";
                return View();
            }

            if (fileImage.ContentLength == 0)
            {
                ViewBag.error = "You need to choose a file";
                return View();  
            }

            var relativeUrl = "/Image/avatar/";
            var AbsoluteUrl = Server.MapPath(relativeUrl);


            string fullUrl = AbsoluteUrl + fileImage.FileName;
            int i = 1;
            while (System.IO.File.Exists(fullUrl) == true)
            {
                string name = Path.GetFileNameWithoutExtension(fileImage.FileName);
                string tail = Path.GetExtension(fileImage.FileName);
                string newFileName = AbsoluteUrl + name + "_" + i + tail;
                i++;
            }

            fileImage.SaveAs(fullUrl);


            mapAvatar map = new mapAvatar();
            map.updateAvatarForUser(email, relativeUrl + Path.GetFileName(fullUrl));


            return View();
        }
    }
}