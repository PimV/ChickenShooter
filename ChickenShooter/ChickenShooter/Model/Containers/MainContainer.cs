using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model.Containers
{
    public class MainContainer : Dictionary<String, EntityContainer>
    {
        public void addEntity(Entity e)
        {
            if (e.IsMovable)
            {
                if (this["movable"].Contains(e))
                {
                    this["movable"].Add(e);
                }
                //Add to movable container
            }

            if (e.IsShootable)
            {
                if (this["shootable"].Contains(e))
                {
                    this["shootable"].Add(e);
                }
                //Add to shootable container
            }

            if (e.IsVisible)
            {
                if (this["visible"].Contains(e))
                {
                    this["visible"].Add(e);
                }
                //Add to visible container
            }
        }

        public void removeEntity(Entity e)
        {
            foreach (EntityContainer container in this.Values)
            {
                if (container.Contains(e))
                {
                    container.Remove(e);
                }
            }
        }

    }
}
