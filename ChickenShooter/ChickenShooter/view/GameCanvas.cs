using ChickenShooter.controller;
using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChickenShooter.view
{
    public class GameCanvas : Canvas
    {

        private MainWindow gameWindow;
        private Game game;

        public GameCanvas(Game game, MainWindow gameWindow)
            : base()
        {

            this.game = game;
            this.gameWindow = gameWindow;

            this.Width = 500;
            this.Height = 300;
            this.MaxHeight = 300;
            this.MaxWidth = 500;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.Name = "paintCanvas";

            this.Background = new SolidColorBrush(Colors.White);

            gameWindow.Content = this;

            game.ControllerThread = new Thread(makeController);
            game.ControllerThread.Start();

        }

        public void makeController()
        {
            ShootController sc = new ShootController(game, gameWindow);
            BulletTimeController bc = new BulletTimeController(game, gameWindow);

            while (this.game.Running)
            {
                Thread.Sleep(10);
            }
        }

        public void renderSingleChicken(Chicken chicken)
        {
            Image chickenIcon = new Image();
            BitmapImage chickenImage = new BitmapImage(new Uri("..\\images\\chicken.jpg", UriKind.RelativeOrAbsolute));
            chickenIcon.Source = chickenImage;
            chickenIcon.Width = chicken.Width;
            chickenIcon.Height = chicken.Height;
            Canvas.SetLeft(chickenIcon, chicken.X);
            Canvas.SetTop(chickenIcon, chicken.Y);
            this.Children.Add(chickenIcon);
        }

        public void renderChickens()
        {
            foreach (Chicken chicken in game.Chickens)
            {
                renderSingleChicken(chicken);
            }
        }

        public void update()
        {
            //Utilize UI Thread to update GUI
            this.Dispatcher.Invoke(new Action(() =>
            {
                clearCanvas();
                renderChickens();
            }));
        }

        public void clearCanvas()
        {
            this.Children.Clear();
        }

    }
}
