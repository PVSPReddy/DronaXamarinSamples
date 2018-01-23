using System;
using System.Collections.Generic;

using Xamarin.Forms;
using PunchCardExample;

namespace PunchCardExample.Views
{
    public partial class PunchCardTest : ContentPage
    {
        PunchCardFive dataTwo;
        public PunchCardTest()
        {
            InitializeComponent();
            /*
            data = new PunchCardFour(20, 6)
            {
                IsRoundCell = true,
                ShallSetDefaultPunch = false,
                PunchCellHeight = 20,
                PunchCellWidth = 20,
                PunchLayoutHeight = 200,
                PunchLayoutWidth = 200,
                PunchCellSpacing = 10,
                ShallSetDefaultPunched = true
            };
            */

            data.OnViewRequired += async (EventArgs e) => 
            {
                StackLayout startCard = new StackLayout()
                {
                    //HeightRequest=30,
                    //WidthRequest=30,
                    BackgroundColor = Color.Red
                };
                //await DisplayAlert("Alert", "This is working", "Ok");
                return startCard;
            };
            data.GetPunchCardView(10, 3);
            //ContentView data = new PunchCardFour(20, 6)
            //{
            //    IsRoundCell = true,
            //    ShallSetDefaultPunch = true,
            //    PunchCellHeight = 20,
            //    PunchCellWidth = 20,
            //    ShallSetDefaultPunched = true
            //};
            //StackPunchHolder.Children.Add(data);
            //SetPunchCard();
            dataTwo = new PunchCardFive(20, 6)
            {
                IsRoundCell = true,
                ShallSetDefaultPunch = false,
                PunchCellHeight = 20,
                PunchCellWidth = 20,
                PunchLayoutHeight = 200,
                PunchLayoutWidth = 200,
                PunchCellSpacing = 10,
                ShallSetDefaultPunched = true
            };
            dataTwo.OnViewRequired += async (EventArgs e) =>
            {
                StackLayout startCard = new StackLayout()
                {
                    //HeightRequest=30,
                    //WidthRequest=30,
                    BackgroundColor = Color.Red
                };
                //await DisplayAlert("Alert", "This is working", "Ok");
                return startCard;
            };
            SetPunchCard();
        }

        public async void SetPunchCard()
        {
            try
            {
                //data = new PunchCardFive(10, 3)
                //{
                //    IsRoundCell = true,
                //    PunchLayout = startCard,
                //    ShallSetDefaultPunch = false,
                //    PunchCellHeight = 30,
                //    PunchCellWidth = 30,
                //    PunchLayoutHeight = 400,
                //    PunchLayoutWidth = 300,
                //    PunchCellSpacing = 10,
                //    ShallSetDefaultPunched = true,
                //};
                var views = await dataTwo.GetPunchCardView(10, 3);
                StackPunchHolderTwo.Children.Add(views);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void BackButtonTapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public void FillLabelData(string fillableData)
        {
            try
            {
                //lblObtainedData.Text = fillableData;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
