using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChickenShooter.controller
{
    public class ShootController
    {

        private Game game;
        private MainWindow gameWindow;
        private Boolean hasEvents;
        private double x;
        private double y;
        public Boolean HasEvents { get { return hasEvents; } set { hasEvents = value; } }
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }

        public ShootController(Game game, MainWindow gameWindow)
        {
            this.game = game;
            this.gameWindow = gameWindow;
            this.game.ShootControl = this;
            this.addMouseListener();
        }

        public void addMouseListener()
        {
            game.Canvas.MouseDown += new MouseButtonEventHandler(shootEvent);
            game.GameWindow.KeyDown += new KeyEventHandler(shootEventKeyboard);
        }

        public void shootEventKeyboard(object sender, KeyEventArgs e)
        {
            Console.WriteLine("SHOOOT");
            if (e.Key == Key.Space)
            {                
                Point p = System.Windows.Input.Mouse.GetPosition(game.Canvas);
                this.X = p.X;
                this.Y = p.Y;
                this.hasEvents = true;
                game.ProcessInput = true;
            }

            if (e.Key == Key.T)
            {
                Point p = System.Windows.Input.Mouse.GetPosition(game.Canvas);
                this.X = p.X;
                this.Y = p.Y;
                this.hasEvents = true;
                game.ProcessInput = true;
            }
        }

        public void shootEvent(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
            {
                Point p = System.Windows.Input.Mouse.GetPosition(game.Canvas);
                this.X = p.X;
                this.Y = p.Y;
                this.hasEvents = true;
                game.ProcessInput = true;
            }
        }

    }
}
