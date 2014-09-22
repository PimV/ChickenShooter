using ChickenShooter.Helper;
using System;
namespace ChickenShooter.Model.Entities
{
    public class EntityFactory
    {

        public static Entity createEntity(EntityTypes entity)
        {
            var entityAttribute = entity.GetAttribute<EntityInfoAttribute>();
            if (entityAttribute == null)
            {
                return null;
            }
            var type = entityAttribute.Type;
            Entity result = Activator.CreateInstance(type) as Entity;
            return result;
        }

        public static Bullet createBullet(double x, double y)
        {
            var entityAttribute = EntityTypes.Bullet.GetAttribute<EntityInfoAttribute>();
            if (entityAttribute == null)
            {
                return null;
            }
            var type = entityAttribute.Type;
            Bullet result = Activator.CreateInstance(type, x, y) as Bullet;
            return result;
        }

    }
}
