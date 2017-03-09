using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSneaky.Items
{
    abstract class Item
    {
        protected Vector2 position;

        protected Texture2D texture;

        protected bool alive;

        protected Rectangle meRect;

        protected bool collidingWithPlayer;


        // Getters
        public bool getPlayerCollision() { return collidingWithPlayer; }

        // Setters
        public void setPlayerCollision(bool _playerCollision) { collidingWithPlayer = _playerCollision; }


        // Constructor
        public Item (Texture2D _texture, Vector2 _position)
        {
            position = _position;
            texture = _texture;
            alive = true;
            meRect = new Rectangle((int)_position.X - _texture.Width / 2, (int)_position.Y - _texture.Height / 2, _texture.Width, _texture.Height);
        }

        public bool getAlive() { return alive; }

        public Vector2 getPosition() { return position; }
       
        protected virtual void OnPlayerCollision()
        {
            if (!collidingWithPlayer)
            {
                collidingWithPlayer = true;
            }
        }
        

        public virtual void Update()
        {
            if (meRect.Intersects(GameStuff.Instance.player.playerHitbox) && alive)
                OnPlayerCollision();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, meRect, Color.White);
        }

    }
}
