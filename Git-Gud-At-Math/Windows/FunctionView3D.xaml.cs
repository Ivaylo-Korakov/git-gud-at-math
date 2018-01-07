using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Git_Gud_At_Math.Models;
using HelixToolkit.Wpf;

namespace Git_Gud_At_Math.Windows
{
    /// <summary>
    /// Interaction logic for FunctionView3D.xaml
    /// </summary>
    public partial class FunctionView3D : Window
    {
        public FunctionView3D()
        {
            InitializeComponent();
            // Open window center screen
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // -(^(x,2),^(y,2))
            // -(*(x,^(y,3)),*(y,^(x,3)))
            // /(1,+(^(x,2),^(y,2)))
            // /(s(x),c(y))
            // ^(+(^(x,2),^(y,2)),0.5) Upside Cone
            // /(*(s(*(5,x)),c(*(5,y))),5) Bumps
            // /(s(*(10,+(^(x,2),^(y,2)))),10)
        }

        private void SolveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get input string
            var input = this.InputStringField.Text;
            if (input.Length >= 1)
            {
                Function newFuncToAdd = new Function(input);
                newFuncToAdd.Calculate3D(-10, 10, -10, 10);
                
                PlotFunction3D(newFuncToAdd);
            }
        }

        public void PlotFunction3D(Function function3d)
        {
            MeshBuilder meshBuilder = new MeshBuilder();
            var plotModel = new Model3DGroup();

            int xLenght = function3d.FunctionSolutions3D.Count;
            int yLenght = function3d.FunctionSolutions3D[0].Count;
            double sclae = 0.5;

            for (int xIndex = 0; xIndex < xLenght - 1; xIndex++)
            {
                // Get list for this x
                for (int yIndex = 0; yIndex < yLenght - 1; yIndex++)
                {
                    Point3D pointA = function3d.FunctionSolutions3D[xIndex][yIndex];
                    pointA.Z = pointA.Z / sclae;
                    Point3D pointB = function3d.FunctionSolutions3D[xIndex][yIndex + 1];
                    pointB.Z = pointB.Z / sclae;
                    Point3D pointC = function3d.FunctionSolutions3D[xIndex + 1][yIndex];
                    pointC.Z = pointC.Z / sclae;

                    Point3D pointD = function3d.FunctionSolutions3D[xIndex + 1][yIndex + 1];
                    pointD.Z = pointD.Z / sclae;

                    List<Point3D> points = new List<Point3D>();
                    points.Add(pointA);
                    points.Add(pointB);
                    points.Add(pointC);

                    List<Point3D> points2 = new List<Point3D>();
                    points2.Add(pointC);
                    points2.Add(pointB);
                    points2.Add(pointD);

                    meshBuilder.AddPolygon(points);
                    meshBuilder.AddPolygon(points2);

                    List<Point3D> points3 = new List<Point3D>();
                    points3.Add(pointB);
                    points3.Add(pointC);
                    points3.Add(pointD);

                    List<Point3D> points4 = new List<Point3D>();
                    points4.Add(pointB);
                    points4.Add(pointA);
                    points4.Add(pointC);
                    
                    meshBuilder.AddPolygon(points3);
                    meshBuilder.AddPolygon(points4);
                }
            }
            
            var surfaceModel = new GeometryModel3D(meshBuilder.ToMesh(), Materials.Blue);
            plotModel.Children.Add(surfaceModel);

            ModelVisual3D a = new ModelVisual3D { Content = plotModel };
            this.Viewport3D.Children.Add(a);
        }
    }
}
