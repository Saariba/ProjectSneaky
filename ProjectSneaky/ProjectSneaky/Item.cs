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
    abstract class Item
    {
        public Vector2 position;
        public Texture2D texture;
        public bool alive;

        public Item (Texture2D _texture, Vector2 _position)
        {
            position = _position;
            texture = _texture;
            alive = true;
        }

        protected virtual void OnPlayerCollision()
        {
            alive = false;
        }
        

        public virtual void Update(GameTime gTime)
        {
            Rectangle me = new Rectangle(position.ToPoint(), texture.Bounds.Size);
            Rectangle player = new Rectangle(GameStuff.Instance.player.playerPosition.ToPoint(),
                GameStuff.Instance.player.size.ToPoint());

            if (me.Intersects(player) && alive)
                OnPlayerCollision();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
