namespace prizaLinearAlgebra;
public static class FirstDerivative
{
    /// <summary>
    /// Input : previous next step
    /// </summary>
    public static Func<double, double, double, double> CentralDifference;  

    /// <summary>
    /// Input : central, previous, step
    /// </summary>
    public static Func<double, double, double, double> BackwardDifference1;

    /// <summary>
    /// Input : central, previous1, previous2, step
    /// </summary>
    public static Func<double, double, double, double, double> BackwardDifference2;

    /// <summary>
    /// Input : central, next, step
    /// </summary>
    public static Func<double, double, double, double> ForwardDifference1;

    /// <summary>
    /// Input : central, next1, next2, step
    /// </summary>
    public static Func<double, double, double, double, double> ForwardDifference2;

    static FirstDerivative ()
    {
        CentralDifference = (tPrevious1, tNext1, Step) => (tNext1 - tPrevious1) / (2d * Step);  

        ForwardDifference1 = (tCentral, tNext1, Step) => (tNext1 - tCentral) / Step;
        ForwardDifference2 = (tCentral, tNext1, tNext2, Step) => (-3d * tCentral +  4d * tNext1 - tNext2) /  (2d *Step);

        BackwardDifference1 = (tCentral, tPrevious1, Step) => (tCentral - tPrevious1) / Step;
        BackwardDifference2 = (tCentral, tPrevious1, tPrevious2, Step) => (3d * tCentral - 4d * tPrevious1 +  tPrevious2) / (2d *Step);

    }
}
