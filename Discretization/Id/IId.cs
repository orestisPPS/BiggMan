namespace Discretization
{
    public interface IId
    {
        int Global   {get; set;}
        int Internal {get; set;}
        int Boundary {get; set;}
    }
}