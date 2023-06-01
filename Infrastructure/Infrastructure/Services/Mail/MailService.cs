using System.Net;
using System.Net.Mail;
using System.Text;
using Application.Abstractions.Services;
using Application.DTOs.Order;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Mail;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {
        await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
    }
    public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {
        MailMessage mail = new();
        mail.IsBodyHtml = isBodyHtml;
        foreach (var to in tos)
        {
            mail.To.Add(to);
        }

        mail.Subject = subject;
        mail.Body = body;
        mail.From = new(_configuration["Mail:UserName"], "BoutiqueApp", System.Text.Encoding.UTF8);//Gonderilicek bilgileri gir

        SmtpClient smtpClient = new();
        smtpClient.Credentials =
            new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:Password"]);
        smtpClient.Port = 587;
        smtpClient.Host = _configuration["Mail:Host"];
        await smtpClient.SendMailAsync(mail);
    }
    public async Task SendPasswordResetMailAsync(string to,string userId, string resetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine("Hi<br> We take a reset password request If it is from you, " +
                        "You can easily reset your password from below Link. <br><strong><a target = \"_blank\" " +
                        "href=\"");
        mail.AppendLine(_configuration["BaseURLs:ClientURL"]);
        mail.AppendLine("update-password");
        mail.AppendLine(userId);
        mail.AppendLine("/");
        mail.AppendLine(resetToken);
        mail.AppendLine(
            "\">Please click for new Password request</a></strong><br><br><span style=\"font-size:12px;\">" +
            "Note: If this request didnt send from you Please dont care.</span><br><br><br> BoutiqueApp");

        await SendMailAsync(to, "ResetPassword", mail.ToString());
        //todo Kullanilacak Urlleri girmeyi unutma Client Urlleri Seq loglamayi unutma
         
        

    }
    public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName, string userSurname)
    {
        string mail = $"Merhaba {userName} {userSurname} " +
                      $"{orderDate} tarihinde verdiginiz {orderCode} numarali siparsiniz tamamlanmistir ve kargoya verilmistir";
        await SendMailAsync(to, $"{orderCode} numarali siparisiniz tamamlandir", mail);
    }
}