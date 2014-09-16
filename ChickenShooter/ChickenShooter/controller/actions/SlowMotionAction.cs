using ChickenShooter.Model;

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
            foreach (Entity e in game.VisibleEntities)
            {
                e.slowDown();
            }
        }
    }
}
