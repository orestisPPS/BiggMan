namespace Constitutive
{
    public class X : Position
    {
        public override DegreeOfFreedomType Type => DegreeOfFreedomType.X;
        public X()
        {
            Value = -1d;
        }

        public X(double value)
        {
            Value = value;
        }
    }
}