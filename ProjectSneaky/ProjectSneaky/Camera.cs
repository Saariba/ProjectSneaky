using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSneaky
{
    class Camera
    {
        Vector2 position, origin;

        public Camera(Viewport _viewport)
        {
            origin = new Vector2(_viewport.Width / 2, _viewport.Height / 2);
        }

        public Matrix GetViewMatrix()
        {
            position = GameStuff.Instance.player.playerPosition;


            return Matrix.CreateTranslation(new Vector3(-position + origin, 1));
        }
    }
}
