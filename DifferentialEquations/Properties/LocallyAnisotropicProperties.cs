namespace DifferentialEquations
{
    public abstract class LocallyAnisotropicProperties : DifferentialEquationProperties
    {
        public virtual DifferentialEquationPropertiesType Type => DifferentialEquationPropertiesType.LocallyAnisotropic;
    }
}