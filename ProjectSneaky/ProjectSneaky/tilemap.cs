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
    class Tilemap
    {
        Tile[,] tileMap;
        int tileSize;
        private Texture2D[] texture2D;

        public Tilemap(Texture2D[] textures, Texture2D bitMap, int _tileSize)
        {
            tileSize = _tileSize;
            tileMap = new Tile[bitMap.Width, bitMap.Height];

            BuildMap(textures, bitMap);
        }

        public Tilemap(Texture2D[] texture2D)
        {
            this.texture2D = texture2D;
        }

        private void BuildMap(Texture2D[] textures, Texture2D bitMap)
        {
            Color[] colors = new Color[bitMap.Width * bitMap.Height];

            bitMap.GetData(colors);

            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    if (colors[y * tileMap.GetLength(0) + x] == Color.White)
                    {
                        //Wooden Floor
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 0);
                    }
                    else
                    {
                        //Walls
                        tileMap[x, y] = new Tile(textures[1], new Vector2(x * tileSize, y * tileSize), 1);
                    }
                }
            }

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int y = 0; y<tileMap.GetLength(1); y++)
            {
                for (int x = 0; x<tileMap.GetLength(0); x++)
                {
                    tileMap[x, y].Draw(spriteBatch);
                }
            }
        }



    }
}
