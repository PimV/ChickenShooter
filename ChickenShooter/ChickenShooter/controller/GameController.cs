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
        private int interval;
        private StatTracker statTracker;

        public GameController()
        {
            mainView = new MainWindow(this);
            mainView.Show();
            chicken = new Chicken(5, 5);
            statTracker = new StatTracker();

            fps = 60;
            interval = 1000 / fps;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(update);
            timer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            timer.Start();
            running = true;
        }

        public void update(object sender, EventArgs e)
        {
            if (running)
            {
                statTracker.increaseTime(interval);
                chicken.moveRandomly();
                mainView.updateScore(statTracker.Score);
                mainView.updateTime(statTracker.GameTime);
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
                statTracker.increaseScore();
                //running = false;
            }


        }
    }
}
