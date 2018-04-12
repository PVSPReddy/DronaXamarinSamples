using System;
using System.Threading.Tasks;

namespace TextToSpeechToText
{
    public interface ITextToSpeechService
    {
        Task<bool> StartClick(string textValue);

        void Talk(string speakValue);
    }
}
