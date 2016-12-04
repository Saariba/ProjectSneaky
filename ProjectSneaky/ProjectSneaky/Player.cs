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
        

        public Player(Texture2D _playerTexture,Vector2 _playerPosition)
        {
            playerTexture = _playerTexture;
            playerPosition = _playerPosition;
           
        }

         void Movement()
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Up) || (key.IsKeyDown(Keys.W)))
                playerPosition.Y -= 1;

            if (key.IsKeyDown(Keys.Down) || (key.IsKeyDown(Keys.S)))
                playerPosition.Y += 1;

            if (key.IsKeyDown(Keys.Left) || (key.IsKeyDown(Keys.A)))
                playerPosition.X -= 1;

            if (key.IsKeyDown(Keys.Right) || (key.IsKeyDown(Keys.D)))
                playerPosition.X += 1;
        }


        public void Update()
        {
            Movement();
            System.Console.WriteLine(playerPosition); // Console Output playerPosition
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(playerTexture, playerPosition, Color.White);
        }
    }

}
