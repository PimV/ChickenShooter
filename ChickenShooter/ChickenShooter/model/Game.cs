using ChickenShooter.controller;
using ChickenShooter.controller.actions;
using ChickenShooter.helper;
using ChickenShooter.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChickenShooter.model
{
    public class Game
    {
        #region helpers
        private SoundPlayer player;
        private Boolean running;
        private Boolean gameRunning;
        public Boolean GameRunning { get { return gameRunning; } set { gameRunning = value; } }
        private helper.Timer hqTimer;
        public Boolean Running { get { return running; } set { running = value; } }
        public SoundPlayer Player { get { return player; } set { player = value; } }
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

            initModels();
            initView();
            this.running = true;
            runGameLoop();
        }

        public void restartGame()
        {
            if (statusTracker.GameRunning == false)
            {
                gameLoopThread.Abort();
                initModels();

                runGameLoop();
            }
        }

        public void runGameLoop()
        {
            gameLoopThread = new Thread(new ThreadStart(gameLoop));
            gameLoopThread.Name = "Game Loop";
            gameLoopThread.IsBackground = true;
            gameLoopThread.Start();
            this.statusTracker.GameRunning = true;

        }

        public void initModels()
        {

            actionsContainer = new ActionContainer();
            //Entity Models
            chickens = new List<Chicken>();
            chickens.Add(new Chicken(0, 0));
            //chickens.Add(new Chicken(80, 80));
            //chickens.Add(new Chicken(150, 80));
            //chickens.Add(new Chicken(80, 250));
            //chickens.Add(new Chicken(45, 80));
            //chickens.Add(new Chicken(190, 150));

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
            player = new SoundPlayer();
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
                if (hqTimer.ElapsedTicks > 10000000)
                {
                    return;
                }

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


                if (statusTracker.GameRunning)
                {
                    this.gameLogic(delta);
                    this.handleInput();

                }

                this.renderGame(delta);
                this.checkGameStatus();

               Thread.Sleep(155);
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
            ControllerAction action;
            lock (actionsContainer)
            {
                while (actionsContainer.Count > 0)
                {
                    actionsContainer.TryDequeue(out action);
                    if (action == null)
                    {
                        continue;
                    }
                    action.execute();
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
                this.statusTracker.StatusMsg = "Congratulations, you won! Press 'T' to start over!";
                this.statusTracker.GameRunning = false;
            }
            else if (statusTracker.Bullets == 0)
            {
                this.statusTracker.StatusMsg = "Too bad, you lost (no bullets left)! Press 'T' to start over!";
                this.statusTracker.GameRunning = false;
            }
        }
    }
}
