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
    class Player
    {
        Texture2D playerTexture;
        public Vector2 playerPosition;
        Vector2 move;


        public Player(Texture2D _playerTexture,Vector2 _playerPosition)
        {
            playerTexture = _playerTexture;
            playerPosition = _playerPosition;
            move = Vector2.Zero;
           
            

        }


        public void Update()
        {
            KeyboardState key = Keyboard.GetState();

            move.X = 0;
            move.Y = 0;

            if (key.IsKeyDown(Keys.Left))
                move.X -= 1;

            if (key.IsKeyDown(Keys.Right))
                move.X += 1;

            if (key.IsKeyDown(Keys.Up))
                move.Y -= 1;
            if (key.IsKeyDown(Keys.Down))
                move.Y += 1;

            Tilemap tileMap = GameStuff.Instance.tileMap;

            if (tileMap.Walkable(playerPosition + new Vector2(0, playerTexture.Height) + new Vector2(0, move.Y))
                && tileMap.Walkable(playerPosition + new Vector2(playerTexture.Width, playerTexture.Height) + new Vector2(0, move.Y)))
            {
               
                playerPosition.Y += move.Y;
            }
            else
            {
                while (tileMap.Walkable(playerPosition + new Vector2(0, playerTexture.Height))
                || tileMap.Walkable(playerPosition + new Vector2(playerTexture.Width, playerTexture.Height)))
                    playerPosition.Y -= 0.1f;
              
                move.Y = 0;
            }

            if (tileMap.Walkable(playerPosition + new Vector2(move.X, 0))
               && tileMap.Walkable(playerPosition + new Vector2(move.X, 0) + new Vector2(playerTexture.Width, 0)))
            {
                playerPosition.X += move.X;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(playerTexture, playerPosition, Color.White);
        }
    }

}
