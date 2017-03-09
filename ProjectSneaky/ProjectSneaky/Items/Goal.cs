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
    class Goal : Item
    {
        public Goal(Texture2D _texture, Vector2 _position) : base(_texture, _position)
        {
            
        }

        protected override void OnPlayerCollision()
        {
            GameStuff.Instance.StageWon = true;
        }

        override public void Update()
        {
            base.Update();
            
        }

    }
}
