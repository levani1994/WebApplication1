using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Account;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseController
    {
       
        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult PasswordRecover()
        {
            return View();
        }

        public ActionResult Succes(string email)
        {
            ViewBag.message = "რეგისტრაცია დასრულდა"+ email;
            return View();
        }

        public ActionResult RecoverSucces()
        {
            ViewBag.message = "პაროლი წარმატებით შეიცვალა";
            return View();
        }

        public ActionResult Confirmation(string id)
        {
            var NotConfirmedUser = db.NotConfirmedUsers.FirstOrDefault(x => x.ConfirmationCode == id);
            db.Users.InsertOnSubmit(
            new User()
            {
                Name = NotConfirmedUser.Name,
                Surname = NotConfirmedUser.Surname,
                Email=NotConfirmedUser.Email,
                Password=NotConfirmedUser.Password,
                RequestIp = NotConfirmedUser.RequestIp,
                CreateDate = DateTime.Now,
            });

            db.NotConfirmedUsers.DeleteAllOnSubmit(db.NotConfirmedUsers.Where(x => x.Email == NotConfirmedUser.Email));
            db.SubmitChanges();

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            if (String.IsNullOrEmpty(registrationViewModel.Name) || String.IsNullOrEmpty(registrationViewModel.SurName) || String.IsNullOrEmpty(registrationViewModel.Email) || String.IsNullOrEmpty(registrationViewModel.Password) || String.IsNullOrEmpty(registrationViewModel.RepeatPassword))
            {
                ViewBag.error = "საჭიროა ყველა ველის შევსება";
                return View();
            }


            if (registrationViewModel.Password != registrationViewModel.RepeatPassword)
            {
                ViewBag.error = "პაროლები არ ემთხვევა";
                return View();
            }

            var NotConfirmedUser = new NotConfirmedUser()
            {
                Name = registrationViewModel.Name,
                Surname = registrationViewModel.SurName,
                Email = registrationViewModel.Email,
                Password = Helper.ComputeSha256Hash(registrationViewModel.Password + Helper.AuthKey),
                CreateDate = DateTime.Now,
                ConfirmationCode = Helper.RandomString(),
                RequestIp = Request.UserHostAddress,
            };
            db.NotConfirmedUsers.InsertOnSubmit(NotConfirmedUser);
            db.SubmitChanges();

            return RedirectToAction("succes", new { email = NotConfirmedUser.Email });

        }

     
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)

        {
            if (String.IsNullOrEmpty(loginViewModel.Email) || String.IsNullOrEmpty(loginViewModel.Password))
            {
                ViewBag.error = "შეავსეთ ყველა ველი";
                return View();
            }
            string password = Helper.ComputeSha256Hash(loginViewModel.Password + Helper.AuthKey);
            var User = db.Users.FirstOrDefault(x => x.Email == loginViewModel.Email && x.Password == password);
            if (User == null)
            {
                ViewBag.error = "ასეთი მომხმარებელი არ არსებობს";
                return View();
            }
            else
            {



                Session["user"] = User;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


        [HttpPost] 
        public ActionResult PasswordRecover(Recover recover)
        {
            
            var rec = db.Users.FirstOrDefault(x => x.Email == recover.EmailToBeRecover);
            
            if (rec == null)
            {
                ViewBag.error = "ამ ე.ლ ფოსტით დარეგისტრირებული მომხმარებელი არ იძებნება";
                return View();
            }
            else
            {


                rec.Password = Helper.ComputeSha256Hash(recover.NewPassword + Helper.AuthKey);
                db.SubmitChanges();
               
                return  RedirectToAction("RecoverSucces");


            }

         
        }

        public ActionResult dadad()
        {
            return View();
        }

    }
}