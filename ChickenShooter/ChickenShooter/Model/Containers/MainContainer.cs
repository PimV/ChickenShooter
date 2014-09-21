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
            foreach (EntityContainer container in this.Values)
            {
                if (!container.Contains(e))
                {
                    container.Add(e);
                }
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
