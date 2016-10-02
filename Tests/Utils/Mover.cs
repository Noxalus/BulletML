using BulletML;
using Microsoft.Xna.Framework;

namespace Tests.Utils
{
    public class Mover : Bullet
    {
        public Vector2 Position;

        public override float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public override float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public bool Used { get; set; }

        public Mover(IBulletManager bulletManager) : base(bulletManager)
        {
        }

        public void Init()
        {
            Used = true;
        }
    }
}