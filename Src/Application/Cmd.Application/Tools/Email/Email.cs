using Microsoft.Extensions.Options;
using System.Net.Mail;


namespace Cmd.Application.Tools.Email;

public class SendEmail
{
    private readonly EmailOp _appSettings;

    public SendEmail(IOptions<EmailOp> option)
    {
        _appSettings = option.Value;
    }


    public void Send(string To, string Subject, string Body)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_appSettings.MailServer);
            mail.From = new MailAddress(_appSettings.Email, _appSettings.EmailSubject);
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Port = _appSettings.Port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(_appSettings.Email, _appSettings.Password);
            SmtpServer.EnableSsl = _appSettings.Ssl;

            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}