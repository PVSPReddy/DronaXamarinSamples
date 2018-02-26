using System;
using Xamarin.Forms;
using FileDownloaderExample.iOS;
using FileDownloaderExample;
using MessageUI;
using UIKit;
using Foundation;
using System.Net.Mail;
using System.Collections.Generic;
using System.Net;
using System.Collections;

[assembly : Dependency(typeof(IEmailService))]
namespace FileDownloaderExample.iOS
{
	public class IEmailService : IEmail
	{
		
		public IEmailService()
		{
		}

		public void ShareFile(string name, string mimeType)
		{
			try
			{
				MFMailComposeViewController messageController = new MFMailComposeViewController();
				if (MFMessageComposeViewController.CanSendAttachments)
				{
					//MFMailComposeViewController messageController = new MFMailComposeViewController();

					string filename = name; // + "." + ext;
					NSData data = NSData.FromUrl(NSUrl.FromString(filename));

					messageController.SetSubject("Document: " + filename);
					messageController.SetMessageBody("Document: " + filename, false);
					messageController.AddAttachmentData(data, mimeType, filename);

					messageController.Finished += (object s, MFComposeResultEventArgs args) =>
					{
						//UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null)

						if (args.Result == MFMailComposeResult.Sent)
						{
							UIAlertView alert = new UIAlertView("Success", "The file has been sent successfully", null, "Close", null);
							alert.Show();
						}

						args.Controller.DismissViewController(true, null);
					};

					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(messageController, true, null);
				}
				else {
					UIAlertView alert = new UIAlertView("Error", "The file could not be sent", null, "Close", null);
					alert.Show();
				}
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}

		public void Sendmail(string[] recipients, string subject, string messagebody = null, Action<bool> completed = null)
		{
			try
			{
				var controller = new MFMailComposeViewController();
				controller.SetToRecipients(recipients);
				controller.SetSubject(subject);
				if (!string.IsNullOrEmpty(messagebody))
					controller.SetMessageBody(messagebody, false);
				controller.Finished += (object sender, MFComposeResultEventArgs e) =>
				{
					if (completed != null)
						completed(e.Result == MFMailComposeResult.Sent);
					e.Controller.DismissViewController(true, null);
				};

				////Adapt this to your app structure
				//var rootController = ((AppDelegate)(UIApplication.SharedApplication.Delegate)).Window.RootViewController.ChildViewControllers[0].ChildViewControllers[1].ChildViewControllers[0];
				//var navcontroller = rootController as UINavigationController;
				//if (navcontroller != null)
				//	rootController = navcontroller.VisibleViewController;
				//rootController.PresentViewController(controller, true, null);

				//var rootController = ((AppDelegate)(UIApplication.SharedApplication.Delegate)).Window.RootViewController.PresentedViewController;
				var rootController = ((AppDelegate)(UIApplication.SharedApplication.Delegate)).Window.RootViewController.ChildViewControllers[0].ChildViewControllers[1].ChildViewControllers[0];
				var navcontroller = rootController as UINavigationController;
				if (navcontroller != null)
					rootController = navcontroller.VisibleViewController;
				rootController.PresentViewController(controller, true, null);
			}
			catch (Exception ex)
			{
				var msg = ex.Message;
			}
		}


		//public void Sends_Mail(string theme, string message, List<T> files, string mainAdress)
		/*public void Sends_Mail(string theme, string message, List<string> files, string mainAdress)
		{
			var result = "error";
			try
			{
				MailMessage mail = new MailMessage();
				mail.From = new MailAddress(@"mobile@proffinstal.ru");
				mail.To.Add(mainAdress == string.Empty ? Catalog.personalManager.email : mainAdress);
				mail.Subject = "Subject";

				string text = string.Empty;

				text = @"<html><head><title></title></head><body><table border='1' width='100%' cellpadding='10'>";

				text += String.Format(@"<p>Добрый день!</p><br>", Catalog.User.title);
				text += @"<p>Subject: " + theme + "</p><br>";
				text += @"<p>Message: " + message + "</p><br>";

				if (files != null && files.Count > 0)
				{
					foreach (var file in files)
						mail.Attachments.Add(new Attachment(file));
				}

				text += @"</body></html>";

				mail.Body = text;
				mail.IsBodyHtml = true;

				SmtpClient smtpServer = new SmtpClient(@"smtp.yandex.ru", 587);
				smtpServer.UseDefaultCredentials = false;
				smtpServer.EnableSsl = true;
				smtpServer.Credentials = new NetworkCredential(@"user", @"pass");

				smtpServer.SendAsync(mail, null);

				result = "Done";
			}
			catch (Exception ex)
			{
				result = "Error";
			}
			return result;
		}
		public void SendMail(IEnumerable mailAddresses, string message, string subject)
		{
			var mails = mailAddresses.Select(m => m);
			//var items = pFiles.Items.ToList();
			var items = _images
			.Select(i => i.path)
			.ToList();

			System.Threading.Tasks.Task.Run(() =>
			{
				foreach (var mail in mails)
				{
					var res = Catalog.Methods.SendMessageMail(
						subject,
						message,
						items,
						mail);
				}
			});
		}*/


	}
}

