using System;
using System.Text;
using System.Net.Mail;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("medicalcalendar123","mc12345!");

            Console.Write("Enter username (mail address): ");
            string mailTo = Console.ReadLine();

            try //Mail built-in  validation function.
            {
                MailAddress m = new MailAddress(mailTo);
            }

            catch (FormatException)
            {
                Console.WriteLine("Are you sure that you entered a valid mail address? Try again please.");
                return;
            }

            Console.Write("Enter password: ");
            string pass = Console.ReadLine();

            Random rnd = new Random();
            int randNum = rnd.Next(10000, 100000);
            MailMessage mm = new MailMessage("medicalcalendar123@donotreply.com", mailTo, "Authentication Code for Medical-Calendar","Authentication number is: " + randNum.ToString() + " .");
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
            Console.WriteLine("A mail with an authentication code was sent to your mail (username), please check your inbox.\n");
            int attempts = 3;
            for(int i = 0; i < attempts ; i++)
            {
                Console.Write("Attempt #" + (i+1).ToString() + "\nEnter Authentication code: ");
                string temp = Console.ReadLine();
                if(temp == randNum.ToString())
                {
                    Console.WriteLine("Authentication succeeded!\n");
                    break;
                }
                else
                {
                    Console.WriteLine("Authentication Failed!\n");
                    if(i == 2)
                    {
                        Console.Write("Failed to connect, please try again.");
                    }
                }
            }
        }
    }
}