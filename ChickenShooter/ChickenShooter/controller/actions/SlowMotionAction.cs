using ChickenShooter.Model;
using ChickenShooter.Model.Entities;

namespace ChickenShooter.controller.actions
{
    class SlowMotionAction : ControllerAction
    {
        public SlowMotionAction(Game game)
        {
            this.game = game;
        }

        public override void execute()
        {
            game.Player.SoundLocation = "Sounds\\Flame woosh.wav";
            game.Player.Play();
            foreach (Entity e in game.MainContainer[Behaviour.Visible])
            {
                e.slowDown();
            }
        }
    }
}
