namespace Constitutive
{
    public class Y : Position
    {
        public override DegreeOfFreedomType Type => DegreeOfFreedomType.Y;
        public Y()
        {
            Value = -1d;
        }

        public Y(double value)
        {
            Value = value;
        }
    }
}