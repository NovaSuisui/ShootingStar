using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingStar.GameScore
{
    class _GameScore
    {
        Color colorL, colorR;

        long CurrentTime, Time;
        double _timeLimit;

        public _GameScore()
        {
            _timeLimit = (Singleton.Instance.gameLevel * 600000000 * 1.5);
        }

        public void Update(_GameScore gameScore, GameTime gameTime)
        {
            Time += gameTime.ElapsedGameTime.Ticks;
            CurrentTime = (long)_timeLimit - Time;

            if (CurrentTime < 0)
            {
                Singleton.Instance.CurrentGameState = Singleton.GameState.GameLose;
            }

            if(Singleton.Instance.CurrentMouse.X >= 498 &&
                Singleton.Instance.CurrentMouse.X <= 582 &&
                Singleton.Instance.CurrentMouse.Y >= 745 &&
                Singleton.Instance.CurrentMouse.Y <= 795)
            {
                colorL = Color.Gray;
                if(Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                    Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                {
                    Singleton.Instance.CurrentGameState = Singleton.GameState.GameReset;
                }
            }
            else
            {
                colorL = Color.White;
            }
            if (Singleton.Instance.CurrentMouse.X >= 704 &&
                Singleton.Instance.CurrentMouse.X <= 788 &&
                Singleton.Instance.CurrentMouse.Y >= 745 &&
                Singleton.Instance.CurrentMouse.Y <= 795)
            {
                colorR = Color.Gray;
                if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                    Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                {
                    Singleton.Instance.CurrentGameState = Singleton.GameState.ToMainMenu;
                }
            }
            else
            {
                colorR = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Singleton.Instance._gamefont, " Score : " + Singleton.Instance.gameScore, new Vector2(545, 250), Color.White);
            spriteBatch.DrawString(Singleton.Instance._gamefont, "Time : " + String.Format("{0}:{1:00}", CurrentTime / 600000000, (CurrentTime / 10000000) % 60), new Vector2(570, 350), Color.White);
            spriteBatch.DrawString(Singleton.Instance._gamefont, "Reset", new Vector2(498, 745), colorL);
            spriteBatch.DrawString(Singleton.Instance._gamefont, "Menu", new Vector2(704, 745), colorR);
        }
    }
}
