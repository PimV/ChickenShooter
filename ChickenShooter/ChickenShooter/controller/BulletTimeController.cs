using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChickenShooter.controller
{
    public class BulletTimeController
    {

        private Game game;
        private MainWindow gameWindow;
        private Boolean hasEvents;
        public Boolean HasEvents { get { return hasEvents; } set { hasEvents = value; } }

        public BulletTimeController(Game game, MainWindow gameWindow)
        {
            this.game = game;
            this.gameWindow = gameWindow;
            this.game.BulletTimeControl = this;
            this.addMouseListener();
        }

        public void addMouseListener()
        {
            game.Canvas.MouseDown += new MouseButtonEventHandler(shootEvent);
        }

        public void shootEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
               // Console.WriteLine("Bullet Time!");
                this.hasEvents = true;
                game.ProcessInput = true;
            }
        }

    }
}
