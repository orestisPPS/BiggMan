namespace Discretization
{
    public class NaturalY : ICoordinate
    {
        public CoordinateType Type => CoordinateType.NaturalY;
        public double Value { get; set; }
        public Direction Direction => Direction.Two;

        public NaturalY()
        {
            Value = double.NaN;
        }

        public NaturalY(double value)
        {
            this.Value = value;
        }
    }
}
