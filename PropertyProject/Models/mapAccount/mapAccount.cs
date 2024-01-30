using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropertyProject.Models.mapAccount
{
    public class mapAccount
    {
        PropertyProjectEntities db = new PropertyProjectEntities();


        //find single account with email
        public Account findAccountWithEmail(String email)
        {
            try
            {
                PropertyProjectEntities db = new PropertyProjectEntities();
                var model = db.Accounts.SingleOrDefault(m => m.email == email.ToLower());
                return model;
            }
            catch
            {
                return null;
            }
        }




        //update password for single account
        public bool updateAccount(Account model)
        {
            var updateModel = db.Accounts.Find(model.email);

            //check null
            if (updateModel == null)
            {
                return false;
            }

            updateModel.email = model.email;
            updateModel.password = model.password;
            updateModel.role = model.role;

            db.SaveChanges();
            return true;
        }





        //delete single account from admin side
        public bool deleteAccountOnlyForAdmin(String email)
        {

            var accountAdmin = db.Accounts.Find(email);
            var detailAdmin = db.Admins.Find(email);

            if (accountAdmin != null)
            {
                if(detailAdmin != null)
                {
                    db.Admins.Remove(detailAdmin);
                    db.Accounts.Remove(accountAdmin);
                }

                db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}