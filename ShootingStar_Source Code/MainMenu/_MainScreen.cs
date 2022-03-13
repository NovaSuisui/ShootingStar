using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShootingStar.MainMenu
{
    public abstract class _MainScreen
    {
        protected ContentManager _content;

        protected GraphicsDeviceManager _graphicsDevice;

        protected Main _game;

        public abstract void PostUpdate(GameTime gameTime);
        public _MainScreen(Main game, GraphicsDeviceManager graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;

        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}