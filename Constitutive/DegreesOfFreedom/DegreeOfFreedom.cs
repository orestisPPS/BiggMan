namespace Constitutive
{
    public enum DegreeOfFreedomType
    {
        X,
        Y,
        Z,
        Temperature,
        Pressure,
        DisplacementX,
        DisplacementY,
        DisplacementZ,
        VelocityX,
        VelocityY,
        VelocityZ,
        UnknownVariableX,
        UnknownVariableY,
        UnknownVariableZ
    }
    public abstract class DegreeOfFreedom : IDegreeOfFreedom
    {
        public virtual double Value {get; set;}
        public virtual DegreeOfFreedomType Type {get;}
    }
}