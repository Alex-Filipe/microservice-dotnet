using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using EmailService.Model;

namespace EmailService.Services
{
    public class SendEmailServices(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task Send(SendEmail data)
        {
            try
            {
                // Variáveis
                var emailConfig = _configuration.GetSection("SmtpSettings");
                var baseUrl = _configuration.GetValue<string>("BaseUrl:Url");

                // BaseUrl
                data.Body = data.Body.Replace("{{baseUrl}}", baseUrl ?? "");

                // Others
                if (data.Other != null)
                {
                    foreach (PropertyInfo atribute in data.Other.GetType().GetProperties())
                    {
                        string atributeName = atribute.Name;
                        string marker = $"{{{{{atributeName}}}}}";
                        string atributeValue = (string)(atribute.GetValue(data.Other) ?? "");                        
                        string atributeValueString = atributeValue.ToString();

                        data.Body = data.Body.Replace(marker, atributeValueString);
                    }
                }

                // Smtp
                using var smtpClient = new SmtpClient(emailConfig["Server"], int.Parse(emailConfig["Port"] ?? throw new Exception("A porta não foi configurada.")));
                smtpClient.EnableSsl = bool.Parse(emailConfig["EnableSsl"] ?? throw new Exception("EnableSsl não foi configurado."));
                smtpClient.Credentials = new System.Net.NetworkCredential(emailConfig["Username"], emailConfig["Password"]);

                // Email
                foreach (var email in data.Email)
                {

                    data.Body = data.Body;
                    //.Replace("{{userEmail}}", email ?? "")
                    //.Replace("{{userName}}", user.Name ?? "");

                    using var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(emailConfig["From"] ?? throw new Exception("O endereço de e-mail do remetente não foi configurado."));
                    mailMessage.To.Add(email);
                    mailMessage.Subject = data.Subject;
                    mailMessage.Body = data.Body;
                    mailMessage.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao enviar o email: " + e.Message);
            }
        }

    }
}
