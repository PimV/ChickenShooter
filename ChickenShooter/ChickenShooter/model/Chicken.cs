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

        private double defaultDx;
        private double defaultDy;
        private double stdMoveSpeed;
        private Boolean slowDownActive;
        private Boolean speedUpActive;
        private double slowDownTime = 1500; //Hang for 1500ms in slowmow
        private Boolean isAlive = true;
        public Boolean IsAlive { get { return isAlive; } set { isAlive = value; } }

        #region Constructors
        public Chicken()
            : base()
        {

            this.Height = 50;
            this.Width = 50;
            this.defaultDx = -5;
            this.defaultDy = 3;
            this.dx = this.defaultDx;
            this.dy = this.defaultDy;
            movingLeft = true;
            movingDown = true;
            moveSpeed = 5;
            stdMoveSpeed = moveSpeed;
            slowDownActive = false;
            maxSpeed = 2;
        }

        public Chicken(double x, double y)
            : base(x, y)
        {

            this.Height = 50;
            this.Width = 50;
            this.defaultDx = -5;
            this.defaultDy = 3;
            this.dx = this.defaultDx;
            this.dy = this.defaultDy;
            movingLeft = false;
            movingDown = true;
            moveSpeed = 5;
            stdMoveSpeed = moveSpeed;
            slowDownActive = false;
            maxSpeed = 5;
        }

        public Chicken(double x, double y, double dx, double dy)
            : base(x, y)
        {

            this.Height = 50;
            this.Width = 50;
            this.defaultDx = dx;
            this.defaultDy = dy;
            this.dx = this.defaultDx;
            this.dy = this.defaultDy;
            movingLeft = false;
            movingDown = true;
            moveSpeed = 5;
            stdMoveSpeed = moveSpeed;
            maxSpeed = 8;
            slowDownActive = false;
        }
        #endregion

        public void update()
        {
            if (isAlive)
            {
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

            if (movingUp)
            {

                dy -= moveSpeed * deltaTime;

                if (dy < -moveSpeed)
                {
                    dy = -moveSpeed;
                }
            }
            else if (movingDown)
            {

                dy += moveSpeed * deltaTime;

                if (dy > moveSpeed)
                {
                    dy = moveSpeed;
                }
            }
        }

        public void moveRandomlyDynamic()
        {
            if (dx > 0)
            {
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

            if (dy > 0)
            {
                if (y + (dy * deltaTime) + height > screen_height)
                {
                    dy = 0;
                    movingUp = true;
                    movingDown = false;
                }
                else
                {
                    y += dy * deltaTime;
                }
            }

            if (dy < 0)
            {
                if (y + (dy * deltaTime) < 0)
                {
                    dy = 0;
                    movingUp = false;
                    movingDown = true;
                }
                else
                {
                    y += dy * deltaTime;
                }
            }
        }
        #endregion

        #region Slow Motion
        public void slowDown()
        {
            if (this.slowDownActive == false && this.speedUpActive == false)
            {
                this.slowDownActive = true;
            }
        }

        private void activateSlowDown()
        {
            if (moveSpeed > stdMoveSpeed / 8)
            {
                moveSpeed -= stdMoveSpeed / 50;
            }
            else
            {
                freezeAcceleration();
            }
        }

        private void freezeAcceleration()
        {
            slowDownTime -= (deltaTime * 50);
            if (slowDownTime <= 0)
            {
                slowDownTime = 1500; //Freeze for approx 1500 ms
                speedUpActive = true;
                slowDownActive = false;
            }
        }

        private void activateSpeedUp()
        {
            if (moveSpeed < stdMoveSpeed)
            {
                moveSpeed += stdMoveSpeed / 50;
            }
            else
            {
                moveSpeed = stdMoveSpeed;
                speedUpActive = false;
            }
        }
        #endregion

        #region Hit Check
        public Boolean isHit(double x, double y)
        {
            if ((x >= X && x < (Width + X)) && (y > Y && y < (Height + Y)))
            {
                isAlive = false;
                return true;
            }
            return false;
        }
        #endregion
    }
}
