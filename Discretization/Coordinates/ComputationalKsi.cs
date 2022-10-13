namespace Discretization
{
    public class ParametricKsi : ICoordinate
    {
        public CoordinateType Type => CoordinateType.ParametricKsi;
        public double Value { get; set; }
        public Direction Direction => Direction.One;

        public ParametricKsi()
        {
            Value = double.NaN;
        }

        public ParametricKsi(double value)
        {
            this.Value = value;
        }
    }
}