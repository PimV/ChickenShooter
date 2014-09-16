using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model
{
    public abstract class Entity
    {

        //Position and Vector
        protected double x;
        protected double y;
        protected double dy;
        protected double dx;
        //Dimensions
        protected int width;
        protected int height;
        //Movement
        protected Boolean movingLeft;
        protected Boolean movingRight;
        protected Boolean movingUp;
        protected Boolean movingDown;
        //Movement Attributes
        protected double moveSpeed;
        protected double maxSpeed;
        protected double minSpeed;
        protected double stopSpeed;

        protected Boolean slowDownActive;
        protected Boolean speedUpActive;
        protected double slowDownTime = 1500; //Hang for 1500ms in slowmow

        protected double stdMoveSpeed;



        protected double deltaTime;

        protected int screen_width = 500;
        protected int screen_height = 300;

        public double DeltaTime { get { return deltaTime; } set { deltaTime = value; } }

        public double Dx
        {
            get { return dx; }
            set { dx = value; }
        }
        public double Dy
        {
            get { return dy; }
            set { dy = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public Entity(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Entity()
        {
            this.X = 0;
            this.Y = 0;
        }

        public abstract void update(double dt);

        protected void determineDirection()
        {
            if (dx > 0)
            {
                this.movingRight = true;
                this.movingLeft = false;
            }
            else if (dx < 0)
            {
                this.movingRight = false;
                this.movingLeft = true;
            }

            if (dy > 0)
            {
                this.movingDown = true;
                this.movingUp = false;
            }
            else if (dy < 0)
            {
                this.movingDown = false;
                this.movingUp = true;
            }

        }

        #region Slow Motion
        public void slowDown()
        {
            if (this.slowDownActive == false && this.speedUpActive == false)
            {
                this.slowDownActive = true;
            }
        }

        protected void activateSlowDown()
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

        protected void freezeAcceleration()
        {
            slowDownTime -= (deltaTime * 50);
            if (slowDownTime <= 0)
            {
                slowDownTime = 1500; //Freeze for approx 1500 ms
                speedUpActive = true;
                slowDownActive = false;
            }
        }

        protected void activateSpeedUp()
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
    }
}
