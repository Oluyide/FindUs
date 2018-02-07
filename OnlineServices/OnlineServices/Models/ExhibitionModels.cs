using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineServices.Models
{
    public class ExhibitionModels
    {
        public string ImagePath { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string description { get; set; }
        public List<ExhibitionModels> ServicesGallery { get; set; }
    }
}