using ChickenShooter.controller;
using ChickenShooter.controller.actions;
using ChickenShooter.helper;
using ChickenShooter.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ChickenShooter.model
{
    public class Game
    {
        #region helpers
        private Boolean running;
        private helper.Timer hqTimer;
        public Boolean Running { get { return running; } set { running = value; } }
        #endregion
        #region models
        private StatTracker statusTracker;
        private List<Chicken> chickens;
        private List<Bullet> bullets;
        public StatTracker StatusTracker { get { return statusTracker; } set { statusTracker = value; } }
        public List<Chicken> Chickens { get { return chickens; } set { chickens = value; } }
        public List<Bullet> Bullets { get { return bullets; } set { bullets = value; } }
        #endregion
        #region views
        private MainWindow gameWindow;
        private GameCanvas canvas;
        public GameCanvas Canvas { get { return canvas; } set { canvas = value; } }
        public MainWindow GameWindow { get { return gameWindow; } set { gameWindow = value; } }
        #endregion
        #region controllers
        private ActionContainer actionsContainer;
        private ShootController shootControl;
        private BulletTimeController bulletTimeControl;
        private Boolean processInput;
        public ActionContainer ActionsContainer { get { return actionsContainer; } set { actionsContainer = value; } }
        public ShootController ShootControl { get { return shootControl; } set { shootControl = value; } }
        public BulletTimeController BulletTimeControl { get { return bulletTimeControl; } set { bulletTimeControl = value; } }
        public Boolean ProcessInput { get { return processInput; } set { processInput = value; } }
        #endregion
        #region threads
        private Thread gameLoopThread;
        private Thread controllerThread;
        public Thread GameLoopThread { get { return gameLoopThread; } set { gameLoopThread = value; } }
        public Thread ControllerThread { get { return controllerThread; } set { controllerThread = value; } }
        #endregion

        public Game()
        {
            actionsContainer = new ActionContainer();
            initModels();
            initView();
            this.running = true;

            runGameLoop();


        }

        public void runGameLoop()
        {
            gameLoopThread = new Thread(new ThreadStart(gameLoop));
            gameLoopThread.Name = "Game Loop";
            gameLoopThread.IsBackground = true;
            gameLoopThread.Start();

        }

        public void initModels()
        {
            //Entity Models
            chickens = new List<Chicken>();
            //chickens.Add(new Chicken(5, 5, -5, 5));
            // chickens.Add(new Chicken(80, 80, 5, -5));
            chickens.Add(new Chicken(80, 80));
            chickens.Add(new Chicken(150, 80));
            chickens.Add(new Chicken(80, 250));
            chickens.Add(new Chicken(45, 80));
            chickens.Add(new Chicken(190, 150));

            bullets = new List<Bullet>();


            //Game Status
            statusTracker = new StatTracker();
            statusTracker.MAX_SCORE = chickens.Count;

            //Timer
            hqTimer = new helper.Timer();
        }

        public void initView()
        {
            gameWindow = new MainWindow(this);
            canvas = new GameCanvas(this, gameWindow);
            gameWindow.Show();
        }



        /**
         * Game loop
         */
        public void gameLoop()
        {
            double TARGET_FPS = 60;
            double OPTIMAL_TIME = 1000 / TARGET_FPS;

            hqTimer.Start();
            long lastLoopTime = hqTimer.ElapsedMilliSeconds;

            long lastFpsTime = 0;
            int fps = 0;

            while (this.running)
            {
                long now = hqTimer.ElapsedMilliSeconds;
                long updateLength = now - lastLoopTime;
                lastLoopTime = now;
                double delta = updateLength / OPTIMAL_TIME;

                lastFpsTime += updateLength;
                fps++;

                if (lastFpsTime >= 1000)
                {
                    statusTracker.RealTimeFps = fps;
                    lastFpsTime = 0;
                    fps = 0;
                }



                this.handleInput();
                this.gameLogic(delta);
                this.renderGame(delta);
                this.checkGameStatus();


                if (lastLoopTime - hqTimer.ElapsedMilliSeconds + OPTIMAL_TIME > 0)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(lastLoopTime - hqTimer.ElapsedMilliSeconds + OPTIMAL_TIME));
                }

            }
        }

        /**
         * Handle the input from the controllers
         */
        public void handleInput()
        {
            if (!this.ProcessInput)
            {
                return;
            }
            ControllerAction ca;
            while (actionsContainer.Count > 0)
            {
                actionsContainer.TryDequeue(out ca);
                if (ca == null)
                {
                    MessageBox.Show("SHOULD NOT BE HAPPENING!");
                    this.ProcessInput = false;
                    continue;
                }
                else
                {
                    switch (ca.ActionName)
                    {
                        case "shoot":
                            ca = (ShootAction)ca;
                            bullets.Add(new Bullet(ca.X, ca.Y));
                            foreach (Chicken chicken in chickens)
                            {
                                if (chicken.IsAlive && chicken.isHit(ca.X, ca.Y))
                                {
                                    chicken.IsAlive = false;
                                    statusTracker.increaseScore();
                                    break;
                                }
                            }
                            statusTracker.decreaseBullets();
                            ShootControl.HasEvents = false;
                            break;
                        case "slow motion":
                            foreach (Chicken chicken in chickens)
                            {
                                chicken.slowDown();
                            }
                            BulletTimeControl.HasEvents = false;
                            break;
                    }
                }
            }
            this.ProcessInput = false;
        }


        /**
         * Update Models
         */
        public void gameLogic(double dt)
        {
            statusTracker.GameTime = hqTimer.ElapsedMilliSeconds;

            foreach (Chicken chicken in chickens)
            {
                chicken.DeltaTime = dt;
                chicken.update();
            }


        }

        /**
         * Update View
         */
        public void renderGame(double dt)
        {

            this.canvas.update();
        }

        /**
         * Check the game status
         */
        public void checkGameStatus()
        {
            if (statusTracker.Score == statusTracker.MAX_SCORE)
            {
                Console.WriteLine("FINISH GAME");
                this.running = false;
            }
        }

        public void calculateFps(double dt)
        {

        }
    }
}
