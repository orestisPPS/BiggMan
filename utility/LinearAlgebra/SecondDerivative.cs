public static class SecondDerivative
{
    public static Func<double, double, double, double, double> CentralDifference;  
 
    public static Func<double, double, double, double, double> ForwardDifference1;
    public static Func<double, double, double, double, double, double> ForwardDifference2;
    public static Func<double, double, double, double, double> BackwardDifference1;
    public static Func<double, double, double, double, double, double> BackwardDifference2;  


    static SecondDerivative ()
    {
        CentralDifference = (tPrevious1, tCentral, tNext1, step) => (1d * tNext1 - 2 * tCentral + 1d * tPrevious1) / (2d * Math.Pow(step, 2d));

        ForwardDifference1 = (tCentral, tNext1, tNext2, step) => (1d * tCentral - 2d * tNext1 + 1d * tNext2) / Math.Pow(step, 2d);
        ForwardDifference2 = (tCentral, tNext1, tNext2, tNext3, step) =>  (2d * tCentral - 5d * tNext1 + 4d * tNext2 - 1d * tNext3) / Math.Pow(step, 2d);  

        BackwardDifference1 = (tCentral, tPrevious1, tPrevious2, step) => (1d * tCentral - 2d * tPrevious1 + 2d * tPrevious2) / Math.Pow(step, 2d);
        BackwardDifference2 = (tCentral, tPrevious1, tPrevious2, tPrevious3, step) => (2d * tCentral - 5d * tPrevious1 + 4d * tPrevious2 - 1d * tPrevious3) / Math.Pow(step, 2d);  
    }
}
