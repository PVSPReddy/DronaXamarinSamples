using System;

using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DynamicXAML
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }


        //public void DisplayAlert(string title, string message)
        //{
        //          //DisplayAlert(title, message);

        //    string[] values = { title, message };
        //          Xamarin.Forms.MessagingCenter.Send<Bas string[]>(this, "DisplayAlert", values);
        //}
    }
}