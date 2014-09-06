using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.controller.actions
{
    public class ControllerAction
    {
        protected String actionName;
        public String ActionName { get { return actionName; } set { actionName = value; } }

        protected double x;
        protected double y;
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }

    }


}
