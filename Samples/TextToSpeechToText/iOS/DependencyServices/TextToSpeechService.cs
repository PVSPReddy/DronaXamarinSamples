using System;
using System.Threading.Tasks;
using TextToSpeechToText.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeechService))]
namespace TextToSpeechToText.iOS
{
    public class TextToSpeechService : ITextToSpeechService
    {
        public TextToSpeechService(){}

        public async Task<bool> StartClick()
        {
            bool returnValue = false;
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return returnValue;
        }

        public Task<bool> StartClick(string textValue)
        {
            throw new NotImplementedException();
        }

        public void Talk(string speakValue)
        {
            throw new NotImplementedException();
        }
    }
}

