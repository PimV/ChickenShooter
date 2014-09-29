using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenShooter.Helper;

namespace ChickenShooter.Model.GameState
{
    public class GameStateFactory
    {
        public static GameState createGameState(GameStateType gameState, GameStateManager gsm)
        {
            var gameStateAttribute = gameState.GetAttribute<GameStateInfoAttribute>();
            if (gameStateAttribute == null)
            {
                return null;
            }
            var type = gameStateAttribute.Type;
            GameState result = Activator.CreateInstance(type, gsm) as GameState;
            return result;
        }

    }
}
