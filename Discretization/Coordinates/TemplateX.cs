namespace Discretization
{
    public class TemplateX : ICoordinate
    {
        public CoordinateType Type => CoordinateType.TemplateX;
        public double Value { get; set; }
        public Direction Direction => Direction.One;
        
        public TemplateX()
        {
            Value = double.NaN;
        }

        public TemplateX(double value)
        {
            this.Value = value;
        }
    }
}
