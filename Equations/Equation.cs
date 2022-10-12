namespace Equations
{
    public enum EquationType
    {
        DifferentialEquation
    }
    public abstract class Equation
    {
        public abstract EquationType Type { get; }
    }
}