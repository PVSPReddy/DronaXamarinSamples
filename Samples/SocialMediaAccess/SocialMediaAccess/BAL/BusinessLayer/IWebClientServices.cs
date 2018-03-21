using System;
using System.Threading.Tasks;

namespace SocialMediaAccess
{
    public interface IWebClientServices
    {
        Task<string> getAuthKeys();
    }
}
