using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSneaky.Lighting
{
    class BasicLighting4Shades : Lighting
    {
        private Texture2D[] shades;

        private int[,] lightingLevels;

        private int lightingTileSize;

        private int[,] tempGuardLight;
        private Vector2 tempGuardPosition;
        private Rectangle tempGuardFieldOfView;
        private String tempGuardDirectionFacing;

        // Constructor
        public BasicLighting4Shades(Tilemap _tileMap, List<Guards> _levelGuards, Texture2D[] _shades) : base(_tileMap, _levelGuards)
        {
            shades = _shades;
            lightingLevels = new int[_tileMap.tileMap.GetLength(0) * 2, _tileMap.tileMap.GetLength(1) * 2];            
            lightingTileSize = _tileMap.tileSize / 2;
        }

        private void determineLightingLevels()
        {
            // filling array with 0s
            for (int y = 0; y < lightingLevels.GetLength(1); y++)
            {
                for (int x = 0; x < lightingLevels.GetLength(0); x++)
                {
                    lightingLevels[x, y] = 0;
                }
            }

            foreach (Guards guard in levelGuards)
            {
                tempGuardLight = guard.getLightField();
                tempGuardPosition = guard.getGuardPosition();
                tempGuardFieldOfView = guard.getFieldOfView();
                tempGuardDirectionFacing = guard.getDirectionFacing();


                switch (tempGuardDirectionFacing)
                {
                    case "north":

                        for (int y = 0; y < tempGuardLight.GetLength(1); y++)
                        {
                            for (int x = 0; x < tempGuardLight.GetLength(0); x++)
                            {

                                if ((int)(tempGuardPosition.X / lightingTileSize) + x - (int)(tempGuardFieldOfView.Width / 2 / lightingTileSize) >= 0 &&
                                    (int)(tempGuardPosition.X / lightingTileSize) + x - (int)(tempGuardFieldOfView.Width / 2 / lightingTileSize) 
                                    < lightingLevels.GetLength(0) &&
                                    (int)(tempGuardPosition.Y / lightingTileSize) + y - (int)(tempGuardFieldOfView.Height / lightingTileSize) >= 0 &&
                                    (int)(tempGuardPosition.Y / lightingTileSize) + y - (int)(tempGuardFieldOfView.Height / lightingTileSize)
                                    < lightingLevels.GetLength(1) &&
                                    lightingLevels[(int)(tempGuardPosition.X / lightingTileSize) + x - (int)(tempGuardFieldOfView.Width / 2 / lightingTileSize),
                                    (int)(tempGuardPosition.Y / lightingTileSize) + y - (int)(tempGuardFieldOfView.Height / lightingTileSize)]
                                    < tempGuardLight[x, y])
                                {
                                    lightingLevels[(int)(tempGuardPosition.X / lightingTileSize) + x - (int)(tempGuardFieldOfView.Width / 2 / lightingTileSize),
                                    (int)(tempGuardPosition.Y / lightingTileSize) + y - (int)(tempGuardFieldOfView.Height / lightingTileSize)]
                                    = tempGuardLight[x, y];
                                }
                            }
                        }

                        break;

                    case "east":

                        for (int y = 0; y < tempGuardLight.GetLength(1); y++)
                        {
                            for (int x = 0; x < tempGuardLight.GetLength(0); x++)
                            {

                                if (((int)tempGuardPosition.X / lightingTileSize) + x >= 0 &&
                                    ((int)tempGuardPosition.X / lightingTileSize) + x < lightingLevels.GetLength(0) &&
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize) >= 0 &&
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize)
                                    < lightingLevels.GetLength(1) &&
                                    lightingLevels[((int)tempGuardPosition.X / lightingTileSize) + x,
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize)]
                                    < tempGuardLight[x, y])
                                {
                                    lightingLevels[((int)tempGuardPosition.X / lightingTileSize) + x,
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize)]
                                    = tempGuardLight[x, y];
                                }
                            }
                        }

                        break;

                    case "south":

                        for (int y = 0; y < tempGuardLight.GetLength(1); y++)
                        {
                            for (int x = 0; x < tempGuardLight.GetLength(0); x++)
                            {

                                if (((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / 2 / lightingTileSize) >= 0 &&
                                    ((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / 2 / lightingTileSize)
                                    < lightingLevels.GetLength(0) &&
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y >= 0 &&
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y < lightingLevels.GetLength(1) &&
                                    lightingLevels[((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / 2 / lightingTileSize),
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y]
                                    < tempGuardLight[x, y])
                                {
                                    lightingLevels[((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / 2 / lightingTileSize),
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y]
                                    = tempGuardLight[x, y];
                                }
                            }
                        }

                        break;

                    case "west":

                        for (int y = 0; y < tempGuardLight.GetLength(1); y++)
                        {
                            for (int x = 0; x < tempGuardLight.GetLength(0); x++)
                            {

                                if (((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / lightingTileSize) >= 0 &&
                                    ((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / lightingTileSize)
                                    < lightingLevels.GetLength(0) &&
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize) >= 0 &&
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize)
                                    < lightingLevels.GetLength(1) &&
                                    lightingLevels[((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / lightingTileSize),
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize)]
                                    < tempGuardLight[x, y])
                                {
                                    lightingLevels[((int)tempGuardPosition.X / lightingTileSize) + x - ((int)tempGuardFieldOfView.Width / lightingTileSize),
                                    ((int)tempGuardPosition.Y / lightingTileSize) + y - ((int)tempGuardFieldOfView.Height / 2 / lightingTileSize)]
                                    = tempGuardLight[x, y];
                                }
                            }
                        }

                        break;
                }
            }
        }

        public override void Update()
        {
            base.Update();

            determineLightingLevels();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int y = 0; y < lightingLevels.GetLength(1); y++)
            {
                for (int x = 0; x < lightingLevels.GetLength(0); x++)
                {
                    switch(lightingLevels[x, y])
                    {
                        case 4: // FullyLite
                            break;
                        case 3: // Darkness1
                            spriteBatch.Draw(shades[3],new Rectangle(x * lightingTileSize, y * lightingTileSize, lightingTileSize, lightingTileSize),Color.White);                                
                            break;
                        case 2: // Darkness2
                            spriteBatch.Draw(shades[2], new Rectangle(x * lightingTileSize, y * lightingTileSize, lightingTileSize, lightingTileSize), Color.White);
                            break;
                        case 1: // Darkness3
                            spriteBatch.Draw(shades[1], new Rectangle(x * lightingTileSize, y * lightingTileSize, lightingTileSize, lightingTileSize), Color.White);
                            break;
                        case 0: // FullDarkness
                            spriteBatch.Draw(shades[0], new Rectangle(x * lightingTileSize, y * lightingTileSize, lightingTileSize, lightingTileSize), Color.White);
                            break;
                    }
                }
            }
        }
    }
}
