namespace prizaLinearAlgebra
{
    public interface ILinearSystem
    {
        IMatrix Matrix { get; }
        double[] Vector { get; }
        double[] Solution { get; internal set; }
        //List<double[]> PreviousSolutions { get; set; }
    }
}