namespace Discretization
{
    public interface IMesh2D : IMesh
    {

        /// <summary> 
        /// Number of nodes in the y,η,Θ direction.
        /// </summary>  
        /// <value></value>
        int NNDirectionTwo { get; }

        /// <summary>
        /// Node distribution in 2d array.
        /// </summary>
        /// <value></value>
        Node[,] NodesArray { get; set; }
    }
}