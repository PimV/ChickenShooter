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
        private Boolean slowDownActive;

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
            movingLeft = true;
            movingDown = true;
            moveSpeed = 5;
            slowDownActive = false;
            maxSpeed = 2;
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
            movingLeft = false;
            movingDown = true;
            moveSpeed = 2;
            slowDownActive = false;
            maxSpeed = 5;
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
            movingLeft = false;
            movingDown = true;
            moveSpeed = 5;
            maxSpeed = 8;
            slowDownActive = false;
        }

        public void getNextPosition()
        {

            if (movingLeft)
            {


                dx -= moveSpeed;

                if (dx < -moveSpeed)
                {
                    dx = -moveSpeed;
                }
            }
            else if (movingRight)
            {

                dx += moveSpeed;


                if (dx > moveSpeed)
                {
                    dx = moveSpeed;
                }
            }

            if (movingUp)
            {

                dy -= moveSpeed;

                if (dy < -moveSpeed)
                {
                    dy = -moveSpeed;
                }
            }
            else if (movingDown)
            {

                dy += moveSpeed;

                if (dy > moveSpeed)
                {
                    dy = moveSpeed;
                }
            }
        }

        public void update()
        {
            getNextPosition();

            moveRandomlyDynamic();
        }

        public void moveRandomlyDynamic()
        {
            if (dx > 0)
            {
                if (x + dx + width > screen_width)
                {
                    dx = 0;
                    movingLeft = true;
                    movingRight = false;
                }
                else
                {
                    x += dx;
                }
            }
            if (dx < 0)
            {
                if (x + dx < 0)
                {
                    dx = 0;
                    movingLeft = false;
                    movingRight = true;
                }
                else
                {
                    x += dx;
                }
            }

            if (dy > 0)
            {
                if (y + dy + height > screen_height)
                {
                    dy = 0;
                    movingUp = true;
                    movingDown = false;
                }
                else
                {
                    y += dy;
                }
            }

            if (dy < 0)
            {
                if (y + dy < 0)
                {
                    dy = 0;
                    movingUp = false;
                    movingDown = true;
                }
                else
                {
                    y += dy;
                }
            }
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
            this.slowDownActive = true;
        }

        public void speedUp()
        {
            //this.slowDownActive = false;
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
