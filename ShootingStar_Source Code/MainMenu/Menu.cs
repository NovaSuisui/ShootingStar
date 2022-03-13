using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

using ShootingStar.MainMenu.Button;

namespace ShootingStar.MainMenu
{
    public class Menu : _MainScreen
    {
        private List<Component> _components;
        private Texture2D _backgroundTexture, _solarGroup, _shootingStar;

        private SoundEffectInstance _retroClick;
        public Menu(Main game, GraphicsDeviceManager graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            /*-------------------------------------TAR WORK SPACE -------------------------------------*/

            _retroClick = (SoundEffectInstance)Singleton.Instance.instanceRetroClickObj;
            _retroClick.Volume = 0.8F;

            _backgroundTexture = (Texture2D)Singleton.Instance.instanceBackgroundSquare;
            _solarGroup = (Texture2D)Singleton.Instance.instanceSolorGroupBig;
            _shootingStar = (Texture2D)Singleton.Instance.instanceShootingStar;

            var _buttonFont = (SpriteFont)Singleton.Instance.instanceSpriteFont;
            var _playGameButtonTexture = (Texture2D)Singleton.Instance.instancePlayGameButtonTexture;
            var _tutorialGameButtonTexture = (Texture2D)Singleton.Instance.instanceTutorialButtonTexture;
            var _exitGameButtonTexture = (Texture2D)Singleton.Instance.instanceExitGameButtonTexture;

            /*-------------------------------------TAR WORK SPACE -------------------------------------*/

            var _playGameButton = new _Button(_playGameButtonTexture, _buttonFont)
            {
                Position = new Vector2(338, 591)
            };

            _playGameButton.Click += playGameButton_Click;

            var _tutorialGameButton = new _Button(_tutorialGameButtonTexture, _buttonFont)
            {
                Position = new Vector2(307, 641)
            };

            _tutorialGameButton.Click += tutorialGameButton_Click;


            var _exitGameButton = new _Button(_exitGameButtonTexture, _buttonFont)
            {
                Position = new Vector2(348, 697)
            };

            _exitGameButton.Click += exitGameButton_Click;

            _components = new List<Component>() {
                _playGameButton,
                _tutorialGameButton,
                _exitGameButton
            };
        }
        private void playGameButton_Click(object sender, EventArgs e)
        {
            // Load into the game screen
            _retroClick.Play();
            Singleton.Instance.CurrentGameState = Singleton.GameState.GameStart;
        }

        private void tutorialGameButton_Click(object sender, EventArgs e)
        {
            // Load into tutorial screen
            _retroClick.Play();
            Singleton.Instance.CurrentGameState = Singleton.GameState.Tutorial;
        }

        private void exitGameButton_Click(object sender, EventArgs e)
        {
            // Test Lose and Win Scene
            _retroClick.Play();
            _game.Exit();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Background

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);


            // Mascot

            spriteBatch.Draw(_solarGroup, new Vector2(90, 51), Color.White);

            // Shooting Star

            spriteBatch.Draw(_shootingStar, new Vector2(176, 510), Color.White);

            // Button

            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }
        public override void PostUpdate(GameTime gameTime)
        {
            //Remove Sprites if they're not needed
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
