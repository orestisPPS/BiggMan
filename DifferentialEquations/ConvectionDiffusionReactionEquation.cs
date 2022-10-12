namespace DifferentialEquations
{
    public class ConvectionDiffusionReactionEquation : IDifferentialEquation
    {
        public DifferentialEquationType DifferentialEquationType { get; }
        public bool IsTransient { get; }
        public DifferentialEquationProperties Coefficients { get; }
        public ConvectionDiffusionReactionEquation(DifferentialEquationProperties coefficients)
        {
            this.Coefficients = coefficients;
        }
    }
}