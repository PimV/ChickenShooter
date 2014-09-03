using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.model
{
    public class Entity
    {

        protected double x;
        protected double y;
        protected double dy;
        protected double dx;
        protected int width;
        protected int height;

        protected int screen_width = 500;
        protected int screen_height = 300;

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
    }
}
