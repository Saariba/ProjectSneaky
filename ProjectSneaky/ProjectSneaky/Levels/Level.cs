using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSneaky.Levels
{
    abstract class Level
    {
        protected Tilemap tileMap;

        public Tilemap getTileMap() { return tileMap; }

        public Level(Texture2D _BitMap)
        {
            tileMap = new Tilemap( GameStuff.Instance.defaultLevelTextures, _BitMap, 16);
        }

        abstract public void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);
        }
    }
}
