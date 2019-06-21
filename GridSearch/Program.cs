using System;
using System.Collections.Generic;
using System.Linq;

namespace GridSearch
{
    class Program
    {
        private static float _gridWidth = 4f;
        private static float _gridDepth = 4f;

        private static float _partWidth = 3f;
        private static float _partDepth = 3f;

        private static List<StockLocation> _stockLocations = new List<StockLocation>();

        private static string[] _alpha = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };

        static void Main(string[] args)
        {
            GetStockLocations();

            int colCount = 4;
            int rowCount = 3;

            // Loop through the rows in the grid
            for (int i = 1; i <= rowCount; i++)
            {
                // Loop through the columns for the current row
                LoopThroughCols(i, colCount);
            }
        }

        static void LoopThroughCols(int row, int colCount)
        {
            int partHorzGridFootPrint = 2;
            int partVertGridFootPrint = 2;

            for (int i = 1; i <= colCount; i++)
            {
                if (PartFootPrintSpaceFound(partHorzGridFootPrint, partVertGridFootPrint, row, i))
                {
                    Console.WriteLine($"Cell {GetCellAddress(row, i)} open!");
                }
            }
        }

        private static bool PartFootPrintSpaceFound(int horzCells, int vertCells, int row, int col)
        {
            for (int i = 0; i < horzCells; i++)
            {
                for (int x = 0; x < vertCells; x++)
                {
                    if (_stockLocations.Any(y => y.CellRow == row + i && y.CellCol == col + x))
                        return false;
                }
            }

            return true;
        }

        private static void GetStockLocations()
        {
            _stockLocations.Clear();
            _stockLocations.Add(new StockLocation(1, 1, 1, "A1"));
            _stockLocations.Add(new StockLocation(2, 2, 1, "A2"));
            _stockLocations.Add(new StockLocation(3, 3, 1, "A3"));
            _stockLocations.Add(new StockLocation(4, 1, 3, "C1"));
            _stockLocations.Add(new StockLocation(4, 1, 4, "D1"));
            _stockLocations.Add(new StockLocation(4, 2, 4, "D2"));
        }

        private static string GetCellAddress(int row, int col)
        {
            try
            {
                return $"{_alpha[col - 1]}{row}";
            }
            catch (Exception) { }

            return "";
        }
    }
}
