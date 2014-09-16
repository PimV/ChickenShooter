using System;

namespace ChickenShooter.Model
{
    public interface IShootable
    {
        Boolean IsAlive { get; set; }
        Boolean isHit(double x, double y);


    }
}
