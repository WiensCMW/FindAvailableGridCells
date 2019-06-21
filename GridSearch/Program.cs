using System;
using System.Collections.Generic;
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

        private static int _colCount = 3;
        private static int _rowCount = 3;

        private static List<Location> _stockLocations = new List<Location>();

        static void Main(string[] args)
        {
            GenerateSampleLocations();

            int partHorzGridFootPrint = 2;
            int partVertGridFootPrint = 2;

            // Loop through the rows in the grid
            for (int r = 1; r <= _rowCount; r++)
            {
                // Loop through all the columns for current loop row
                for (int c = 1; c <= _colCount; c++)
                {
                    // Check if the parts foot print can fit anchored to the current row and column
                    if (CheckPartFootPrint(partHorzGridFootPrint, partVertGridFootPrint, r, c))
                    {
                        Console.WriteLine($"Cell {GetCellAddress(r, c)} open!");
                    }
                }
            }

            Console.ReadLine();
        }

        private static bool CheckPartFootPrint(int partHorzGrids, int partVertGrids, int row, int col)
        {
            for (int i = 0; i < partHorzGrids; i++)
            {
                for (int x = 0; x < partVertGrids; x++)
                {
                    if (_stockLocations.Any(y => y.CellRow == row + i && y.CellCol == col + x))
                        return false;
                }
            }

            return true;
        }

        private static void GenerateSampleLocations()
        {
            _stockLocations.Clear();
            _stockLocations.Add(new Location(1, 1, 1, "A1"));
            _stockLocations.Add(new Location(2, 2, 1, "A2"));
            _stockLocations.Add(new Location(3, 3, 1, "A3"));
            _stockLocations.Add(new Location(4, 1, 3, "C1"));
            //_stockLocations.Add(new Location(4, 1, 4, "D1"));
            //_stockLocations.Add(new Location(4, 2, 4, "D2"));
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
        static int tableWidth = 77;

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
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
