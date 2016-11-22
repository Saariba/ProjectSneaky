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
    class tilemap
    {
        Tile[,] tileMap;
        int tileSize;

        public tilemap(Texture2D[] textures, Texture2D bitMap, int _tileSize)
        {
            tileSize = _tileSize;
            tileMap = new Tile[bitMap.Width, bitMap.Height];

    
        }

        private void BuildMap(Texture2D[] textures, Texture2D bitMap)
        {
            Color[] colors = new Color[bitMap.Width * bitMap.Height];
            bitMap.GetData<colors>;
        }
    }
}
