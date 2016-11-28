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
        Rectangle rectPlayer;
        

        public Player(Texture2D _playerTexture,Vector2 _playerPosition)
        {
            playerTexture = _playerTexture;
            playerPosition = _playerPosition;
           
        }

        public void Update()
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Up))
                playerPosition.Y -= 1;

            if (key.IsKeyDown(Keys.Down))
                playerPosition.Y += 1;

            if (key.IsKeyDown(Keys.Left))
                playerPosition.X -= 1;

            if (key.IsKeyDown(Keys.Right))
                playerPosition.X += 1;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(playerTexture, playerPosition, Color.White);
        }
    }

}
