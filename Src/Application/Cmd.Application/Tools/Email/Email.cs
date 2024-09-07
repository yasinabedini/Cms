using Microsoft.Extensions.Options;
using System.Net;
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
        MailAddress toEmail = new MailAddress(To);
        MailAddress from = new MailAddress(_appSettings.Email);

        MailMessage email = new MailMessage(from, toEmail)
        {
            Subject = Subject,
            Body = Body
        };

        SmtpClient smtp = new SmtpClient(_appSettings.MailServer)
        {
            Port = 25,
            Credentials = new NetworkCredential(_appSettings.Email, _appSettings.Password),
            EnableSsl = true
        };

        try
        {
            smtp.Send(email);
            Console.WriteLine("Email sent successfully!");
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}