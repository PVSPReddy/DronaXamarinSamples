using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PunchCardExample
{
    public class MatrixLayoutArrangement
    {
        public MatrixLayoutArrangement()
        {                      }
         public async Task<int[]> UseEqualRowsAndColumnsIrrespectiveOfScreenSize(int startNumber, int endNumber)         {
            int[] intRowsColumns = new int[2];             try             {                 int outValue = 0;
                //startNumber = 1;
                //endNumber = 0;                  for (int i = startNumber; i <= endNumber; i++)                 {                     var columns = (int)Math.Sqrt(i);//columns                     while ((columns * columns) < i)                     {                         columns++;                     };                     var rows = columns;//rows                      if ((columns * rows) > i)                     {                         var difference = (columns * rows) - i;                         if (difference >= columns)                         {                             rows--;                         }                     } 
                    intRowsColumns[0] = rows;
                    intRowsColumns[1] = columns;
                     var totalCellsFormed = rows * columns;                     bool isValidMatrix = false;                     if (totalCellsFormed >= i)                     {                         isValidMatrix = true;                     }                     else                     {                         isValidMatrix = false;                     }                 }              }             catch (Exception ex)             {                 var msg = ex.Message;
                intRowsColumns[0] = 0;
                intRowsColumns[1] = 0;             }
            return intRowsColumns;         }          public async Task<int[]> UseEqualRowsAndColumnsWithRespectiveToScreenSize(int startNumber, int endNumber, double cellSize, double screenSize, double spacings)         {
            int[] intRowsColumns = new int[2];             try             {                 int outValue = 0;
                //startNumber = 1;
                //endNumber = 0;                  var columnSize = cellSize + spacings;                 var noOfValidColumns = (int)(screenSize / columnSize);                  for (int i = startNumber; i <= endNumber; i++)                 {                     var columns = (int)Math.Sqrt(i);//columns                     while ((columns * columns) < i)                     {                         columns++;                     };                     var rows = columns;//rows                      if (columns > noOfValidColumns)                     {                         columns = noOfValidColumns;                         rows = (int)(i / columns);                         while ((columns * rows) >= i)                         {                             rows++;                         }                     }                      if ((columns * rows) > i)                     {                         var difference = (columns * rows) - i;                         if (difference >= columns)                         {                             rows--;                         }                     }

                    intRowsColumns[0] = rows;
                    intRowsColumns[1] = columns;                      var totalCellsFormed = rows * columns;                     bool isValidMatrix = false;                     if (totalCellsFormed >= i)                     {                         isValidMatrix = true;                     }                     else                     {                         isValidMatrix = false;                     }
                }             }             catch (Exception ex)             {                 var msg = ex.Message;
                intRowsColumns[0] = 0;
                intRowsColumns[1] = 0;             }
            return intRowsColumns;         }           public async Task<int[]> UseRestrictedColumnsOrRows(int startNumber, int endNumber, int noOfValidColumns)         {
            int[] intRowsColumns = new int[2];             try             {                 int outValue = 0;
                //startNumber = 1;
                //endNumber = 100;                  for (int i = startNumber; i <= endNumber; i++)                 {                     var columns = (int)Math.Sqrt(i);//columns                     while ((columns * columns) < i)                     {                         columns++;                     };                     var rows = columns;//rows                      if (columns > noOfValidColumns)                     {                         columns = noOfValidColumns;                         rows = (int)(i / columns);                         while ((columns * rows) <= i)                         {                             rows++;                         }                     }                      if ((columns * rows) > i)                     {                         var difference = (columns * rows) - i;                         if (difference >= columns)                         {                             rows--;                         }                     }

                    intRowsColumns[0] = rows;
                    intRowsColumns[1] = columns;                      var totalCellsFormed = rows * columns;                     bool isValidMatrix = false;                     if (totalCellsFormed >= i)                     {                         isValidMatrix = true;                     }                     else                     {                         isValidMatrix = false;                     }                 }             }             catch (Exception ex)             {                 var msg = ex.Message;
                intRowsColumns[0] = 0;
                intRowsColumns[1] = 0;             }
            return intRowsColumns;         }      } } 