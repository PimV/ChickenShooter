using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChickenShooter.controller
{
    public class ShootController
    {

        private Game game;
        private MainWindow gameWindow;
        private Boolean hasEvents;
        public Boolean HasEvents { get { return hasEvents; } set { hasEvents = value; } }

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
        }

        public void shootEvent(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
            {
                //Console.WriteLine("Shoot!");
                this.hasEvents = true;
                game.ProcessInput = true;
               // game.shoot();
            }
        }

    }
}
