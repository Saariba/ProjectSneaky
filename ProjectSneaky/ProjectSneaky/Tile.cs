using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSneaky
{
    class Tile
    {
        Vector2 position;
        Texture2D texture;
        int id;


        public Tile(Texture2D _texture, Vector2 _position, int _id)
        {
            _texture = texture;
            _position = position;
            id = _id;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }

}
