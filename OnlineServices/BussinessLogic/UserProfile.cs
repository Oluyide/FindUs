//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BussinessLogic
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public int Id { get; set; }
        public int countryId { get; set; }
        public int StateId { get; set; }
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
        public Nullable<int> officialcountryId { get; set; }
        public Nullable<int> OfficialStateId { get; set; }
        public Nullable<int> OfficialLGAId { get; set; }
        public string UserId { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string RegCode { get; set; }
    }
}