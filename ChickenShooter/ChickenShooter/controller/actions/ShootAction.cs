using ChickenShooter.Model;
using ChickenShooter.Model.Entities;
using ChickenShooter.Model.GameState;
using System;

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

        public ShootAction(GameStateManager gsm, double x, double y)
        {
            this.GSM = gsm;
            this.x = x;
            this.y = y;
        }


        public override void execute()
        {
            this.GSM.Player.SoundLocation = "Sounds\\Small_Gun_Shot.wav";
            this.GSM.Player.Play();

            //game.Bullets.Add(EntityFactory.createBullet(X, Y));

            //game.MainContainer[Behaviour.Shootable].shoot();

            foreach (IShootable e in this.GSM.CurrentState.MainContainer[Behaviour.Shootable])
            {
                
                if (e.IsAlive && e.isHit(X, Y))
                {
                    Console.WriteLine("HIT");
                    this.GSM.Player.SoundLocation = "Sounds\\Blood_Hit.wav";
                    this.GSM.Player.Play();
                    e.IsAlive = false;
                    //game.StatusTracker.increaseScore();
                    break;
                }
            }
            // game.StatusTracker.decreaseBullets();
        }

    }
}
