using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmailService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailService.Consumers
{
    public class MessageWelcomeConsumer(IServiceScopeFactory scopeFactory, IWebHostEnvironment environment) : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly IWebHostEnvironment _environment = environment;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => ConsumeMessages(), cancellationToken);
            return Task.CompletedTask;
        }

        private void ConsumeMessages()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "user_email_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var userEmail = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received message: {userEmail}");

                    using var scope = _scopeFactory.CreateScope();
                    var emailService = scope.ServiceProvider.GetRequiredService<SendEmailServices>();

                    // Path to the template
                    var emailTemplatePath = Path.Combine(_environment.ContentRootPath, "Templates", "WelcomeEmailTemplate.html");
                    string emailBody;
                    
                    using (var reader = new StreamReader(emailTemplatePath))
                    {
                        emailBody = await reader.ReadToEndAsync();
                    }

                    // Placeholders in the template if necessary
                    emailBody = emailBody.Replace("{{userEmail}}", userEmail);

                    var data = new Model.SendEmail
                    {
                        Email = new List<string> { userEmail },
                        Subject = "Welcome",
                        Body = emailBody
                    };

                    await emailService.Send(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao enviar o email: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: "user_email_queue", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
