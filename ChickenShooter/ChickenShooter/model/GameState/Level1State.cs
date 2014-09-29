using ChickenShooter.controller.actions;
using ChickenShooter.Model.Containers;
using ChickenShooter.Model.Entities;
using ChickenShooter.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model.GameState
{
    public class Level1State : GameState
    {
        public ActionContainer ac { get; set; }
        public MainContainer MainContainer { get; set; }
        public GameStateManager GSM { get; set; }

        public Level1State(GameStateManager gsm)
        {
            this.GSM = gsm;
            init();
        }

        public void init()
        {
            ac = new ActionContainer();

            //Create Entities
            Entity c1 = EntityFactory.createEntity(EntityType.Chicken);
            Entity b1 = EntityFactory.createEntity(EntityType.Balloon);

            //Create Entity Containers         
            MainContainer = new MainContainer();
            MainContainer.addEntity(c1);
            MainContainer.addEntity(b1);

            bullets = new List<Bullet>();

            //Game Status
            statusTracker = new StatTracker();
            //statusTracker.MAX_SCORE = chickens.Count;
            statusTracker.MAX_SCORE = MainContainer[Behaviour.Shootable].OfType<Chicken>().ToList().Count;
        }

        public void update(double dt)
        {
            MainContainer.update(dt);
        }

        public void draw()
        {
            this.GSM.canvas.renderEntities(MainContainer[Behaviour.Visible]);
        }



        public List<Bullet> bullets { get; set; }

        public StatTracker statusTracker { get; set; }




        public void handleInput()
        {
            ControllerAction action;
            lock (ac)
            {
                while (ac.Count > 0)
                {
                    ac.TryDequeue(out action);
                    if (action == null)
                    {
                        continue;
                    }
                    action.execute();
                }
            }
        }
    }
}
