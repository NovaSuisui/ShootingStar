using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;


namespace ShootingStar
{
    class Singleton
    {
        //all screen
        public const int ALLSCREENWIDTH = 800;
        public const int ALLSCREENHIGHT = 800;

        //game screen
        public Vector2 GAMESCREEN = new Vector2(480, 800);
        //score screen
        public Vector2 SCORESCREEN = new Vector2(320, 800);

        //star size
        public const int STARSIZE = 60;
        //check new star
        public bool newStar = false;
        //check star is shoothing
        public bool isShooting = false;

        //position star in catapult
        public const int _INCATAPULTPOS_X = 210;
        public const int _INCATAPULTPOS_Y = 710;
        //position star in reserve
        public const int _INRESERVEPOS_X = 0;
        public const int _INRESERVEPOS_Y = 740;

        //level
        public int gameLevel = 1;
        //count for create new top line object
        public int shootingCount = 0;
        public int newLineCount = 7;
        //count of new top line of any level
        public int newLineOfLevel = 5;

        //map color id of star
        public int[,] STARMAP;
        public int[,] STARMAP_Sub;
        //contain color id of star in catapult
        public int starBuffer;
        //state for swap algorithm of abb and even line
        public enum StarSign
        {
            Normal,
            Invert
        }
        public StarSign CurrentSign;

        //TODO: Game State Machine
        public enum GameState
        {
            Start,
            MainMenu,
            Tutorial,
            GameStart,
            GamePlaying,
            NextLevel,
            GameLose,
            GameReset,
            ToMainMenu,
            GameWin,
        }
        public GameState CurrentGameState;

        //contain mouse state for nay object
        public MouseState CurrentMouse, PreviousMouse;

        // ------------------------------------------------- Kue -------------------------------------------------//
        public SoundEffect VFX;
        public SoundEffectInstance VFX_popInstance;
        public SoundEffectInstance VFX_EndOfTheWorldInstance;

        public int gameScore;
        public SpriteFont _gamefont;
        // ------------------------------------------------- Kue -------------------------------------------------//

        /*-------------------------------------TAR WORK SPACE -------------------------------------*/

        // Main Menu

        public object instanceRetroClickObj;
        public object instanceBackgroundSquare;
        public object instanceSolorGroupBig;
        public object instanceShootingStar;
        public object instanceSpriteFont;
        public object instancePlayGameButtonTexture;
        public object instanceTutorialButtonTexture;
        public object instanceExitGameButtonTexture;

        // Tutorial

        public object instanceJupiter;
        public object instanceMar;
        public object instanceMercury;
        public object instanceSaturn;
        public object instanceUranusBig;
        public object instanceFirstLine;
        public object instanceSecondLine;
        public object instanceExtraLine;
        public object instanceThirdLine;
        public object instanceFourthLine;

        public object instanceReturnButtonTexture;

        // Win
        public object instanceSolorGroupMedium;
        public object instanceKaboom;
        public object instanceGreenLightSaber;

        // LOSE
        public object instanceRedLightSaber;

        /*-------------------------------------TAR WORK SPACE -------------------------------------*/

        //singleton
        private static Singleton instance;

        private Singleton()
        {
        }
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
