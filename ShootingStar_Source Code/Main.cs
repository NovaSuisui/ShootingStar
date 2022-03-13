using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

using ShootingStar.GameObjects;
using ShootingStar.MainMenu;
using ShootingStar.GameScore;

namespace ShootingStar
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager _content;

        private SoundEffect VFX;

        private List<_MainScreen> mainMenu;

        List<GameObject> _gameObject;
        GameObject _gameCatapult;

        Texture2D[] _textureStar;
        Texture2D _textureCatapult, _gameBackground, _gameBackgroundScore, _nextLevel;


        int _starSize;
        int _randomStarID;
        Random random;

        MouseState _mouseState;

        SpriteFont f;
        //----------------------KUE----------------------//
        _GameScore GGZ;
        private SoundEffect BGM;
        private SoundEffect VFX_pop;
        private SoundEffect VFX_EndOfTheWorld;
        private SoundEffectInstance VFX_EndOfTheWorldInstance;
        private SoundEffectInstance VFX_popInstance;
        private SoundEffectInstance BGMinstance;
        Rectangle rect;
        //----------------------KUE----------------------//

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = (int)Singleton.ALLSCREENWIDTH;
            _graphics.PreferredBackBufferHeight = (int)Singleton.ALLSCREENHIGHT;

            _graphics.ApplyChanges();

            mainMenu = new List<_MainScreen>();

            rect = new Rectangle((int)Singleton.Instance.GAMESCREEN.X, 0, (int)Singleton.Instance.SCORESCREEN.X, (int)Singleton.ALLSCREENHIGHT);
            _gameObject = new List<GameObject>();

            _textureStar = new Texture2D[9];

            random = new Random();

            _starSize = Singleton.STARSIZE;

            Singleton.Instance.STARMAP = new int[8, 10];
            Singleton.Instance.STARMAP_Sub = new int[8, 10];

            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Singleton.Instance.STARMAP[j, i] = 0;
                    Singleton.Instance.STARMAP_Sub[j, i] = 0;
                }
            }

            Singleton.Instance.CurrentGameState = Singleton.GameState.Start;

            Singleton.Instance.CurrentSign = Singleton.StarSign.Normal;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _textureStar[1] = this.Content.Load<Texture2D>("Star/Earth");
            _textureStar[2] = this.Content.Load<Texture2D>("Star/Mars");
            _textureStar[3] = this.Content.Load<Texture2D>("Star/Jupiter");
            _textureStar[4] = this.Content.Load<Texture2D>("Star/Venus");
            _textureStar[5] = this.Content.Load<Texture2D>("Star/Neptune");
            _textureStar[6] = this.Content.Load<Texture2D>("Star/Uranus");
            _textureStar[7] = this.Content.Load<Texture2D>("Star/Mercury");
            _textureStar[8] = this.Content.Load<Texture2D>("Star/Saturn");

            _gameBackground = this.Content.Load<Texture2D>("Background/GameBackground");

            _textureCatapult = this.Content.Load<Texture2D>("Catapult/SlingShot_before_with_aim");

            /*-------------------------------------TAR WORK SPACE -------------------------------------*/

            //Main Menu

            Singleton.Instance.instanceRetroClickObj = this.Content.Load<SoundEffect>("Audio/retro_arcade_click").CreateInstance();
            Singleton.Instance.instanceBackgroundSquare = this.Content.Load<Texture2D>("Controls/Background_Square");
            Singleton.Instance.instanceSolorGroupBig = this.Content.Load<Texture2D>("Controls/Solar_Group_Big");
            Singleton.Instance.instanceShootingStar = this.Content.Load<Texture2D>("Controls/SHOOTING STAR");
            Singleton.Instance.instanceSpriteFont = this.Content.Load<SpriteFont>("Fonts/Font");
            Singleton.Instance.instancePlayGameButtonTexture = this.Content.Load<Texture2D>("Controls/PLAY");
            Singleton.Instance.instanceTutorialButtonTexture = this.Content.Load<Texture2D>("Controls/TUTORIAL");
            Singleton.Instance.instanceExitGameButtonTexture = this.Content.Load<Texture2D>("Controls/EXIT");

            //Tutorial

            Singleton.Instance.instanceJupiter = this.Content.Load<Texture2D>("Controls/Jupiter_Big");
            Singleton.Instance.instanceMar = this.Content.Load<Texture2D>("Controls/Mars");
            Singleton.Instance.instanceMercury = this.Content.Load<Texture2D>("Controls/Mercury");
            Singleton.Instance.instanceSaturn = this.Content.Load<Texture2D>("Controls/Saturn");
            Singleton.Instance.instanceUranusBig = this.Content.Load<Texture2D>("Controls/Uranus_Big");

            Singleton.Instance.instanceFirstLine = this.Content.Load<Texture2D>("Controls/first line");
            Singleton.Instance.instanceSecondLine = this.Content.Load<Texture2D>("Controls/second line");
            Singleton.Instance.instanceThirdLine = this.Content.Load<Texture2D>("Controls/third line");
            Singleton.Instance.instanceFourthLine = this.Content.Load<Texture2D>("Controls/fourth line");
            Singleton.Instance.instanceExtraLine = this.Content.Load<Texture2D>("Controls/extra line");

            Singleton.Instance.instanceReturnButtonTexture = this.Content.Load<Texture2D>("Controls/RETURN");

            //Win

            Singleton.Instance.instanceSolorGroupMedium = this.Content.Load<Texture2D>("Controls/Solar_Group_Medium");
            Singleton.Instance.instanceKaboom = this.Content.Load<Texture2D>("Controls/kaboom!");
            Singleton.Instance.instanceGreenLightSaber = this.Content.Load<Texture2D>("Controls/greenlightsaber");

            //Lose

            Singleton.Instance.instanceRedLightSaber = this.Content.Load<Texture2D>("Controls/lightsaber");

            //Next Level

            _nextLevel = this.Content.Load<Texture2D>("Controls/Next Level");

            
            /*-------------------------------------TAR WORK SPACE -------------------------------------*/

            //----------------------KUE----------------------//
            VFX = this.Content.Load<SoundEffect>("Audio/sfx_fly");
            VFX_pop = this.Content.Load<SoundEffect>("Audio/pop");
            VFX_EndOfTheWorld = this.Content.Load<SoundEffect>("Audio/BottleBreakEQ");

            VFX_popInstance = VFX_pop.CreateInstance();

            VFX_EndOfTheWorldInstance = VFX_EndOfTheWorld.CreateInstance();
            VFX_EndOfTheWorldInstance.Volume -= 0.4f;



            Singleton.Instance._gamefont = Content.Load<SpriteFont>("Fonts/GameFontA");
            Singleton.Instance.VFX = VFX;
            Singleton.Instance.VFX_popInstance = VFX_popInstance;
            Singleton.Instance.VFX_EndOfTheWorldInstance = VFX_EndOfTheWorldInstance;

            BGM = this.Content.Load<SoundEffect>("Audio/pixie_dust_ft._dianne_zeta");
            BGMinstance = BGM.CreateInstance();
            BGMinstance.Volume -= 0.95f;
            BGMinstance.IsLooped = true;
            BGMinstance.Play();

            _gameBackgroundScore = this.Content.Load<Texture2D>("Background/Background_Score1");
            _gameBackground = this.Content.Load<Texture2D>("Background/GameBackground");

            //----------------------KUE----------------------//

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.Start:
                    mainMenu.Clear();
                    mainMenu.Add(new Menu(this, _graphics, _content)); //0
                    mainMenu.Add(new Tutorial(this, _graphics, _content)); //1
                    mainMenu.Add(new Win(this, _graphics, _content)); //2
                    mainMenu.Add(new Lose(this, _graphics, _content)); //3
                    Singleton.Instance.CurrentGameState = Singleton.GameState.MainMenu;
                    break;
                case Singleton.GameState.MainMenu:
                    mainMenu[0].Update(gameTime);
                    break;
                case Singleton.GameState.Tutorial:
                    mainMenu[1].Update(gameTime);
                    break;
                case Singleton.GameState.GameStart:

                    GGZ = new _GameScore();

                    _gameCatapult = new Catapult(_textureCatapult);

                    StarGenerate(3);

                    int id = random.Next(1, 2 + Singleton.Instance.gameLevel);
                    _gameObject.Add(new Star(_textureStar[id], "InCatapult", id)
                    {
                        _name = "InCatapult",
                        _position = new Vector2(Singleton._INCATAPULTPOS_X, Singleton._INCATAPULTPOS_Y),
                        _viewport = new Rectangle()

                    });

                    //add reserve
                    id = random.Next(1, 2 + Singleton.Instance.gameLevel);
                    _gameObject.Add(new Star(_textureStar[id], "InReserve", id)
                    {
                        _name = "InReserve",
                        _position = new Vector2(Singleton._INRESERVEPOS_X, Singleton._INRESERVEPOS_Y),
                        _viewport = new Rectangle()

                    });

                    Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;
                    break;
                case Singleton.GameState.GamePlaying:

                    _mouseState = Mouse.GetState();
                    Singleton.Instance.CurrentMouse = _mouseState;

                    GGZ.Update(GGZ, gameTime);

                    _gameCatapult.Update(_gameCatapult);

                    foreach(GameObject obj in _gameObject){
                        obj.Update(_gameObject);
                    }

                    for (int i = 0; i < _gameObject.Count; i++)
                    {
                        if (!_gameObject[i]._isActive)
                        {
                            _gameObject.RemoveAt(i);
                        }
                    }

                    if (Singleton.Instance.newStar && !Singleton.Instance.isShooting)
                    {
                        Singleton.Instance.shootingCount++;

                        if (Singleton.Instance.shootingCount == Singleton.Instance.newLineCount)
                        {
                            for(int i = 0; i < 8; i++)
                            {
                                if(Singleton.Instance.STARMAP[i, 9] > 0)
                                {
                                    Singleton.Instance.CurrentGameState = Singleton.GameState.GameLose;
                                }
                            }

                            Singleton.Instance.shootingCount = 0;
                            for (int i = 0; i < 10; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    Singleton.Instance.STARMAP_Sub[j, i] = Singleton.Instance.STARMAP[j, i];
                                    Singleton.Instance.STARMAP[j, i] = 0;
                                }
                            }

                            _gameObject.Clear();

                            switch (Singleton.Instance.CurrentSign)
                            {
                                case Singleton.StarSign.Normal:
                                    Singleton.Instance.CurrentSign = Singleton.StarSign.Invert;
                                    if(Singleton.Instance.newLineOfLevel > 0) StarGenerate(1 /*Singleton.Instance.gameLevel*/);
                                    StarSlide();
                                    break;
                                case Singleton.StarSign.Invert:
                                    Singleton.Instance.CurrentSign = Singleton.StarSign.Normal;
                                    if (Singleton.Instance.newLineOfLevel > 0) StarGenerate(1 /*Singleton.Instance.gameLevel*/);
                                    StarSlide();
                                    break;
                            }
                            Singleton.Instance.newLineOfLevel--;

                            _randomStarID = Singleton.Instance.starBuffer;
                            _gameObject.Add(new Star(_textureStar[_randomStarID], "InCatapult", _randomStarID)
                            {
                                _name = "InCatapult",
                                _position = new Vector2(Singleton._INCATAPULTPOS_X, Singleton._INCATAPULTPOS_Y),
                                _viewport = new Rectangle()
                            });
                        }

                        _randomStarID = random.Next(1, 2 + Singleton.Instance.gameLevel);
                        _gameObject.Add(new Star(_textureStar[_randomStarID], "InReserve", _randomStarID)
                        {
                            _name = "InReserve",
                            _position = new Vector2(Singleton._INRESERVEPOS_X, Singleton._INRESERVEPOS_Y),
                            _viewport = new Rectangle()
                        });

                        Singleton.Instance.newStar = false;
                    }

                    if(_gameObject.Count < 3)
                    {
                        Singleton.Instance.CurrentGameState = Singleton.GameState.NextLevel;
                    }

                    Singleton.Instance.PreviousMouse = _mouseState;

                    break;
                case Singleton.GameState.NextLevel:
                    _mouseState = Mouse.GetState();
                    Singleton.Instance.CurrentMouse = _mouseState;
                    if (Singleton.Instance.gameLevel > 6) Singleton.Instance.CurrentGameState = Singleton.GameState.GameWin;
                    else if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        _gameObject.Clear();
                        Singleton.Instance.gameLevel++;
                        Singleton.Instance.shootingCount = 0;
                        Singleton.Instance.newLineOfLevel = 5;
                        Singleton.Instance.CurrentGameState = Singleton.GameState.GameStart;
                        Singleton.Instance.CurrentSign = Singleton.StarSign.Normal;
                        resetArray();
                    }
                    Singleton.Instance.PreviousMouse = _mouseState;
                    break;
                case Singleton.GameState.GameLose:
                    _mouseState = Mouse.GetState();
                    Singleton.Instance.CurrentMouse = _mouseState;
                    mainMenu[3].Update(gameTime);
                    _gameObject.Clear();
                    GGZ = null;
                    break;
                case Singleton.GameState.GameReset:
                    _gameObject.Clear();
                    Singleton.Instance.gameLevel = 1;
                    Singleton.Instance.shootingCount = 0;
                    Singleton.Instance.newLineOfLevel = 5;
                    Singleton.Instance.CurrentGameState = Singleton.GameState.GameStart;
                    Singleton.Instance.CurrentSign = Singleton.StarSign.Normal;
                    Singleton.Instance.gameScore = 0;
                    resetArray();
                    GGZ = null;
                    break;
                case Singleton.GameState.ToMainMenu:
                    _gameObject.Clear();
                    Singleton.Instance.gameLevel = 1;
                    Singleton.Instance.shootingCount = 0;
                    Singleton.Instance.newLineOfLevel = 5;
                    Singleton.Instance.CurrentGameState = Singleton.GameState.MainMenu;
                    Singleton.Instance.CurrentSign = Singleton.StarSign.Normal;
                    Singleton.Instance.gameScore = 0;
                    resetArray();
                    GGZ = null;
                    break;
                case Singleton.GameState.GameWin:
                    _mouseState = Mouse.GetState();
                    Singleton.Instance.CurrentMouse = _mouseState;
                    mainMenu[2].Update(gameTime);
                    _gameObject.Clear();
                    GGZ = null;
                    break;
            }

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.Start:
                    break;
                case Singleton.GameState.MainMenu:
                    mainMenu[0].Draw(gameTime, _spriteBatch);
                    break;
                case Singleton.GameState.Tutorial:
                    mainMenu[1].Draw(gameTime, _spriteBatch);
                    break;
                case Singleton.GameState.GameStart:
                    break;
                case Singleton.GameState.GamePlaying:
                    drawGame();
                    break;
                case Singleton.GameState.NextLevel:
                    _spriteBatch.Draw(_nextLevel, new Vector2(0, 0), null, Color.White);
                    /*drawGame();*/
                    break;
                case Singleton.GameState.GameLose:
                    mainMenu[3].Draw(gameTime, _spriteBatch);
                    break;
                case Singleton.GameState.GameReset:
                    break;
                case Singleton.GameState.ToMainMenu:
                    break;
                case Singleton.GameState.GameWin:
                    mainMenu[2].Draw(gameTime, _spriteBatch);
                    break;
            }

            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void drawGame()
        {
            _spriteBatch.Draw(_gameBackground, new Vector2(0, 0), null, Color.White);

            _gameCatapult.Draw(_spriteBatch);

            foreach (var obj in _gameObject)
            {
                obj.Draw(_spriteBatch);
            }

            _spriteBatch.Draw(_gameBackgroundScore, rect, Color.White);

            GGZ.Draw(_spriteBatch);

            //ckeck id in array
            /*_spriteBatch.Draw(_gameBackground, rect, Color.Yellow);
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 8; j++)
                    _spriteBatch.DrawString(f, Singleton.Instance.STARMAP[j, i] + " ", new Vector2(500 + (50 * j), 0 + (25 * i)), Color.Red);*/
        }

        private void resetArray()
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Singleton.Instance.STARMAP[j, i] = 0;
                    Singleton.Instance.STARMAP_Sub[j, i] = 0;
                }
            }
        }

        private void StarGenerate(int row)
        {
            switch (Singleton.Instance.CurrentSign)
            {
                case Singleton.StarSign.Normal:
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            _randomStarID = random.Next(1, 2 + Singleton.Instance.gameLevel);
                            _gameObject.Add(new Star(_textureStar[_randomStarID], "InAir", _randomStarID)
                            {
                                _name = "InAir",
                                _position = new Vector2(_starSize * j + 30 * (i % 2), _starSize * i),
                                _viewport = new Rectangle()
                            });
                        }
                    }
                    break;
                case Singleton.StarSign.Invert:
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < 8 - ( (i+1) % 2); j++)
                        {
                            _randomStarID = random.Next(1, 2 + Singleton.Instance.gameLevel);
                            _gameObject.Add(new Star(_textureStar[_randomStarID], "InAir", _randomStarID)
                            {
                                _name = "InAir",
                                _position = new Vector2(_starSize * j + 30 * ( (i+1) % 2), _starSize * i),
                                _viewport = new Rectangle()
                            });
                        }
                    }
                    break;
            }
        }

        private void StarSlide()
        {
            switch (Singleton.Instance.CurrentSign)
            {
                case Singleton.StarSign.Normal:
                    for (int i = 1; i < 10; i++)
                    {
                        for (int j = 0; j < 8 - (i % 2); j++)
                        {
                            _randomStarID = Singleton.Instance.STARMAP_Sub[j, i - 1];
                            if(_randomStarID > 0 && _randomStarID < 8)
                            {
                                _gameObject.Add(new Star(_textureStar[_randomStarID], "InAir", _randomStarID)
                                {
                                    _name = "InAir",
                                    _position = new Vector2(_starSize * j + 30 * (i % 2), _starSize * i),
                                    _viewport = new Rectangle()
                                });
                            }
                        }
                    }    
                    break;
                case Singleton.StarSign.Invert:
                    for (int i = 1; i < 10; i++)
                    {
                        for (int j = 0; j < 8 - ( (i + 1) % 2); j++)
                        {
                            _randomStarID = Singleton.Instance.STARMAP_Sub[j, i - 1];
                            if(_randomStarID > 0 && _randomStarID < 8)
                            {
                                _gameObject.Add(new Star(_textureStar[_randomStarID], "InAir", _randomStarID)
                                {
                                    _name = "InAir",
                                    _position = new Vector2(_starSize * j + 30 * ((i + 1) % 2), _starSize * i),
                                    _viewport = new Rectangle()
                                });
                            }
                        }
                    }
                    break;
            }
        }



    }
}
