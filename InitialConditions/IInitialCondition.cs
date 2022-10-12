namespace InitialConditions
{
    public interface IInitialCondition
    {
        public string Type { get;}
    
        public Func<double, double, double> Value { get; set; }

    }
}