using System;
using AVFoundation;
using TextToSpeechToText.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeechGoogleServicesOne))]
namespace TextToSpeechToText.iOS
{
    public class TextToSpeechGoogleServicesOne : ITextToSpeechGoogleServicesOne
    {
        public TextToSpeechGoogleServicesOne(){}

        public void Speak(string text)
        {
            var speechSynthesizer = new AVSpeechSynthesizer();

            var speechUtterance = new AVSpeechUtterance(text)
            {
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                Volume = 0.5f,
                PitchMultiplier = 1.0f
            };

            speechSynthesizer.SpeakUtterance(speechUtterance);
        }

    }
}

