using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_Data;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;
        private readonly SmtpClient _smtpClient;
        private readonly EmailNotificationsConfig _emailSettings;

        public EmailService(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _emailSettings = new EmailNotificationsConfig();
            configuration.GetSection("EmailNotificationsConfig").Bind(_emailSettings);
            _smtpClient = SetupSmtpClient();
        }

        public async System.Threading.Tasks.Task ListenToEmailSendingMessagesForUserAsync(EmailUserModel model)
        {
            var emailSubject = $"{DateTime.Now.ToShortDateString()} створення користувача";
            var emailMessage = CreateUserMessage(model);
            await SendUserMessageAsync(emailMessage, emailSubject, model.Email);
        }

        public async System.Threading.Tasks.Task ListenToEmailSendingMessagesForTaskAsync(EmailTaskModel model)
        {

            var emailSubject = $"{DateTime.Now.ToShortDateString()} створення завдання";
            var groupIds = model.GroupIds;
            var students = await _userRepository.GetByCondition(u => groupIds.Any(gid => u.GroupId.Value == gid))
                    .ToListAsync();

            if (students.Count == 0)
            {
                throw new InvalidOperationException($"Студенти не знайдені для відправлення мейлу");
            }
            var emailMessage = await CreateTaskMessageAsync(model, students);
            await SendTaskMessageAsync(emailMessage, emailSubject, students);
        }

        private SmtpClient SetupSmtpClient()
        {
            var smtpClient = new SmtpClient(_emailSettings.SmtpServerHostname, _emailSettings.SmtpServerPort)
            {
                EnableSsl = _emailSettings.EnableSSL,
                DeliveryFormat = SmtpDeliveryFormat.International
            };

            if (!_emailSettings.AnonymousAuth)
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Login, _emailSettings.Password);
            }

            return smtpClient;
        }

        private async System.Threading.Tasks.Task SendTaskMessageAsync(string messageBody, string emailSubject, List<User> students)
        {
            var message = new MailMessage
            {
                Subject = emailSubject,
                Body = messageBody,
                From = new MailAddress(_emailSettings.AddressFrom),
            };
            foreach (var student in students)
            {
                message.To.Add(student.Email);
            }

            await _smtpClient.SendMailAsync(message);
        }

        private async System.Threading.Tasks.Task SendUserMessageAsync(string messageBody, string emailSubject, string email)
        {
            var message = new MailMessage
            {
                Subject = emailSubject,
                Body = messageBody,
                From = new MailAddress(_emailSettings.AddressFrom),
            };
            message.To.Add(email);

            await _smtpClient.SendMailAsync(message);
        }

        private static string CreateUserMessage(EmailUserModel model)
        {
            var body = new StringBuilder();
            body.AppendLine($"Шановний(шановна) {model.LastName} {model.FirstName}");
            body.AppendLine($"Ваш пароль для входу в SDLCSimulator {model.Password}. Змініть пароль після входу в систему, будь ласка");
            return body.ToString();
        }

        private async Task<string> CreateTaskMessageAsync(EmailTaskModel model, List<User> students)
        {
            var teacher = await _userRepository.GetSingleByConditionAsync(u => u.Id == model.TeacherId);

            if (teacher == null)
            {
                throw new InvalidOperationException($"Вчитель не знайдений для відправлення імейлу");
            }

            var body = new StringBuilder();
            body.Append("Шановні ");
            for(int i = 0; i < students.Count; i++)
            {
                body.Append($"{students[i].LastName} {students[i].FirstName}");
                if (i != students.Count - 1)
                {
                    body.Append(", ");
                }
            }

            body.AppendLine();
            body.AppendLine($"{teacher.LastName} {teacher.FirstName} створив завдання на тему '{model.Topic}'.");

            return body.ToString();
        }
    }
}
