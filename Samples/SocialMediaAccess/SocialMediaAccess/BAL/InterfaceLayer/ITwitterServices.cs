using System;
using System.Threading.Tasks;

namespace SocialMediaAccess
{
    public interface ITwitterServices : IDisposable
    {
        Task<string> getAuthToken(string name);
    }
}
