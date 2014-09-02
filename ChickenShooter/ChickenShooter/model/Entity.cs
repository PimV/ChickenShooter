using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.model
{
    public class Entity
    {

        protected int x;
        protected int y;
        protected int dy;
        protected int dx;
        protected int width;
        protected int height;

        protected int screen_width = 500;
        protected int screen_height = 300;

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
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

        public int X
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
        public int Y
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

        public Entity(int x, int y)
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
