using BussinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineServices.Models
{
    public class ProfilingModel
    {

        public ProfilingModel()
        {

            countryList = new List<SelectListItem>();
            stateList = new List<SelectListItem>();
            LGAList = new List<SelectListItem>();
            ServiceList= new List<SelectListItem>();
            
            //officecountryList = new List<SelectListItem>();
            //officestateList = new List<SelectListItem>();
            officeLGAList = new List<SelectListItem>();

        }

        public IEnumerable<SelectListItem> cutomerList { get; set; }
        //public IEnumerable<SelectListItem> cutomerList { get; set; }
        //public IEnumerable<SelectListItem> cutomerList { get; set; }
        public IList<SelectListItem> countryList { get; set; }
        public IList<SelectListItem> stateList { get; set; }
        public IList<SelectListItem> LGAList { get; set; }
        public IList<SelectListItem> ServiceList { get; set; }
        //public IList<SelectListItem> officecountryList { get; set; }
        //public IList<SelectListItem> officestateList { get; set; }
        public IList<SelectListItem> officeLGAList { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("[0-9]")]
        [MaxLength (11)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("[0-9]")]
        [MaxLength(11)]
        [Display(Name = " Alternate Phone Number")]
        public string AltPhoneNumber { get; set; }

        [RegularExpression("[a-zA-Z]")]
        public string country { get; set; }
        [Required]
        [Display(Name = "Home Address")]
        public string HomeAddress { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]

        [Display(Name = "Service")]
        public int ServiceId { get; set; }
          [Required]
        public string ServiceName { get; set; }
          [Required]
        public string ServiceDes { get; set; }
          [Required]
        public int IdServive { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Office Country")]
        public int officeCountryId { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Required]
        [Display(Name = "LGA")]
        public int LGAId { get; set; }

        [Required]
        [Display(Name = "Office State")]
        public int officeStateId { get; set; }
        [Required]
        [Display(Name = "Office LGA")]
        public int OfficeLGAId { get; set; }


        [Display(Name = "Alternate Email")]
        public string AdditionalEmail { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

       [DataType(DataType.Upload)]
       public HttpPostedFileBase profileImage { get; set; }
        public string profileImageUrl { get; set; }
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "Official Telephone")]
        public string OfficialPhone { get; set; }

        [Required]
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "Official Email Address")]
        public string OfficialEmail { get; set; }

        //[Required]
        //[Display(Name = "Nature of work")]
        //public string Nature { get; set; }

        [Required]
        [Display(Name = "Mission Statement")]
        public string MissionStatement { get; set; }


        [Required]
        [Display(Name = "Skill")]
        public string Skills { get; set; }

        [Required]
        [Display(Name = "Past Project")]
        public string PastProjects { get; set; }

        [Required]
        [Display(Name = "Best Quote")]
        public string BestQuote { get; set; }

        public int Id { get; set; }
        public List<ProfilingModel> postList { get; set; }
        public string Post { get; set; }
        public string timeposted { get; set; }
        public int postId { get; set; }
        public DateTime DatePosted { get; set; }
        public string RegCode { get; set; }
        public string serviceImages { get; set; }
        public string Description { get; set; }
        public List<ProfilingModel> servicy { get; set; }

        public List<ProfilingModel> gallery { get; set; }

        public string galleryImages { get; set; }
        }
   
}