using Equations;
namespace DifferentialEquations
{
    public enum DifferentialEquationType
    {
        ConvectionDiffusionReaction
    }
    public interface IDifferentialEquation
    {
        DifferentialEquationType DifferentialEquationType { get; }
        bool IsTransient { get; }
        DifferentialEquationProperties Coefficients { get; }
    }
}