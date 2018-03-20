using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace DigitalAlbum.ValueConverters
{
    public class TextToUniCodeSymbolCoverter : IValueConverter
    {
        public TextToUniCodeSymbolCoverter(){}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //ScreenHeight = BaseContentPage.screenHeight;
            string UniCodeValue = "";
            try
            {
                //double outValue = 0.0;
                //if (double.TryParse(parameter.ToString(), out outValue))
                //{
                //    percentHeight = ((ScreenHeight * (System.Convert.ToDouble(parameter))) / 100);
                //}

                /*
                var parameters = parameter as string[];
                if(string.IsNullOrEmpty((parameters[1]).ToString()))
                {
                    parameters[1] = GetSymbolValue((parameters[2]).ToString());
                }
                if(parameters[0] == SenderType.FromCSharp.ToString())
                {
                    UniCodeValue = HexToChar(parameters[1]).ToString();
                }
                else if(parameters[0] == SenderType.FromXAML.ToString())
                {
                    UniCodeValue = "&#x"+parameters[1]+";";
                }
                else
                {
                    
                }
                */

                UniCodeValue = GetSymbolValue(parameter.ToString());
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return UniCodeValue;
        }

        public static char HexToChar(string hex)
        {
            return (char)ushort.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }

        public static string CharToHex(char c)
        {
            return ((ushort)c).ToString("X4");
        }

        public static string GetSymbolValue(string key)
        {
            string strSymbol="";
            try
            {
                Dictionary<string, string> symbolsList = new Dictionary<string, string>()
                {
                    {"Plus","002B"},
                    {"Minus",""},
                    {"Multiply",""},
                    {"LeftArrow","2190"},
                    {"RightArrow","2192"},
                    {"HamBurger","2630"},
                    {"LeftMagnifier","1F50D"},
                    {"RightMagnifier",""}//,
                    //{"",""}
                };
                strSymbol = symbolsList.Where(X => X.Key == key).FirstOrDefault().Value;
                strSymbol = HexToChar(strSymbol).ToString();
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
            return strSymbol;
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

        public enum SenderType
        {
            FromXAML,
            FromCSharp
        }

    }
}

