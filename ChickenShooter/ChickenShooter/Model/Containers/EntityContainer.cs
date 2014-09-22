using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenShooter.Model.Entities;

namespace ChickenShooter.Model.Containers
{
    public abstract class EntityContainer : List<Entity>
    {

        public abstract void update(double dt);

    }
}
