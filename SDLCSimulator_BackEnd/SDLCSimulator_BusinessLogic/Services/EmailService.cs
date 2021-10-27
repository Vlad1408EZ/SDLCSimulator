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
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async System.Threading.Tasks.Task ListenToEmailSendingMessagesAsync(EmailModel model)
        {

            var emailSubject = $"{DateTime.Now.ToShortDateString()} створення завдання";
            var groupIds = model.GroupIds;
            var students = await _userRepository.GetByCondition(u => groupIds.Any(gid => u.GroupId.Value == gid))
                    .ToListAsync();

            if (students.Count == 0)
            {
                throw new InvalidOperationException($"Студенти не знайдені для відправлення мейлу");
            }
            var emailSettings = new EmailNotificationsConfig();
            _configuration.GetSection("EmailNotificationsConfig").Bind(emailSettings);
            var client = SetupSmtpClient(emailSettings);
            var emailMessage = await CreateMessage(model, students);
            await SendMessageAsync(client, emailSettings, emailMessage, emailSubject, students);
        }

        private SmtpClient SetupSmtpClient(EmailNotificationsConfig emailSettings)
        {
            var smtpClient = new SmtpClient(emailSettings.SmtpServerHostname, emailSettings.SmtpServerPort)
            {
                EnableSsl = emailSettings.EnableSSL,
                DeliveryFormat = SmtpDeliveryFormat.International
            };

            if (!emailSettings.AnonymousAuth)
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Login, emailSettings.Password);
            }

            return smtpClient;
        }

        private async System.Threading.Tasks.Task SendMessageAsync(SmtpClient smtpClient, EmailNotificationsConfig emailSettings, string messageBody, string emailSubject, List<User> students)
        {
            var message = new MailMessage
            {
                Subject = emailSubject,
                Body = messageBody,
                From = new MailAddress(emailSettings.AddressFrom),
            };
            foreach (var student in students)
            {
                message.To.Add(student.Email);
            }

            await smtpClient.SendMailAsync(message);
        }

        private async Task<string> CreateMessage(EmailModel model, List<User> students)
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
