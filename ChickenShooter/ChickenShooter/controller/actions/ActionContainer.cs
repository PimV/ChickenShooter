using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.controller.actions
{
    public class ActionContainer : System.Collections.Concurrent.ConcurrentQueue<ControllerAction>
    {
        public ActionContainer()
        {
        }

    }
}
