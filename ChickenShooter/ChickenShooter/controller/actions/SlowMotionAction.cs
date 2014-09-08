﻿using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.controller.actions
{
    class SlowMotionAction : ControllerAction
    {
        public SlowMotionAction(Game game)
        {
            this.game = game;
        }

        public override void execute()
        {
            game.Player.SoundLocation = "Sounds\\Flame woosh.wav";
            game.Player.Play();
            foreach (Chicken chicken in game.Chickens)
            {
                chicken.slowDown();
            }
        }
    }
}
