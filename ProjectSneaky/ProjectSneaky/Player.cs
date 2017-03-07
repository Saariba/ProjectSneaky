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
        private Vector2 stageStartPosition;
        private Vector2 currStartPosition;
        public Rectangle playerHitbox;
        
        float speed;
        Vector2 move;
        public Vector2 size;
        
        // Getters
        public Vector2 getCurrStartPosition() { return currStartPosition; }

        public Vector2 getStageStartPosition() { return stageStartPosition; }

        // Setters
        public void setCurrStartPosition(Vector2 _currStartPosition) { currStartPosition = _currStartPosition; }

        public Player(Texture2D _playerTexture,Vector2 _playerPosition, float _speed)
        {
            playerTexture = _playerTexture;
            playerPosition = _playerPosition;
            currStartPosition = _playerPosition;
            stageStartPosition = _playerPosition;

        speed = _speed;
            size = new Vector2(_playerTexture.Width, _playerTexture.Height);
            playerHitbox = new Rectangle((int)_playerPosition.X - _playerTexture.Width / 2, (int)_playerPosition.Y - _playerTexture.Height / 2, _playerTexture.Width, _playerTexture.Height);
        }

         void Movement(Tilemap tileMap)
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Up) || (key.IsKeyDown(Keys.W)))
                move.Y -= 1 * speed;

            if (key.IsKeyDown(Keys.Down) || (key.IsKeyDown(Keys.S)))
                move.Y += 1 * speed;

            if (key.IsKeyDown(Keys.Left) || (key.IsKeyDown(Keys.A)))
                move.X -= 1 * speed;

            if (key.IsKeyDown(Keys.Right) || (key.IsKeyDown(Keys.D)))
                move.X += 1 * speed;

            if (tileMap.Walkable(playerPosition + move)
               && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width / 2, - playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width / 2, 0))
               && tileMap.Walkable(playerPosition + move + new Vector2(playerTexture.Width / 2, playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(- playerTexture.Width / 2, - playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(- playerTexture.Width / 2, 0))
               && tileMap.Walkable(playerPosition + move + new Vector2(- playerTexture.Width / 2, playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(0, - playerTexture.Height / 2))
               && tileMap.Walkable(playerPosition + move + new Vector2(0, playerTexture.Height / 2)))
            {
                playerPosition += move;
            }

            move.X = 0;
            move.Y = 0;
        }


        public void Update(Tilemap tileMap)
        {
            Movement(tileMap);

            playerHitbox.X = (int)playerPosition.X - playerTexture.Width / 2;
            playerHitbox.Y = (int)playerPosition.Y - playerTexture.Height / 2;

            System.Console.WriteLine(playerPosition); // Console Output playerPosition
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(playerTexture, new Vector2 (playerPosition.X - playerTexture.Width/2, playerPosition.Y - playerTexture.Height / 2), Color.White);
        }
    }

}
