using System;
using ARCoreXamSample.DependencyServices;
using ARCoreXamSample.iOS.CustomControls;
using ARCoreXamSample.iOS.DependencyServices;
using Xamarin.Forms;

[assembly : Dependency(typeof(ARKitAccessService))]
namespace ARCoreXamSample.iOS.DependencyServices
{
    public class ARKitAccessService : IARKitAccessService
    {
        public ARKitAccessService(){}

        public void OpenARKit()
        {
            try
            {
                AppDelegate.window.RootViewController = new ARKitController();
                AppDelegate.window.MakeKeyAndVisible();
            }
            catch(Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}

