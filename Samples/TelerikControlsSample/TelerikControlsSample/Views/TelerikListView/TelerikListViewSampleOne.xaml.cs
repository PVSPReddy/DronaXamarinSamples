using System;
using System.Collections.Generic;
using Telerik.XamarinForms.DataControls;
using Telerik.XamarinForms.DataControls.ListView;


using Xamarin.Forms;

namespace TelerikControlsSample.Views.TelerikListView
{
    public partial class TelerikListViewSampleOne : ContentPage
    {
        public TelerikListViewSampleOne()
        {
            InitializeComponent();

            #region for page Header 
            Label labelHeaderTitle = new Label()
            {
                Text = "Telerik Controls ListView",
                TextColor = Color.BurlyWood,
                BackgroundColor=Color.Green,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            StackLayout stackHeaderHolder = new StackLayout()
            {
                Children = { labelHeaderTitle },
                BackgroundColor = Color.Maroon,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            #endregion

            #region for page Body 
            var telerikListView = new RadListView()
            {
                ItemsSource = new ViewModel().Source,
                ItemTemplate = new DataTemplate(()=>
                {
                    var label = new Label()
                    {
                        Margin = new Thickness(10),
                        TextColor = Color.Blue 
                    };
                    var content = new Grid();
                    content.Children.Add(label);
                    label.SetBinding(Label.TextProperty, new Binding(nameof(SourceItem.Name)));

                    return new ListViewTemplateCell
                    {
                        View = content
                    };
                }),
            };

            StackLayout stackBodyHolder = new StackLayout()
            {
                Children = { telerikListView },
                BackgroundColor = Color.Teal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            #endregion

            #region for main Layout
            AbsoluteLayout absHolder = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            AbsoluteLayout.SetLayoutBounds(stackHeaderHolder, new Rectangle(0, 0, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(stackHeaderHolder, AbsoluteLayoutFlags.All);
            absHolder.Children.Add(stackHeaderHolder);

            AbsoluteLayout.SetLayoutBounds(stackBodyHolder, new Rectangle(1, 1, 1, 0.9));
            AbsoluteLayout.SetLayoutFlags(stackBodyHolder, AbsoluteLayoutFlags.All);
            absHolder.Children.Add(stackBodyHolder);


            Content = absHolder;
            #endregion
        }
    }


    public class SourceItem
    {
        public SourceItem(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }

    public class ViewModel
    {
        public ViewModel()
        {
            this.Source = new List<SourceItem> { new SourceItem("Tom"), new SourceItem("Anna"), new SourceItem("Peter"), new SourceItem("Teodor"), new SourceItem("Lorenzo"), new SourceItem("Andrea"), new SourceItem("Martin") };
        }
        public List<SourceItem> Source { get; set; }
    }
}
