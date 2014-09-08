﻿using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.controller.actions
{
    class ShootAction : ControllerAction
    {

        protected double x;
        protected double y;
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }

        public ShootAction(Game game, double x, double y)
        {
           
            this.game = game;
            this.x = x;
            this.y = y;
        }

        public override void execute()
        {
            game.Player.SoundLocation = "Sounds\\Small_Gun_Shot.wav";
            game.Player.Play();
            game.Bullets.Add(new Bullet(X, Y));

            foreach (Chicken chicken in game.Chickens)
            {
                if (chicken.IsAlive && chicken.isHit(X, Y))
                {
                    game.Player.SoundLocation = "Sounds\\Blood_Hit.wav";

                    game.Player.Play();
                    chicken.IsAlive = false;
                    game.StatusTracker.increaseScore();
                    break;
                }
            }
            game.StatusTracker.decreaseBullets();
        }

    }
}
