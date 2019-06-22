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

        // Debug variables
        private static int _loopCount;
        private static Stopwatch _stopWatch = new Stopwatch();

        static void Main(string[] args)
        {
            _stopWatch.Restart();
            GenerateSampleLocations();

            int partHorzGridFootPrint = 2;
            int partVertGridFootPrint = 2;

            // Loop through the rows in the grid
            for (int r = 1; r <= _rowCount; r++)
            {
                _loopCount++;
                // Loop through all the columns for current loop row
                for (int c = 1; c <= _colCount; c++)
                {
                    _loopCount++;
                    // Check if the parts foot print can fit anchored to the current row and column
                    if (CheckPartFootPrint(partHorzGridFootPrint, partVertGridFootPrint, r, c))
                    {
                        Console.WriteLine($"Cell {GetCellAddress(r, c)} open!");
                    }
                }
            }


            Console.WriteLine();
            Console.WriteLine($"Loop Count: {_loopCount}");
            Console.WriteLine($"Time(ms): {_stopWatch.Elapsed.TotalMilliseconds}");

            Console.ReadLine();
        }

        /// <summary>
        /// item1 = ID, item2 = Row, item3 = Colum
        /// </summary>
        static List<Tuple<int, int, int>> _availableCellsFound = new List<Tuple<int, int, int>>();

        private static bool CheckPartFootPrint(int partHorzGrids, int partVertGrids, int row, int col)
        {
            for (int i = 0; i < partHorzGrids; i++)
            {
                _loopCount++;
                for (int x = 0; x < partVertGrids; x++)
                {
                    _loopCount++;
                    if (_locations.Any(y => y.CellRow == row + i && y.CellCol == col + x))
                        return false;

                    _availableCellsFound.Add(new Tuple<int, int, int>(1, row + 1, col + x));
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
