namespace InitialConditions
{
    public abstract class InitialCondition : IInitialCondition
    {
        public virtual string Type { get; set; }
        
        public virtual Func<double, double, double> Value { get; set; }
        
    }
}