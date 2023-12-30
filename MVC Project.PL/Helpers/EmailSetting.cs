using MVC_Project.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_Project.PL.Helpers
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl= true;
			client.Credentials = new NetworkCredential("eng.mohamedmahmoud24@gmail.com", "noeulzzyhzybclli");
			client.Send("eng.mohamedmahmoud24@gmail.com", email.To,email.Subject,email.Body);
			
		}
	}
}
