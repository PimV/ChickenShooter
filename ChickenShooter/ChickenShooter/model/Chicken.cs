using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickenShooter.model
{
    public class Chicken : Entity
    {

        private Random rnd;

        private double defaultDx;
        private double defaultDy;
        private double minFactor = 0.5;
        private double maxFactor = 1;

        public Chicken()
            : base()
        {
            rnd = new Random();
            this.Height = 50;
            this.Width = 50;
            this.defaultDx = -5;
            this.defaultDy = 3;
            this.dx = this.defaultDx;
            this.dy = this.defaultDy;
        }

        public Chicken(double x, double y)
            : base(x, y)
        {
            rnd = new Random();
            this.Height = 50;
            this.Width = 50;
            this.defaultDx = -2;
            this.defaultDy = 3;
            this.dx = this.defaultDx;
            this.dy = this.defaultDy;
        }

        public Chicken(double x, double y, double dx, double dy)
            : base(x, y)
        {
            rnd = new Random();
            this.Height = 50;
            this.Width = 50;
            this.defaultDx = dx;
            this.defaultDy = dy;
            this.dx = this.defaultDx;
            this.dy = this.defaultDy;
        }

        public void moveRandomly()
        {
            if (x + dx + width > screen_width || x + dx < 0)
            {
                dx = -dx;
            }
            x += dx;
            if (y + dy + height > screen_height || y + dy < 0)
            {
                dy = -dy;
            }
            y += dy;
        }

        public void slowDown()
        {
            
            this.dx = defaultDx * minFactor;
            this.dy = defaultDy * minFactor;
        }

        public void speedUp()
        {
            this.dx = defaultDx * maxFactor;
            this.dy = defaultDy * maxFactor;
        }

        public Boolean isHit(double x, double y)
        {
            if ((x >= X && x < (Width + X)) && (y > Y && y < (Height + Y)))
            {
                return true;
            }
            return false;
        }
    }
}
