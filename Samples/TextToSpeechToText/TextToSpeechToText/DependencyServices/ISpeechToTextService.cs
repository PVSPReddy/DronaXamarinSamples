using System;
using System.Threading.Tasks;

namespace TextToSpeechToText
{
    public interface ISpeechToTextService
    {
        Task<bool> StartClick();
    }
}
