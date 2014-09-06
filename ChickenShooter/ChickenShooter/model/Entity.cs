using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.model
{
    public class Entity
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

        public struct State
        {
            public float x;
            public float v;
        };

        public struct Derivative
        {
            public float dx;
            public float dv;
        };

        protected float t = 0.0f;
        protected float dt = 0.1f;

        protected float accumulator = 0.0f;

        protected State current;
        protected State previous;

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

        public Derivative evaluate(State initial, float t, float dt, Derivative d)
        {
            State state;
            state.x = initial.x + d.dx * dt;
            state.v = initial.v + d.dv * dt;

            Derivative output;
            output.dx = state.v;
            output.dv = acceleration(state, t + dt);
            return output;
        }

        public float acceleration(State state, float t)
        {
            float k = 10;
            float b = 1;
            return -k * state.x - b * state.v;
        }

        public void integrate(State state, float t, float dt)
        {
            Derivative a, b, c, d;
            a = evaluate(state, t, 0.0f, new Derivative());
            b = evaluate(state, t, dt * 0.5f, a);
            c = evaluate(state, t, dt * 0.5f, b);
            d = evaluate(state, t, dt, c);

            float dxdt = 1.0f / 6.0f * (a.dx + 2.0f * (b.dx + c.dx) + d.dx);
            float dvdt = 1.0f / 6.0f * (a.dv + 2.0f * (b.dv + c.dv) + d.dv);

            state.x = state.x + dxdt * dt;
            state.v = state.v + dvdt * dt;
        }

        public State interpolate(State previous, State current, float alpha)
        {
            State state;
            state.x = current.x * alpha + previous.x * (1 - alpha);
            //Console.WriteLine((current.x*alpha) + "+" + (previous.x * (1-alpha)));
            state.v = current.v * alpha + previous.v * (1 - alpha);
            return state;
        }


    }
}
