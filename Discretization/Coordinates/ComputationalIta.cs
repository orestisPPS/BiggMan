namespace Discretization
{
    public class ParametricIta : ICoordinate
    {
        public CoordinateType Type => CoordinateType.ParametricIta;
        public double Value { get; set; }
        public Direction Direction => Direction.Two;
        
        public ParametricIta()
        {
            Value = double.NaN;
        }

        public ParametricIta(double value)
        {
            this.Value = value;
        }
    }
}