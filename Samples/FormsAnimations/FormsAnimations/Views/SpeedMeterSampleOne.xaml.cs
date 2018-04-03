using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FormsAnimations.Views
{
    public partial class SpeedMeterSampleOne : ContentPage
    {
        double screenHeight, screenWidth;
        bool stopAnimation = true;
        uint unitRotationSpeed = 1;
        //TimeSpan rotationSpeed = TimeSpan.FromTicks(100000);

        public SpeedMeterSampleOne()
        {
            InitializeComponent();

            screenHeight = (App.screenHeight * 1) / 100;
            screenWidth = (App.screenWidth * 1) / 100;

            gridOverLay.HeightRequest = screenWidth * 50;
            gridOverLay.WidthRequest = screenWidth * 60;

            getUnitsLayout();
        }

        private void getUnitsLayout()
        {
            try
            {
                var startPointX = absUnits.X;
                var endPointX = screenWidth * 20;//absUnits.Width;
                var startPointY= absUnits.Y;
                var endPointY = screenWidth * 50;//absUnits.Height;

                var numsStartPointY = (endPointY / 2) - ((screenWidth * 10) / 2);

                for (int i = 1; i <= 10; i++)
                {
                    NumbersLayout numbersLayout = new NumbersLayout(i);

                    if (i < 4)
                    {
                        var y = (screenWidth * 10) + (i * (screenWidth * 10));
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, (screenWidth * 10), (screenWidth * 10)));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        absUnits.Children.Add(numbersLayout);
                    }
                    else if (i < 9)
                    {

                        var y = endPointY - ((i - 3) * (screenWidth * 10));
                        var x = screenWidth * 10;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, (screenWidth * 10), (screenWidth * 10)));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        absUnits.Children.Add(numbersLayout);
                    }
                    else
                    {
                        var y = (i - 9) * (screenWidth * 10);
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, (screenWidth * 10), (screenWidth * 10)));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        absUnits.Children.Add(numbersLayout);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            } 
        }

        private void StartAnimationClicked(object sender, EventArgs e)
        {
            try
            {
                stopAnimation = true;
                //double x, y, height, width;
                //string valuesObtained = "";
                //var children = ((absUnits.Children));
                Device.StartTimer(TimeSpan.FromMilliseconds(unitRotationSpeed), startRotationAnimation);
                //foreach(var item in children)
                //{
                //    stopAnimation = false;
                //    startRotationAnimation(item);
                //    //Device.StartTimer(TimeSpan.FromMilliseconds(250), startRotationAnimation);
                //    //valuesObtained += item.X.ToString() + "\n" + item.Y.ToString() + "\n" +
                //    //                     item.Height.ToString() + "\n" + item.Width.ToString() + "\n";
                //    //x = item.X;
                //    //y = item.Y;
                //    //height = item.Height;
                //    //width = item.Width;
                //    //if(item.X<=0 && y== 0)
                //    //{
                //        ////if (y == 0)
                //        ////{
                //        ////    x = 0;
                //        ////    y = screenWidth * 10;
                //        ////}
                //        ////else if (y == screenWidth * 40)
                //        ////{
                //        ////    x = 0;
                //        ////    y = 0;
                //        ////}
                //        ////else
                //        ////{
                //        ////}
                //        //if(item.Y == ((screenWidth * 10) + (1 * (screenWidth * 10))))
                //        //{
                //        //    item.LayoutTo();
                //        //}
                //        //var y = ;
                //        //var x = 0;
                //    //}
                //}
                //System.Diagnostics.Debug.WriteLine(valuesObtained);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private bool startRotationAnimation()
        {
            try
            {
                var children = ((absUnits.Children));
                foreach (var item in children)
                {
                    double x, y, height, width;
                    x = item.X;
                    y = item.Y;
                    height = item.Height;
                    width = item.Width;
                    string valuesObtained = "";

                    if (x <= width && y <= 0)
                    {
                        x = x + 10;
                        y = 0;
                    }
                    else if (x >= width && y <= (screenWidth * 40))
                    {
                        x = width;
                        y = y + 10;
                    }
                    else if (x > 0 && y >= (screenWidth * 40))
                    {
                        x = x - 10;
                        y = (screenWidth * 40);
                    }
                    else if (x <= 0 && y >= 0)
                    {
                        x = 0;
                        y = y - 10;
                    }
                    else
                    {

                    }

                    item.LayoutTo((new Rectangle(x, y, height, width)), unitRotationSpeed, Easing.Linear);

                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return stopAnimation;
        }

        private void startRotationAnimation(View item)
        {
            try
            {
                double x, y, height, width;
                x = item.X;
                y = item.Y;
                height = item.Height;
                width = item.Width;
                string valuesObtained = "";
                while(!stopAnimation)
                {
                    //if (x <= 0 && y <= 0)
                    //{
                    //    x = x + 10;
                    //    y = 0;
                    //}
                    //else if (x >= width && y <= 0)
                    //{
                    //    x = width;
                    //    y = y + 10;
                    //}
                    //else if (x >= width && y >= Height)
                    //{
                    //    x = x - 10;
                    //    y = height;
                    //}
                    //else if (x <= 0 && y >= Height)
                    //{
                    //    x = 0;
                    //    y = y - 10;
                    //}
                    //else
                    //{

                    //}

                    if (x <= width && y <= 0)
                    {
                        x = x + 10;
                        y = 0;
                    }
                    else if (x >= width && y <= (screenWidth * 40))
                    {
                        x = width;
                        y = y + 10;
                    }
                    else if (x > 0 && y >= (screenWidth * 40))
                    {
                        x = x - 10;
                        y = (screenWidth * 40);
                    }
                    else if (x <= 0 && y >= 0)
                    {
                        x = 0;
                        y = y - 10;
                    }
                    else
                    {

                    }

                    item.LayoutTo((new Rectangle(x,y,height,width)), unitRotationSpeed, Easing.Linear);

                }
                //System.Diagnostics.Debug.WriteLine(valuesObtained);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
        private void StopAnimationClicked(object sender, EventArgs e)
        {
            try
            {
                stopAnimation = false;
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }


    public class NumbersLayout : StackLayout
    {
        double screenHeight, screenWidth;
        public NumbersLayout (int number)
        {
            screenHeight = (App.screenHeight * 1) / 100;
            screenWidth = (App.screenWidth * 1) / 100;
            Label displayNumber = new Label()
            {
                Text = number.ToString(),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            BackgroundColor = Color.AliceBlue;
            this.Children.Add(displayNumber);
            HeightRequest = screenWidth * 10;
            WidthRequest = screenWidth * 10;
        }
        
    }
}
