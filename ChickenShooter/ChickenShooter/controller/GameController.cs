using ChickenShooter.helper;
using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ChickenShooter.controller
{
    public class GameController
    {
        public MainWindow mainView;
        public Chicken chicken;
        private Boolean running;
        private DispatcherTimer timer;
        private int fps;

        public GameController()
        {
            mainView = new MainWindow(this);
            mainView.Show();
            chicken = new Chicken(5, 5);

            fps = 60;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(update);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / fps);
            timer.Start();
            running = true;
        }

        public void update(object sender, EventArgs e)
        {
            if (running)
            {
                chicken.moveRandomly();
                mainView.renderChicken(chicken);
            }
            else
            {
                timer.Stop();
            }
        }

        public void shoot(double x, double y)
        {
            bool hit = chicken.isHit((int)x, (int)y);
            if (hit)
            {
                running = false;
            }


        }
    }
}
