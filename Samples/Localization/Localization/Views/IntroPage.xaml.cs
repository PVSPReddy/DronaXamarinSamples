using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Globalization;
using Xamarin.Forms;

namespace Localization.Views
{
    public partial class IntroPage : ContentPage
    {
        
        public static string cultureInfo;
        Dictionary<string, string> languagesDictionary;
        public IntroPage()
        {
            InitializeComponent();
            languagesDictionary = new Dictionary<string, string>()
            {
                {"fr-FR", "French"},
                {"ja-JP", "Japanese (Japan)"},
                {"zh-TW", "Chinese"},
                {"es-MX", "Spanish"},
                {"de-CH", "German"}
            };
            List<string> languageItems = new List<string>();
            foreach(var item in languagesDictionary)
            {
                languageItems.Add(item.Value);
            }
            languagePicker.ItemsSource = languageItems;
            languagePicker.SelectedIndexChanged += async (object sender, EventArgs e) => 
            {
                try
                {
                    if(languagePicker.SelectedIndex > -1)
                    {
                        var doesContainValue = languagesDictionary.ContainsValue(languagePicker.SelectedItem.ToString());
                        if(doesContainValue)
                        {
                            foreach(var item in languagesDictionary)
                            {
                                if(item.Value == languagePicker.SelectedItem.ToString())
                                {
                                    cultureInfo = item.Key;
                                }
                            }
                        }
                        else
                        {
                            
                        }
                        //cultureInfo = ((Dictionary<string,string>)(languagesDictionary.Where(X => X.Value == languagePicker.SelectedItem.ToString()))).Keys[0];
                    }
                    else
                    {
                        
                    }
                }
                catch(Exception ex)
                {
                    var msg = ex.Message + "\n" + ex.StackTrace;
                    System.Diagnostics.Debug.WriteLine(msg);
                }
            };
        }

        private void SubmitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushModalAsync(new TestPage());
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }
}
