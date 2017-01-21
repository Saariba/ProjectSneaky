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
        public Vector2 guardPosition;
        float speed;
        Vector2 move;

        Vector2 targetPos1;
        Vector2 targetPos2;
        Vector2 currentTarget;

        String facingDirection;        // north,east,south,west
        Rectangle fieldOfView;

        Player player;
        public bool playerDetected;

        //lässt einen von außen den Guard auf den Start zurücksetzen
        public bool changeDetectionStatus(bool status)
        {
            return playerDetected = status;
        }


        public Guards (Texture2D _guardTexture, Vector2 _guardPosition,Vector2 _targetPos1,Vector2 _targetPos2,
            float _speed,String _facingDirection)
        {
            guardTexture = _guardTexture;
            guardPosition = _guardPosition;
            speed = _speed;

            playerDetected = false;
            player = GameStuff.Instance.player;

            targetPos1 = _targetPos1;
            targetPos2 = _targetPos2;
            currentTarget = _targetPos1;

            facingDirection = _facingDirection;

            // fieldOfView is a 600x1000/1000x600 Rectangle starting 
            // at the Guard and going out in the direction he is facing.
            switch (facingDirection)
            {
                case "north":     // facing north
                    fieldOfView = new Rectangle((int)_guardPosition.X - 300, (int)_guardPosition.Y - 1000, 600, 1000);
                    break;
                case "east":     // facing east
                    fieldOfView = new Rectangle((int)_guardPosition.X, (int)_guardPosition.Y - 300, 1000, 600);
                    break;
                case "south":     // facing south
                    fieldOfView = new Rectangle((int)_guardPosition.X - 300, (int)_guardPosition.Y, 600, 1000);
                    break;
                case "west":     // facing west
                    fieldOfView = new Rectangle((int)_guardPosition.X - 1000, (int)_guardPosition.Y - 300, 1000, 600);
                    break;
            }
        }


        void Movement()
        {
            if (playerDetected)
                PlayerChase();
            else
                Patrol();
        }

        void Patrol() // Patroling between tragetPos1 and targetPos2
        {
            if (Vector2.Distance(guardPosition, targetPos1) < 0.0001)
            {
                currentTarget = targetPos2;
            }

            if (Vector2.Distance(guardPosition, targetPos2) < 0.0001)
            {
                currentTarget = targetPos1;
            }

            move = currentTarget - guardPosition;

            if (move.Length() > 1)
            {
                move.Normalize();
                move *= speed;
            }

            guardPosition += move;

        }


        void PlayerDetection()  // Changing playerDetected to true if Player is inside fieldOfView
        {
            if (player.playerPosition.Y > fieldOfView.Top && player.playerPosition.Y < fieldOfView.Bottom
                && player.playerPosition.X > fieldOfView.Left && player.playerPosition.X < fieldOfView.Right)
                playerDetected = true;
        }

        void PlayerChase()  // chasing player
        {
            currentTarget = player.playerPosition;

            move = currentTarget - guardPosition;

            if (move.Length() > 1)
            {
                move.Normalize();
                move *= speed;
            }

            guardPosition += move;
        }

        void DirectionFacing()   //changing facingDirection and fieldOfView depending on the direction the guard is moving in.
        {
            
            if (move.Y < 0)                 // moving north
                facingDirection = "north";
            else if (move.X > 0)            // moving east
                facingDirection = "east";
            else if (move.Y > 0)            // moving south
                facingDirection = "south";
            else if (move.X < 0)            // moving west
                facingDirection = "west";

            // fieldOfView is a 600x1000/1000x600 Rectangle starting 
            // at the Guard and going out in the direction he is facing.

            switch (facingDirection)
            {
                case "north":     // facing north
                    fieldOfView.X = (int)guardPosition.X - 300;
                    fieldOfView.Y = (int)guardPosition.Y - 1000;
                    fieldOfView.Width = 600;
                    fieldOfView.Height = 1000;
                    break;
                case "east":     // facing east
                    fieldOfView.X = (int)guardPosition.X;
                    fieldOfView.Y = (int)guardPosition.Y - 300;
                    fieldOfView.Width = 1000;
                    fieldOfView.Height = 600;
                    break;
                case "south":     // facing south
                    fieldOfView.X = (int)guardPosition.X - 300;
                    fieldOfView.Y = (int)guardPosition.Y;
                    fieldOfView.Width = 600;
                    fieldOfView.Height = 1000;
                    break;
                case "west":     // facing west
                    fieldOfView.X = (int)guardPosition.X - 1000;
                    fieldOfView.Y = (int)guardPosition.Y - 300;
                    fieldOfView.Width = 1000;
                    fieldOfView.Height = 600;
                    break;
            }
        }


        public void Update()
        {
            PlayerDetection();
            Movement();
            DirectionFacing();

            //Überprüft, ob Schaden applyt werden soll
            Vector2 distance = currentTarget - guardPosition;

            if (distance.Length() <= 30)
            {
                player.ApplyDamage(5);
            }

        }
           

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(guardTexture, guardPosition, Color.White);
        }
    }
}

