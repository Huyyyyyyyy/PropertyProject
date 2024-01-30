using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyProject.Models.mapAvatar
{
    public class mapAvatar
    {
        //update avatar for user 
        public bool updateAvatarForUser(String email, String imageUrl)
        {
            try
            {
                PropertyProjectEntities db = new PropertyProjectEntities();
                var customer = db.Customers.Find(email);
                customer.avatarCus = imageUrl;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }  
        }
    }
}