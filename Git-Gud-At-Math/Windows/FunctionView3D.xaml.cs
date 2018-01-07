using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;
using Git_Gud_At_Math.Models;
using HelixToolkit.Wpf;

namespace Git_Gud_At_Math.Windows
{
    /// <summary>
    /// Interaction logic for FunctionView3D.xaml
    /// </summary>
    public partial class FunctionView3D : Window
    {
        public List<ModelVisual3D> FunctionModels { get; set; }

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

            FunctionModels = new List<ModelVisual3D>();
        }

        private void SolveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get input string
            var input = this.InputStringField.Text;

            var xStartField = double.Parse(XStartField.Text);
            var xEndField = double.Parse(XEndField.Text);
            var yStartField = double.Parse(YStartField.Text);
            var yEndField = double.Parse(YEndField.Text);
            var densityField = double.Parse(DensityField.Text);
            var scaleField = double.Parse(ScaleField.Text);

            if (input.Length >= 1)    
            {
                Function newFuncToAdd = new Function(input);
                newFuncToAdd.Calculate3D(xStartField, xEndField, yStartField, yEndField, densityField);
                
                PlotFunction3D(newFuncToAdd, scaleField);
            }
        }

        public void PlotFunction3D(Function function3D, double scale)
        {
            MeshBuilder meshBuilder = new MeshBuilder();
            var plotModel = new Model3DGroup();

            int xLenght = function3D.FunctionSolutions3D.Count;
            int yLenght = function3D.FunctionSolutions3D[0].Count;

            for (int xIndex = 0; xIndex < xLenght - 1; xIndex++)
            {
                // Get list for this x
                for (int yIndex = 0; yIndex < yLenght - 1; yIndex++)
                {
                    Point3D pointA = function3D.FunctionSolutions3D[xIndex][yIndex];
                    pointA.Z = pointA.Z / scale;
                    Point3D pointB = function3D.FunctionSolutions3D[xIndex][yIndex + 1];
                    pointB.Z = pointB.Z / scale;
                    Point3D pointC = function3D.FunctionSolutions3D[xIndex + 1][yIndex];
                    pointC.Z = pointC.Z / scale;

                    Point3D pointD = function3D.FunctionSolutions3D[xIndex + 1][yIndex + 1];
                    pointD.Z = pointD.Z / scale;

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

            ModelVisual3D functionModel = new ModelVisual3D { Content = plotModel };
            this.FunctionModels.Add(functionModel);
            this.Viewport3D.Children.Add(functionModel);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        { 
            this.InputStringField.Text = string.Empty;
            foreach (var functionModel in this.FunctionModels)
            {
                try
                {
                    this.Viewport3D.Children.Remove(functionModel);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        
    }
}
