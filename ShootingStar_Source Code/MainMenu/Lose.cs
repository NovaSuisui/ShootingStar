using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

using ShootingStar.MainMenu.Button;

namespace ShootingStar.MainMenu
{
    public class Lose : _MainScreen
    {
        private List<Component> _components;
        private Texture2D _backgroundTexture, // Background
                          _solarGroupMedium, _kaboom, _lightsaber;
        private SoundEffectInstance _retroClick;
        public Lose(Main game, GraphicsDeviceManager graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            /*-------------------------------------TAR WORK SPACE -------------------------------------*/

            // SOUND

            _retroClick = (SoundEffectInstance)Singleton.Instance.instanceRetroClickObj;
            _retroClick.Volume = 0.8F;

            //OBJECT

            _backgroundTexture = (Texture2D)Singleton.Instance.instanceBackgroundSquare;
            _solarGroupMedium = (Texture2D)Singleton.Instance.instanceSolorGroupMedium;

            _kaboom = (Texture2D)Singleton.Instance.instanceKaboom;
            _lightsaber = (Texture2D)Singleton.Instance.instanceRedLightSaber;

            //FONT

            var _buttonFont = (SpriteFont)Singleton.Instance.instanceSpriteFont;

            //BUTTON

            var _returnToMenuButtonTexture = (Texture2D)Singleton.Instance.instanceReturnButtonTexture;

            /*-------------------------------------TAR WORK SPACE -------------------------------------*/

            var _returnToMenuButton = new _Button(_returnToMenuButtonTexture, _buttonFont)
            {
                Position = new Vector2(605, 645)
            };

            _returnToMenuButton.Click += returnToMenu_Click;

            _components = new List<Component>() {
                _returnToMenuButton
            };

        }

        private void returnToMenu_Click(object sender, EventArgs e)
        {
            // Load to MainMenu Screen
            _retroClick.Play();
            Singleton.Instance.CurrentGameState = Singleton.GameState.ToMainMenu;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Background

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            // Mascot Right

            spriteBatch.Draw(_solarGroupMedium, new Vector2(322, 78), Color.White);

            // KABOOM !

            spriteBatch.Draw(_kaboom, new Vector2(360, 451), Color.White);

            // KABOOM !

            spriteBatch.Draw(_lightsaber, new Vector2(-284, -223), Color.White);

            // Button

            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }
        public override void PostUpdate(GameTime gameTime)
        {
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

    }
}
