using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

namespace CommonLayer.Models
{
    public class MSMQModel
    {
        MessageQueue msg = new MessageQueue();
        public void MsmqSender(string Token)
        {
            msg.Path = @".\private$\Token";
            if (!MessageQueue.Exists(msg.Path))
            {
                MessageQueue.Create(msg.Path);

            }
            msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            msg.ReceiveCompleted += MessageQueue_ReceiveCompleted;//tab
            msg.Send(Token);
            msg.BeginReceive();
            msg.Close();
        }
        public void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = msg.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string Subject = "Fundoo Notes Password Reset";
            string Body = token;
            string mailReceiver = GetEmailFromToken(token);

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("rajanganep28@gmail.com", "Secret@123"),//give dummy gmail
                EnableSsl = true,
            };
            smtpClient.Send("rajanganep28@gmail.com", mailReceiver, Subject, Body);
            msg.BeginReceive();

        }

        public static string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decoded = handler.ReadJwtToken((token));
            var result = decoded.Claims.FirstOrDefault().Value;
            return result;
        }

    }
}
