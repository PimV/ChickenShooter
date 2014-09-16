using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickenShooter.Model
{
    public class Balloon : Entity
    {
        private Boolean isAlive = true;
        public Boolean IsAlive { get { return isAlive; } set { isAlive = value; } }

        #region Constructors
        public Balloon()
            : base()
        {
            this.Height = 50;
            this.Width = 50;
            this.y = 40;
            this.moveSpeed = 5;
            this.dx = this.moveSpeed;
            this.stdMoveSpeed = moveSpeed;
            this.slowDownActive = false;
            this.determineDirection();
        }

        public Balloon(double x, double y)
            : base(x, y)
        {
            this.Height = 50;
            this.Width = 50;
            this.y = 40;
            this.dx = dx;
            this.dy = 0;
            this.moveSpeed = 5;
            this.stdMoveSpeed = moveSpeed;
            this.slowDownActive = false;
            this.determineDirection();
        }

        public Balloon(double x, double y, double dx, double dy)
            : base(x, y)
        {
            this.Height = 50;
            this.Width = 50;
            this.y = 40;
            this.dx = dx;
            this.dy = dy;
            this.moveSpeed = 5;
            this.stdMoveSpeed = moveSpeed;
            this.slowDownActive = false;
            this.determineDirection();
        }
        #endregion

        public override void update(double dt)
        {
            if (isAlive)
            {
                this.deltaTime = dt;
                getNextPosition();
                if (slowDownActive)
                {
                    activateSlowDown();
                }
                if (speedUpActive)
                {
                    activateSpeedUp();
                }
                moveRandomlyDynamic();
            }
        }

        #region Movement
        public void getNextPosition()
        {
            if (movingLeft)
            {
                dx -= moveSpeed * deltaTime;

                if (dx < -moveSpeed)
                {
                    dx = -moveSpeed;
                }
            }
            else if (movingRight)
            {
                dx += moveSpeed * deltaTime;

                if (dx > moveSpeed)
                {
                    dx = moveSpeed;
                }
            }
        }

        public void moveRandomlyDynamic()
        {
            if (dx > 0)
            {
                Console.WriteLine((x + (dx * deltaTime) + width));
                if (x + (dx * deltaTime) + width > screen_width)
                {
                    dx = 0;
                    movingLeft = true;
                    movingRight = false;
                }
                else
                {
                    x += dx * deltaTime;
                }
            }
            if (dx < 0)
            {
                if (x + (dx * deltaTime) < 0)
                {
                    dx = 0;
                    movingLeft = false;
                    movingRight = true;
                }
                else
                {
                    x += dx * deltaTime;
                }
            }
        }
        #endregion
    }
}
