using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootingStar.GameObjects
{
    class Catapult : GameObject
    {
        public Catapult(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameObject gameObjects)
        {
            _mousePos = new Vector2(Singleton.Instance.CurrentMouse.X, Singleton.Instance.CurrentMouse.Y);

            _position = new Vector2(Singleton.Instance.GAMESCREEN.X / 2 - Singleton.STARSIZE / 2, 700 - Singleton.STARSIZE);

            Vector2 direction = _mousePos - _position;

            direction.Normalize();

            _rotation = (float)Math.Atan2((double)direction.X, (double)-direction.Y);

            base.Update(gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(Singleton.Instance.GAMESCREEN.X / 2, 740), null, Color.White, _rotation, new Vector2(125, 220), 1f, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }
    }
}
