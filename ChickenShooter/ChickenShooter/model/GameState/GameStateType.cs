using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model.GameState
{
    public enum GameStateType
    {
        [GameStateInfoAttribute(typeof(Level1State))]
        level1
    }
}
