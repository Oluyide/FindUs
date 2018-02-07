using Microsoft.AspNet.Identity.Owin;
using OnlineServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using BussinessLogic.Implementation;
using BussinessLogic;
using System.Threading.Tasks;
using CaptchaMvc.HtmlHelpers;
using Recaptcha;
using System.Data.Entity;

namespace OnlineServices.Controllers
{

    public class ProfileSetupController : Controller
    {

        private ApplicationUserManager _userManager;
        Register reg = new Register();
        // GET: ProfileSetup
        ServicesEntities serviceContext = new ServicesEntities();
        

        
        [HttpGet]
        public ActionResult AddServices()
        {
            ProfilingModel model = new ProfilingModel();

            var categories = reg.CountryList();
            var states = reg.StateList();
            var lgas = reg.LGAList();
            var services = reg.ServiceList();
            foreach (var category in categories)
            {
                model.countryList.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
                model.CountryId = 150;
            }
            foreach (var state in states)
            {
                model.stateList.Add(new SelectListItem()
                {
                    Text = state.Name,
                    Value = state.Id.ToString()
                });
                model.StateId = 1;
            }
            foreach (var state in lgas)
            {
                model.LGAList.Add(new SelectListItem()
                {
                    Text = state.Name,
                    Value = state.Id.ToString()
                });
            }

            foreach (var state in lgas)
            {
                model.officeLGAList.Add(new SelectListItem()
                {
                    Text = state.Name,
                    Value = state.Id.ToString()
                });
                model.LGAId = 21;
            }

            foreach (var srv in services)
            {
                model.ServiceList.Add(new SelectListItem()
                {
                    Text = srv.Name,
                    Value = srv.Id.ToString()
                });

            }
            return View(model);
        }

        
        [HttpGet]
        public ActionResult ProfileSetup()
        
        {
            
            //ViewBag.Approve = User.Identity.3();
            Session["userid"] = User.Identity.GetUserId().ToString();

            ProfilingModel model = new ProfilingModel();
            try
            {
                var query = reg.UserByIdentifier(User.Identity.GetUserId().ToString());
                model.FirstName = query.SingleOrDefault().FirstName;
                model.LastName = query.SingleOrDefault().LastName;
                model.Email = query.SingleOrDefault().Email;
                model.HomeAddress = query.SingleOrDefault().Address;
                model.PhoneNumber = query.SingleOrDefault().PhoneNumber;
            }
            catch
            {
                TempData["Register"] = "Please Register";

                return RedirectToAction("Register", "Account");
            }
            var categories = reg.CountryList();
            var states = reg.StateList();
            var lgas = reg.LGAList();
            var services = reg.ServiceList();
            foreach (var category in categories)
            {
                model.countryList.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
                model.CountryId = 150;
            }
            foreach (var state in states)
            {
                model.stateList.Add(new SelectListItem()
                {
                    Text = state.Name,
                    Value = state.Id.ToString()
                });
                model.StateId = 1;
            }
            foreach (var state in lgas)
            {
                model.LGAList.Add(new SelectListItem()
                {
                    Text = state.Name,
                    Value = state.Id.ToString()
                });
            }

            foreach (var state in lgas)
            {
                model.officeLGAList.Add(new SelectListItem()
                {
                    Text = state.Name,
                    Value = state.Id.ToString()
                });
                model.LGAId = 21;
            }

            foreach (var srv in services)
            {
                model.ServiceList.Add(new SelectListItem()
                {
                    Text = srv.Name,
                    Value = srv.Id.ToString()
                });
               
            }
            return View(model);
        }


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

        [HttpPost]
      
