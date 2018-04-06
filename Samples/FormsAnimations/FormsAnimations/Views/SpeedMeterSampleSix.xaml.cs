using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsAnimations.Views
{
    public partial class SpeedMeterSampleSix : ContentPage
    {
        #region for global variables
        double screenHeight, screenWidth;
        bool stopAnimation = true;
        uint unitRotationSpeed = 1;
        int currentUnitsChildNo = 0, currentTensChildNo, currentHundredsChildNo, currentThousandsChildNo;
        int currentUnitsChildValue = 0, currentTensChildValue, currentHundredsChildValue, currentThousandsChildValue;
        #endregion

        public SpeedMeterSampleSix()
        {
            InitializeComponent();

            screenHeight = (App.screenHeight * 1) / 100;
            screenWidth = (App.screenWidth * 1) / 100;

            gridOverLay.HeightRequest = screenWidth * 50;
            gridOverLay.WidthRequest = screenWidth * 60;

            getAllLayouts();
        }

        #region for back button click event
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
                    Navigation.PushModalAsync(new SpeedMeterSampleSeven());
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
        #endregion

        #region to call get all layouts of speed meter
        private async void getAllLayouts()
        {
            try
            {
                await getLayouts(absUnits);

                await getLayouts(absTens);

                await getLayouts(absHundreds);

                await getLayouts(absThousands);
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
        #endregion

        #region to call all layouts of speed meter
        private async Task<bool> getLayouts(AbsoluteLayout availableLayout)
        {
            var boxHeight = screenWidth * 10;
            var boxWidth = screenWidth * 10;
            try
            {
                var startPointX = availableLayout.X;
                var endPointX = screenWidth * 20;//absUnits.Width;
                var startPointY = availableLayout.Y;
                var endPointY = screenWidth * 50;//absUnits.Height;

                var numsStartPointY = (endPointY / 2) - ((screenWidth * 10) / 2);

                int startIndex = 0;
                int endIndex = 9;

                for (int i = startIndex; i <= endIndex; i++)
                {
                    NumbersLayoutSix numbersLayout = new NumbersLayoutSix(i);

                    if (i == startIndex)
                    {
                        var y = boxHeight;
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, boxWidth, boxHeight));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        availableLayout.Children.Add(numbersLayout);
                    }
                    else if (i == endIndex)
                    {
                        var y = 0;
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, boxWidth, boxHeight));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        availableLayout.Children.Add(numbersLayout);
                    }
                    else
                    {
                        var y = boxHeight * 2;
                        var x = 0;
                        AbsoluteLayout.SetLayoutBounds(numbersLayout, new Rectangle(x, y, boxWidth, boxHeight));
                        AbsoluteLayout.SetLayoutFlags(numbersLayout, AbsoluteLayoutFlags.None);
                        availableLayout.Children.Add(numbersLayout);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return true;
        }
        #endregion

        #region to call timer of Animation start
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
        #endregion

        #region to call Animation start
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
        #endregion

        #region animation of common units starts here
        private int moveAbsCommon(AbsoluteLayout currentLayout, int currentItemNo, double interval)
        {
            try
            {
                double x, y, height, width;
                x = 0;
                y = 0;
                height = screenWidth * 10;
                width = screenWidth * 10;

                View pastItem, currentItem, nextItem;
                if (currentItemNo == 0)
                {
                    pastItem = currentLayout.Children[(currentLayout.Children.Count - 1)];
                    currentItem = currentLayout.Children[currentItemNo];
                    nextItem = currentLayout.Children[currentItemNo + 1];
                }
                else if (currentItemNo == (currentLayout.Children.Count - 1))
                {
                    pastItem = currentLayout.Children[currentItemNo - 1];
                    currentItem = currentLayout.Children[currentItemNo];
                    nextItem = currentLayout.Children[0];
                }
                else
                {
                    pastItem = currentLayout.Children[currentItemNo - 1];
                    currentItem = currentLayout.Children[currentItemNo];
                    nextItem = currentLayout.Children[currentItemNo + 1];
                }

                if (nextItem != null)
                {
                    if (nextItem.Opacity == 0)
                    {
                        nextItem.Opacity = 1;
                    }
                }


                if (pastItem.Y <= (interval / 4))
                {
                    currentItemNo++;
                    pastItem.Opacity = 0;
                    pastItem.LayoutTo((new Rectangle(x, (height * 2), height, width)));
                    currentItem.LayoutTo((new Rectangle(x, ((currentItem.Y) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                    if (currentItem.Y <= height)
                    {
                        nextItem.LayoutTo((new Rectangle(x, ((currentItem.Y + height) - interval), height, width)), unitRotationSpeed, Easing.Linear);
                    }
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
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return currentItemNo;
        }
        #endregion

        #region animation of units place starts here
        private async Task<bool> startRotationAnimation()
        {
            try
            {
                var interval = (screenWidth * 5);
                if (stopAnimation)
                {
                    if (currentUnitsChildNo > absUnits.Children.Count - 1)
                    {
                        currentUnitsChildNo = 0;
                        currentTensChildNo++;
                        moveAbsTens(interval);
                    }
                    currentUnitsChildNo = moveAbsCommon(absUnits, currentUnitsChildNo, interval);
                }
                else
                {
                    currentUnitsChildValue = 0;
                    currentUnitsChildValue = await AfterEffects(absUnits, currentUnitsChildNo);
                    //currentTensChildNo += (currentUnitsChildValue == 0) ? (1) : (0);
                    moveAbsTens(interval);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
            return stopAnimation;
        }
        #endregion

        #region animation of Tens place starts here
        private async void moveAbsTens(double interval)
        {
            try
            {
                if (stopAnimation)
                {
                    if (currentTensChildNo > absTens.Children.Count - 1)
                    {
                        currentTensChildNo = 0;
                        currentHundredsChildNo++;
                        moveAbsHundreds(interval);
                    }
                    currentTensChildNo = moveAbsCommon(absTens, currentTensChildNo, (screenWidth * 10));
                }
                else
                {
                    currentTensChildValue = 0;
                    currentTensChildValue = (await AfterEffects(absTens, currentTensChildNo)) * 10;
                    //currentHundredsChildNo += (currentTensChildValue == 0) ? (1) : (0);
                    moveAbsHundreds(interval);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
        #endregion

        #region animation of Hundreds place starts here
        private async void moveAbsHundreds(double interval)
        {
            try
            {
                if (stopAnimation)
                {
                    if (currentHundredsChildNo > absHundreds.Children.Count - 1)
                    {
                        currentHundredsChildNo = 0;
                        currentThousandsChildNo++;
                        moveAbsThousands(interval);
                    }
                    currentHundredsChildNo = moveAbsCommon(absHundreds, currentHundredsChildNo, (screenWidth * 10));
                }
                else
                {
                    currentHundredsChildValue = 0;
                    currentHundredsChildValue = (await AfterEffects(absHundreds, currentHundredsChildNo)) * 100;
                    //currentThousandsChildNo += (currentHundredsChildValue == 0) ? (1) : (0);
                    moveAbsThousands(interval);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
        #endregion

        #region animation of Thousands place starts here
        private async void moveAbsThousands(double interval)
        {
            try
            {
                if (stopAnimation)
                {
                    if (currentThousandsChildNo > absThousands.Children.Count - 1)
                    {
                        currentThousandsChildNo = 0;
                    }
                    currentThousandsChildNo = moveAbsCommon(absThousands, currentThousandsChildNo, (screenWidth * 10));
                }
                else
                {
                    currentThousandsChildValue = 0;
                    currentThousandsChildValue = (await AfterEffects(absThousands, currentThousandsChildNo)) * 1000;

                    var total = currentThousandsChildValue + currentHundredsChildValue + currentTensChildValue + currentUnitsChildValue;

                    labelResult.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }
        #endregion

        #region to stop animations
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
        #endregion

        #region for after effect of animation
        public async Task<int> AfterEffects(AbsoluteLayout currentLayout, int currentItemNo)
        {
            try
            {
                double x, y, height, width;
                x = 0;
                y = 0;
                height = screenWidth * 10;
                width = screenWidth * 10;

                View pastItem, currentItem, nextItem;
                if (currentItemNo == 0)
                {
                    pastItem = currentLayout.Children[(currentLayout.Children.Count - 1)];
                    currentItem = currentLayout.Children[currentItemNo];
                    nextItem = currentLayout.Children[currentItemNo + 1];
                }
                else if (currentItemNo == (currentLayout.Children.Count - 1))
                {
                    pastItem = currentLayout.Children[currentItemNo - 1];
                    currentItem = currentLayout.Children[currentItemNo];
                    nextItem = currentLayout.Children[0];
                }
                else
                {
                    pastItem = currentLayout.Children[currentItemNo - 1];
                    currentItem = currentLayout.Children[currentItemNo];
                    nextItem = currentLayout.Children[currentItemNo + 1];
                }

                var a = ((pastItem.Y - height) > 0) ? (pastItem.Y - height) : (height - pastItem.Y);
                var b = ((currentItem.Y - height) > 0) ? (currentItem.Y - height) : (height - currentItem.Y);
                var c = ((nextItem.Y - height) > 0) ? (nextItem.Y - height) : (height - nextItem.Y);
                View _pastItem, _currentItem, _nextItem;

                int selectedId = currentItemNo;

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

                currentItemNo = ((currentLayout.Children).IndexOf(_currentItem));
                System.Diagnostics.Debug.WriteLine(currentItemNo.ToString());

                if (currentItemNo == 0)
                {
                    _pastItem = currentLayout.Children[(currentLayout.Children.Count - 1)];
                    _currentItem = currentLayout.Children[currentItemNo];
                    _nextItem = currentLayout.Children[currentItemNo + 1];
                }
                else if (currentItemNo == (currentLayout.Children.Count - 1))
                {
                    _pastItem = currentLayout.Children[currentItemNo - 1];
                    _currentItem = currentLayout.Children[currentItemNo];
                    _nextItem = currentLayout.Children[0];
                }
                else
                {
                    _pastItem = currentLayout.Children[currentItemNo - 1];
                    _currentItem = currentLayout.Children[currentItemNo];
                    _nextItem = currentLayout.Children[currentItemNo + 1];
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

                var _selectedItem = currentLayout.Children[currentItemNo];
                var selectedItem = ((((StackLayout)_selectedItem).Children)[0]);
                var selection = Convert.ToInt32(((Label)selectedItem).Text);
                labelResult.Text = (selection).ToString();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return currentItemNo;
        }
        #endregion
    }

    class NumbersLayoutSix : StackLayout
    {
        double screenHeight, screenWidth;
        public NumbersLayoutSix(int number)
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
