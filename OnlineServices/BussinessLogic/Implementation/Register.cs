
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Implementation
{
    public class Register
    {
        ServicesEntities serviceContext = new ServicesEntities();
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        public void SaveUsersOnRegister(string FirstName, string LastName,string identifier,string email,string phoneNumber,string Address,bool IsApproved)
        {
            User regUser = new User();
            regUser.FirstName = FirstName;
            regUser.LastName = LastName;
            regUser.UserId = identifier;
            regUser.Email = email;
            regUser.IsApproved = IsApproved;
            regUser.PhoneNumber = phoneNumber;
            regUser.Address = Address;
            serviceContext.Users.Add(regUser);
            try
            {
                serviceContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public IList<Country> CountryList()
        {
            List<Country> countries = new List<Country>();
            countries = serviceContext.Countries.ToList();

            return countries.ToList();
        }

        public IList<service> ServiceList()
        {
            List<service> Services = new List<service>();
            Services = serviceContext.services.ToList();
            return Services.ToList();

        }
        public IList<State> StateList()
        {
            List<State> states = new List<State>();
            states = serviceContext.States.ToList();
            return states.ToList();
        }
        public IList<LGA> LGAList()
        {
            List<LGA> lgas = new List<LGA>();
           lgas = serviceContext.LGAs.ToList();
           return lgas.ToList();
        }


        public IList<State> GetAllStatesByCountrytId(int countryId)
        {
            var query = from category in serviceContext.States
                        where category.Country == countryId
                        select category;
            var content = query.ToList<State>();
            return content;
        }

        public IList<LGA> GetAllLGAByStatesId(int stateId)
        {
            var query = from category in serviceContext.LGAs
                        where category.StateId==stateId
                        select category;
            var content = query.ToList<LGA>();
            return content;
        }

        public IList<User> UserByIdentifier(string Identifier)
        {
            var query = from category in serviceContext.Users
                        where category.UserId == Identifier
                        select category;
            var content = query.ToList<User>();
            return content;
        }
        public IList<UserProfile> UserProfile(string Identifier)
        {
            var query = from category in serviceContext.UserProfiles
                        where category.UserId == Identifier
                        select category;
            var content = query.ToList<UserProfile>();
        return content;
        }

        public IList<UserService> Userservices(string identifier)
        {
            var query = from category in serviceContext.UserServices
                        where category.UserId == identifier
                        select category;
            var content = query.ToList<UserService>();
            return content;
        }
       
        public string Time(DateTime postdate)
        {

            TimeSpan span = DateTime.Now - postdate;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("about {0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("about {0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("about {0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;

            
        }



        public IQueryable<Post> GetpostperUser(string userId)
        {
            return serviceContext.Set<Post>().Where(x=>x.UserId==userId);
        }
        //public string UserByIdentifier(string Identifier)
        //{
        //    var query = from category in serviceContext.Users
        //                where category.UserId == Identifier
        //                select category;
        //    return query.SingleOrDefault().;
            
        //}
    }
}
