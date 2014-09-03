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
        private List<System.Windows.Shapes.Rectangle> chickenRects;

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

        public void renderChickenRenewCanvas(Chicken chicken)
        {

            System.Windows.Shapes.Rectangle chickenRectangle = new System.Windows.Shapes.Rectangle();
            chickenRectangle.Stroke = new SolidColorBrush(Colors.Black);
            chickenRectangle.Fill = new SolidColorBrush(Colors.Black);

            chickenRectangle.Width = chicken.Width;
            chickenRectangle.Height = chicken.Height;
            Canvas.SetLeft(chickenRectangle, chicken.X);
            Canvas.SetTop(chickenRectangle, chicken.Y);

            paintCanvas.Children.Add(chickenRectangle);

        }
        public void renderChickenImage(Chicken chicken)
        {
            Image chickenIcon = new Image();
            BitmapImage chickenImage = new BitmapImage(new Uri("..\\images\\chicken.jpg", UriKind.RelativeOrAbsolute));
            chickenIcon.Source = chickenImage;
            chickenIcon.Width = chicken.Width;
            chickenIcon.Height = chicken.Height;
            Canvas.SetLeft(chickenIcon, chicken.X);
            Canvas.SetTop(chickenIcon, chicken.Y);
            paintCanvas.Children.Add(chickenIcon);




        }

        public void renderChickens(List<Chicken> chickens)
        {
            //paintCanvas.Children.Clear();
            clearCanvas();
            foreach (Chicken chicken in chickens)
            {
                //renderChickenRenewCanvas(chicken);
                renderChickenImage(chicken);
            }
        }

        public void clearCanvas()
        {
            //String fpsContent = (String) fpsLabel.Content;
            //String bulletCountLabelContent = (String) bulletCountLabel.Content;
            //String hitCountLabelContent = (String)hitCountLabel.Content;
            //String timeLabelContent = (String)timeLabel.Content;

            System.Windows.UIElement fps = fpsLabel;
            System.Windows.UIElement bulletCount = bulletCountLabel;
            System.Windows.UIElement hitCount = hitCountLabel;
            System.Windows.UIElement time = timeLabel;

            paintCanvas.Children.Clear();

            paintCanvas.Children.Add(fps);
            paintCanvas.Children.Add(bulletCount);
            paintCanvas.Children.Add(hitCount);
            paintCanvas.Children.Add(time);
        }

        public void updateFPS(double fps)
        {
            fpsLabel.Content = "FPS: " + fps;
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


        public void mouse_down(object sender, EventArgs e)
        {
            Point p = System.Windows.Input.Mouse.GetPosition(paintCanvas);
            gameController.shoot(p.X, p.Y);
        }



    }
}
