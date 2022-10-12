namespace Constitutive
{
    public interface IDegreeOfFreedom
    {
        public  double Value {get; set;}
        public  DegreeOfFreedomType Type {get;}
    }
}