using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace GridSearch
{
    class Program
    {
        //private static float _gridWidth = 4f;
        //private static float _gridDepth = 4f;

        //private static float _partWidth = 3f;
        //private static float _partDepth = 3f;

        private static int _colCount = 2;
        private static int _rowCount = 3;

        private static List<Location> _locations = new List<Location>();
        private static List<Location> _availableCellsFound = new List<Location>();

        // Debug variables
        private static int _loopCount;
        private static Stopwatch _stopWatch = new Stopwatch();

        static void Main(string[] args)
        {
            _stopWatch.Restart();
            GenerateSampleLocations();

            int partHorzGridFootPrint = 1;
            int partVertGridFootPrint = 4;

            // Check for available locations based on parts horz and vert foot print
            CheckForAvailableLocations(partHorzGridFootPrint, partVertGridFootPrint);

            /* If the part's horz and vert foot prints are NOT square, we check for available
             * locations again. But this time we flip the horz and vert foot print. This checks
             * for locations with the part flipped 90 degreeds. */
            if (partHorzGridFootPrint != partVertGridFootPrint)
            {
                Console.WriteLine();
                PrintLine();
                Console.WriteLine("Flipped!!");
                PrintLine();
                Console.WriteLine();
                _availableCellsFound.Clear();
                CheckForAvailableLocations(partVertGridFootPrint, partHorzGridFootPrint);
            }

            //Console.ReadLine();
        }

        private static void CheckForAvailableLocations(int partHorzGrid, int partVertGrid)
        {
            Console.WriteLine($"Horz:{partHorzGrid} Vert:{partVertGrid}");
            PrintLine();

            // Loop through the rows in the grid
            for (int r = 1; r <= _rowCount; r++)
            {
                _loopCount++;
                // Loop through all the columns for current loop row
                for (int c = 1; c <= _colCount; c++)
                {
                    _loopCount++;
                    // Check if the parts foot print can fit anchored to the current row and column
                    if (CheckPartFootPrint(partHorzGrid, partVertGrid, r, c))
                    {
                        Console.WriteLine($"Cell {GetCellAddress(r, c)} can anchor the part!!!!");
                    }
                }
            }

            // Print Available Cells if found
            if (_availableCellsFound.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Available Cells:");
                for (int i = 0; i < _availableCellsFound.Count; i++)
                {
                    Console.WriteLine($"LocationID:{_availableCellsFound[i].LocationID} " +
                        $"Row:{_availableCellsFound[i].CellRow} " +
                        $"Col:{_availableCellsFound[i].CellCol} " +
                        $"Cell:{_availableCellsFound[i].CellAddress}");
                }
            }
            else
            {
                PrintLine();
                Console.WriteLine("NOTHING FOUND!!");
                PrintLine();
            }

            Console.WriteLine();
            Console.WriteLine($"Loop Count: {_loopCount}");
            Console.WriteLine($"Time(ms): {_stopWatch.Elapsed.TotalMilliseconds}");
        }

        private static bool CheckPartFootPrint(int partHorzGrids, int partVertGrids, int anchorRow, int anchorCol)
        {
            /* Check if the current part's horz and vert foot print cells will run out of the grid's
             * bounds based on the current row and cell. If the part's foot print will run out of the
             * grid's bounds, the part can't fit from the current row/col so we return false. */
            if (partHorzGrids > 1
                && anchorCol + (partHorzGrids - 1) > _colCount)
                return false;

            if (partVertGrids > 1
                && anchorRow + (partVertGrids - 1) > _rowCount)
                return false;

            // Get max location ID from found cells list
            int maxFoundLocationID = (_availableCellsFound.Count > 0) ? _availableCellsFound[_availableCellsFound.Count - 1].LocationID : 0;
            maxFoundLocationID++;

            /* Loop through part's horz and vert foot print cells and check if any of them intersect with
             * existing locations. If any intersects are found, the current part can't fit so we return false.
             * NOTE: In the following two loops, the outer loops through the COLUMNS and the inner loops through
             * the ROWS.*/
            for (int c = 0; c < partHorzGrids; c++)
            {
                _loopCount++;
                for (int r = 0; r < partVertGrids; r++)
                {
                    _loopCount++;
                    /* About the anchorRow vs r and anchorCol vs c variables:
                     * The passed in anchorRow and anchorCol variables are the row and column that this method was
                     * called from and is evaluating from. The "r" and "c" variables are the incremental rows and
                     * columns that the current loop is checking based on the part's horizontal and vertical grid
                     * foot print. */
                    int rowVal = anchorRow + r;
                    int colVal = anchorCol + c;
                    string cellAddr = GetCellAddress(rowVal, colVal);

                    // If current loop cell is occupied by another location, we abort and return false.
                    if (_locations.Any(y => y.CellRow == rowVal && y.CellCol == colVal))
                        return false;

                    // Log available cell to list
                    _availableCellsFound.Add(new Location(maxFoundLocationID, rowVal, colVal, cellAddr));
                }
            }

            return true;
        }

        private static void GenerateSampleLocations()
        {
            _locations.Clear();
            _locations.Add(new Location(1, 1, 1, "A1"));
            //_locations.Add(new Location(2, 2, 1, "A2"));
            //_locations.Add(new Location(3, 3, 1, "A3"));
        }

        private static string GetCellAddress(int row, int col)
        {
            try
            {
                char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                return $"{alpha[col - 1]}{row}";
            }
            catch (Exception) { }

            return "";
        }

        #region Print Table to Console
        static int _tableWidth = 77;

        static void PrintTable()
        {
            // Print Column Header Row
            string[] cols = new string[_colCount];
            for (int c = 0; c < cols.Length; c++)
            {
                cols[c] = GetCellAddress(1, c + 1).Replace("1", "");
            }
            PrintLine();
            PrintRow(cols);
            PrintLine();
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', _tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (_tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        #endregion
    }
}
