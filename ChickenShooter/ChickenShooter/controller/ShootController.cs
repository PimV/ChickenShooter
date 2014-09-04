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

        public ShootController(Game game, MainWindow gameWindow)
        {
            this.game = game;
            this.gameWindow = gameWindow;
            this.addMouseListener();
        }

        public void addMouseListener()
        {
            game.Canvas.MouseDown += new MouseButtonEventHandler(shootEvent);
        }

        public void shootEvent(object sender, MouseEventArgs e)
        {
            Console.WriteLine("SHOOTING");
        }

    }
}
