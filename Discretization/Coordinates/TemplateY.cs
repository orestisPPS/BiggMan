namespace Discretization
{
    public class TemplateY : ICoordinate
    {
        public CoordinateType Type => CoordinateType.TemplateY;
        public double Value { get; set; }
        public Direction Direction => Direction.Two;
        
        public TemplateY()
        {
            Value = double.NaN;
        }

        public TemplateY(double value)
        {
            this.Value = value;
        }
    }
}
