using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

using ShootingStar.MainMenu.Button;

namespace ShootingStar.MainMenu
{
    public class Tutorial : _MainScreen
    {
        private List<Component> _components;
        private Texture2D _backgroundTexture, // Background
                            _jupiter, _mar, _mercury, _saturn, _uranus, // Star
                            _shootingStar, _firstLine, _secondLine, _thirdLine, _forthLine, _extraLine; // Text
        private SoundEffectInstance _retroClick;
        public Tutorial(Main game, GraphicsDeviceManager graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            /*-------------------------------------TAR WORK SPACE -------------------------------------*/

            // SOUND

            _retroClick = (SoundEffectInstance)Singleton.Instance.instanceRetroClickObj;
            _retroClick.Volume = 0.8F;

            // OBJECT 

            _backgroundTexture = (Texture2D)Singleton.Instance.instanceBackgroundSquare;
            _shootingStar = (Texture2D)Singleton.Instance.instanceShootingStar;

            // STAR

            _jupiter = (Texture2D)Singleton.Instance.instanceJupiter;
            _mar = (Texture2D)Singleton.Instance.instanceMar;
            _mercury = (Texture2D)Singleton.Instance.instanceMercury;
            _saturn = (Texture2D)Singleton.Instance.instanceSaturn;
            _uranus = (Texture2D)Singleton.Instance.instanceUranusBig;


            // TUTORIAL TEXT

            _firstLine = (Texture2D)Singleton.Instance.instanceFirstLine;
            _secondLine = (Texture2D)Singleton.Instance.instanceSecondLine;
            _extraLine = (Texture2D)Singleton.Instance.instanceExtraLine;
            _thirdLine = (Texture2D)Singleton.Instance.instanceThirdLine;
            _forthLine = (Texture2D)Singleton.Instance.instanceFourthLine;

            //FONT

            var _buttonFont = (SpriteFont)Singleton.Instance.instanceSpriteFont;

            //BUTTON

            var _returnToMenuButtonTexture = (Texture2D)Singleton.Instance.instanceReturnButtonTexture;

            /*-------------------------------------TAR WORK SPACE -------------------------------------*/
            
            var _returnToMenuButton = new _Button(_returnToMenuButtonTexture, _buttonFont)
            {
                Position = new Vector2(320, 697)
            };

            _returnToMenuButton.Click += returnToMenu_Click;

            _components = new List<Component>() {
                _returnToMenuButton
            };
        }
        private void returnToMenu_Click(object sender, EventArgs e)
        {
            // Load into the MainMenu screen
            _retroClick.Play();
            Singleton.Instance.CurrentGameState = Singleton.GameState.MainMenu;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Background

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            // Shooting Star

            spriteBatch.Draw(_shootingStar, new Vector2(176, 80), Color.White);

            // First Line Text

            spriteBatch.Draw(_firstLine, new Vector2(30, 242), Color.White);

            // Second Line Text

            spriteBatch.Draw(_secondLine, new Vector2(122, 313), Color.White);

            // Extra Line Text

            spriteBatch.Draw(_extraLine, new Vector2(62, 384), Color.White);

            // Third Line Text

            spriteBatch.Draw(_thirdLine, new Vector2(219, 483), Color.White);

            // Forth Line Text

            spriteBatch.Draw(_forthLine, new Vector2(275, 533), Color.White);

            // Big Jupiter

            spriteBatch.Draw(_jupiter, new Vector2(0, 562), Color.White);

            // Big Uranus

            spriteBatch.Draw(_uranus, new Vector2(546, 570), Color.White);

            // Mar

            spriteBatch.Draw(_mar, new Vector2(64, 96), Color.White);

            // Saturn

            spriteBatch.Draw(_saturn, new Vector2(658, 88), Color.White);

            // Mercury

            spriteBatch.Draw(_mercury, new Vector2(658, 666), Color.White);

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
