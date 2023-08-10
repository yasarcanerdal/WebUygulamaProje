using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebUygulamaProje.Context
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Email gönderme işlemlerinin yapıldığı yer. (Ben yapmadım.)
            return Task.CompletedTask;
        }
    }
}
