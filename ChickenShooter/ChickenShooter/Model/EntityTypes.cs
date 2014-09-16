
namespace ChickenShooter.Model
{
    public enum EntityTypes
    {
        [EntityInfoAttribute(typeof(Chicken))]
        Chicken,
        [EntityInfoAttribute(typeof(Balloon))]
        Balloon,
        [EntityInfoAttribute(typeof(Bullet))]
        Bullet,
    }
}
