using ChickenShooter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model
{
    public enum EntityTypes
    {
        [EntityInfoAttribute(typeof(Chicken))]
        Chicken,
        [EntityInfoAttribute(typeof(Balloon))]
        Balloon,
        [EntityInfoAttribute(typeof(Bullet))]
        Bullet,
    }
}
