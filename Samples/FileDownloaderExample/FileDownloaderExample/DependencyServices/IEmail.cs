using System;
namespace FileDownloaderExample
{
	public interface IEmail
	{
		void Sendmail(string[] recipients, string subject, string messagebody = null, Action<bool> completed = null);


		void ShareFile(string name, string mimeType);
	}
}

