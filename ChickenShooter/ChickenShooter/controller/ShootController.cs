﻿using ChickenShooter.controller.actions;
using ChickenShooter.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace ChickenShooter.controller
{
    public class ShootController
    {

        private Game game;
        private MainWindow gameWindow;
        private Boolean hasEvents;
        private double x;
        private double y;
        public Boolean HasEvents { get { return hasEvents; } set { hasEvents = value; } }
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }

        public ShootController(Game game, MainWindow gameWindow)
        {
            this.game = game;
            this.gameWindow = gameWindow;
            this.game.ShootControl = this;
            this.addMouseListener();
        }

        public void addMouseListener()
        {
            game.Canvas.MouseDown += new MouseButtonEventHandler(shootEvent);
            game.GameWindow.KeyDown += new KeyEventHandler(shootEventKeyboard);
        }

        public void shootEventKeyboard(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Point p = System.Windows.Input.Mouse.GetPosition(game.Canvas);
                ShootAction sa = new ShootAction(this.game, p.X, p.Y);
                //sa.X = p.X;
                //sa.Y = p.Y;
                game.ActionsContainer.Enqueue(sa);
                game.ProcessInput = true;
            }
            else if (e.Key == Key.T)
            {
                game.restartGame();
            }
        }

        public void shootEvent(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
            {
                Point p = System.Windows.Input.Mouse.GetPosition(game.Canvas);
                ShootAction sa = new ShootAction(this.game, p.X, p.Y);
                //sa.X = p.X;
                //sa.Y = p.Y;
                game.ActionsContainer.Enqueue(sa);
                game.ProcessInput = true;
            }
        }

    }
}
