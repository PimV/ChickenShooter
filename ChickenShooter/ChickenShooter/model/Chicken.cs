using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.model
{
    public class Chicken : Entity
    {

        private Random rnd;

        public Chicken()
            : base()
        {
            rnd = new Random();
            this.Height = 50;
            this.Width = 50;
            this.dx = 5;
            this.dy = 3;
        }

        public Chicken(int x, int y)
            : base(x, y)
        {
            rnd = new Random();
            this.Height = 50;
            this.Width = 50;
            this.dx = 2;
            this.dy = 3;
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

        public Boolean isHit(int x, int y)
        {
            if ((x >= X && x < (Width + X)) && (y > Y && y < (Height + Y)))
            {
                return true;
            }
            return false;
        }
    }
}
