
namespace ChickenShooter.Model.Entities
{
    public enum EntityType
    {
        [EntityInfoAttribute(typeof(Chicken))]
        Chicken,
        [EntityInfoAttribute(typeof(Balloon))]
        Balloon,
        [EntityInfoAttribute(typeof(Bullet))]
        Bullet,
    }
}
