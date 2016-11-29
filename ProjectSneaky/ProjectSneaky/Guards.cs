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
        float speed;
        Vector2 move;

        Vector2 targetPos1;
        Vector2 targetPos2;
        Vector2 currentTarget;

        int facingDirection;        // 0 = North, 1 = East , 2 = South, 3 = West
        Rectangle fieldOfView;

        Player player;
        bool playerDetected;


        public Guards (Texture2D _guardTexture, Vector2 _guardPosition,Vector2 _targetPos1,Vector2 _targetPos2,
            float _speed,int _facingDirection, Player _player)
        {
            guardTexture = _guardTexture;
            guardPosition = _guardPosition;
            speed = _speed;

            playerDetected = false;
            player = _player;

            targetPos1 = _targetPos1;
            targetPos2 = _targetPos2;
            currentTarget = _targetPos1;

            facingDirection = _facingDirection;
            
            // fieldOfView is a 600x1000/1000x600 Rectangle starting 
            // at the Guard and going out in the direction he is facing.
            if (_facingDirection == 0)              // facing north
                fieldOfView = new Rectangle((int)_guardPosition.X - 300, (int)_guardPosition.Y - 1000, 600, 1000);
            else if (_facingDirection == 1)         //facing east
                fieldOfView = new Rectangle((int)_guardPosition.X, (int)_guardPosition.Y - 300, 1000, 600);
            else if (_facingDirection == 2)         //facing south
                fieldOfView = new Rectangle((int)_guardPosition.X -300, (int)_guardPosition.Y, 600, 1000);
            else if (_facingDirection == 3)         //facing west
                fieldOfView = new Rectangle((int)_guardPosition.X - 1000, (int)_guardPosition.Y - 300, 1000, 600);



        }


        void Patrol() // Patrol
        {
            if (playerDetected == false)
            {
                if (Vector2.Distance(guardPosition, targetPos1) < 0.0001)
                {
                    currentTarget = targetPos2;
                }

                if (Vector2.Distance(guardPosition, targetPos2) < 0.0001)
                {
                    currentTarget = targetPos1;
                }
            }
            else
            {
                currentTarget = player.playerPosition;    //PlayerChase
            }

            move = currentTarget - guardPosition;

            if (move.Length() * speed > 1)
                move.Normalize();


            move *= speed;
            guardPosition += move;
        }


        void PlayerDetection()  // Changing playerDetected to true if Player is inside fieldOfView
        {
            if (player.playerPosition.Y > fieldOfView.Top && player.playerPosition.Y < fieldOfView.Bottom
                && player.playerPosition.X > fieldOfView.Left && player.playerPosition.X < fieldOfView.Right)
                playerDetected = true;
        }

        void PlayerChase()  // starting to chase if player is detected else continue patrol
        {
            if (playerDetected)
                currentTarget = player.playerPosition;

            move = currentTarget - guardPosition;

            if (move.Length() * speed > 1)
                move.Normalize();


            move *= speed;
            guardPosition += move;
        }

        void DirectionFacing()   //changing facingDirection depending on the direction the guard is moving in.
        {
            if (move.Y < 0)                 // moving north
                facingDirection = 0;
            else if (move.X > 0)            // moving east
                facingDirection = 1;
            else if (move.Y > 0)            // moving south
                facingDirection = 2;
            else if (move.X < 0)            // moving west
                facingDirection = 3;

            // fieldOfView is a 600x1000/1000x600 Rectangle starting 
            // at the Guard and going out in the direction he is facing.
            if (facingDirection == 0)              // facing north
                fieldOfView = new Rectangle((int)guardPosition.X - 300, (int)guardPosition.Y - 1000, 600, 1000);
            else if (facingDirection == 1)         //facing east
                fieldOfView = new Rectangle((int)guardPosition.X, (int)guardPosition.Y - 300, 1000, 600);
            else if (facingDirection == 2)         //facing south
                fieldOfView = new Rectangle((int)guardPosition.X - 300, (int)guardPosition.Y, 600, 1000);
            else if (facingDirection == 3)         //facing west
                fieldOfView = new Rectangle((int)guardPosition.X - 1000, (int)guardPosition.Y - 300, 1000, 600);

        }


        public void Update()
        {
            Patrol();
            PlayerDetection();  
            DirectionFacing();
        }  

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(guardTexture, guardPosition, Color.White);
        }
    }
}

