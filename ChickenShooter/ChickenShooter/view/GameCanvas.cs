using ChickenShooter.controller;
using ChickenShooter.Model;
using ChickenShooter.Model.Containers;
using ChickenShooter.Model.Entities;
using ChickenShooter.Model.GameState;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChickenShooter.view
{
    public class GameCanvas : Canvas
    {

        private MainWindow gameWindow;
        private Game game;
        private Label bulletCountLabel;
        private Label hitCountLabel;
        private Label timeLabel;
        private Label fpsLabel;
        private Label statusLabel;
        private BitmapImage chickenImage = new BitmapImage(new Uri("..\\Images\\chicken.png", UriKind.RelativeOrAbsolute));
        private BitmapImage bulletImage = new BitmapImage(new Uri("..\\Images\\gunshot-clipart-bullet-hole-hi.png", UriKind.RelativeOrAbsolute));
        private BitmapImage hitsplatImage = new BitmapImage(new Uri("..\\Images\\hitsplat.png", UriKind.RelativeOrAbsolute));
        private BitmapImage balloonImage = new BitmapImage(new Uri("..\\Images\\balloon.png", UriKind.RelativeOrAbsolute));


        public GameCanvas(Game game, MainWindow gameWindow)
            : base()
        {

            this.game = game;
            this.gameWindow = gameWindow;

            this.Name = "paintCanvas";
            this.Background = new SolidColorBrush(Colors.White);
            this.SetValue(Grid.ColumnProperty, 1);
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.MaxWidth = 500;
            this.MaxHeight = 300;
            this.SetValue(Grid.ColumnSpanProperty, 2);

            this.createInfoLabels();

            gameWindow.mainGrid.Children.Add(this);

            game.ControllerThread = new Thread(makeController);
            game.ControllerThread.Name = "Controller Thread";
            game.ControllerThread.Start();

        }

        public GameCanvas(GameStateManager gsm, MainWindow gameWindow)
        {
            this.GSM = gsm;
            this.gameWindow = gameWindow;

            this.Name = "paintCanvas";
            this.Background = new SolidColorBrush(Colors.White);
            this.SetValue(Grid.ColumnProperty, 1);
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.MaxWidth = 500;
            this.MaxHeight = 300;
            this.SetValue(Grid.ColumnSpanProperty, 2);

            this.createInfoLabels();

            gameWindow.mainGrid.Children.Add(this);

            Thread t = new Thread(makeController);
            t.Name = "Controller Thread";
            t.Start();
        }

        public void setBulletLabel(int bullets)
        {
            if (bulletCountLabel == null)
            {
                bulletCountLabel = new Label();
            }
            bulletCountLabel.Content = "Bullets: " + bullets;
            bulletCountLabel.Name = "bulletCountLabel";
            bulletCountLabel.Width = 114;
            Canvas.SetZIndex(bulletCountLabel, 100);
            Canvas.SetTop(bulletCountLabel, 248);
            this.Children.Add(bulletCountLabel);
        }

        public void setHitCountLabel(int hitCount)
        {
            if (hitCountLabel == null)
            {
                hitCountLabel = new Label();
            }

            hitCountLabel.Name = "hitCountLabel";
            hitCountLabel.Content = "Hits: " + hitCount;
            Canvas.SetZIndex(hitCountLabel, 100);
            Canvas.SetTop(hitCountLabel, 274);
            hitCountLabel.Width = 114;
            this.Children.Add(hitCountLabel);
        }

        public void setTimeLabel(double time)
        {

            if (timeLabel == null)
            {
                timeLabel = new Label();
            }

            timeLabel.Name = "timeLabel";
            timeLabel.Content = "Time: " + time;
            Canvas.SetZIndex(timeLabel, 100);
            Canvas.SetTop(timeLabel, -1);
            this.Children.Add(timeLabel);
        }

        public void setFpsLabel(double fps)
        {
            if (fpsLabel == null)
            {
                fpsLabel = new Label();
            }
            fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Content = "FPS: " + fps;
            Canvas.SetLeft(fpsLabel, 421);
            Canvas.SetZIndex(fpsLabel, 100);
            Canvas.SetTop(fpsLabel, -1);
            this.Children.Add(fpsLabel);
        }

        public void setStatusLabel(String statusMsg)
        {

            if (statusLabel == null)
            {
                statusLabel = new Label();
            }
            statusLabel.Name = "statusLabel";
            statusLabel.Content = statusMsg;
            statusLabel.Width = this.MaxWidth;
            statusLabel.FontSize = 18;
            statusLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            statusLabel.Height = 36;
            Canvas.SetTop(statusLabel, Math.Round(this.MaxHeight / 2) - statusLabel.Height);
            Canvas.SetZIndex(statusLabel, 150);
            this.Children.Add(statusLabel);
        }

        public void createInfoLabels()
        {
            if (game != null)
            {
                setBulletLabel(game.StatusTracker.Bullets);
                setHitCountLabel(game.StatusTracker.Score);
                setTimeLabel(game.StatusTracker.GameTime);
                setFpsLabel(game.StatusTracker.RealTimeFps);
                setStatusLabel(game.StatusTracker.StatusMsg);
            }
            else
            {
               // Console.WriteLine("GOO");
            }
        }

        public void makeController()
        {

            // ShootController sc = new ShootController(game, gameWindow);
            ShootController sc = new ShootController(this.GSM, gameWindow);
            BulletTimeController bc = new BulletTimeController(game, gameWindow);

            while (true)
            {
                Thread.Sleep(10);
            }
        }

        public void renderSingleChicken(Chicken chicken)
        {
            Image chickenIcon = new Image();
            chickenIcon.Source = chickenImage;
            chickenIcon.Width = chicken.Width;
            chickenIcon.Height = chicken.Height;
            Canvas.SetLeft(chickenIcon, chicken.X);
            Canvas.SetTop(chickenIcon, chicken.Y);
            this.Children.Add(chickenIcon);
        }

        public void renderBullet(Bullet bullet)
        {
            Image bulletIcon = new Image();
            bulletIcon.Source = bulletImage;
            bulletIcon.Width = bullet.Width;
            bulletIcon.Height = bullet.Height;
            Canvas.SetLeft(bulletIcon, bullet.X - bullet.Width / 2);
            Canvas.SetTop(bulletIcon, bullet.Y - bullet.Height / 2);
            Canvas.SetZIndex(bulletIcon, -1);
            this.Children.Add(bulletIcon);
        }

        public void renderHitsplat(Chicken chicken)
        {
            Image hitsplatIcon = new Image();
            hitsplatIcon.Source = hitsplatImage;
            hitsplatIcon.Width = chicken.Width;
            hitsplatIcon.Height = chicken.Height;
            Canvas.SetLeft(hitsplatIcon, chicken.X);
            Canvas.SetTop(hitsplatIcon, chicken.Y);
            Canvas.SetZIndex(hitsplatIcon, 0);
            this.Children.Add(hitsplatIcon);
        }


        public void renderBalloon(Balloon balloon)
        {
            Image balloonIcon = new Image();
            balloonIcon.Source = balloonImage;
            balloonIcon.Width = balloon.Width;
            balloonIcon.Height = balloon.Height;
            Canvas.SetLeft(balloonIcon, balloon.X);
            Canvas.SetTop(balloonIcon, balloon.Y);
            Canvas.SetZIndex(balloonIcon, -1);
            this.Children.Add(balloonIcon);
        }

        public void renderChickens()
        {
            foreach (Chicken chicken in game.MainContainer[Behaviour.Visible].OfType<Chicken>())
            {
                if (chicken.IsAlive)
                {
                    renderSingleChicken(chicken);
                }
                else
                {
                    renderHitsplat(chicken);
                }
            }
        }


        public void renderBalloons()
        {
            foreach (Balloon balloon in game.MainContainer[Behaviour.Visible].OfType<Balloon>())
            {
                renderBalloon(balloon);
            }
        }

        public void renderBullets()
        {
            foreach (Bullet bullet in game.Bullets)
            {
                renderBullet(bullet);
            }
        }

        public void renderEntities(EntityContainer ec)
        {


            this.Dispatcher.Invoke(new Action(() =>
            {
                this.clearCanvas();
                foreach (Entity e in ec)
                {
                    Image entityIcon = new Image();
                    if (e.GetType() == typeof(Chicken))
                    {
                        entityIcon.Source = chickenImage;
                    }
                    else if (e.GetType() == typeof(Bullet))
                    {
                        entityIcon.Source = bulletImage;
                    }
                    else if (e.GetType() == typeof(Balloon))
                    {
                        entityIcon.Source = balloonImage;
                    }
                    entityIcon.Width = e.Width;
                    entityIcon.Height = e.Height;
                    Canvas.SetLeft(entityIcon, e.X);
                    Canvas.SetTop(entityIcon, e.Y);
                    Canvas.SetZIndex(entityIcon, -1);
                    this.Children.Add(entityIcon);

                }
            }));
        }

        public void update()
        {
            //Utilize UI Thread to update GUI
            this.Dispatcher.Invoke(new Action(() =>
            {
                clearCanvas();
                renderChickens();
                renderBalloons();
                renderBullets();
            }));
        }

        public void clearCanvas()
        {
            this.Children.Clear();
            createInfoLabels();
        }


        public GameStateManager GSM { get; set; }
    }
}
