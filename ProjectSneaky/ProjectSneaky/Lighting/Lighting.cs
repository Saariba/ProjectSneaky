using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSneaky.Lighting
{
    abstract class Lighting
    {
        protected Tilemap tileMap;

        protected List<Guards> levelGuards = new List<Guards>();

        // Constructor
        public Lighting(Tilemap _tileMap, List<Guards> _levelGuards)
        {
            tileMap = _tileMap;
            levelGuards = _levelGuards;
        }

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
