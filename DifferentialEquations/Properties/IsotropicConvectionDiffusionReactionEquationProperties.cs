namespace DifferentialEquations
{
    public class IsotropicConvectionDiffusionReactionEquationProperties : IsotropicProperties
    {
        public double CapacityCoefficient { get; }

        public double DiffusionCoefficient { get; }

        public double ConvectionCoefficient { get; }

        public double DependentReactionCoefficient { get; }

        public double IndependentReactionCoefficient { get; }
        
        public IsotropicConvectionDiffusionReactionEquationProperties(double capacityCoefficient,
                                                                      double diffusionCoefficient,
                                                                      double convectionCoefficient,
                                                                      double dependentReactionCoefficient,
                                                                      double independentReactionCoefficient)
        {
            this.CapacityCoefficient = capacityCoefficient;
            this.DiffusionCoefficient = diffusionCoefficient;
            this.ConvectionCoefficient = convectionCoefficient;
            this.DependentReactionCoefficient = dependentReactionCoefficient;
            this.IndependentReactionCoefficient = independentReactionCoefficient;
        }
        
        public IsotropicConvectionDiffusionReactionEquationProperties(double diffusionCoefficient,
                                                                      double convectionCoefficient,
                                                                      double dependentReactionCoefficient,
                                                                      double independentReactionCoefficient)
        {
            this.DiffusionCoefficient = diffusionCoefficient;
            this.ConvectionCoefficient = convectionCoefficient;
            this.DependentReactionCoefficient = dependentReactionCoefficient;
            this.IndependentReactionCoefficient = independentReactionCoefficient;
        }

        public override bool IsTransient()
        {
            return this.CapacityCoefficient != 0;
        }
    }
}
