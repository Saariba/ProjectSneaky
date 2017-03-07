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
        public Tile[,] tileMap;
        public int tileSize;
        public int offset; // to get positions in the middle of the texture and not to the top left corner

        private Texture2D[] texture2D;

        private List<Vector2> doorPositions = new List<Vector2>();
        private List<int> doorRotation = new List<int>(); // 0 = Horizontal, 1 = Vertical

        private List<Vector2> variousPositions = new List<Vector2>();

        //Getters
        public List<Vector2> getDoorPositions() { return doorPositions; }

        public List<int> getDoorRotations() { return doorRotation; }

        public List<Vector2> getVariousPositions() { return variousPositions; }

        //Constructor
        public Tilemap(Texture2D[] textures, Texture2D bitMap, int _tileSize)
        {
            tileSize = _tileSize;
            offset = _tileSize / 2;
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
                    else if(colors[y * tileMap.GetLength(0) + x] == Color.Red)
                    {
                        //Wooden Floor
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 0);

                        variousPositions.Add(new Vector2(x * tileSize + offset, y * tileSize + offset));
                    }
                    else if (colors[y * tileMap.GetLength(0) + x] == Color.Blue && colors[(y + 1) * tileMap.GetLength(0) + x] == Color.Blue)  // Vertical Door
                    {
                        //Wooden Floor
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 0);
                        tileMap[x, y + 1] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 0);

                        doorPositions.Add(new Vector2(x * tileSize + offset, y * tileSize + tileSize));
                        doorRotation.Add(1);
                    }
                    else if (colors[y * tileMap.GetLength(0) + x] == Color.Blue && colors[y * tileMap.GetLength(0) + x + 1] == Color.Blue)  // Horizontal Door
                    {
                        //Wooden Floor
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 0);
                        tileMap[x + 1, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 0);

                        doorPositions.Add(new Vector2(x * tileSize + tileSize, y * tileSize + offset));
                        doorRotation.Add(0);
                    }
                    else if(colors[y * tileMap.GetLength(0) + x] == Color.Black)
                    {
                        //Walls
                        tileMap[x, y] = new Tile(textures[1], new Vector2(x * tileSize, y * tileSize), 1);
                    }
                }
            }

        }

        public bool Walkable(Vector2 currentPosition)
        {
            return tileMap[(int)currentPosition.X / tileSize, (int)currentPosition.Y / tileSize].Walkable(); 
        }

        public void Update(GameTime gameTime)
        {

        }

         public void Draw(SpriteBatch spriteBatch)
         {
             for(int y = (int) GameStuff.Instance.camera.position.Y - (int) GameStuff.Instance.camera.origin.Y - tileSize; y< (int) GameStuff.Instance.camera.position.Y + (int) GameStuff.Instance.camera.origin.Y + tileSize; y += tileSize)
             {
                 for (int x = (int)GameStuff.Instance.camera.position.X - (int)GameStuff.Instance.camera.origin.X - tileSize; x < (int)GameStuff.Instance.camera.position.X + (int)GameStuff.Instance.camera.origin.X + tileSize; x += tileSize)
                 {   
                     if(x >=0 && y >= 0 && x<tileMap.GetLength(0)*tileSize && y<tileMap.GetLength(1)*tileSize)
                     tileMap[x/tileSize, y/tileSize].Draw(spriteBatch);
                 }
             }
         }

       /* public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = (int)GameStuff.Instance.camera.position.Y - (int)GameStuff.Instance.camera.origin.Y - tileSize; y < (int)GameStuff.Instance.camera.position.Y + (int)GameStuff.Instance.camera.origin.Y + tileSize; y++)
            {
                for (int x = (int)GameStuff.Instance.camera.position.X - (int)GameStuff.Instance.camera.origin.X - tileSize; x < (int)GameStuff.Instance.camera.position.X + (int)GameStuff.Instance.camera.origin.X + tileSize; x++)
                {
                    if (x >= 0 && y >= 0 && x < tileMap.GetLength(0) && y < tileMap.GetLength(1))
                        tileMap[x , y].Draw(spriteBatch);
                }
            }
        }*/



    }
}
