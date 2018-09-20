using System;
using System.Globalization;
using Localization.Views;
using UsingResxLocalization.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Localization.Utilities
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }
        //public static CultureInfo cultureInfo;

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            
            //return AppCommonText.ResourceManager.GetString(Text, CultureInfo.CurrentCulture);
            //return AppCommonText.ResourceManager.GetString(Text, new CultureInfo(IntroPage.cultureInfo));
            return AppResources.ResourceManager.GetString(Text, new CultureInfo("fr"));
        }
    }
}

