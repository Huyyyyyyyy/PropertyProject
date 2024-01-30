using CaptchaMvc.HtmlHelpers;
using PropertyProject.Models;
using PropertyProject.Models.mapAccount;
using PropertyProject.Models.mapContactEmail;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace PropertyProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }



        // check email exist on database
        public JsonResult checkEmailValid(String email)
        {

            PropertyProjectEntities db = new PropertyProjectEntities();
            bool isEmailValid = true;
            string error = "";

            try
            {
                if (db.Accounts.Find(email) != null)
                {
                    isEmailValid = false;

                    Account tempAccount = new Account();

                    tempAccount.email = email;

                    Session["tempAccount"] = tempAccount;
                }
            }catch (Exception ex)
            {
                error = ex.Message;

            }



            return Json(new { result = isEmailValid , error = error }, JsonRequestBehavior.AllowGet);

        }





        //check account exist and match password on datasbase
        public JsonResult checkAccountValid(String email, String password)
        {
            bool isValid = false;
            string error = "";
            try
            {
                PropertyProjectEntities db = new PropertyProjectEntities();

                Account account = new Account();

                account = db.Accounts.FirstOrDefault(e => e.email == email);
                if (account != null)
                {
                    if (password == account.password)
                    {
                        isValid = true;
                    }

                }
            }catch (Exception ex) {
                error =ex.Message;
            }


            return Json(new { result = isValid , error =error}, JsonRequestBehavior.AllowGet);
        }





        // send confirm code to email 

        public JsonResult sendConfirmCodeToEmail(String receivedEmail) {
            bool isSent = false;
            string otp = "";
            if (!receivedEmail.IsEmpty()) { 
                mapContactEmail mapContact = new mapContactEmail();
                otp = mapContact.GenerateOTP();

                mapContact.SendEmail(receivedEmail,
                    "confirm your email to complete register",
                    "<div style=\"font-family: helvetica,arial,sans-serif;min-width:1000px;overflow:auto;line-height:2\">\r\n  <div style=\"margin:50px auto;width:70%;padding:20px 0\">\r\n    <div style=\"border-bottom:1px solid #eee\">\r\n      <a href=\"\" style=\"font-size:1.4em;color: #eeab5a;text-decoration:none;font-weight:600\">nhà nhanh</a>\r\n    </div>\r\n    <p style=\"font-size:1.1em\">xin chào,</p>\r\n    <p>cảm ơn bạn đã đăng ký tài khoản, vui lòng nhập mã xác nhận để hoàn tất quy trình đăng ký</p>\r\n    <h2 style=\"background: #eeab5a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;\">" + otp + "</h2>\r\n    <p style=\"font-size:0.9em;\">regards,<br />nha nhanh</p>\r\n    <hr style=\"border:none;border-top:1px solid #eee\" />\r\n    <div style=\"float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300\">\r\n      <p>nhanhanh</p>\r\n      <p>tòa nhà 45 tầng, vinhomes grand park, thủ đức, hồ chí minh</p>\r\n      <p>vietnam</p>\r\n    </div>\r\n  </div> ");

                isSent = true;

                Session["otp"] = otp;
                receivedEmail = mapContact.MaskEmail(receivedEmail);
            }

            return Json(new {result = isSent, otp = otp, email = receivedEmail }, JsonRequestBehavior.AllowGet);
        }




        //complete register
        [HttpPost]
        public ActionResult completeRegister(string email, string password) {
            string status = "Captcha or password not match !";

            try
            {
                if (this.IsCaptchaValid("Captcha is not valid") && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    using (PropertyProjectEntities db = new PropertyProjectEntities())
                    {
                        // Kiểm tra xem email đã tồn tại chưa
                        var check = db.Accounts.FirstOrDefault(e => e.email == email);

                        if (check == null)
                        {


                            // Bắt đầu một giao dịch
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    db.Configuration.ValidateOnSaveEnabled = false;
                                    // Tạo tài khoản mới
                                    Account account = new Account();
                                    account.email = email;
                                    account.password = password;
                                    account.role = "customer";
                                    db.Accounts.Add(account);
                                    db.SaveChanges();

                                    // Tạo thông tin khách hàng mới
                                    Customer customer = new Customer();
                                    customer.idCustomer = new Models.mapCreateId.createIdCustomer().createIdForCustomer();
                                    customer.emailCus = account.email;
                                    customer.introduceCode = new Models.mapCreateId.createIdCustomer().createIntroduceIdForCustomer();
                                    customer.phoneCus = null;
                                    customer.birthCus = null;
                                    customer.locationCus = null;
                                    customer.availableProperty = 0;
                                    customer.expiredProperty = 0;
                                    customer.pendingProperty = 0;
                                    customer.balance = 0;
                                    customer.avatarCus = null;
                                    customer.nameCus = null;
                                    customer.genderCus = null;

                                    db.Customers.Add(customer);
                                    db.SaveChanges();

                                    // Commit giao dịch nếu mọi thứ đều thành công
                                    transaction.Commit();

                                    // Set Session và trả về trang chính
                                    var find = new mapAccount().findAccountWithEmail(account.email);
                                    Session["user"] = find;
                                    status = "Welcome, " + email;

                                    return RedirectToAction("HomePage", "Home");
                                }
                                catch (DbUpdateException ex)
                                {
                                    // Nếu có lỗi, hủy giao dịch
                                    status = status + ex.Message;
                                    var innerException = ex.InnerException;

                                    while (innerException != null)
                                    {
                                        status = status + innerException.Message;
                                        innerException = innerException.InnerException;
                                    }

                                    transaction.Rollback();

                                }
                                finally
                                {
                                    db.Configuration.ValidateOnSaveEnabled = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = status + ex.Message;
            }

            Session["status"] = status;
            return RedirectToAction("HomePage", "Home");
        }





        //login account
        [HttpPost]
        public ActionResult loginAccount(String email, String password)
        {
            // kiem tra ten dang nhap & mat khau null >> thong bao thieu thong tin 
            if (string.IsNullOrEmpty(email) == true | string.IsNullOrEmpty(password) == true)
            {
                return RedirectToAction("HomePage", "Home");

            }

            // tim tai khoan theo ten dang nhap trong database
            var find = new mapAccount().findAccountWithEmail(email);

            // kiem tra ton tai tai khoan  if null >> return form dang nhap 
            if (find == null)
            {
                return RedirectToAction("HomePage", "Home");

            }
            // if exist >> kiem tra mat khau if flase >> tro ve trang dang nhap 
            if (find.password != password)
            {
                return RedirectToAction("HomePage", "Home");

            }
            // kiem tra phan quyen admin hay user
            if (find.role == "admin")
            {
                // if true >> save section server 
                Session["user"] = find;
                // chuyen huong sang trang chu admin
                return RedirectToAction("HomeAdmin", "Admin");
            }
            else
            {
                // if true >> save section server 
                Session["user"] = find;
                return RedirectToAction("HomePage","Home");
            }
        }




        // log out account 
        public ActionResult LogoutAccount()
        {
            Session.Clear();//remove session
            return RedirectToAction("HomePage", "Home");

        }
    }
}