namespace Discretization
{
    public enum ComputationalDomainType
    {
        Natural,
        Parametric
    }
    
    public enum CoordinateType
    {
        NaturalX,
        NaturalY,
        NaturalZ,
        ParametricKsi,
        ParametricIta,
        ComputationalZeta,
        TemplateX,
        TemplateY,
        TemplateZ,
        NaturalR,
        NatruralTheta,
        NaturalPhi,
    }
    public interface ICoordinate
    {
        public double Value {get; set;}
        public CoordinateType Type {get; }
    }
}