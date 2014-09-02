using ChickenShooter.controller;
using ChickenShooter.helper;
using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChickenShooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public GameController gameController;
        private System.Windows.Shapes.Rectangle chickenRect;

        public MainWindow(GameController gameController)
            : base()
        {
            InitializeComponent();
            this.gameController = gameController;
            paintCanvas.MouseDown += new MouseButtonEventHandler(mouse_down);
        }


        public void renderChicken(Chicken chicken)
        {
            if (chickenRect == null)
            {
                chickenRect = new System.Windows.Shapes.Rectangle();
                chickenRect.Stroke = new SolidColorBrush(Colors.Black);
                chickenRect.Fill = new SolidColorBrush(Colors.Black);
            }
            chickenRect.Width = chicken.Width;
            chickenRect.Height = chicken.Height;
            Canvas.SetLeft(chickenRect, chicken.X);
            Canvas.SetTop(chickenRect, chicken.Y);
            if (!paintCanvas.Children.Contains(chickenRect))
            {
                paintCanvas.Children.Add(chickenRect);
            }
        }

        public void updateScore(int score)
        {
            hitCountLabel.Content = "Hits: " + score;
        }

        public void updateBullets(int bullets)
        {
            bulletCountLabel.Content = "Bullets: " + bullets;
        }

        public void updateTime(long time)
        {

            timeLabel.Content = "Time busy: " + time;
        }

        public void clearCanvas()
        {
            paintCanvas.Children.Clear();
        }

        public void mouse_down(object sender, EventArgs e)
        {
            Point p = System.Windows.Input.Mouse.GetPosition(paintCanvas);
            gameController.shoot(p.X, p.Y);
        }



    }
}
