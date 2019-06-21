namespace GridSearch
{
    public class StockLocation
    {
        public int LocationID { get; set; }
        public int CellRow { get; set; }
        public int CellCol { get; set; }
        public string CellAddress { get; set; }

        public StockLocation(int locationID,
            int cellRow,
            int cellCol,
            string cellAddress)
        {
            LocationID = locationID;
            CellRow = cellRow;
            CellCol = cellCol;
            CellAddress = cellAddress;
        }
    }
}
