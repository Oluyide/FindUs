using BussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineServices.Models
{
    public class ServicesModel
    {
        public string images { get; set; }
        public string ServiceName { get; set; }

        public List<UserService> services { get; set; }
    }
}