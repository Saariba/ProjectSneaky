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
        private List<Item> museumRoomItems = new List<Item>();

        private List<Door> museumRoomDoors = new List<Door>();

        private List<Guards> museumRoomGuards = new List<Guards>();

        //private Vector2 museumRoomPlayerStart;

        // Getters
        public List<Item> getMuseumRoomItems() { return museumRoomItems; }
        
        public List<Door> getMuseumRoomDoors() { return museumRoomDoors; }

        public List<Guards> getMuseumRoomGuards() { return museumRoomGuards; }

        //public Vector2 getMuseumRoomPlayerStart() { return museumRoomPlayerStart; }
        
        // Setters
        //public void setMuseumRoomPlayerStart(Vector2 _playerStart) { museumRoomPlayerStart = _playerStart; }

        // Constructors
            // Contructor empty room
        public MuseumRoom (Texture2D _BitMap) : base(_BitMap) { }

            // Contructor 2 Doors
        public MuseumRoom (Texture2D _BitMap, GameStuff.GameStates door0destinationGamestate, GameStuff.GameStates door1destinationGamestate) : base(_BitMap)
        {
            //Doors
            museumRoomItems.Add(new Door(GameStuff.Instance.defaultDoorTextures[tileMap.getDoorRotations()[0]], tileMap.getDoorPositions()[0], door0destinationGamestate));
            museumRoomItems.Add(new Door(GameStuff.Instance.defaultDoorTextures[tileMap.getDoorRotations()[1]], tileMap.getDoorPositions()[1], door1destinationGamestate));

            foreach(Door door in museumRoomItems)
            {
                museumRoomDoors.Add(door);
            }

            GameStuff.Instance.museumRoomLevels.Add(this);            
        }

        public void addGuard(Guards _guard) { museumRoomGuards.Add(_guard); }

        public void addItem(Item _item) { museumRoomItems.Add(_item); }

        public override void Update()
        {
                // Update all museumRoomItems
            for(int i = 0; i < museumRoomItems.Count(); i++)
            {
                museumRoomItems[i].Update();
            }

                // Update all museumRoomGuards
            for (int i = 0; i < museumRoomGuards.Count(); i++)
            {
                museumRoomGuards[i].Update(tileMap);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

                // Draw all museumRoomItems
            for (int i = 0; i < museumRoomItems.Count(); i++)
            {
                museumRoomItems[i].Draw(spriteBatch);
            }
                
                // Draw all museumRoomGuards
            for (int i = 0; i < museumRoomGuards.Count(); i++)
            {
                museumRoomGuards[i].Draw(spriteBatch);
            }
        }
    }
}
