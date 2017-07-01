using BulletML;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Visualizer_Core
{
    public class Mover : Bullet
    {
        public Texture2D Texture;
        public Vector2 Position;
        private short _currentSpriteIndex;

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
            _currentSpriteIndex = SpriteIndex;
            Texture = ((MoverManager)BulletManager).BulletTextures[SpriteIndex];
        }

        public void Init()
        {
            Used = true;
        }

        public override void Update()
        {
            base.Update();

            // SpriteIndex changed? => we need to update the bullet texture
            if (_currentSpriteIndex != SpriteIndex)
            {
                _currentSpriteIndex = SpriteIndex;
                var moverManager = (MoverManager)BulletManager;
                if (SpriteIndex < moverManager.BulletTextures.Count)
                    Texture = moverManager.BulletTextures[SpriteIndex];
            }

            if (X < -Texture.Width / 2f || X > Config.GameAeraSize.X + (Texture.Width / 2f) ||
                Y < -Texture.Height / 2f || Y > Config.GameAeraSize.Y + (Texture.Height / 2f))
            {
                Used = false;
            }
        }
    }
}