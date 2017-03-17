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
        private Vector2 guardPosition;
        private Vector2 guardStartPosition;
        float speed;
        Vector2 move;

        Vector2 targetPos1;
        Vector2 targetPos2;
        Vector2 currentTarget;

        String directionFacing;        // north,east,south,west
        Rectangle fieldOfView;
        int fieldOfViewLongSideLength;
        int fieldOfViewShortSideLength;

        private int[,] lightFieldCurr;
        private int[,] lightFieldNorth;
        private int[,] lightFieldEast;
        private int[,] lightFieldSouth;
        private int[,] lightFieldWest;

        Vector2 guardToPLayer;
        float guardToPLayerLength;
        Vector2 temp;

        Player player;
        public bool playerDetected;

        // Getters
        public Vector2 getGuardPosition() { return guardPosition; }

        public Vector2 getGuardStartPosition() { return guardStartPosition; }

        public Rectangle getFieldOfView() { return fieldOfView; }

        public int[,] getLightField() { return lightFieldCurr; }

        public String getDirectionFacing() { return directionFacing; }

        // Setters
        public void setGuardPosition(Vector2 _GuardPosition) { guardPosition = _GuardPosition; }

        public void setPLayerDetected(bool _PlayerDetected) { playerDetected = _PlayerDetected; }

        
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

            directionFacing = _facingDirection;


            // fieldOfView is a Rectangle starting 
            // at the Guard and going out in the direction he is facing.

            fieldOfViewLongSideLength = 33 * 8;

            fieldOfViewShortSideLength = 17 * 8;

            // Initializin fieldOfView Rectangle
            switch (directionFacing)
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

            // Initalizing lightField int Array for each direction
                // lightFieldNorth
            lightFieldNorth = new int[GameStuff.Instance.guardLight[0].Width, GameStuff.Instance.guardLight[0].Height];
            Color[] colorsGuardLight = new Color[lightFieldNorth.GetLength(0) * lightFieldNorth.GetLength(1)];
            GameStuff.Instance.guardLight[0].GetData(colorsGuardLight);

            for(int y = 0; y < lightFieldNorth.GetLength(1); y++)
            {
                for (int x = 0; x < lightFieldNorth.GetLength(0); x++)
                {
                    if (colorsGuardLight[y * lightFieldNorth.GetLength(0) + x] == Color.Yellow)
                    {
                        lightFieldNorth[x, y] = 4;
                    }
                    else if (colorsGuardLight[y * lightFieldNorth.GetLength(0) + x] == Color.Red)
                    {
                        lightFieldNorth[x, y] = 3;
                    }
                    else if (colorsGuardLight[y * lightFieldNorth.GetLength(0) + x] == Color.Green)
                    {
                        lightFieldNorth[x, y] = 2;
                    }
                    else if (colorsGuardLight[y * lightFieldNorth.GetLength(0) + x] == Color.Blue)
                    {
                        lightFieldNorth[x, y] = 1;
                    }
                    else
                    {
                        lightFieldNorth[x, y] = 0;
                    }
                }
            }

                // lightFieldEast
            lightFieldEast = new int[GameStuff.Instance.guardLight[1].Width, GameStuff.Instance.guardLight[1].Height];
            colorsGuardLight = new Color[lightFieldEast.GetLength(0) * lightFieldEast.GetLength(1)];
            GameStuff.Instance.guardLight[1].GetData(colorsGuardLight);

            for (int y = 0; y < lightFieldEast.GetLength(1); y++)
            {
                for (int x = 0; x < lightFieldEast.GetLength(0); x++)
                {
                    if (colorsGuardLight[y * lightFieldEast.GetLength(0) + x] == Color.Yellow)
                    {
                        lightFieldEast[x, y] = 4;
                    }
                    else if (colorsGuardLight[y * lightFieldEast.GetLength(0) + x] == Color.Red)
                    {
                        lightFieldEast[x, y] = 3;
                    }
                    else if (colorsGuardLight[y * lightFieldEast.GetLength(0) + x] == Color.Green)
                    {
                        lightFieldEast[x, y] = 2;
                    }
                    else if (colorsGuardLight[y * lightFieldEast.GetLength(0) + x] == Color.Blue)
                    {
                        lightFieldEast[x, y] = 1;
                    }
                    else
                    {
                        lightFieldEast[x, y] = 0;
                    }
                }
            }

                // lightFieldSouth
            lightFieldSouth = new int[GameStuff.Instance.guardLight[2].Width, GameStuff.Instance.guardLight[2].Height];
            colorsGuardLight = new Color[lightFieldSouth.GetLength(0) * lightFieldSouth.GetLength(1)];
            GameStuff.Instance.guardLight[2].GetData(colorsGuardLight);

            for (int y = 0; y < lightFieldSouth.GetLength(1); y++)
            {
                for (int x = 0; x < lightFieldSouth.GetLength(0); x++)
                {
                    if (colorsGuardLight[y * lightFieldSouth.GetLength(0) + x] == Color.Yellow)
                    {
                        lightFieldSouth[x, y] = 4;
                    }
                    else if (colorsGuardLight[y * lightFieldSouth.GetLength(0) + x] == Color.Red)
                    {
                        lightFieldSouth[x, y] = 3;
                    }
                    else if (colorsGuardLight[y * lightFieldSouth.GetLength(0) + x] == Color.Green)
                    {
                        lightFieldSouth[x, y] = 2;
                    }
                    else if (colorsGuardLight[y * lightFieldSouth.GetLength(0) + x] == Color.Blue)
                    {
                        lightFieldSouth[x, y] = 1;
                    }
                    else
                    {
                        lightFieldSouth[x, y] = 0;
                    }
                }
            }

                // lightFieldWest
            lightFieldWest = new int[GameStuff.Instance.guardLight[3].Width, GameStuff.Instance.guardLight[3].Height];
            colorsGuardLight = new Color[lightFieldWest.GetLength(0) * lightFieldWest.GetLength(1)];
            GameStuff.Instance.guardLight[3].GetData(colorsGuardLight);

            for (int y = 0; y < lightFieldWest.GetLength(1); y++)
            {
                for (int x = 0; x < lightFieldWest.GetLength(0); x++)
                {
                    if (colorsGuardLight[y * lightFieldWest.GetLength(0) + x] == Color.Yellow)
                    {
                        lightFieldWest[x, y] = 4;
                    }
                    else if (colorsGuardLight[y * lightFieldWest.GetLength(0) + x] == Color.Red)
                    {
                        lightFieldWest[x, y] = 3;
                    }
                    else if (colorsGuardLight[y * lightFieldWest.GetLength(0) + x] == Color.Green)
                    {
                        lightFieldWest[x, y] = 2;
                    }
                    else if (colorsGuardLight[y * lightFieldWest.GetLength(0) + x] == Color.Blue)
                    {
                        lightFieldWest[x, y] = 1;
                    }
                    else
                    {
                        lightFieldWest[x, y] = 0;
                    }
                }
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
                directionFacing = "north";
            else if (move.X > 0)            // moving east
                directionFacing = "east";
            else if (move.Y > 0)            // moving south
                directionFacing = "south";
            else if (move.X < 0)            // moving west
                directionFacing = "west";

            // fieldOfView is a 600x1000/1000x600 Rectangle starting 
            // at the Guard and going out in the direction he is facing.

            switch (directionFacing)
            {
                case "north":     // facing north
                    fieldOfView.X = (int)guardPosition.X - fieldOfViewShortSideLength/2;
                    fieldOfView.Y = (int)guardPosition.Y - fieldOfViewLongSideLength;
                    fieldOfView.Width = fieldOfViewShortSideLength;
                    fieldOfView.Height = fieldOfViewLongSideLength;

                    lightFieldCurr = lightFieldNorth;
                    break;

                case "east":     // facing east
                    fieldOfView.X = (int)guardPosition.X;
                    fieldOfView.Y = (int)guardPosition.Y - fieldOfViewShortSideLength/2;
                    fieldOfView.Width = fieldOfViewLongSideLength;
                    fieldOfView.Height = fieldOfViewShortSideLength;

                    lightFieldCurr = lightFieldEast;
                    break;

                case "south":     // facing south
                    fieldOfView.X = (int)guardPosition.X - fieldOfViewShortSideLength/2;
                    fieldOfView.Y = (int)guardPosition.Y;
                    fieldOfView.Width = fieldOfViewShortSideLength;
                    fieldOfView.Height = fieldOfViewLongSideLength;

                    lightFieldCurr = lightFieldSouth;
                    break;

                case "west":     // facing west
                    fieldOfView.X = (int)guardPosition.X - fieldOfViewLongSideLength;
                    fieldOfView.Y = (int)guardPosition.Y - fieldOfViewShortSideLength/2;
                    fieldOfView.Width = fieldOfViewLongSideLength;
                    fieldOfView.Height = fieldOfViewShortSideLength;

                    lightFieldCurr = lightFieldWest;
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
            spriteBatch.Draw(guardTexture,
                new Vector2(guardPosition.X - guardTexture.Width/2, guardPosition.Y - guardTexture.Height/2), Color.White);
        }
    }
}

