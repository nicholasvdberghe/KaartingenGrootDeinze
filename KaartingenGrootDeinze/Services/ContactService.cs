using System;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace KaartingenGrootDeinze.Services
{
    public class ContactService
    {
        public void VerstuurEmail(string afzender, string afzenderAdres, string vanAdres, string naarAdres, string onderwerp, string boodschap)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    const string email = "nicholasvdberghe@hotmail.com";
                    const string wachtwoord = "@Milibeetenn1234";

                    var loginInfo = new NetworkCredential(email, wachtwoord);

                    mail.From = new MailAddress(vanAdres, "Kaartingen Deinze");
                    mail.To.Add(new MailAddress(naarAdres));
                    mail.To.Add(new MailAddress(vanAdres));
                    mail.To.Add("gaby.van.den.berghe@hotmail.com");
                    mail.Subject = onderwerp;
                    mail.Body = boodschap;
                    mail.IsBodyHtml = true;
                    try
                    {
                        using (var smtpClient = new SmtpClient("smtp.live.com", 587))
                        {
                            smtpClient.EnableSsl = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Credentials = loginInfo;
                            smtpClient.Send(mail);
                        }
                    }
                    finally
                    {
                        //dispose the client
                        mail.Dispose();
                    }
                }
            }
            catch (SmtpFailedRecipientsException ex)
            {
                foreach (SmtpFailedRecipientsException t in ex.InnerExceptions)
                {
                    var status = t.StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy || status == SmtpStatusCode.MailboxUnavailable)
                    {
                        HttpContext.Current.Response.Write("Aflevering mislukt - nieuwe poging binnen 5 seconden.");
                        System.Threading.Thread.Sleep(5000);
                        //resend
                        //smtpClient.Send(boodschap)
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("Aflevering boodschap mislukt");
                    }
                }
            }
            catch (SmtpException smtpex)
            {
                //handle exception here
                HttpContext.Current.Response.Write(smtpex.ToString());
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
        }
    }
}