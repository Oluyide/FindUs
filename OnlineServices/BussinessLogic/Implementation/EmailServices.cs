using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Implementation
{
    public class EmailServices
    {
        public void ToAdminEmail(string message)
        {

            var myMessage = new MailMessage();
            myMessage.To.Add(new MailAddress("ajibolabiodun@gmail.com"));  // replace with valid value 
            myMessage.CC.Add(new MailAddress("gbengaoluyide@gmail.com"));
            myMessage.CC.Add(new MailAddress("ayodeleenitilo@yahoo.com"));
            myMessage.CC.Add(new MailAddress("edekin02@yahoo.com"));

            myMessage.From = new MailAddress("festusoluyide@gmail.com");  // replace with valid value
            myMessage.Subject = "Account Registration";
            myMessage.Body = message;
            myMessage.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "festusoluyide@gmail.com",  // replace with valid value
                    Password = "7343good"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(myMessage);
            }
        }

        public string GetSMSAPIUrl(string phoneNumber, string message)
        {
            phoneNumber = "234" + phoneNumber.Substring(1);
            return string.Format("http://smsmobile24.com/components/com_spc/smsapi.php?username=oluyide&password=festinati&sender=Findus.com&recipient={0}&message={1}",phoneNumber,message);
        }
        public async Task<byte[]> SendSMS(string phoneNumber, string message)
        {
            var content = new MemoryStream();
            try
            {

                var SMSUrl = GetSMSAPIUrl(phoneNumber, message);
                var webReq = (HttpWebRequest)WebRequest.Create(SMSUrl);
                try
                {
                    using (WebResponse response = webReq.GetResponse()) //wait for the response
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            // Read the bytes in responseStream and copy them to content. 
                            // await responseStream.CopyToAsync(content);
                            responseStream.CopyTo(content);

                        }
                    }
                }
                catch
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return content.ToArray();
        }
        public void ToAdminSMS(string message,string number)
        {

            var myMessage = new MailMessage();
            //recipient-mobile-number@mail2smsv2.vianett.no
            string Tovendor = string.Format("+234{0}@mail2smsv2.vianett.no", number);
            myMessage.To.Add(new MailAddress("+2348135361806@mail2smsv2.vianett.no"));  // replace with valid value 
            myMessage.CC.Add(new MailAddress("+2348024814138@mail2smsv2.vianett.no"));
            myMessage.CC.Add(new MailAddress("+2348178147974@mail2smsv2.vianett.no"));
            myMessage.From = new MailAddress("festusoluyide@gmail.com");  // replace with valid value
            myMessage.Subject = "username=festusoluyide@gmail.com&password=oorta&sourceAddr=test";
            myMessage.Body = message;
            myMessage.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "festusoluyide@gmail.com",  // replace with valid value
                    Password = "7343good"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(myMessage);
            }
        }
    }
}
