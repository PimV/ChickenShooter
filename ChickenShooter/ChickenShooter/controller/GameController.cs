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

                if (statTracker.Score == StatTracker.MAX_SCORE)
                {
                    running = false;
                    mainView.winGame();
                }
                else if (statTracker.Bullets == 0)
                {
                    running = false;
                    mainView.loseGame();
                }

                while (hqTimer.ElapsedMilliSeconds - timeElapsed < interval)
                {
                }


            }
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