        public async Task<ActionResult> ProfileSetup (ProfilingModel model, HttpPostedFileBase uploadedFile)
        {
          
            if (reg.UserProfile(User.Identity.GetUserId()).Count!=0)
            {
                TempData["duplicate"] = "Please you already have an account on FindUs try and Login using ur details or recover your password";
                TempData["Success"] = "You have completed this stage, go to add a service";
                return RedirectToAction("ProfileSetup");
            }
           
            var validImageTypes = new string[]
            {
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png"
             };

            if (uploadedFile == null || uploadedFile.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
             return RedirectToAction("ProfileSetup");
            }
            else if (!validImageTypes.Contains(uploadedFile.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                return RedirectToAction("ProfileSetup");
            }
           
            if (model != null && uploadedFile != null)
            {


                UserProfile profile = new UserProfile();
                try
                {
                    //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                    profile.countryId = model.CountryId;
                    profile.StateId = model.StateId;
                    profile.LGAId = model.LGAId;
                    profile.HomeAddress = model.HomeAddress;
                    profile.OfficialPhoneNumber = model.OfficialPhone;

                    string ImageName =User.Identity.GetUserId()+ System.IO.Path.GetFileName(uploadedFile.FileName);
                    string physicalPath = Server.MapPath("~/images/" + ImageName);

                    uploadedFile.SaveAs(physicalPath);
                    profile.Image = ImageName;
                    profile.MissionStatement = model.MissionStatement;
                    profile.Skills = model.Skills;
                    profile.UserId = User.Identity.GetUserId().ToString();
                    serviceContext.UserProfiles.Add(profile);
                    serviceContext.SaveChanges();
                    TempData["Success"] = "Profile created successfuly.Please click on Add Service tab to continue";
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    
                }
                catch (Exception ex)
                {
                    TempData["ex"] = ex.Message;


                }
                //return View(model);
            }
                return RedirectToAction("ProfileSetup");
        }
        [HttpPost]
        public ActionResult VerifyCode(ProfilingModel model)
        {
       
            var users = reg.UserByIdentifier(User.Identity.GetUserId()).SingleOrDefault();
            if(users.IsApproved==false || users.RegCode!=model.RegCode)
            {
                TempData["regcode"] = "Please Wait and try again later";
                return RedirectToAction("ProfileSetup");
            }
            else
            return RedirectToAction("ProfilePage");
        }
        [HttpGet]
        public ActionResult LoadPost(ProfilingModel model)
        {
            var mypost = posts(User.Identity.GetUserId());
            model.postList = mypost.OrderBy(x=>x.DatePosted).ToList();
            return Json(model.postList, JsonRequestBehavior.AllowGet);
        }
        public List<ProfilingModel> posts(string userId)
        {
            List<ProfilingModel> list = new List<ProfilingModel>();
            List<Post> objlist = serviceContext.Posts.Where(x => x.UserId == userId).ToList();

            foreach (var a in objlist)
            {
                ProfilingModel model = new ProfilingModel();

             
                model.Post = a.Post1;
                model.timeposted = reg.Time(a.Date.Value);
                model.postId = a.Id;
                list.Add(model);

            }
            return list.OrderByDescending(x=>x.postId).ToList();
        }


        public List<ProfilingModel> service(string userId)
        {
            List<ProfilingModel> list = new List<ProfilingModel>();
            List<UserService> objlist = serviceContext.UserServices.Where(x => x.UserId == userId).ToList();

            foreach (var a in objlist)
            {
                ProfilingModel model = new ProfilingModel();


                model.serviceImages = a.Images;
                model.ServiceDes = a.ServiceDescription;
                model.IdServive = a.Id;
                list.Add(model);

            }
            return list.OrderByDescending(x => x.postId).ToList();
        }


        [HttpGet]
        public ActionResult EditServices(int Id)
        {
            if(Id ==null)
            {
                return View("ProfilePage");
            }
            ProfilingModel model = new ProfilingModel();
            var objlist = serviceContext.UserServices.Where(x => x.Id == Id).SingleOrDefault();
            model.ServiceName = objlist.ServiceName;
            model.ServiceDes = objlist.ServiceDescription;
            
            model.serviceImages = objlist.Images;
            model.HomeAddress = objlist.FullAddress;
            model.Id=objlist.Id;

            return View(model);

        }

        [HttpPost]
        public ActionResult EditServices(ProfilingModel model)
        {
            if( model==null)
            {
                return View("ProfilePage");
            }
                UserService service = new UserService();
                service.Id = model.Id;
                service.ServiceName = model.ServiceName;
                service.ServiceDescription=model.ServiceDes;
                 service.UserId=User.Identity.GetUserId();
                 service.Images = model.serviceImages;
                service.FullAddress = model.HomeAddress;
                try
                {
                    serviceContext.Entry(service).State = EntityState.Modified;
                    serviceContext.SaveChanges();
                    //serviceContext.UserServices.Attach(service);
                    //serviceContext.Entry(service).Property(x => x.ServiceName).IsModified = true;
                    //serviceContext.Entry(service).Property(x => x.ServiceDescription).IsModified = true;
                    //serviceContext.Entry(service).Property(x=>x.UserId).IsModified=true;
                    //if (!string.IsNullOrEmpty(model.serviceImages))
                    //{
                    //    serviceContext.Entry(service).Property(x => x.Images).IsModified = true;
                    //}
                  
                    //serviceContext.SaveChanges();
                    Session["Success"] = "Edited  Sucessfully";
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                return RedirectToAction("ProfilePage");

        }
        public List<ProfilingModel> ExhibitionImageList(string userId)
        {
            List<ProfilingModel> list = new List<ProfilingModel>();
            List<Exhibition> objlist = serviceContext.Exhibitions.Where(x => x.UserId == userId).ToList();

            foreach (var a in objlist)
            {
                ProfilingModel model = new ProfilingModel();
                model.galleryImages = a.ImagePath;
                list.Add(model);

            }
            return list.OrderByDescending(x => x.postId).ToList();
        }


        [HttpPost, RecaptchaControlMvc.CaptchaValidator]
        public ActionResult UserServices(ProfilingModel model, HttpPostedFileBase uploadedFile, bool captchaValid, string captchaErrorMessage)
        {
           if (!captchaValid)
           {

            ModelState.AddModelError("captcha", captchaErrorMessage);

           }
                if (model != null)
                {

                    UserService services = new UserService();
                    services.ServiceId = model.ServiceId;
                    services.ServiceName = model.ServiceName;
                    services.ServiceDescription = model.ServiceDes;
                    services.CountryId = model.CountryId;
                    services.StateId = model.StateId;
                    services.LGAId = model.LGAId;
                    services.UserId = User.Identity.GetUserId().ToString();
                    services.FullAddress = model.HomeAddress;
                    if (uploadedFile != null)
                    {
                        string ImageName = User.Identity.GetUserId() + System.IO.Path.GetFileName(uploadedFile.FileName);
                        string physicalPath = Server.MapPath("~/images/" + ImageName);
                        uploadedFile.SaveAs(physicalPath);
                        services.Images = ImageName;
                    }
                    try
                    {
                        serviceContext.UserServices.Add(services);
                        serviceContext.SaveChanges();
                        TempData["service"] = "Services Created Successfully.You can now continue to view your profile";
                        return RedirectToAction("ProfileSetup");
                    }
                    catch
                    {
                        return RedirectToAction("ProfileSetup");
                    }

                }
                else
                {
                    ViewBag.ErrMessage = "Error: captcha is not valid.";
                    return RedirectToAction("ProfileSetup");
                }
        }
        
        [HttpGet]
        public ActionResult ExhibitionSetup()
        {
            ExhibitionModels model = new ExhibitionModels();
            if(User.Identity.IsAuthenticated)
            {
             
                model.ServicesGallery = GalleryPage(User.Identity.GetUserId().ToString());
            }
            else
            {
                RedirectToAction("LogOff", "Account");
            }

            return View(model);
        }
        public List<ExhibitionModels> GalleryPage(string userId)
        {
            List<ExhibitionModels> list = new List<ExhibitionModels>();
            List<Exhibition> objlist = serviceContext.Exhibitions.Where(x=>x.UserId==userId).ToList();
            if(objlist==null)
            {
                TempData["gal"] = "Please Upload your pictures For ";
            }
            foreach (var a in objlist)
            {
                ExhibitionModels model = new ExhibitionModels();
                model.description = a.Description;
                model.ImagePath = a.ImagePath;
                model.UserId = a.UserId;
                model.Title = a.Title;
                list.Add(model);

            }
            return list;
        }

        [HttpPost]
        public ActionResult ExhibitionSetup(ExhibitionModels model, HttpPostedFileBase[] uploadedFile)
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (HttpPostedFileBase upload in uploadedFile)
                {

              
                var validImageTypes = new string[]
            {
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png"
             };

                if (upload == null || upload.ContentLength == 0)
                {
                    ModelState.AddModelError("ImageUpload", "This field is required");
                    return View();
                }
                   
                else if (!validImageTypes.Contains(upload.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                    return View();
                }
                Exhibition exhibit = new Exhibition();
                exhibit.Description = model.description;
                exhibit.Title = model.Title;
                exhibit.UserId = User.Identity.GetUserId().ToString();
               string ImageName = System.IO.Path.GetFileName(upload.FileName);
               string physicalPath = Server.MapPath("~/images/" + ImageName);
               upload.SaveAs(physicalPath);
               exhibit.ImagePath = ImageName;
                serviceContext.Exhibitions.Add(exhibit);
                serviceContext.SaveChanges();
              

                }

            }

            Session["Success"] = "Exhibition Created Sucessfully";
            return RedirectToAction("ExhibitionSetup");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetStatesByCountryId(string countryId)
        {
            if (String.IsNullOrEmpty(countryId))
            {
                throw new ArgumentNullException("countryId");
            }
            int id = 0;
            bool isValid = Int32.TryParse(countryId, out id);
            var states =reg.GetAllStatesByCountrytId(id);

            var result = (from s in states
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();
            if(result.Count==0)
            {
               TempData["Empty"] = "State Not set up for your contry Yet";
               return View();
            }
            else
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdatedisplayProfile(ProfilingModel model)
        {
            if(User.Identity.IsAuthenticated)
            {
                try
                {
                    
                    var user = serviceContext.Users.SingleOrDefault(b => b.UserId == User.Identity.GetUserId().ToString());

                    if (user != null)
                    {
                        user.PhoneNumber = model.PhoneNumber;
                        var entry = serviceContext.Entry(user);
                        entry.Property(e => e.PhoneNumber).IsModified = true;
                        serviceContext.SaveChanges();
                    }

                    var userservice = serviceContext.UserServices.Where(b => b.UserId == User.Identity.GetUserId().ToString());
                    if (userservice != null)
                    {
                        //userservice = model.CompanyAddress;
                        //userservice.ServiceName = model.ServiceName;
                        //var entry2 = serviceContext.Entry(serve);
                        //entry2.Property(e => e.ServiceName).IsModified = true;
                        //serviceContext.SaveChanges();
                    }




                    
                }
                catch
                {

                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetServices()
        {
            ServicesModel model = new ServicesModel();
    
            var userService = reg.Userservices(User.Identity.GetUserId()).ToList();
            model.services = userService;
            return View(model);
        }

        [HttpGet]
        public ActionResult ProfilePage()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    ProfilingModel model = new ProfilingModel();
                    var name = reg.UserByIdentifier(User.Identity.GetUserId()).SingleOrDefault().FirstName;
                    var userprofile = reg.UserProfile(User.Identity.GetUserId()).SingleOrDefault();
                    var userService = reg.Userservices(User.Identity.GetUserId()).FirstOrDefault();
                    var users = reg.UserByIdentifier(User.Identity.GetUserId()).SingleOrDefault();
                    //var postal = reg.GetpostperUser(User.Identity.GetUserId());
                    var identity = User.Identity.GetUserId().ToString();
                    model.postList = posts(identity);

                    if (userService != null)
                    {
                        ViewBag.service = userService.ServiceName;
                        ViewBag.service = userService.FullAddress;
                        ViewBag.service = userService.ServiceName;
                        var services= reg.Userservices(User.Identity.GetUserId()).ToList();
                        model.servicy = service(identity);
                        model.gallery = ExhibitionImageList(identity);

                       // return RedirectToAction("ProfilePage", "ProfileSetup");
                    }
                    else
                    {
                        TempData["Please"] = "We discovered that you have no service please set up your services here for you to proceed";
                        return RedirectToAction("ProfileSetup");
                    }

                    if (userprofile != null)
                    {
                        ViewBag.UserName = name;
                        ViewBag.Userprofile = userprofile.Image;
                        ViewBag.company = userprofile.CompanyName;
                        //ViewBag.Location = userService.FullAddress;
                        ViewBag.phone = users.PhoneNumber;
                    }
                    else
                    {
                        TempData["Please"] = "Please set up your profile for you to proceed";
                        return RedirectToAction("ProfileSetup");
                    }
                        return View(model);
                }
                catch
                {
                    TempData["ddd"] = "ddd";
                    return RedirectToAction("ProfileSetup");
                }
            }
            else

                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult ProfilePage(ProfilingModel model)
        {
            if(model!=null)
            {
                Post post = new Post();
                post.Post1 = model.Post;
                post.UserId = User.Identity.GetUserId().ToString();
                post.Date = DateTime.Now;
                serviceContext.Posts.Add(post);
                serviceContext.SaveChanges();

            }
            return RedirectToAction("ProfilePage");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetLagBystateId(string stateId)
        {
            if (String.IsNullOrEmpty(stateId))
            {
                throw new ArgumentNullException("stateId");
            }
            int id = 0;
            bool isValid = Int32.TryParse(stateId, out id);
            var states = reg.GetAllLGAByStatesId(id);

            var result = (from s in states
                          select new
                          {
                              id = s.Id,
                              name = s.Name
                          }).ToList();
            if (result.Count == 0)
            {
                TempData["Empty"] = "State Not set up for your contry Yet";
                return View();
            }
            else
                return Json(result, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> CountryList(ProfilingModel model)
        {
            List<Country> products = new List<Country>();
            products = serviceContext.Countries.ToList();
            var list = from s in products
                       select new SelectListItem
                       {
                           Selected = s.Id.ToString() == model.CountryId.ToString(),
                           Text = s.Name,
                           Value = s.Id.ToString()
                       };

            return list;
        }



       
    }
}