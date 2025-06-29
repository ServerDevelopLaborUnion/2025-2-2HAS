namespace Server.Objects
{
    internal interface IHittable
    {
        int Health { get; set; }
        bool IsDead { get; }
        void Revive();
        void Hit();
    }
}
