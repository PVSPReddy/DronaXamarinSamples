﻿using System;

using Xamarin.Forms;

namespace PunchCardExample
{
    public class PunchCardTwo : ContentView
    {
        public PunchCardTwo(int punchesNumber, int punchedNumber, int punchHeight, int columnLimit)
        {
            int noOfRows = 0, noOfColumns = 0;

            var matrixNumber = Convert.ToInt32(Math.Sqrt(punchesNumber));
            if (matrixNumber < columnLimit)
            {
                if ((matrixNumber * matrixNumber) == punchesNumber)
                {
                    noOfRows = matrixNumber;
                    noOfColumns = matrixNumber;
                }
                else if ((matrixNumber * matrixNumber) < punchesNumber)
                {
                    noOfColumns = matrixNumber + 1;
                    if (punchesNumber % noOfColumns == 0)
                    {
                        noOfRows = punchesNumber / noOfColumns;
                    }
                    else
                    {
                        noOfRows = (punchesNumber / noOfColumns) + 1;
                    }
                    int repititons = 3;
                    while ((noOfRows * noOfColumns) < punchesNumber)
                    {
                        if (repititons % 2 == 0)
                        {
                            noOfColumns += 1;
                            if (punchesNumber % noOfColumns == 0)
                            {
                                noOfRows = punchesNumber / noOfColumns;
                            }
                            else
                            {
                                noOfRows = (punchesNumber / noOfColumns) + 1;
                            }
                        }
                        else
                        {
                            noOfRows += 1;
                            if (punchesNumber % noOfRows == 0)
                            {
                                noOfColumns = punchesNumber / noOfRows;
                            }
                            else
                            {
                                noOfColumns = (punchesNumber / noOfRows) + 1;
                            }
                        }
                        repititons++;
                    }

                }
                else
                {
                    noOfRows = matrixNumber - 1;
                    if (punchesNumber % noOfRows == 0)
                    {
                        noOfColumns = punchesNumber / noOfRows;
                    }
                    else
                    {
                        noOfColumns = (punchesNumber / noOfRows) + 1;
                    }
                    int repititons = 3;
                    while ((noOfRows * noOfColumns) < punchesNumber)
                    {
                        if (repititons % 2 == 0)
                        {
                            noOfRows += 1;
                            if (punchesNumber % noOfRows == 0)
                            {
                                noOfColumns = punchesNumber / noOfRows;
                            }
                            else
                            {
                                noOfColumns = (punchesNumber / noOfRows) + 1;
                            }
                        }
                        else
                        {
                            noOfColumns += 1;
                            if (punchesNumber % noOfColumns == 0)
                            {
                                noOfRows = punchesNumber / noOfColumns;
                            }
                            else
                            {
                                noOfRows = (punchesNumber / noOfColumns) + 1;
                            }
                        }
                        repititons++;
                    }

                }
            }
            else
            {
                noOfColumns = columnLimit;
                if (punchesNumber % noOfColumns == 0)
                {
                    noOfRows = punchesNumber / noOfColumns;
                }
                else
                {
                    noOfRows = (punchesNumber / noOfColumns) + 1;
                }
            }

            RelativeLayout relativePunchCard = new RelativeLayout()
            {
                BackgroundColor = Color.White,
                HeightRequest = (punchHeight) * noOfRows,
                WidthRequest = (punchHeight) * noOfColumns
            };
            int imagesAllocated = 0;

            for (int i = 0; i < noOfRows; i++)
            {
                for (int j = 0; j < noOfColumns; j++)
                {
                    if (imagesAllocated < punchesNumber)
                    {
                        Image punchImage = new Image()
                        {
                            Source = ImageSource.FromFile("imgUnChecked.png"),
                            HeightRequest = punchHeight,
                            WidthRequest = punchHeight
                        };
                        TapGestureRecognizer punchImageTap = new TapGestureRecognizer();
                        punchImageTap.NumberOfTapsRequired = 1;
                        punchImageTap.Tapped += (object sender, EventArgs e) =>
                        {

                        };

                        if (imagesAllocated < punchedNumber)
                        {
                            punchImage.Source = ImageSource.FromFile("imgChecked.png");
                        }
                        else
                        {
                            punchImage.Source = ImageSource.FromFile("imgUnChecked.png");
                        }
                        imagesAllocated++;
                        double imageXPoint, imageYPoint;
                        imageYPoint = i * punchHeight;
                        imageXPoint = (j * punchHeight);
                        relativePunchCard.Children.Add(punchImage,
                                                       Constraint.Constant(imageXPoint),
                                                       Constraint.Constant(imageYPoint),
                                                       Constraint.Constant(punchHeight),
                                                       Constraint.Constant(punchHeight));
                    }

                }
            }
            string responseData = "No of Rows: " + noOfRows + ", \n" + "No of Columns: " + noOfColumns;
            PunchCardTestPage.punchCardTestPage.FillLabelData(responseData);
            Content = relativePunchCard;
        }
    }
}