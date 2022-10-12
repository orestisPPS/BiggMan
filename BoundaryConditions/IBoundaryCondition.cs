namespace BoundaryConditions
{
    public enum BoundaryConditionType
    {
        Dirichlet,
        Neumann
    }
    public interface IBoundaryCondition
    {
        BoundaryConditionType Type { get; }
    
        Func<double, double> Function1D { get; }

        Func<double, double, double> Function2D { get; }

        Func<double, double, double, double> Function3D { get; }


    }
}