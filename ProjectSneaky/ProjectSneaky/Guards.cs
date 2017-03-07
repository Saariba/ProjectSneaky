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
        private Vector2 guardStartPosition;
        float speed;
        Vector2 move;

        Vector2 targetPos1;
        Vector2 targetPos2;
        Vector2 currentTarget;

        String facingDirection;        // north,east,south,west
        Rectangle fieldOfView;
        int fieldOfViewLongSideLength;
        int fieldOfViewShortSideLength;

        Vector2 guardToPLayer;
        float guardToPLayerLength;
        Vector2 temp;

        Player player;
        public bool playerDetected;

        //Getters
        public Vector2 getGuardStartPosition() { return guardStartPosition; }

        //lässt einen von außen den Guard auf den Start zurücksetzen
        public bool changeDetectionStatus(bool status)
        {
            return playerDetected = status;
        }

        // Constructor
        public Guards (Texture2D _guardTexture, Vector2 _guardPosition,Vector2 _targetPos1,Vector2 _targetPos2,
            float _speed,String _facingDirection)
        {
            guardTexture = _guardTexture;
            guardPosition = _guardPosition;
            guardStartPosition = _guardPosition;
            speed = _speed;

            playerDetected = false;
            player = GameStuff.Instance.player;

            targetPos1 = _targetPos1;
            targetPos2 = _targetPos2;
            currentTarget = _targetPos1;

            facingDirection = _facingDirection;


            // fieldOfView is a 400x400 Rectangle starting 
            // at the Guard and going out in the direction he is facing.

            fieldOfViewLongSideLength = 400;

            fieldOfViewShortSideLength = 400;

            switch (facingDirection)
            {
                case "north":     // facing north
                    fieldOfView = new Rectangle((int)_guardPosition.X - fieldOfViewShortSideLength/2, (int)_guardPosition.Y - fieldOfViewLongSideLength,
                        fieldOfViewShortSideLength, fieldOfViewLongSideLength);
                    break;
                case "east":     // facing east
                    fieldOfView = new Rectangle((int)_guardPosition.X, (int)_guardPosition.Y - fieldOfViewShortSideLength/2,
                        fieldOfViewLongSideLength, fieldOfViewShortSideLength);
                    break;
                case "south":     // facing south
                    fieldOfView = new Rectangle((int)_guardPosition.X - fieldOfViewShortSideLength/2, (int)_guardPosition.Y,
                        fieldOfViewShortSideLength, fieldOfViewLongSideLength);
                    break;
                case "west":     // facing west
                    fieldOfView = new Rectangle((int)_guardPosition.X - fieldOfViewLongSideLength, (int)_guardPosition.Y - fieldOfViewShortSideLength/2,
                        fieldOfViewLongSideLength, fieldOfViewShortSideLength);
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


        void PlayerDetection(Tilemap _tileMap)  // Changing playerDetected to true if Player is inside fieldOfView and no wall between guard and player
        {   

            if(fieldOfView.Contains(GameStuff.Instance.player.playerPosition))
            {
                guardToPLayer = player.playerPosition - guardPosition;

                guardToPLayerLength = guardToPLayer.Length();

                guardToPLayer.Normalize();

                // check every tile on a line between guard and player and break if it's a wall
                for(int i = 0; i < (int) guardToPLayerLength; i++)
                {
                    temp = guardPosition + (guardToPLayer * i);

                    if (_tileMap.tileMap[(int)temp.X / _tileMap.tileSize, (int)temp.Y / _tileMap.tileSize].id == 1)
                        return;
                }

                playerDetected = true;

            }
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
                    fieldOfView.X = (int)guardPosition.X - fieldOfViewShortSideLength/2;
                    fieldOfView.Y = (int)guardPosition.Y - fieldOfViewLongSideLength;
                    fieldOfView.Width = fieldOfViewShortSideLength;
                    fieldOfView.Height = fieldOfViewLongSideLength;
                    break;
                case "east":     // facing east
                    fieldOfView.X = (int)guardPosition.X;
                    fieldOfView.Y = (int)guardPosition.Y - fieldOfViewShortSideLength/2;
                    fieldOfView.Width = fieldOfViewLongSideLength;
                    fieldOfView.Height = fieldOfViewShortSideLength;
                    break;
                case "south":     // facing south
                    fieldOfView.X = (int)guardPosition.X - fieldOfViewShortSideLength/2;
                    fieldOfView.Y = (int)guardPosition.Y;
                    fieldOfView.Width = fieldOfViewShortSideLength;
                    fieldOfView.Height = fieldOfViewLongSideLength;
                    break;
                case "west":     // facing west
                    fieldOfView.X = (int)guardPosition.X - fieldOfViewLongSideLength;
                    fieldOfView.Y = (int)guardPosition.Y - fieldOfViewShortSideLength/2;
                    fieldOfView.Width = fieldOfViewLongSideLength;
                    fieldOfView.Height = fieldOfViewShortSideLength;
                    break;
            }
        }


        public void Update(Tilemap _tileMap)
        {
            PlayerDetection(_tileMap);
            Movement();
            DirectionFacing();
        }
           

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Texture
            spriteBatch.Draw(guardTexture, guardPosition, Color.White);
        }
    }
}

