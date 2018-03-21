using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DynamicXAML.ValueConverters
{
    class HeightConverter : IValueConverter
    {
        public static int ScreenHeight;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ScreenHeight = App.screenHeight;
            double percentHeight = 0.0;
            try
            {
                double outValue = 0.0;
                if(double.TryParse(parameter.ToString(), out outValue))
                {
                    percentHeight = ((ScreenHeight * (System.Convert.ToDouble(parameter)))/100);
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
            return percentHeight;
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
