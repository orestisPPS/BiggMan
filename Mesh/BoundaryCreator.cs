using BoundaryConditions;
using prizaLinearAlgebra;
using utility;
namespace MeshGeneration
{
    public abstract class BoundaryCreator
    {
        public abstract Dictionary<int, List<IBoundaryCondition>> DomainDirichletX { get; internal set; }
        public abstract Dictionary<int, List<IBoundaryCondition>> DomainDirichletY { get; internal set; }
    }
}



