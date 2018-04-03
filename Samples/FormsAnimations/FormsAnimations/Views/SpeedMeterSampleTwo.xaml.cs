using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsAnimations.Views
{
    public partial class SpeedMeterSampleTwo : ContentPage
    {
        double screenHeight, screenWidth;
        bool stopAnimation = true;
        uint unitRotationSpeed = 250;
        int currentChild = 0, previousChild, nextChild;
        //TimeSpan rotationSpeed = TimeSpan.FromTicks(100000);
        public SpeedMeterSampleTwo()
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
            var boxHeight = screenWidth * 10;
            var boxWidth = screenWidth * 10;
            try
            {
                var startPointX = absUnits.X;
                var endPointX = screenWidth * 20;//absUnits.Width;
                var startPointY = absUnits.Y;
                var endPointY = screenWidth * 50;//absUnits.Height;

                var numsStartPointY = (endPointY / 2) - ((screenWidth * 10) / 2);

                for (int i = 1; i <= 10; i++)
                {
                    NumbersLayout numbersLayout = new NumbersLayout(i);

                    if (i == 1)
                    {
                        var y = boxHeight;
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, boxWidth, boxHeight));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        absUnits.Children.Add(numbersLayout);
                    }
                    else
                    {
                        var y = boxHeight * 2;
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, boxWidth, boxHeight));
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
                Device.StartTimer(TimeSpan.FromMilliseconds(unitRotationSpeed), startRotationAnimie);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        private bool startRotationAnimie()
        {
            try
            {
                startRotationAnimation();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return stopAnimation;
        }

        private async Task<bool> startRotationAnimation()
        {
            try
            {
                var children = ((absUnits.Children));
                var interval = (screenWidth);

                if (currentChild > (absUnits.Children.Count - 1))
                {
                    currentChild = 0;
                }
                var item = absUnits.Children[currentChild];
                double x, y, height, width;
                x = item.X;
                y = item.Y;
                height = item.Height;
                width = item.Width;

                for (double i = item.Y; ; i--)
                {
                    if (i <= 0)
                    {
                        currentChild++;
                        if (currentChild > (absUnits.Children.Count - 1))
                        {
                            currentChild = 0;
                        }
                        y = height * 2;
                    }
                    else if(i<height)
                    {

                        nextChild = currentChild++;
                        if (nextChild > (absUnits.Children.Count - 1))
                        {
                            nextChild = 0;
                        }
                        var nextItem = absUnits.Children[nextChild];
                        await nextItem.LayoutTo((new Rectangle((nextItem.X), (nextItem.Y + interval), height, width)), unitRotationSpeed, Easing.Linear);
                    }
                    //if(i>height)
                    item.LayoutTo((new Rectangle(x, y, height, width)), unitRotationSpeed, Easing.Linear);
                }


                //if (y <= 0)
                //{
                //    x = 0;
                //    y = height * 2;
                //    item.Opacity = 0;
                //}
                //else if (y >= (height * 2))
                //{
                //    x = 0;
                //    y = y - interval;
                //    item.Opacity = 1;
                //}
                //else
                //{
                //    x = 0;
                //    y = y - interval;
                //    if (item.Opacity == 0)
                //    {
                //        item.Opacity = 1;
                //    }
                //}


                if (item.Y < (height / 4))
                {
                    previousChild = currentChild;
                    //continuePreviousChildAnimation(previousChild);
                    currentChild++;
                    //y = height * 2;
                    //item.Opacity = 0;
                    System.Diagnostics.Debug.WriteLine(currentChild.ToString());
                }

                item.LayoutTo((new Rectangle(x, y, height, width)), unitRotationSpeed, Easing.Linear);


            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return stopAnimation;
        }

        /*
        private bool startRotationAnimation()
        {
            try
            {
                var children = ((absUnits.Children));
                var interval = (screenWidth);

                if (currentChild > (absUnits.Children.Count - 1))
                {
                    currentChild = 0;
                }
                var item = absUnits.Children[currentChild];
                double x, y, height, width;
                x = item.X;
                y = item.Y;
                height = item.Height;
                width = item.Width;

                var firstItem = children[0];

                if (y <= 0)
                {
                    x = 0;
                    y = height * 2;
                    item.Opacity = 0;
                }
                else if (y >= (height * 2))
                {
                    x = 0;
                    y = y - interval;
                    item.Opacity = 1;
                }
                else
                {

                    x = 0;
                    y = y - interval;
                    if (item.Opacity == 0)
                    {
                        item.Opacity = 1;
                    }
                }


                if (item.Y < (height / 4))
                {
                    previousChild = currentChild;
                    continuePreviousChildAnimation(previousChild);
                    currentChild++;
                    //y = height * 2;
                    //item.Opacity = 0;
                    System.Diagnostics.Debug.WriteLine(currentChild.ToString());
                }

                item.LayoutTo((new Rectangle(x, y, height, width)), unitRotationSpeed, Easing.Linear);


            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return stopAnimation;
        }

        private void continuePreviousChildAnimation(int child)
        {
            try
            {
                var children = ((absUnits.Children));
                var interval = (screenWidth);

                var item = absUnits.Children[child];
                double x, y, height, width;
                x = item.X;
                y = item.Y;
                height = item.Height;
                width = item.Width;

                if (y <= 0)
                {
                    x = 0;
                    y = height * 2;
                    item.Opacity = 0;
                }
                else if (y >= (height * 2))
                {
                    x = 0;
                    y = y - interval;
                    item.Opacity = 1;
                }
                else
                {

                    x = 0;
                    y = y - interval;
                    if (item.Opacity == 0)
                    {
                        item.Opacity = 1;
                    }
                }
                item.LayoutTo((new Rectangle(x, y, height, width)), unitRotationSpeed, Easing.Linear);


            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
        */

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

        private void BackButtonClicked(object sender, EventArgs e)
        {
            try
            {
                stopAnimation = false;
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
    }

    public class NumbersLayoutTwo : StackLayout
    {
        double screenHeight, screenWidth;
        public NumbersLayoutTwo(int number)
        {
            screenHeight = (App.screenHeight * 1) / 100;
            screenWidth = (App.screenWidth * 1) / 100;
            Label displayNumber = new Label()
            {
                Text = number.ToString(),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
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
