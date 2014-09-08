using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.controller.actions
{
    public abstract class ControllerAction
    {

        protected Game game;

        public abstract void execute();




    }


}
