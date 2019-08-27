namespace GridSearch
{
    public class PartFootPrint
    {
        public int HorzGrids { get; set; }
        public int VertGrids { get; set; }

        public PartFootPrint(float partWidth,
            float partDepth)
        {
            CalculatePartGridSizes(partWidth, partDepth);
        }

        private void CalculatePartGridSizes(float partWidth, float partDepth)
        {
            bool calcWidth = true;
            while (calcWidth)
            {

            }
        }
    }
}
