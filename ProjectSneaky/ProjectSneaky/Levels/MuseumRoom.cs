using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectSneaky.Items;

namespace ProjectSneaky.Levels
{
    class MuseumRoom : Level
    {        
        // Constructors
            // Contructor empty room
        public MuseumRoom (Texture2D _BitMap) : base(_BitMap) { }

            // Contructor 2 Doors
        public MuseumRoom (Texture2D _BitMap, GameStuff.GameStates door0destinationGamestate, GameStuff.GameStates door1destinationGamestate) : base(_BitMap)
        {
            //Doors
            levelItems.Add(new Door(GameStuff.Instance.defaultDoorTextures[tileMap.getDoorRotations()[0]], tileMap.getDoorPositions()[0], door0destinationGamestate));
            levelItems.Add(new Door(GameStuff.Instance.defaultDoorTextures[tileMap.getDoorRotations()[1]], tileMap.getDoorPositions()[1], door1destinationGamestate));

            foreach(Door door in levelItems)
            {
                levelDoors.Add(door);
            }

            GameStuff.Instance.museumRoomLevels.Add(this);            
        }
        
        public override void Update()
        {
            base.Update();
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
                        
        }
    }
}
