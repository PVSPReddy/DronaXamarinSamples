using System;
namespace FileDownloaderExample
{
	public interface IPdfView
	{
		void ShowPdf(string filePath);

		void PrintPdf(string filePath);
	}
}

