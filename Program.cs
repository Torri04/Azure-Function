using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Azure_Functions.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.Configure<MailSettings>(options =>
                        {
                            options.Mail = Environment.GetEnvironmentVariable("Mail");
                            options.DisplayName = Environment.GetEnvironmentVariable("DisplayName");
                            options.Password = Environment.GetEnvironmentVariable("Password");
                            options.Host = Environment.GetEnvironmentVariable("Host");
                            options.Port = int.Parse(Environment.GetEnvironmentVariable("Port"));
                        });

        services.AddScoped<IEmailSender, SendMailService>();
    })
    .Build();

host.Run();



