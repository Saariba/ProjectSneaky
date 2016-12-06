using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSneaky
{
    class GameStuff
    {
        static GameStuff instance;
        public Tilemap tileMap;
        public Player player;
        public Guards guard1;
        public Guards guard2;
        public Camera camera;

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
