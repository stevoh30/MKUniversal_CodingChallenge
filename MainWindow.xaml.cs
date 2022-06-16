using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace MKUniversal_CodingSegment
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Draws circles depending on user input from button-click
        public void CreateEllipse(int circle_number)
        {
            int drawn_circles = circle_number;
            // Variables to calculate number of circles
            // and distance between
            int diameter = 300/circle_number;
            int total_diameter = diameter;

            // Creates ellipse and first point at center of
            // ellipse if points are greater than 0
            if (drawn_circles > 0)
            {
                DrawEllipse(300);
                DrawPoint(0, 0, 0, 0);
            }

            // Creates ellipses evenly spaced within 300 pixels
            // depending on points from user input
            while(drawn_circles > 1)
            {
                // Creates Ellipse using diameter
                DrawEllipse(total_diameter);            
                // Creates points at specific coordinates on canvas
                CreatePoints(total_diameter);
                total_diameter += diameter;
                drawn_circles--;
            }
            // Reset values after function runs
            total_diameter = 0;
        }
        public void DrawEllipse(int diameter)
        {
            Ellipse myEllipse = new Ellipse();
            myEllipse.Stroke = System.Windows.Media.Brushes.Black;
            myEllipse.Width = diameter;
            myEllipse.Height = diameter;
            myCanvas.Children.Add(myEllipse);
        }

        public void CreatePoints(int diameter)
        {
            // Creates points on axis
            DrawPoint(diameter, 0, 0, 0);
            DrawPoint(0, diameter, 0, 0);
            DrawPoint(0, 0, diameter, 0);
            DrawPoint(0, 0, 0, diameter);

            // calculates angle of coordinates for points not on axis
            double angle = 45 * Math.PI / 180;
            double x = diameter * Math.Cos(angle);
            double y = diameter * Math.Sin(angle);
            DrawPoint(Convert.ToInt32(x), Convert.ToInt32(y), 0, 0);
            DrawPoint(0, 0, Convert.ToInt32(x), Convert.ToInt32(y));
            DrawPoint(Convert.ToInt32(x), 0,0, Convert.ToInt32(y));
            DrawPoint(0, Convert.ToInt32(y), Convert.ToInt32(x), 0);
        }
        public void DrawPoint(int left, int top, int right, int bottom)
        {
            Ellipse myEllipse = new Ellipse();
            myEllipse.Stroke = System.Windows.Media.Brushes.Red;
            myEllipse.Fill = System.Windows.Media.Brushes.Red;
            myEllipse.Width = 5;
            myEllipse.Height = 5;
            myEllipse.Margin = new Thickness(left, top, right, bottom);
            myCanvas.Children.Add(myEllipse);
        }

        // Calcuates number of circles created by the number of points
        // entered by the user
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Clears canvas of previous ellipse objects
            ResetCanvas();

            // Declare variables
            int circle_number = 0;

            // Attempts to convert user input to int value
            int points = 0;
            try
            {
                points = Int32.Parse(txtBoxPoints.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Checks to see user input value and how many
            // circles need to be created
            if(points == 1)
            {
                circle_number = 1;
            }
            else if(points%8-1 == 0)
            {
                circle_number = (points -1) / 8 + 1;
            }
            else
            {
                // Displays error message if number of points does not create circle
                // (divisible by 8) - 1
                circle_number = 0;
                MessageBox.Show("Error in Number of Points: Please try again! (1, 9, 17 etc)");
            }

            // if circle != 0, create circles
            if (circle_number != 0)
            {
                CreateEllipse(circle_number);       
            }
        }
        // Removes all ellipses from canvas
        public void ResetCanvas()
        {
            for(int i = myCanvas.Children.Count - 1; i >= 0; i += -1)
            {
                UIElement Child = myCanvas.Children[i];
                if(Child is Ellipse)
                {
                    myCanvas.Children.Remove(Child);
                }
            }
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point getpos = PointToScreen(Mouse.GetPosition(this));
            MessageBox.Show(getpos.ToString());
        }
    }
}
