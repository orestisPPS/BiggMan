namespace Constitutive
{
    public class Z : Position
    {
        public override DegreeOfFreedomType Type => DegreeOfFreedomType.Z;
        public Z()
        {
            Value = -1d;
        }

        public Z(double value)
        {
            Value = value;
        }
    }
}