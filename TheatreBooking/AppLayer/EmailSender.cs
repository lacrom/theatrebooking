using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace TheatreBooking.AppLayer
{
    public class EmailSender
    {
        public void Send(string email, string subject, string message)
        {
            string FROM = "mirrorrevolution1@gmail.com";   // Replace with your "From" address. This address must be verified.
            string TO = email;  // Replace with a "To" address. If your account is still in the
                                                  // sandbox, this address must be verified.

            String SUBJECT = subject;
            String BODY = message;

            // Supply your SMTP credentials below. Note that your SMTP credentials are different from your AWS credentials.
            const String SMTP_USERNAME = "AKIAJ3Q3T5GMXUZES7SA";  // Replace with your SMTP username. 
            const String SMTP_PASSWORD = "AiUQN5AGTM17QA87bYQtDdGkrEu1IJYBAng8JSFA4FKy";  // Replace with your SMTP password.

            // Amazon SES SMTP host name. This example uses the US West (Oregon) region.
            const String HOST = "email-smtp.eu-west-1.amazonaws.com";

            // Port we will connect to on the Amazon SES SMTP endpoint. We are choosing port 587 because we will use
            // STARTTLS to encrypt the connection.
            const int PORT = 587;

            // Create an SMTP client with the specified host name and port.
            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {
                // Create a network credential with your SMTP user name and password.
                client.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                // Use SSL when accessing Amazon SES. The SMTP session will begin on an unencrypted connection, and then 
                // the client will issue a STARTTLS command to upgrade to an encrypted connection using SSL.
                client.EnableSsl = true;

                // Send the email. 
                try
                {
                    client.Send(FROM, TO, SUBJECT, BODY);
                }
                catch (Exception ex)
                {
                    
                }
            }

            //var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("mirrorrevolution1@gmail.com", "HYPER1337protection"),
            //    EnableSsl = true
            //};
            //client.Send("mirrorrevolution1@gmail.com", email, "Билеты в большой театр успешно забронированы", message);

            //RegionEndpoint region = RegionEndpoint.EUWest1;
            ////var objClient = new Amazon.SimpleEmail.AmazonSimpleEmailServiceClient("AKIAJ3Q3T5GMXUZES7SA", "AiUQN5AGTM17QA87bYQtDdGkrEu1IJYBAng8JSFA4FKy", region);

            //AWSCredentials objAWSCredentials = new BasicAWSCredentials("AKIAJ3Q3T5GMXUZES7SA", "AiUQN5AGTM17QA87bYQtDdGkrEu1IJYBAng8JSFA4FKy");

            //Destination destination = new Destination(new List<string>() { email });

            //// Create the subject and body of the message.
            //Content subject = new Content("Билеты в большой театр успешно забронированы");
            //Content textBody = new Content(message);
            //Body body = new Body(textBody);
            ////Body body = new Body().WithText(textBody);

            //// Create a message with the specified subject and body.
            //Message msg = new Message(subject, body);

            //// Assemble the email.
            //SendEmailRequest request = new SendEmailRequest("mirrorrevolution1@gmail.com", destination, msg);

            //using (var client = AWSClientFactory.CreateAmazonSimpleEmailServiceClient(objAWSCredentials, region))
            //{
            //    return client.SendEmail(request).ToString();
            //}
        }
    }
}