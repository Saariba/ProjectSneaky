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
        public Guards guard3;
        public Camera camera;
        public Goal goal;
        public List<Item> items = new List<Item>();

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
