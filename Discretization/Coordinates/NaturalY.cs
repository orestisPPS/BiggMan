namespace Discretization
{
    public class NaturalY : IDirectionTwo
    {
        public CoordinateType Type => CoordinateType.NaturalY;
        public double Value { get; set; }
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
