namespace Discretization
{
    public class ElementId : IId
    {
        public int Global {get; set;}
        public int Internal {get; set;}
        public int Boundary {get; set;}
        public ElementId()
        {
            this.Global = -1;
            this.Internal = -1;
            this.Boundary = -1;
        }
    }
}