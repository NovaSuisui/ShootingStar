using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingStar.GameObjects
{
    class Star : GameObject
    {
        private int colID, rowID, starID;
        private int _speed = 10;
        private Vector2 _movement;

        public enum StarState
        {
            CreateStar,
            InAir,
            InCatapult,
            InReserve,
            Shooted,
            Collided,
        }
        StarState _state;

        public Star(Texture2D texture, string setState, int starID) : base(texture)
        {
            this.starID = starID;
            switch (setState)
            {
                case "InAir":
                    _state = StarState.CreateStar;
                    break;
                case "InCatapult":
                    _state = StarState.InCatapult;
                    break;
                case "InReserve":
                    _state = StarState.InReserve;
                    break;
                case "Shooted":
                    _state = StarState.Shooted;
                    break;
                case "Collided":
                    _state = StarState.Collided;
                    break;
            }
        }

        public override void Update(List<GameObject> gameObjects)
        {
            switch (_state)
            {
                case StarState.CreateStar:
                    colID = (int)(_position.X / Singleton.STARSIZE);
                    rowID = (int)(_position.Y / Singleton.STARSIZE);
                    Singleton.Instance.STARMAP[colID, rowID] = starID;
                    _state = StarState.InAir;
                    break;
                case StarState.InAir:
                    //star in state
                    if (Singleton.Instance.STARMAP[colID, rowID] != starID)
                    {
                        _isActive = false;
                        Singleton.Instance.STARMAP[colID, rowID] = 0;
                    }
                    break;
                case StarState.InCatapult:
                    //main star waithing shoot
                    if (Singleton.Instance.CurrentMouse.Y < 600)
                    {
                        _mousePos = new Vector2(Singleton.Instance.CurrentMouse.X, Singleton.Instance.CurrentMouse.Y);

                        Vector2 Position = new Vector2(Singleton.Instance.GAMESCREEN.X / 2 - Singleton.STARSIZE / 2, 700 - Singleton.STARSIZE);

                        if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                        {
                            Singleton.Instance.VFX.Play();
                            _state = StarState.Shooted;
                            _movement = new Vector2(
                                (float)(1 * _speed * Math.Cos((float)Math.Atan2((double)(_mousePos.Y - _position.Y), (double)(_mousePos.X - _position.X)))),
                                (float)(1 * _speed * Math.Sin((float)Math.Atan2((double)(_mousePos.Y - _position.Y), (double)(_mousePos.X - _position.X))))
                                );
                            Singleton.Instance.isShooting = true;
                        }
                    }
                    if(Singleton.Instance.CurrentMouse.RightButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.RightButton == ButtonState.Released)
                    {
                        _state = StarState.InReserve;
                        _name = "InReserve";
                        _position.X = Singleton._INRESERVEPOS_X;
                        _position.Y = Singleton._INRESERVEPOS_Y;
                    }
                    break;
                case StarState.InReserve:
                    //reserve star for switch
                    if (Singleton.Instance.CurrentMouse.RightButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.RightButton == ButtonState.Released &&
                        !Singleton.Instance.isShooting)
                    {
                        _state = StarState.InCatapult;
                        _name = "InCatapult";
                        _position.X = Singleton._INCATAPULTPOS_X;
                        _position.Y = Singleton._INCATAPULTPOS_Y;
                    }
                    else if (Singleton.Instance.newStar)
                    {
                        Singleton.Instance.isShooting = false;
                        _state = StarState.InCatapult;
                        _name = "InCatapult";
                        _position.X = Singleton._INCATAPULTPOS_X;
                        _position.Y = Singleton._INCATAPULTPOS_Y;
                        Singleton.Instance.starBuffer = starID;
                    }
                    break;
                case StarState.Shooted:
                    //calculation x,y to a,b

                    _position.X += _movement.X;
                    _position.Y += _movement.Y;
                    //add smooter

                    if(_position.X > Singleton.Instance.GAMESCREEN.X - Singleton.STARSIZE - 5 ||
                        _position.X < 5)
                    {
                        _movement.X *= -1;
                    }

                    if(isCollition(gameObjects))
                    {
                        _state = StarState.Collided;
                    }

                    if (_position.Y < -90)
                    {
                        _state = StarState.InAir;
                        _name = "InNull";
                        _isActive = false;
                        Singleton.Instance.newStar = true;
                    }
                    break;
                case StarState.Collided:
                    //check for matching color

                    colID = (int)(_position.X / Singleton.STARSIZE);
                    rowID = (int)(_position.Y / Singleton.STARSIZE);

                    if(rowID < 10) //TO DO : fix some bug
                    {
                        if(colID < 8 && colID >= 0 && rowID >= 0)
                        {
                            Singleton.Instance.STARMAP[colID, rowID] = starID;
                            Singleton.Instance.newStar = true;
                            _state = StarState.InAir;
                            _name = "InAir";
                            count = 0;
                            isStarMatch(rowID, colID);

                            resetStarMatch();
                        }
                    }
                    else
                    {
                        Singleton.Instance.CurrentGameState = Singleton.GameState.GameLose;
                    }

                    break;
            }
            base.Update(gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, _rectangle, Color.White);
            base.Draw(spriteBatch);
        }

        public bool isCollition(List<GameObject> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if(obj._name.Equals("InAir") &&
                    /*Math.Abs(obj._position.X - _position.X) < (Singleton.Instance.STARSIZE + _speed) / 2 &&
                    Math.Abs(obj._position.Y - _position.Y) < Singleton.Instance.STARSIZE + _speed*/
                    obj._rectangle.Intersects(_rectangle)
                    )
                {

                    if( _position.X + Singleton.STARSIZE / 2 > obj._position.X + Singleton.STARSIZE / 2)
                    {
                        _position.X = obj._position.X + Singleton.STARSIZE / 2;
                    }
                    else
                    {
                        _position.X = obj._position.X - Singleton.STARSIZE / 2;
                    }

                    if(_position.Y + Singleton.STARSIZE / 2 > obj._position.Y + Singleton.STARSIZE / 2)
                    {
                        _position.Y = obj._position.Y + Singleton.STARSIZE;
                    }
                    else
                    {
                        _position.Y = obj._position.Y - Singleton.STARSIZE;
                    }

                    return true;
                }
            }
            return false;
        }

        private int count;
        public void isStarMatch(int row, int col)
        {
            Singleton.Instance.STARMAP[col, row] = -10;
            count++;
            int sign = 0;

            switch (Singleton.Instance.CurrentSign)
            {
                case Singleton.StarSign.Normal:
                    sign = 0;
                    break;
                case Singleton.StarSign.Invert:
                    sign = 1;
                    break;
            }
            if( (row + sign) % 2 == 0)
            {
                //even
                //top left
                // row - 1 col - 1
                if (row - 1 >= 0 && col - 1 >= 0 && Singleton.Instance.STARMAP[col - 1, row - 1] == starID)
                {
                    isStarMatch(row - 1, col - 1);
                }
                //top right
                // row - 1 col
                if (row - 1 >= 0 && col < 7 && Singleton.Instance.STARMAP[col, row - 1] == starID)
                {
                    isStarMatch(row - 1, col);
                }
                //left
                // row col - 1
                if (col - 1 >= 0 && Singleton.Instance.STARMAP[col - 1, row] == starID)
                {
                    isStarMatch(row, col - 1);
                }
                //right
                //row col + 1
                if (col + 1 < 8 && Singleton.Instance.STARMAP[col + 1, row] == starID)
                {
                    isStarMatch(row, col + 1);
                }
                //down left
                // row + 1 col - 1
                if (row + 1 < 10 && col - 1 >= 0 && Singleton.Instance.STARMAP[col - 1, row + 1] == starID)
                {
                    isStarMatch(row + 1, col - 1);
                }
                //down right
                //row + 1 col
                if (row + 1 < 10 && col < 7 && Singleton.Instance.STARMAP[col, row + 1] == starID)
                {
                    isStarMatch(row + 1, col);
                }
            }
            else
            {
                //odd
                //top left
                // row - 1 col
                if (row - 1 >= 0 && Singleton.Instance.STARMAP[col, row - 1] == starID)
                {
                    isStarMatch(row - 1, col);
                }
                //top right
                // row - 1 col + 1
                if (row - 1 >= 0 && col + 1 < 8 && Singleton.Instance.STARMAP[col + 1, row - 1] == starID)
                {
                    isStarMatch(row - 1, col + 1);
                }
                //left
                // row col - 1
                if (col - 1 >= 0 && Singleton.Instance.STARMAP[col - 1, row] == starID)
                {
                    isStarMatch(row, col - 1);
                }
                //right
                //row col + 1
                if (col + 1 < 7 && Singleton.Instance.STARMAP[col + 1, row] == starID)
                {
                    isStarMatch(row, col + 1);
                }
                //down left
                // row + 1 col
                if (row + 1 < 10 && Singleton.Instance.STARMAP[col, row + 1] == starID)
                {
                    isStarMatch(row + 1, col);
                }
                //down right
                //row + 1 col + 1
                if (row + 1 < 10 && col + 1 < 8 && Singleton.Instance.STARMAP[col + 1, row + 1] == starID)
                {
                    isStarMatch(row + 1, col + 1);
                }
            }
        }

        private void resetStarMatch()
        {
            int sign = 0;

            switch (Singleton.Instance.CurrentSign)
            {
                case Singleton.StarSign.Normal:
                    sign = 0;
                    break;
                case Singleton.StarSign.Invert:
                    sign = 1;
                    break;
            }

            if (count < 3)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 8 - ( (i + sign) % 2); j++)
                    {
                        if (Singleton.Instance.STARMAP[j, i] == -10)
                            Singleton.Instance.STARMAP[j, i] = starID;
                    }
                }
                Singleton.Instance.VFX_popInstance.Play();
            }
            else
            {
                Singleton.Instance.VFX_EndOfTheWorldInstance.Play();
                Singleton.Instance.gameScore += 100 * count;
            }

        }


    }
}
