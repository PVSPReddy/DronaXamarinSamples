using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigitalAlbum.ValueConverters
{
    class WidthConverter : IValueConverter
    {
        public static int ScreenWidth;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ScreenWidth = BaseContentPage.screenHeight;
            double percentWidth = 0.0;
            try
            {
                double outValue = 0.0;
                if (double.TryParse(parameter.ToString(), out outValue))
                {
                    percentWidth = ((ScreenWidth * (System.Convert.ToDouble(parameter))) / 100);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return percentWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return null;
        }
    }
}
