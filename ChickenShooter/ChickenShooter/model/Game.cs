﻿using ChickenShooter.controller;
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
        private StatTracker statusTracker;
        private long currentGameTick;
        public GameCanvas Canvas { get { return canvas; } set { canvas = value; } }
        public MainWindow GameWindow { get { return gameWindow; } set { gameWindow = value; } }
        public Boolean Running { get { return running; } set { running = value; } }
        public List<Chicken> Chickens { get { return chickens; } set { chickens = value; } }
        public Boolean ProcessInput { get { return processInput; } set { processInput = value; } }
        public ShootController ShootControl { get { return shootControl; } set { shootControl = value; } }
        public BulletTimeController BulletTimeControl { get { return bulletTimeControl; } set { bulletTimeControl = value; } }
        public StatTracker StatusTracker { get { return statusTracker; } set { statusTracker = value; } }


        //Threads
        private Thread gameLoopThread;
        private Thread controllerThread;
        public Thread GameLoopThread { get { return gameLoopThread; } set { gameLoopThread = value; } }
        public Thread ControllerThread { get { return controllerThread; } set { controllerThread = value; } }



        public Game()
        {

            initModels();
            initView();
            this.running = true;

            gameLoopThread = new Thread(gameLoop);
            gameLoopThread.Name = "Game Loop";
            gameLoopThread.Start();


        }

        public void initModels()
        {
            //Entity Models
            chickens = new List<Chicken>();
            //chickens.Add(new Chicken(5, 5, -5, 5));
            // chickens.Add(new Chicken(80, 80, 5, -5));
            chickens.Add(new Chicken(80, 80));
            //chickens.Add(new Chicken(150, 80, 5, -5));
            //chickens.Add(new Chicken(80, 250, 10, -5));
            //chickens.Add(new Chicken(45, 80, 2, -5));
            //chickens.Add(new Chicken(190, 150, 5, -5));

            //Game Status
            statusTracker = new StatTracker();
            statusTracker.MAX_SCORE = chickens.Count;

            //Timer
            hqTimer = new helper.Timer();
            fps = 60;
            interval = 1000 / fps;
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
            hqTimer.Start();
            float currentTime = hqTimer.ElapsedMilliSeconds / 1000;
            

            while (this.running)
            {
                float newTime = hqTimer.ElapsedMilliSeconds / 1000;
                float frameTime = newTime - currentTime;
                currentTime = newTime;
               // frameTime = (float(frameTime) / 1000.0f);
               // if (frameTime > 0.25)
               // {
               //     frameTime = 0.25;
               // }
               // currentTime = newTime;

               // accumulator += frameTime;

               // while (accumulator >= dt)
               // {
               //     previous = current;
               //     integrate(current, t, dt);
               //     t += dt;
               //     accumulator -= dt;
               // }

               // double alpha = accumulator / dt;

               //// State state = current * alpha + prevoius * (1.0 - alpha);
               // State state = interpolate(previous, current, alpha);

                this.handleInput();
                this.gameLogic(frameTime);
                this.renderGame(frameTime);
                this.checkGameStatus();

                if (frameTime < interval)
                {
                    Thread.Sleep((int)Math.Round(interval - (frameTime)));
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
            lock (ShootControl)
            {
                if (ShootControl.HasEvents)
                {
                    foreach (Chicken chicken in chickens)
                    {
                        if (chicken.isHit(ShootControl.X, ShootControl.Y))
                        {
                            statusTracker.increaseScore();
                            chickens.Remove(chicken);
                            break;
                        }
                    }
                    statusTracker.decreaseBullets();
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
                    Console.WriteLine("Fix Bullet Time");
                    BulletTimeControl.HasEvents = false;
                }
            }

            this.processInput = false;
        }


        /**
         * Update Models
         */
        public void gameLogic(float frameTime)
        {
            statusTracker.GameTime = hqTimer.ElapsedMilliSeconds;

            foreach (Chicken chicken in chickens)
            {
                chicken.update(frameTime);
            }
        }

        /**
         * Update View
         */
        public void renderGame(double deltaTime)
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


    }
}
