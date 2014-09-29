using ChickenShooter.controller.actions;
using ChickenShooter.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model.GameState
{
    public class GameStateManager
    {
        public GameState CurrentState { get; set; }
        public GameCanvas canvas { get; set; }
        public MainWindow GameWindow { get; set; }


        public GameStateManager()
        {
            this.GameWindow = new MainWindow();
            canvas = new GameCanvas(this, GameWindow);
            GameWindow.Show();
            Player = new SoundPlayer();

            CurrentState = GameStateFactory.createGameState(GameStateType.level1, this);
        }

        public void update(double dt)
        {
            CurrentState.update(dt);
        }

        public void handleInput()
        {
            CurrentState.handleInput();
        }

        public void addAction(ControllerAction ca)
        {
            this.CurrentState.ac.Enqueue(ca);
        }

        public void renderGame()
        {
            CurrentState.draw();
        }

        public SoundPlayer Player { get; set; }
    }
}
