﻿using ChickenShooter.Model.Entities;
using System.Collections.Generic;

namespace ChickenShooter.Model.Containers
{
    public class ShootableContainer : EntityContainer
    {
        public override void update(double dt)
        {
            foreach (Entity e in this)
            {
                //e.update(dt);
            }
            //throw new System.NotImplementedException();
        }
    }
}
