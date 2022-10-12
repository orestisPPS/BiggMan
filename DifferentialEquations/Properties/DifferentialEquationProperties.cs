namespace DifferentialEquations
{
    public enum DifferentialEquationPropertiesType
    {
        Isotropic,
        LocallyAnisotropic,
        DirectionallyAnisotropic
    }
    public abstract class DifferentialEquationProperties
    {
        public virtual DifferentialEquationPropertiesType Type { get; }

        public virtual bool IsTransient()
        {
            return false;
        } 
    }
}

