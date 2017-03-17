using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectSneaky.Items;
using ProjectSneaky.Levels;

namespace ProjectSneaky
{
    class GameStuff
    {
        static GameStuff instance;

            // Level List
        public List<MuseumRoom> museumRoomLevels = new List<MuseumRoom>();
                
            //Levels
        public MuseumRoom museumEntryLevel;
        public MuseumRoom museumLeftRoomLevel;
        public MuseumRoom museumRightRoomLevel;
        public MuseumRoom museumTopRoomLevel;
       
            // Standard Textures
        public Texture2D[] defaultLevelTextures;   // Index 0 = Floor, Index 1 = Wall
        public Texture2D[] defaultDoorTextures;    // Index 0 = Horizontal, Index 1 = Vertical

        public Texture2D[] basicLighting4Shades;
        public Texture2D[] guardLight;  // One for each direction in this order North, East, South, West

        public Player player;
        
        public Camera camera;
                
        public bool StageWon;
        

        public enum GameStates
        {
            stage1Start,
            stage1Lost,
            stage1Won,

            museumEntryStandardStart,
            museumEntryLeftDoorStart,
            museumEntryRightDoorStart,

            museumLeftRoomLowerDoorStart,
            museumLeftRoomUpperDoorStart,

            museumRightRoomLowerDoorStart,
            museumRightRoomUpperDoorStart,

            museumTopRoomLeftDoorStart,
            museumTopRoomRightDoorStart
        }
        public GameStates gameStateCurr;
        public GameStates gameStatePrev;
        

        private GameStuff()
        {

        }

        public static GameStuff Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameStuff();

                return instance;
            }
        }
    }
}
