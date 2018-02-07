using BussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineServices.Models
{
    public class AdminProveModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }
        public string LastName { get; set; }
        public int countryId { get; set; }
        public int StateId { get; set; }
        public string EmailAddress { get; set; }
        public string Reason { get; set; }
        public int LGAId { get; set; }
        public string HomeAddress { get; set; }
        public string Image { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string OfficialPhoneNumber { get; set; }
        public string CompanyAddress { get; set; }
        public string MissionStatement { get; set; }
        public string Skills { get; set; }
        public string PastProjects { get; set; }
        public string DisplayName { get; set; }
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
        public List<AdminProveModel> Vendorlist { get; set; }
        public List<AdminProveModel> ApprovedUser { get; set; }
    }
}