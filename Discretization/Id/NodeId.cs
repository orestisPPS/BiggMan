namespace Discretization
{
    public class NodeId : IId
    {
        public int Global {get; set;}
        public int Internal {get; set;}
        public int Boundary {get; set;}
        public NodeId()
        {
            this.Global = -1;
            this.Internal = -1;
            this.Boundary = -1;
        }
    }
}