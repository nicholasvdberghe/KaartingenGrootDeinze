using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace KaartingenGrootDeinze.Services
{
    public class ContactService
    {
        public void VerstuurEmail(string afzender, string afzenderAdres, string ontvanger, string onderwerp, string boodschap)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    const string username = "nicholas@inksniper.be";
                    const string wachtwoord = "@ir77icy1988";
                    var loginInfo = new NetworkCredential(username, wachtwoord);

                    mail.From = new MailAddress(username, "Kaartingen Deinze");
                    mail.To.Add(new MailAddress(ontvanger));
                    mail.Subject = onderwerp;
                    mail.Body = boodschap;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = Encoding.Default;
                    try
                    {
                        using (var smtpClient = new SmtpClient("mail.inksniper.be", 25))
                        {
                            smtpClient.EnableSsl = false;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.Credentials = loginInfo;
                            smtpClient.Timeout = 100000;
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