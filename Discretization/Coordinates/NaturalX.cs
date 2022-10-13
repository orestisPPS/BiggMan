namespace Discretization
{
    public class NaturalX : ICoordinate
    {
        public CoordinateType Type => CoordinateType.NaturalX;
        public double Value { get; set; }
        public Direction Direction => Direction.One;
        
        public NaturalX()
        {
            Value = double.NaN;
        }

        public NaturalX(double value)
        {
            this.Value = value;
        }
    }
}
