using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSneaky
{
    class GameStuff
    {
        static GameStuff instance;
        public Tilemap tileMap;
        public Player player;
        

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
