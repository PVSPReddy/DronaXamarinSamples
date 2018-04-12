using System;
using System.Threading.Tasks;
using TextToSpeechToText.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpeechToTextService))]
namespace TextToSpeechToText.iOS
{
    public class SpeechToTextService : ISpeechToTextService
    {
        public SpeechToTextService(){}

        public Task<bool> StartClick()
        {
            throw new NotImplementedException();
        }
    }
}

