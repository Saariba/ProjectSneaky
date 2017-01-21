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
        float speed;
        Vector2 move;
        

        public Player(Texture2D _playerTexture,Vector2 _playerPosition, float _speed)
        {
            playerTexture = _playerTexture;
            playerPosition = _playerPosition;
            speed = _speed;
        }

         void Movement(Tilemap tileMap)
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Up) || (key.IsKeyDown(Keys.W)))
                //  playerPosition.Y -= 1*5;
                move.Y -= 1 * speed;

            if (key.IsKeyDown(Keys.Down) || (key.IsKeyDown(Keys.S)))
                // playerPosition.Y += 1*5;
                move.Y += 1 * speed;

            if (key.IsKeyDown(Keys.Left) || (key.IsKeyDown(Keys.A)))
                //playerPosition.X -= 1*5;
                move.X -= 1 * speed;

            if (key.IsKeyDown(Keys.Right) || (key.IsKeyDown(Keys.D)))
                // playerPosition.X += 1*5;
                move.X += 1 * speed;

            if (tileMap.Walkable(playerPosition + move)
                && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width, 0))
                && tileMap.Walkable(playerPosition + move + new Vector2(0, playerTexture.Height))
                && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width, playerTexture.Height)))
            {
                playerPosition += move;
            }

            move.X = 0;
            move.Y = 0;
        }


        public void Update(Tilemap tileMap)
        {
            Movement(tileMap);
            System.Console.WriteLine(playerPosition); // Console Output playerPosition
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(playerTexture, playerPosition, Color.White);
        }
    }

}
