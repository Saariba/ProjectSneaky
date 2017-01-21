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
        public Texture2D playerTexture;
        public Vector2 playerPosition;

        float health;
        float speed;
        Vector2 move;
        

        public Player(Texture2D _playerTexture,Vector2 _playerPosition, float _speed)
        {
            playerTexture = _playerTexture;
            playerPosition = _playerPosition;
            health = 5;
           
            speed = _speed;
        }

        public void ApplyDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                playerPosition = new Vector2(100, 100);
                GameStuff.Instance.guard1.guardPosition = new Vector2(0, 40);
                GameStuff.Instance.guard2.guardPosition = new Vector2(200, 80);
                GameStuff.Instance.guard1.changeDetectionStatus(false);
                GameStuff.Instance.guard2.changeDetectionStatus(false);
                //obiges resettet nach dem Tod die Spielerposition, die Guardposition, und, dass die Guards einen nicht weiter verfolgen
                health = 5;
            }

        }

         void Movement()
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
