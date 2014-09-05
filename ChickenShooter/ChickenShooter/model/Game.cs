using ChickenShooter.controller;
using ChickenShooter.helper;
using ChickenShooter.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickenShooter.model
{
    public class Game
    {
        //Variables
        private MainWindow gameWindow;
        private GameCanvas canvas;
        private Boolean running;
        private helper.Timer hqTimer;
        private double fps;
        private double interval;
        private List<Chicken> chickens;
        private Boolean processInput;
        private ShootController shootControl;
        private BulletTimeController bulletTimeControl;
        public GameCanvas Canvas { get { return canvas; } set { canvas = value; } }
        public MainWindow GameWindow { get { return gameWindow; } set { gameWindow = value; } }
        public Boolean Running { get { return running; } set { running = value; } }
        public List<Chicken> Chickens { get { return chickens; } set { chickens = value; } }
        public Boolean ProcessInput { get { return processInput; } set { processInput = value; } }
        public ShootController ShootControl { get { return shootControl; } set { shootControl = value; } }
        public BulletTimeController BulletTimeControl { get { return bulletTimeControl; } set { bulletTimeControl = value; } }


        //Threads
        private Thread gameLoopThread;
        private Thread controllerThread;
        public Thread GameLoopThread { get { return gameLoopThread; } set { gameLoopThread = value; } }
        public Thread ControllerThread { get { return controllerThread; } set { controllerThread = value; } }

        public Game()
        {

            hqTimer = new helper.Timer();
            fps = 60;
            interval = 1000 / fps;


            gameWindow = new MainWindow(this);

            canvas = new GameCanvas(this, gameWindow);
            this.running = true;
            gameWindow.Show();

            chickens = new List<Chicken>();
            chickens.Add(new Chicken(5, 5, -5, 5));
            chickens.Add(new Chicken(80, 80, 5, -5));

            gameLoopThread = new Thread(gameLoop);
            gameLoopThread.Start();


        }

        public void gameLoop()
        {
            hqTimer.Start();
            while (this.running)
            {
                long previousTimeElapsed = hqTimer.ElapsedMilliSeconds;

                if (this.processInput)
                {
                    this.handleInput();
                    Console.WriteLine("Has input to process");
                }

                this.gameLogic();
                this.renderGame();

                
                this.processInput = false;
                if (hqTimer.ElapsedMilliSeconds - previousTimeElapsed < interval)
                {
                    Thread.Sleep((int)Math.Round(interval - (hqTimer.ElapsedMilliSeconds - previousTimeElapsed)));
                }
            }
        }

        public void handleInput()
        {
            lock (ShootControl)
            {
                if (ShootControl.HasEvents)
                {

                    ShootControl.HasEvents = false;
                }
            }

            lock (BulletTimeControl)
            {
                if (BulletTimeControl.HasEvents)
                {
                    foreach (Chicken chicken in chickens)
                    {
                        chicken.slowDown();
                    }
                 
                    foreach (Chicken chicken in chickens)
                    {
                        chicken.speedUp();
                    }
                    Console.WriteLine("BULLET TIME!");
                    BulletTimeControl.HasEvents = false;
                }
            }
        }

        public void shoot()
        {

        }

        public void slowDown()
        {

        }

        /**
         * Update Models
         */
        public void gameLogic()
        {
            foreach (Chicken chicken in chickens)
            {
                chicken.moveRandomly();
            }
        }

        /**
         * Update View
         */
        public void renderGame()
        {
            this.canvas.update();
        }
    }
}
