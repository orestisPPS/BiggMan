using BoundaryConditions;
using prizaLinearAlgebra;
using utility;

namespace MeshGeneration
{
    public class Parallelogram : BoundaryCreator
    {
        public MeshSpecs2D Specs { get; }
        public override Dictionary<int, List<IBoundaryCondition>> DomainDirichletX { get; internal set; } 
        public override Dictionary<int, List<IBoundaryCondition>> DomainDirichletY { get; internal set; } 
        
        public Parallelogram(MeshSpecs2D specs)
        {
            this.Specs = specs;
            this.DomainDirichletX = new Dictionary<int, List<IBoundaryCondition>>();
            this.DomainDirichletY = new Dictionary<int, List<IBoundaryCondition>>();
            CreateBoundaryConditions();
        }
        
        private void CreateBoundaryConditions()
        {
             var rot = Calculators.DegreesToRad(Specs.TemplateRotAngle);
            var ShearX = Calculators.DegreesToRad(Specs.TemplateShearX);
            var ShearY = Calculators.DegreesToRad(Specs.TemplateShearY);

            var pi = Math.Acos(-1d);

            //bottom 
            DomainDirichletX.Add(0, new List<IBoundaryCondition>());
            DomainDirichletY.Add(0, new List<IBoundaryCondition>());
            for (int i = 0; i < Specs.NNDirectionOne; i++)
            {
                var xStep = i;
                var yStep = 0;
                var transformedCoord = Transform(new double[]{xStep, yStep});
                
                var dirichletFunctionX = new Func<double, double, double> ((ksi, ita) => transformedCoord[0]);
                var dirichletFunctionY = new Func<double, double, double> ((ksi, ita) => transformedCoord[1]);

                DomainDirichletX[0].Add(new Dirichlet(dirichletFunctionX));
                DomainDirichletY[0].Add(new Dirichlet(dirichletFunctionY));

            }

            //right
            DomainDirichletX.Add(1, new List<IBoundaryCondition>());
            DomainDirichletY.Add(1, new List<IBoundaryCondition>());
            for (int j = 0; j < Specs.NNDirectionTwo; j++)
            {
                var xStep = Specs.NNDirectionTwo - 1;
                var yStep = j;
                var transformedCoord = Transform(new double[]{xStep, yStep});

                var dirichletFunctionX = new Func<double, double, double> ((ksi, ita) => transformedCoord[0]);
                var dirichletFunctionY = new Func<double, double, double> ((ksi, ita) => transformedCoord[1]);

                DomainDirichletX[1].Add(new Dirichlet(dirichletFunctionX));
                DomainDirichletY[1].Add(new Dirichlet(dirichletFunctionY));
            }

            //top
            DomainDirichletX.Add(2, new List<IBoundaryCondition>());
            DomainDirichletY.Add(2, new List<IBoundaryCondition>());
            for (int i = 0; i < Specs.NNDirectionOne; i++)
            {
                var xStep = Specs.NNDirectionOne - 1 - i;
                var yStep = Specs.NNDirectionTwo - 1;
                var transformedCoord = Transform(new double[]{xStep, yStep});
                
                var dirichletFunctionX = new Func<double, double, double> ((ksi, ita) => transformedCoord[0]);
                var dirichletFunctionY = new Func<double, double, double> ((ksi, ita) => transformedCoord[1]);

                DomainDirichletX[2].Add(new Dirichlet(dirichletFunctionX));
                DomainDirichletY[2].Add(new Dirichlet(dirichletFunctionY));
            }

            //left
            DomainDirichletX.Add(3, new List<IBoundaryCondition>());
            DomainDirichletY.Add(3, new List<IBoundaryCondition>());
            for (int j = 0; j < Specs.NNDirectionTwo; j++)
            {
                var xStep = 0;
                var yStep = Specs.NNDirectionTwo - 1 - j;
                var transformedCoord = Transform(new double[]{xStep, yStep});

                var dirichletFunctionX = new Func<double, double, double> ((ksi, ita) => transformedCoord[0]);
                var dirichletFunctionY = new Func<double, double, double> ((ksi, ita) => transformedCoord[1]);

                DomainDirichletX[3].Add(new Dirichlet(dirichletFunctionX));
                DomainDirichletY[3].Add(new Dirichlet(dirichletFunctionY));
            }
            
            double[] Transform(double[] initialCoord)
            {
                var transformedCoord = TransformationTensors.Rotate (initialCoord, Specs.TemplateRotAngle);
                transformedCoord = TransformationTensors.Shear(transformedCoord, Specs.TemplateShearX, Specs.TemplateShearY);
                transformedCoord = TransformationTensors.Scale(transformedCoord, Specs.TemplateHx, Specs.TemplateHy);
                return transformedCoord;    
            }

        }
    }
}