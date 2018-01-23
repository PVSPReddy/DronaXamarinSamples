using System;

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PunchCardExample
{
    public class PunchCardFive// : ContentView
    {
        public View PunchLayout { get; set; }
        public View PunchCompletedLayout { get; set; }
        public bool ShallSetDefaultPunch { get; set; }
        public bool ShallSetDefaultPunched { get; set; }
        public int PunchesNumber { get; set; }
        public int PunchedNumber { get; set; }
        public double PunchLayoutHeight { get; set; }
        public double PunchLayoutWidth { get; set; }
        public double PunchCellHeight { get; set; }
        public double PunchCellWidth { get; set; }
        public double PunchCellSpacing { get; set; }
        public bool IsRoundCell { get; set; }

        #region for trail one and successful event handler
        public event EventHandler Gestured;
        protected void Button1_Click(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            if (this.Gestured != null)
            {
                this.Gestured(this, e);
            }
        }
        #endregion
        //RelativeLayout relativePunchCard;
        //StackLayout stackPunchCard;
        #region for trail Two and successful user defined event handler
        public event CreateView OnViewRequired;
        public delegate Task<View> CreateView(EventArgs e);
        public async Task<View> OnCalledForView(EventArgs e)
        {
            try
            {
                if (OnViewRequired != null)
                {
                    return await OnViewRequired(e);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
        #endregion

        public PunchCardFive()
        {
            //stackPunchCard = new StackLayout();
            //Content = stackPunchCard;
            //Content = (View)((object)GetPunchCardView(PunchesNumber, PunchedNumber));
        }

        public PunchCardFive(int PunchesNumber, int PunchedNumber)
        {
            //stackPunchCard = new StackLayout();
            //Content = stackPunchCard;
            //Content = (View)((object)GetPunchCardView(PunchesNumber, PunchedNumber));
        }

        public async Task<View> GetPunchCardView(int PunchesNumber, int PunchedNumber)
        {
            //if(ShallSetDefaultPunch == false && PunchLayout == null)
            //{
            //    throw new NullReferenceException("Layout to be displayed after punch accessed is not available. To overcome this error either set a View for \"PunchLayout\" or set \"ShallSetDefaultPunch\" to true");
            //}
            //if(ShallSetDefaultPunched == false && PunchCompletedLayout == null)
            //{
            //    throw new NullReferenceException("View to be displayed after punch accessed is not available");
            //}

            int noOfRows = 0, noOfColumns = 0;

            MatrixLayoutArrangement matrixArrangement = new MatrixLayoutArrangement();
            var rowsColumns = await matrixArrangement.UseEqualRowsAndColumnsWithRespectiveToScreenSize(0, PunchesNumber, PunchCellHeight, PunchLayoutHeight, PunchCellSpacing);
            noOfRows = rowsColumns[0];
            noOfColumns = rowsColumns[1];
            System.Diagnostics.Debug.WriteLine(rowsColumns.ToString() + "\n" + noOfRows.ToString() + "\n" + noOfColumns.ToString());

            //RelativeLayout relativePunchCard = new RelativeLayout()
            //{
            //    BackgroundColor = Color.Green,
            //    HeightRequest = (PunchLayoutHeight != 0) ? PunchLayoutHeight : ((PunchCellHeight + PunchCellSpacing) * noOfRows),
            //    WidthRequest = PunchLayoutWidth
            //    //HeightRequest = (PunchCellHeight) * noOfRows,
            //    //WidthRequest = (PunchCellHeight) * noOfColumns
            //    //HeightRequest = (PunchCellHeight + 10) * noOfRows,
            //    //WidthRequest = (PunchCellHeight + 10) * noOfColumns
            //};

            RelativeLayout relativePunchCard = new RelativeLayout()
            {
                BackgroundColor = Color.Green,
                HeightRequest = (PunchLayoutHeight != 0) ? PunchLayoutHeight : ((PunchCellHeight + PunchCellSpacing) * noOfRows),
                WidthRequest = PunchLayoutWidth
            };

            /*
            StackLayout stackPunchCard = new StackLayout()
            {
                Children = { relativePunchCard }
            };
            //stackPunchCard.Children.Add(relativePunchCard);
            */
            int imagesAllocated = 0;

            for (int i = 0; i < noOfRows; i++)
            {
                for (int j = 0; j < noOfColumns; j++)
                {
                    if (imagesAllocated < PunchesNumber)
                    {
                        double imageXPoint, imageYPoint;
                        imageYPoint = i * (PunchCellHeight + PunchCellSpacing);
                        imageXPoint = j * (PunchCellHeight + PunchCellSpacing);
                        System.Diagnostics.Debug.WriteLine("\n\n X-coordinates:" + imageXPoint.ToString() + "\n Y-coordinates:" + imageYPoint.ToString());


                        if (ShallSetDefaultPunch != true)
                        {
                            var view = await OnCalledForView(null);
                            if (view != null)
                            {
                                var customLayout = view;
                                relativePunchCard.Children.Add(customLayout,
                                                               Constraint.Constant(imageXPoint),
                                                               Constraint.Constant(imageYPoint),
                                                               Constraint.Constant(PunchCellHeight),
                                                               Constraint.Constant(PunchCellHeight));
                            }
                        }
                        else
                        {
                            DotsLayout dts = new DotsLayout((imagesAllocated + 1), PunchCellHeight, PunchCellWidth, IsRoundCell)
                            {
                                MainColor = Color.Gray
                            };
                            relativePunchCard.Children.Add(dts,
                                                           Constraint.Constant(imageXPoint),
                                                           Constraint.Constant(imageYPoint),
                                                           Constraint.Constant(PunchCellHeight),
                                                           Constraint.Constant(PunchCellHeight));
                        }
                        imagesAllocated++;
                    }

                }
            }
            string responseData = "No of Rows: " + noOfRows + ", \n" + "No of Columns: " + noOfColumns;
            //PunchCardTestPage.punchCardTestPage.FillLabelData(responseData);
            return relativePunchCard;
        }
    }
}
