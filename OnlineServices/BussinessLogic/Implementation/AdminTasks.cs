using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Implementation
{
  public   class AdminTasks
    {

        ServicesEntities serviceContext = new ServicesEntities();

        public IList<UserProfile> UnVerifyProfile()
        {
            var query = from category in serviceContext.UserProfiles
                        where category.IsApproved ==false
                        select category;
            var content = query.ToList<UserProfile>();
            return content.ToList();
        }

      public UserProfile profile (string userId)
        {
            var query = from uprofile in serviceContext.UserProfiles
                        where uprofile.UserId == userId
                        select uprofile;

            return query.SingleOrDefault();

        }

      public User Dprofile(string userId)
      {
          var query = from uprofile in serviceContext.Users
                      where uprofile.UserId == userId
                      select uprofile;

          return query.SingleOrDefault();

      }
      public void insertRegCode(string userId,int regCode)
      {
          var query = from uprofile in serviceContext.Users
                      where uprofile.UserId == userId
                      select uprofile;
          var q = query.SingleOrDefault();
          q.RegCode = regCode.ToString();
          try
          {
              serviceContext.Entry(q).State = EntityState.Modified;
              serviceContext.SaveChanges();
          }
          catch
          {
              throw;
          }
      }
      public string Approve(string userId)
      {
         

          var query = serviceContext.Users
                    .Where(b => b.UserId == userId)
                    .FirstOrDefault();

          query.IsApproved = true;
          query.Reason = "Qualified";
           try
           {
               serviceContext.Entry(query).State = EntityState.Modified;
               serviceContext.SaveChanges();
           }
           catch
           {
               throw;
           }
           return "Vendor Approved";
      }


      public string Deny(string userId)
      {
          var query = from uprofile in serviceContext.Users
                      where uprofile.UserId == userId
                      select uprofile;
          var q = query.SingleOrDefault();
          q.IsApproved = false;
          q.Reason = null;
          try
          {
              serviceContext.Entry(q).State = EntityState.Modified;
              serviceContext.SaveChanges();
          }
          catch
          {
              throw;
          }
          return "Vendor Denied";
      }

      public string DisApprove(string userId,string reason)
      {
          var query = from uprofile in serviceContext.Users
                      where uprofile.UserId == userId
                      select uprofile;
          var q = query.SingleOrDefault();
          q.IsApproved = false;
          q.Reason = reason;
          try
          {
              serviceContext.Entry(q).State = EntityState.Modified;
              serviceContext.SaveChanges();
          }
          catch
          {
              throw;
          }
          return "Vendor Disapproved";
      }
    }
}
