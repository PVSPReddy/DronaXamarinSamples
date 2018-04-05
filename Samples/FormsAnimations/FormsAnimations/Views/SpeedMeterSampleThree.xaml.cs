using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsAnimations.Views
{
    public partial class SpeedMeterSampleThree : ContentPage
    {
        double screenHeight, screenWidth;
        bool stopAnimation = true;
        uint unitRotationSpeed = 1;
        int currentChild = 0, previousChild, nextChild;
        public SpeedMeterSampleThree()
        {
            InitializeComponent();

            screenHeight = (App.screenHeight * 1) / 100;
            screenWidth = (App.screenWidth * 1) / 100;

            gridOverLay.HeightRequest = screenWidth * 50;
            gridOverLay.WidthRequest = screenWidth * 60;

            getUnitsLayout();
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            try
            {
                stopAnimation = false;
                if (((Button)sender).Text == "Back")
                {
                    Navigation.PopModalAsync();
                }
                else
                {
                    Navigation.PushModalAsync(new SpeedMeterSampleFour());
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
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

                int startIndex = 0;
                int endIndex = 10;

                for (int i = startIndex; i <= endIndex; i++)
                {
                    NumbersLayoutThree numbersLayout = new NumbersLayoutThree(i);

                    if (i == startIndex)
                    {
                        var y = boxHeight;
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, boxWidth, boxHeight));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        absUnits.Children.Add(numbersLayout);
                    }
                    else if (i == endIndex)
                    {
                        var y = 0;
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

        View pastItem = null, currentItem = null, nextItem = null;
        private async Task<bool> startRotationAnimation()
        {
            try
            {
                var children = ((absUnits.Children));
                var interval = (screenWidth * 5);

                if (currentChild > (absUnits.Children.Count - 1))
                {
                    currentChild = 0;
                }

                double x, y, height, width;

                x = 0;
                y = 0;
                height = screenWidth * 10;
                width = screenWidth * 10;

                if (stopAnimation)
                {
                    if (pastItem != null)
                    {
                        if (pastItem.Opacity == 0)
                        {
                            pastItem.Opacity = 1;
                        }
                    }

                    if (currentChild == 0)
                    {
                        pastItem = absUnits.Children[(absUnits.Children.Count - 1)];
                        currentItem = absUnits.Children[currentChild];
                        nextItem = absUnits.Children[currentChild + 1];
                    }
                    else if (currentChild == (absUnits.Children.Count - 1))
                    {
                        pastItem = absUnits.Children[currentChild - 1];
                        currentItem = absUnits.Children[currentChild];
                        nextItem = absUnits.Children[0];
                    }
                    else
                    {
                        pastItem = absUnits.Children[currentChild - 1];
                        currentItem = absUnits.Children[currentChild];
                        nextItem = absUnits.Children[currentChild + 1];
                    }

                    if (pastItem.Y <= 0)
                    {
                        currentChild++;
                        pastItem.Opacity = 0;
                        pastItem.LayoutTo((new Rectangle(x, (height * 2), height, width)), unitRotationSpeed, Easing.Linear);
                        currentItem.LayoutTo((new Rectangle(x, ((currentItem.Y) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                        if (currentItem.Y <= height)
                        {
                            nextItem.LayoutTo((new Rectangle(x, ((currentItem.Y + height) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                        }
                        //nextItem.LayoutTo((new Rectangle(x, ((currentItem.Y + height) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                    }
                    else
                    {
                        pastItem.LayoutTo((new Rectangle(x, ((pastItem.Y) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                        currentItem.LayoutTo((new Rectangle(x, ((pastItem.Y + height) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                        if (currentItem.Y <= height)
                        {
                            nextItem.LayoutTo((new Rectangle(x, ((currentItem.Y + height) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                        }
                    }
                }
                else
                {
                    var a = ((pastItem.Y - height) > 0) ? (pastItem.Y - height) : (height - pastItem.Y);
                    var b = ((currentItem.Y - height) > 0) ? (currentItem.Y - height) : (height - currentItem.Y);
                    var c = ((nextItem.Y - height) > 0) ? (nextItem.Y - height) : (height - nextItem.Y);
                    View _pastItem, _currentItem, _nextItem;

                    int selectedId = currentChild;

                    if (a < b)
                    {
                        if (a < c)
                        {
                            _currentItem = pastItem;
                        }
                        else
                        {
                            _currentItem = nextItem;
                        }
                    }
                    else
                    {
                        if (b < c)
                        {
                            _currentItem = currentItem;
                        }
                        else
                        {
                            _currentItem = nextItem;
                        }
                    }

                    currentChild = ((absUnits.Children).IndexOf(_currentItem));
                    System.Diagnostics.Debug.WriteLine(currentChild.ToString());

                    if (currentChild == 0)
                    {
                        _pastItem = absUnits.Children[(absUnits.Children.Count - 1)];
                        _currentItem = absUnits.Children[currentChild];
                        _nextItem = absUnits.Children[currentChild + 1];
                    }
                    else if (currentChild == (absUnits.Children.Count - 1))
                    {
                        _pastItem = absUnits.Children[currentChild - 1];
                        _currentItem = absUnits.Children[currentChild];
                        _nextItem = absUnits.Children[0];
                    }
                    else
                    {
                        _pastItem = absUnits.Children[currentChild - 1];
                        _currentItem = absUnits.Children[currentChild];
                        _nextItem = absUnits.Children[currentChild + 1];
                    }

                    _pastItem.LayoutTo((new Rectangle(x, 0, height, width)), unitRotationSpeed, Easing.Linear);
                    _currentItem.LayoutTo((new Rectangle(x, (height), height, width)), unitRotationSpeed, Easing.Linear);
                    _nextItem.LayoutTo((new Rectangle(x, (height * 2), height, width)), unitRotationSpeed, Easing.Linear);

                    if (_pastItem.Opacity == 0)
                    {
                        _pastItem.Opacity = 1;
                    }
                    if (_currentItem.Opacity == 0)
                    {
                        _currentItem.Opacity = 1;
                    }
                    if (_nextItem.Opacity == 0)
                    {
                        _nextItem.Opacity = 1;
                    }

                    var _selectedItem = absUnits.Children[currentChild];
                    var selectedItem = ((((StackLayout)_selectedItem).Children)[0]);
                    var selection = Convert.ToInt32(((Label)selectedItem).Text);
                    labelResult.Text = (selection).ToString();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return stopAnimation;
        }

        private async void StopAnimationClicked(object sender, EventArgs e)
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

    public class NumbersLayoutThree : StackLayout
    {
        double screenHeight, screenWidth;
        public NumbersLayoutThree(int number)
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
            BackgroundColor = Color.Transparent;
            this.Children.Add(displayNumber);
            HeightRequest = screenWidth * 10;
            WidthRequest = screenWidth * 10;
        }

    }
}
