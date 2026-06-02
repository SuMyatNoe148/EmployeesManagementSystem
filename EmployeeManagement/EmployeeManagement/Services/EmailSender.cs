using Microsoft.AspNetCore.Identity.UI.Services;

namespace EmployeeManagement.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Email sending is not configured - just log or do nothing
            return Task.CompletedTask;
        }
    }
}
