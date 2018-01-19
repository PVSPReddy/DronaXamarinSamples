using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Calibrations.Views
{
    public partial class PunchCardCalibrations : ContentPage
    {
        public PunchCardCalibrations()
        {
            InitializeComponent();
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

        async void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                var options = new string[]
                {
                    "UseEqualRowsAndColumnsIrrespectiveOfScreenSize",
                    "UseEqualRowsAndColumnsWithRespectiveToScreenSize",
                    "UseRestrictedColumnsOrRows"
                };
                var Choice = await DisplayActionSheet("Select Calibration Method", "Cancel", null, options);
                switch(Choice)
                {
                    case "UseEqualRowsAndColumnsIrrespectiveOfScreenSize":
                        UseEqualRowsAndColumnsIrrespectiveOfScreenSize();
                        break;
                    case "UseEqualRowsAndColumnsWithRespectiveToScreenSize":
                        await DisplayAlert("Alert", "Not Available in this version", "Ok");
                        break;
                    case "UseRestrictedColumnsOrRows":
                        int outValue = 0, cellsNumber=5;
                        if (Int32.TryParse(entryValidCellsNumber.Text, out outValue))
                        {
                            cellsNumber = (Convert.ToInt32(entryValidCellsNumber.Text) == 0) ? 3 : Convert.ToInt32(entryValidCellsNumber.Text);
                            UseRestrictedColumnsOrRows(cellsNumber);
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Cells number should not be Empty", "Ok");
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public void UseEqualRowsAndColumnsIrrespectiveOfScreenSize()
        {
            try
            {
                labelDataDisplay.Text = "";
                int outValue = 0, startNumber = 1, endNumber = 100;
                if (Int32.TryParse(entryStartNumber.Text, out outValue))
                {
                    startNumber = (Convert.ToInt32(entryStartNumber.Text) == 0) ? 1 : Convert.ToInt32(entryStartNumber.Text);
                }

                if (Int32.TryParse(entryEndNumber.Text, out outValue))
                {
                    endNumber = (Convert.ToInt32(entryEndNumber.Text) == 0) ? 100 : Convert.ToInt32(entryEndNumber.Text);
                }

                for (int i = startNumber; i <= endNumber; i++)
                {
                    var columns = (int)Math.Sqrt(i);//columns
                    while ((columns * columns) < i)
                    {
                        columns++;
                    };
                    var rows = columns;//rows

                    if ((columns * rows) > i)
                    {
                        var difference = (columns * rows) - i;
                        if (difference >= columns)
                        {
                            rows--;
                        }
                    }

                    var totalCellsFormed = rows * columns;
                    bool isValidMatrix = false;
                    if (totalCellsFormed >= i)
                    {
                        isValidMatrix = true;
                    }
                    else
                    {
                        isValidMatrix = false;
                    }


                    labelDataDisplay.Text += "\n\n i: \t" + i.ToString() + "\nRows: \t" + rows.ToString() + "\nColumns: \t" + columns.ToString() + "\nTotalCellsFormed: \t" + totalCellsFormed.ToString() + "\n Is Valid Matrix: \t" + isValidMatrix.ToString();
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public void UseEqualRowsAndColumnsWithRespectiveToScreenSize(double cellSize ,double screenSize, double spacings)
        {
            try
            {
                labelDataDisplay.Text = "";
                int outValue = 0, startNumber = 1, endNumber = 100;
                if (Int32.TryParse(entryStartNumber.Text, out outValue))
                {
                    startNumber = (Convert.ToInt32(entryStartNumber.Text) == 0) ? 1 : Convert.ToInt32(entryStartNumber.Text);
                }

                if (Int32.TryParse(entryEndNumber.Text, out outValue))
                {
                    endNumber = (Convert.ToInt32(entryEndNumber.Text) == 0) ? 100 : Convert.ToInt32(entryEndNumber.Text);
                }

                var columnSize = cellSize + spacings;
                var noOfValidColumns = (int)(screenSize / columnSize);

                for (int i = startNumber; i <= endNumber; i++)
                {
                    var columns = (int)Math.Sqrt(i);//columns
                    while ((columns * columns) < i)
                    {
                        columns++;
                    };
                    var rows = columns;//rows

                    if(columns > noOfValidColumns)
                    {
                        columns = noOfValidColumns;
                        rows = (int)(i / columns);
                        while((columns * rows) >= i)
                        {
                            rows++;
                        }
                    }

                    if ((columns * rows) > i)
                    {
                        var difference = (columns * rows) - i;
                        if (difference >= columns)
                        {
                            rows--;
                        }
                    }

                    var totalCellsFormed = rows * columns;
                    bool isValidMatrix = false;
                    if (totalCellsFormed >= i)
                    {
                        isValidMatrix = true;
                    }
                    else
                    {
                        isValidMatrix = false;
                    }


                    labelDataDisplay.Text += "\n\n i: \t" + i.ToString() + "\nRows: \t" + rows.ToString() + "\nColumns: \t" + columns.ToString() + "\nTotalCellsFormed: \t" + totalCellsFormed.ToString() + "\n Is Valid Matrix: \t" + isValidMatrix.ToString();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }


        public void UseRestrictedColumnsOrRows(int noOfValidColumns)
        {
            try
            {
                labelDataDisplay.Text = "";
                int outValue = 0, startNumber = 1, endNumber = 100;
                if (Int32.TryParse(entryStartNumber.Text, out outValue))
                {
                    startNumber = (Convert.ToInt32(entryStartNumber.Text) == 0) ? 1 : Convert.ToInt32(entryStartNumber.Text);
                }

                if (Int32.TryParse(entryEndNumber.Text, out outValue))
                {
                    endNumber = (Convert.ToInt32(entryEndNumber.Text) == 0) ? 100 : Convert.ToInt32(entryEndNumber.Text);
                }

                for (int i = startNumber; i <= endNumber; i++)
                {
                    var columns = (int)Math.Sqrt(i);//columns
                    while ((columns * columns) < i)
                    {
                        columns++;
                    };
                    var rows = columns;//rows

                    if (columns > noOfValidColumns)
                    {
                        columns = noOfValidColumns;
                        rows = (int)(i / columns);
                        while ((columns * rows) <= i)
                        {
                            rows++;
                        }
                    }

                    if ((columns * rows) > i)
                    {
                        var difference = (columns * rows) - i;
                        if (difference >= columns)
                        {
                            rows--;
                        }
                    }

                    var totalCellsFormed = rows * columns;
                    bool isValidMatrix = false;
                    if (totalCellsFormed >= i)
                    {
                        isValidMatrix = true;
                    }
                    else
                    {
                        isValidMatrix = false;
                    }


                    labelDataDisplay.Text += "\n\n i: \t" + i.ToString() + "\nRows: \t" + rows.ToString() + "\nColumns: \t" + columns.ToString() + "\nTotalCellsFormed: \t" + totalCellsFormed.ToString() + "\n Is Valid Matrix: \t" + isValidMatrix.ToString();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

    }
}
