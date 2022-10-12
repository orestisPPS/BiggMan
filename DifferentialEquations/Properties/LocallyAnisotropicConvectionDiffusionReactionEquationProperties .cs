using Discretization;
namespace DifferentialEquations
{
    public class LocallyAnisotropicConvectionDiffusionReactionEquationProperties : LocallyAnisotropicProperties
    {
        public Dictionary<Node, double[]> CapacityCoefficietnsVector { get; }

        public Dictionary<Node, double[,]> DiffusionCoefficientsMatrix { get; }

        public Dictionary<Node, double[]> ConvectionCoefficientsVector { get; }

        public Dictionary<Node, double[]> DependentReactionCoefficientsVector { get; }

        public Dictionary<Node, double[]> IndependentReactionCoefficientsVector { get; }
        
        public LocallyAnisotropicConvectionDiffusionReactionEquationProperties(Dictionary<Node, double[,]> diffusionCoefficientsMatrix,
                                                                               Dictionary<Node, double[]>  convectionCoefficientsVector,
                                                                               Dictionary<Node, double[]>  dependentReactionCoefficientsVector,
                                                                               Dictionary<Node, double[]>  independentReactionCoefficientsVector)
        {
            this.DiffusionCoefficientsMatrix = diffusionCoefficientsMatrix;
            this.ConvectionCoefficientsVector = convectionCoefficientsVector;
            this.DependentReactionCoefficientsVector = dependentReactionCoefficientsVector;
            this.IndependentReactionCoefficientsVector = independentReactionCoefficientsVector;
        }

        public LocallyAnisotropicConvectionDiffusionReactionEquationProperties(Dictionary<Node, double[]>  capacityCoefficientsVector,
                                                                               Dictionary<Node, double[,]> diffusionCoefficientsMatrix,
                                                                               Dictionary<Node, double[]>  convectionCoefficientsVector,
                                                                               Dictionary<Node, double[]>  dependentReactionCoefficientsVector,
                                                                               Dictionary<Node, double[]>  independentReactionCoefficientsVector)
        {
            this.CapacityCoefficietnsVector = capacityCoefficientsVector;
            this.DiffusionCoefficientsMatrix = diffusionCoefficientsMatrix;
            this.ConvectionCoefficientsVector = convectionCoefficientsVector;
            this.DependentReactionCoefficientsVector = dependentReactionCoefficientsVector;
            this.IndependentReactionCoefficientsVector = independentReactionCoefficientsVector;
        }

        public override bool IsTransient()
        {
            return this.CapacityCoefficietnsVector != null && this.CapacityCoefficietnsVector.Count > 0;
        }
    }
}