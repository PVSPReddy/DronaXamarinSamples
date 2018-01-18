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

        void SubmitClicked(object sender, EventArgs e)
        {
            try
            {
                labelDataDisplay.Text = "";
                for (int i = 0; i < 100; i++ )
                {
                    var rows = (int)Math.Sqrt(i);
                    var columns = (i / rows);
                    var totalCellsFormed = rows * columns;
                    labelDataDisplay.Text +=  "\n\n i: " + i.ToString() +"Rows: " + rows.ToString() + "Columns: " + columns.ToString() + "TotalCellsFormed: " + totalCellsFormed.ToString();
                } 

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
