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
        private ChickenShooter.helper.Timer hqTimer;
        private int fps;
        private int interval;
        private StatTracker statTracker;
        private Thread gameThread;

        public GameController()
        {
            mainView = new MainWindow(this);
            mainView.Show();
            chicken = new Chicken(5, 5);
            statTracker = new StatTracker();
            hqTimer = new helper.Timer();



            fps = 60;
            interval = 1000 / fps;
            running = true;

            gameThread = new Thread(update);
            //hqTimer.Start();
            gameThread.Start();


            //timer = new DispatcherTimer();
            //timer.Tick += new EventHandler(update);
            //timer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            //timer.Start();


        }

        public void update()
        {
            hqTimer.Start();
            int updateCount = 0;
            while (running)
            {
                updateCount++;
                long timeElapsed = hqTimer.ElapsedMilliSeconds;

                //Update Models
                chicken.moveRandomly();
                statTracker.setGameTime(timeElapsed);

                //Update View
                mainView.Dispatcher.Invoke(new Action(() =>
                {
                    mainView.renderChicken(chicken);
                    mainView.updateBullets(statTracker.Bullets);
                    mainView.updateScore(statTracker.Score);
                    mainView.updateTime(statTracker.GameTime);
                    if (timeElapsed / updateCount > 0)
                    {
                        mainView.updateFPS(1000 / (timeElapsed / updateCount));
                    }

                }));


                while (hqTimer.ElapsedMilliSeconds - timeElapsed < interval)
                {
                }


                if (statTracker.Score == StatTracker.MAX_SCORE || statTracker.Bullets == 0)
                    running = false;
            }



            mainView.winGame();
            //mainView.Close();
            
            //mainView.loseGame();
            //mainView.Close();
        }

        public void shoot(double x, double y)
        {
            bool hit = chicken.isHit((int)x, (int)y);
            if (hit)
            {
                statTracker.increaseScore();
            }
            statTracker.decreaseBullets();
            
        }

    }
}
