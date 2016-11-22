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
    class Guards
    {
        Texture2D guardTexture;
        Vector2 guardPosition;
        Vector2 targetPos1;
        Vector2 targetPos2;
        float speed;
        Vector2 currentTarget;
        public Guards (Texture2D _guardTexture, Vector2 _guardPosition,Vector2 _targetPos1,Vector2 _targetPos2,float _speed)
        {
            guardTexture = _guardTexture;
            guardPosition = _guardPosition;
            targetPos1 = _targetPos1;
            speed = _speed;
            targetPos2 = _targetPos2;
            currentTarget = _targetPos1;
        }

        public void Update()
        {
            Vector2 move = currentTarget - guardPosition;
            
            if (move.Length()*speed > 1)
                move.Normalize();
            

            move *= speed;
            guardPosition += move;

            if(Vector2.Distance(guardPosition,targetPos1)<0.0001)
            {
                currentTarget = targetPos2;
            }

            if(Vector2.Distance(guardPosition, targetPos2) < 0.0001)
            {
                currentTarget = targetPos1;
            }

            /* Vector2 move = targetPos1 - guardPosition;
             move.Normalize();
             move *= speed;
             guardPosition += move;


             if (guardPosition == targetPos1)
             {
                 move *= -1;
                 while (guardPosition != targetPos2)
                 {
                     guardPosition += move;
                 }
             }*/
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(guardTexture, guardPosition, Color.White);
        }
    }
}

