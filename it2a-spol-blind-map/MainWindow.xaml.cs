using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace it2a_spol_blind_map
{
    public partial class MainWindow : Window
    {
        List<MapPoint> points = new List<MapPoint>()
        {
            new MapPoint("Brno", 0.72f, 0.7f),
            new MapPoint("Praha", 0.37f, 0.38f),
            new MapPoint("Olomouc", 0.8f, 0.55f),
            new MapPoint("Ostrava", 0.88f, 0.45f),
            new MapPoint("Plzen", 0.18f, 0.54f)
        };

        private MapPoint aktivniMesto;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            Random rnd = new Random();
            int index = rnd.Next(points.Count);

            aktivniMesto = points[index];

            aktivni.Text = $"Najdi město: {aktivniMesto.Name}";
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MapPoint kliknuteMesto = btn.Tag as MapPoint;

            if (kliknuteMesto == aktivniMesto)
            {
                MessageBox.Show("Správně!");

                InitializeGame();
            }
            else
            {
                MessageBox.Show($"Chyba! Klikl jsi na {kliknuteMesto.Name}, ale hledáme {aktivniMesto.Name}.");
            }
        }

        void DrawPoints()
        {
            OverlayCanvas.Children.Clear();

            foreach (var point in points)
            {
                double x = MapImage.ActualWidth * point.XPercent;
                double y = MapImage.ActualHeight * point.YPercent;

                Button btn = new Button()
                {
                    Content = point.Name,
                    Tag = point,
                    Width = 60,
                    Height = 30
                };

                btn.Click += Btn_Click;

                Canvas.SetLeft(btn, x);
                Canvas.SetTop(btn, y);

                OverlayCanvas.Children.Add(btn);
            }
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPoints();
        }

        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MapImage);
            double xPercent = pos.X / MapImage.ActualWidth;
            double yPercent = pos.Y / MapImage.ActualHeight;

            Console.WriteLine($"new MapPoint(\"Název\", {xPercent:F4}f, {yPercent:F4}f),");
        }
    }
}