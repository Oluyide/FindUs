using BussinessLogic;
using BussinessLogic.Implementation;
using OnlineServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineServices.Controllers
{
     [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        AdminTasks reg = new AdminTasks();
        // GET: ProfileSetup
        ServicesEntities serviceContext = new ServicesEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminView()
        {
            AdminProveModel prove = new AdminProveModel();
            prove.Vendorlist = Vendorcustomer();
            prove.ApprovedUser = AllVendorcustomer();
            return View(prove);
        }
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
     
        public async Task<ActionResult> CreateAsAdmin(string userId)
        {
           
                var adminresult = await UserManager.FindByIdAsync(userId);
                        var result = await UserManager.AddToRolesAsync(userId, "Admin");
                        if (result.Succeeded)
                        {
                            Session["admin"] = string.Format("{0} is now an ADMIN", adminresult.Email);
                           
                        }
                        
                        else
                        {
                            Session["obsolete"] = string.Format("{0} is an obsolete user", adminresult.Email);      
                        
                        }
       
            return RedirectToAction("AdminView");
        }

        public ActionResult VendorDetails(string userId)
        {
            AdminProveModel prove = new AdminProveModel();
            //try
            //{
                var userprofile = reg.profile(userId);
                var user = reg.Dprofile(userId);
                prove.Firstname = user.FirstName;
                prove.LastName = user.LastName;
                prove.Skills = userprofile.Skills;
                prove.MissionStatement = userprofile.MissionStatement;
                prove.Image = userprofile.Image;
                prove.EmailAddress = user.Email;
                prove.HomeAddress = userprofile.HomeAddress;
                prove.MissionStatement = userprofile.MissionStatement;
                prove.OfficialPhoneNumber = userprofile.OfficialPhoneNumber;
                prove.UserId = user.UserId;
                Session["y"] = user.UserId;
            //}
            //catch
            //{
            //    return RedirectToAction("AdminView");
            //}
            return View(prove);
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

         [AllowAnonymous]
        public async Task<ActionResult> ApproveByEmail(string UserId)
        {
            AdminProveModel prove = new AdminProveModel();
            EmailServices mail = new EmailServices();
            Random r = new Random();
            int regcode = r.Next(1000, 9999);
            var userprofile = reg.profile(UserId);
            var user = reg.Dprofile(UserId);
            var approve = reg.Approve(UserId);
            reg.insertRegCode(UserId, regcode);
            string sms=string.Format("Your Registration is successful.This is your verification code {0}",regcode);
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.UserId);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.UserId, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(user.UserId, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>, Your verification Code is <p> " + regcode + "</p>");
            mail.SendSMS(user.PhoneNumber, sms);
            return RedirectToAction("Index","Home");
        }

         [AllowAnonymous]
         public async Task<ActionResult> DenyByEmail(string UserId)
         {
             AdminProveModel prove = new AdminProveModel();
             EmailServices mail = new EmailServices();
             var userprofile = reg.profile(UserId);
             var user = reg.Dprofile(UserId);
    
             var deny = reg.Deny(UserId);


             string sms = string.Format("You are denied due to wrong Registration Information");
             string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.UserId);
             var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.UserId, code = code }, protocol: Request.Url.Scheme);
             await UserManager.SendEmailAsync(user.UserId, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>, You are denied due to wrong Registration Information");
             mail.SendSMS(user.PhoneNumber, sms);
             return RedirectToAction("Index", "Home");
         }
         
        public async Task<ActionResult> ApproveUser()
        {
            AdminProveModel prove = new AdminProveModel();
            Random r = new Random();
            int regcode = r.Next(1000, 9999);
            var userprofile = reg.profile(Session["y"].ToString());
            var user = reg.Dprofile(Session["y"].ToString());
            var approve = reg.Approve(Session["y"].ToString());
            reg.insertRegCode(Session["y"].ToString(), regcode);

            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.UserId);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.UserId, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(user.UserId, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>, Your verification Code is <p> "+ regcode + "</p>" );

            return RedirectToAction("AdminView");
        }

        public ActionResult DisApproveUser(AdminProveModel prove)
        {
           
            var userprofile = reg.profile(Session["y"].ToString());
            var user = reg.Dprofile(Session["y"].ToString());
            var dis= reg.DisApprove(Session["y"].ToString(), prove.Reason);
            return RedirectToAction("AdminView");
        }
        public List<AdminProveModel> Vendorcustomer()
        {
            List<AdminProveModel> list = new List<AdminProveModel>();
            List<User> objlist = serviceContext.Users.Where(x => x.IsApproved == false).ToList();

            foreach (var a in objlist)
            {
                AdminProveModel model = new AdminProveModel();
                model.Id = a.Id;
                model.Firstname =a.FirstName;
                model.LastName = a.LastName;
                model.UserId = a.UserId;
                model.Reason = a.Reason;
                model.IsApproved = a.IsApproved.Value;
                list.Add(model);

            }
            return list;
        }

        public List<AdminProveModel> AllVendorcustomer()
        {
            List<AdminProveModel> list = new List<AdminProveModel>();
            List<User> objlist = serviceContext.Users.Where(x => x.IsApproved == true).ToList();

            foreach (var a in objlist)
            {
                AdminProveModel model = new AdminProveModel();
                model.Id = a.Id;
                model.Firstname = a.FirstName;
                model.LastName = a.LastName;
                model.UserId = a.UserId;
                model.Reason = a.Reason;
                model.IsApproved = a.IsApproved.Value;
                list.Add(model);

            }
            return list;
        }
    }
}