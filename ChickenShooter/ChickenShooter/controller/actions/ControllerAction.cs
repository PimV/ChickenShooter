using ChickenShooter.Model;
using ChickenShooter.Model.GameState;

namespace ChickenShooter.controller.actions
{
    public abstract class ControllerAction
    {

        protected Game game;

        protected GameStateManager GSM { get; set; }

        public abstract void execute();




    }


}
