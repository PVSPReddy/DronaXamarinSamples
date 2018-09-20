using System;
using System.Threading.Tasks;
namespace TakeScreenShot.DependencyServices
{
    public interface IScreenshot
    {
        Task<byte[]> CaptureScreen();
    }
}
