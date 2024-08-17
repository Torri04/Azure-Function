using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure_Functions.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Azure_Functions
{
    public class QueueMessageJson
    {
        public string UserEmail = string.Empty;
        public string Subject = string.Empty;
        public string HtmlMessage = string.Empty;
    }
    public class EmailQueueTrigger
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<EmailQueueTrigger> _logger;

        public EmailQueueTrigger(IEmailSender emailSender, ILogger<EmailQueueTrigger> logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }

        [Function(nameof(EmailQueueTrigger))]
        public async Task Run([QueueTrigger("email-queue", Connection = "AzureWebJobsStorage")] QueueMessage message)
        {
            _logger.LogInformation("Email received");

            try
            {
                var queueMessageJson = JsonConvert.DeserializeObject<QueueMessageJson>(message.MessageText);

                if (queueMessageJson == null)
                {
                    _logger.LogWarning("Received a null or invalid queue message");
                    return;
                }

                await _emailSender.SendEmailAsync(queueMessageJson.UserEmail, queueMessageJson.Subject, queueMessageJson.HtmlMessage);

                _logger.LogInformation($"Email sent successfully to {queueMessageJson.UserEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing queue message");
            }
        }
    }
}
