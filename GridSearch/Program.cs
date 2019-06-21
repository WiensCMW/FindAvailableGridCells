using System;

namespace GridSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            int colCount = 10;
            int rowCount = 3;

            for (int x = 0; x < colCount; x++)
            {
                Console.WriteLine($"Column:{x + 1}");
            }
        }
    }
}
