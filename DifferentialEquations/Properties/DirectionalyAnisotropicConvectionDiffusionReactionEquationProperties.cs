namespace DifferentialEquations
{
    public class DirectionalyAnisotropicConvectionDiffusionReactionEquationProperties : DirectionalyAnisotropicProperties
    {
        public double[] CapacityCoefficientsVector { get; }

        public double[,] DiffusionCoefficientsMatrix { get; }

        public double[] ConvectionCoefficientsVector { get; }

        public double[] DependentSourceCoefficientsVector { get; }

        public double[] IndependentSourceCoefficientsVector { get; }
        
        public DirectionalyAnisotropicConvectionDiffusionReactionEquationProperties(double[,] diffusionCoefficientsMatrix,
                                                                                    double[] convectionCoefficientsVector,
                                                                                    double[] dependentSourceCoefficientsVector,
                                                                                    double[] independentSourceCoefficientsVector)
        {
            this.DiffusionCoefficientsMatrix = diffusionCoefficientsMatrix;
            this.ConvectionCoefficientsVector = convectionCoefficientsVector;
            this.DependentSourceCoefficientsVector = dependentSourceCoefficientsVector;
            this.IndependentSourceCoefficientsVector = independentSourceCoefficientsVector;
        }

        public DirectionalyAnisotropicConvectionDiffusionReactionEquationProperties(double[] capacityCoefficientsVector,
                                                                                    double[,] diffusionCoefficientsMatrix,
                                                                                    double[] convectionCoefficientsVector,
                                                                                    double[] dependentSourceCoefficientsVector,
                                                                                    double[] independentSourceCoefficientsVector)
        {
            this.CapacityCoefficientsVector = capacityCoefficientsVector;
            this.DiffusionCoefficientsMatrix = diffusionCoefficientsMatrix;
            this.ConvectionCoefficientsVector = convectionCoefficientsVector;
            this.DependentSourceCoefficientsVector = dependentSourceCoefficientsVector;
            this.IndependentSourceCoefficientsVector = independentSourceCoefficientsVector;
        }

        public override bool IsTransient()
        {
            return this.CapacityCoefficientsVector != null && this.CapacityCoefficientsVector.Length > 0;
        }
    }
}