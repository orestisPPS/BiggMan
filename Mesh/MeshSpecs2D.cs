using Discretization;
using Constitutive;
namespace Meshing
{
    public class MeshSpecs2D
    {
        /// <summary>
        /// The number of nodes in direction one (x, ξ, r, etc.)
        /// </summary>
        /// <value></value>
        public int NNDirectionOne { get;}

        /// <summary>
        /// The number of nodes in direction two (y, η, θ, etc.)
        /// </summary>
        /// <value></value>
        public int NNDirectionTwo { get;}

        /// <summary>
        /// The distance of the nodes in x direction
        /// </summary>
        /// <value></value>
        public double TemplateHx {get;}

        /// <summary>
        /// The distance of the nodes in Y direction
        /// </summary>
        /// <value></value>
        public double TemplateHy {get;}

        /// <summary>
        /// The rotation angle of the mesh
        /// </summary>
        /// <value></value>
        public double TemplateRotAngle {get;}

        /// <summary>
        /// The shear angle of the mesh with the X axis
        /// </summary>
        /// <value></value>
        public double TemplateShearX {get;}

        /// <summary>
        /// The shear angle of the mesh with the Y axis
        /// </summary>
        /// <value></value>
        public double TemplateShearY {get;}

        /// <summary>
        /// The densification method used in the mesh construction. takes information from the mesh boundaries
        /// </summary>
        /// <value></value>
        //public DensificationMethod DensificationMethod {get;}

        //public MeshSpecs(int nnx, int nny,  double templateHx, double templateHy, double templateRotAngle, double templateShearX, double templateShearY, DensificationMethod densificationMethod)
        public MeshSpecs2D(int nnx, int nny,  double templateHx, double templateHy, double templateRotAngle, double templateShearX, double templateShearY)
        {
            this.NNDirectionOne = nnx;
            this.NNDirectionTwo = nny;
            this.TemplateHx = templateHx;
            this.TemplateHy = templateHy;
            this.TemplateRotAngle = templateRotAngle;
            this.TemplateShearX = templateShearX;
            this.TemplateShearY = templateShearY;
            //this.DensificationMethod = densificationMethod;          
        }
    }
}
