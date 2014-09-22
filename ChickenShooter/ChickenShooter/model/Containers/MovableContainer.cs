using ChickenShooter.Model.Entities;
using System.Collections.Generic;

namespace ChickenShooter.Model.Containers
{
    public class MovableContainer : EntityContainer
    {
        public override void update(double dt)
        {
            foreach (Entity e in this)
            {
                //moveRandomly(e);
            }
            //throw new System.NotImplementedException();
        }
    }
}
