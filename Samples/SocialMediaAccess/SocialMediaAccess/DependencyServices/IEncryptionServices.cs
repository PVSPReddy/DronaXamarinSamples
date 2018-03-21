using System;
using System.Threading.Tasks;

namespace SocialMediaAccess
{
    public interface IEncryptionServices
    {
        Task<string> GetSHA1EncryptedString(string strConvert);

        Task<string> GetHMACSHA1EncryptedString(string data, string key);

        Task<DateTime> GetNTPTime();
    }
}
