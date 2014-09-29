using ChickenShooter.controller.actions;
using ChickenShooter.Model.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model.GameState
{
    public interface GameState
    {
        ActionContainer ac { get; set; }
        MainContainer MainContainer { get; set; }

        void update(double dt);
        void handleInput();
        void draw();
        void init();

    }
}
