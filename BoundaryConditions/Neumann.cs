namespace BoundaryConditions
{
    public class Neumann : IBoundaryCondition
    {
        public BoundaryConditionType Type => BoundaryConditionType.Neumann;
        public Func<double, double> Function1D { get; }
        public Func<double, double, double> Function2D { get; }
        public Func<double, double, double, double> Function3D { get; }
        public Func<double, double, double> Value { get; }

        public Neumann(Func<double, double> function1D)
        {
            this.Function1D = function1D;
        }
        public Neumann(Func<double, double, double> function2D)
        {
            this.Function2D = function2D;
        }
        public Neumann(Func<double, double, double, double> function3D)
        {
            this.Function3D = function3D;
        }
    }
}