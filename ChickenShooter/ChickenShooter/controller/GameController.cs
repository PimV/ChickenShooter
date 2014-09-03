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
        public List<Chicken> chickens;
        private Boolean running;
        private ChickenShooter.helper.Timer hqTimer;
        private int fps;
        private int interval;
        private StatTracker statTracker;
        private Thread gameThread;

        public GameController()
        {
            mainView = new MainWindow(this);
            mainView.Show();

            statTracker = new StatTracker();
            hqTimer = new helper.Timer();
            chickens = new List<Chicken>();
            chickens.Add(new Chicken(50, 50));
            chickens.Add(new Chicken(100, 100));
            chickens.Add(new Chicken(5, 5,-8,5));

            fps = 60;
            interval = 1000 / fps;
            running = true;

            gameThread = new Thread(update);
            gameThread.Start();
        }

        public void endGame()
        {
            running = false;
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
                foreach (Chicken chicken1 in chickens)
                {
                    chicken1.moveRandomly();
                }
                statTracker.setGameTime(timeElapsed);

                //Update View
                mainView.Dispatcher.Invoke(new Action(() =>
                {
                    //mainView.renderChicken(chicken);
                    mainView.renderChickens(chickens);
                    mainView.updateBullets(statTracker.Bullets);
                    mainView.updateScore(statTracker.Score);
                    mainView.updateTime(statTracker.GameTime);
                    if (timeElapsed / updateCount > 0)
                    {
                        mainView.updateFPS(1000 / (timeElapsed / updateCount));
                    }
                }));

                if (statTracker.Score == StatTracker.MAX_SCORE)
                {
                    this.endGame();
                    mainView.winGame();
                }
                else if (statTracker.Bullets == 0)
                {
                    this.endGame();
                    mainView.loseGame();
                }

                while (hqTimer.ElapsedMilliSeconds - timeElapsed < interval)
                {
                }


            }
        }

        public void shoot(double x, double y)
        {
            Console.WriteLine("SHOOT");
            foreach (Chicken chicken1 in chickens)
            {
                bool hit = chicken1.isHit((int)x, (int)y);
                if (hit)
                {
                   
                    statTracker.increaseScore();
                    chickens.Remove(chicken1);
                    break;
                }
            }
            
            statTracker.decreaseBullets();
        }

        public void slowDown()
        {
            Thread t = new Thread(new ThreadStart(slowDownThread));
            t.Start();

        }

        public void slowDownThread()
        {
            long startSlowTime = hqTimer.ElapsedMilliSeconds;
            foreach (Chicken chicken in chickens)
            {
                chicken.slowDown();
            }
            while (hqTimer.ElapsedMilliSeconds - startSlowTime < 2000)
            {
                Console.WriteLine("Wait for slow to pass");
            }
            foreach (Chicken chicken in chickens)
            {
                chicken.speedUp();
            }

        }

    }
}
