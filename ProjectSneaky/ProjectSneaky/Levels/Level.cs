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
    abstract class Level
    {
        protected Tilemap tileMap;

        protected List<Item> levelItems = new List<Item>();

        protected List<Door> levelDoors = new List<Door>();

        protected List<Guards> levelGuards = new List<Guards>();

        // Getters
        public Tilemap getTileMap() { return tileMap; }

        public List<Item> getLevelItems() { return levelItems; }

        public List<Door> getLevelDoors() { return levelDoors; }

        public List<Guards> getLevelGuards() { return levelGuards; }

        
        // Constructor
        public Level(Texture2D _BitMap)
        {
            tileMap = new Tilemap( GameStuff.Instance.defaultLevelTextures, _BitMap, 16);
        }
        

        public void addGuard(Guards _guard) { levelGuards.Add(_guard); }

        public void addItem(Item _item) { levelItems.Add(_item); }


        public virtual void Update()
        {
            // Update all levelItems
            for (int i = 0; i < levelItems.Count(); i++)
            {
                levelItems[i].Update();
            }

            // Update all levelGuards
            for (int i = 0; i < levelGuards.Count(); i++)
            {
                levelGuards[i].Update(tileMap);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);

                // Draw all levelItems
            for (int i = 0; i < levelItems.Count(); i++)
            {
                levelItems[i].Draw(spriteBatch);
            }

            // Draw all levelGuards
            for (int i = 0; i < levelGuards.Count(); i++)
            {
                levelGuards[i].Draw(spriteBatch);
            }
        }
    }
}
