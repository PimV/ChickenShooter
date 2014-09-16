using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model
{
    public class EntityFactory
    {

        public static Entity createEntity(EntityTypes type)
        {
            switch (type)
            {
                case EntityTypes.Chicken:
                    return new Chicken();
                case EntityTypes.Duck:
                    //return new Duck();
                    return null;
                case EntityTypes.Goose:
                    return null;
                //return new Goose();
                case EntityTypes.Balloon:
                    //return new Balloon();
                    return null;
            }


            return null;
        }

    }
}
