using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectSneaky.Items
{
    class Door : Item
    {
        private GameStuff.GameStates destinationGamestate;
        
        private Door destinationDoor;

        private string doorLinkSide;


        // Getters
        public GameStuff.GameStates getDestinationGamestate() { return destinationGamestate; }

        public Door getDestinationDoor() { return destinationDoor; }

        public string getDoorLinkSide() { return doorLinkSide; }


        // Constructors
        public Door (Texture2D _texture, Vector2 _position, GameStuff.GameStates _destinationGameState) : base(_texture, _position)
        {
            destinationGamestate = _destinationGameState;
            collidingWithPlayer = false;
        }

        //links Door to the destination Door, needs to be done in game initialization for every door pairs manualy
        public void LinkNorthDoor(Door toTheSouth)
        {
            this.destinationDoor = toTheSouth;
            this.doorLinkSide = "north";

            toTheSouth.destinationDoor = this;
            toTheSouth.doorLinkSide = "south";
        }

        public void LinkEastDoor(Door toTheWest)
        {
            this.destinationDoor = toTheWest;
            this.doorLinkSide = "east";

            toTheWest.destinationDoor = this;
            toTheWest.doorLinkSide = "west";
        }

        public void LinkSouthDoor(Door toTheNorth)
        {
            this.destinationDoor = toTheNorth;
            this.doorLinkSide = "south";

            toTheNorth.destinationDoor = this;
            toTheNorth.doorLinkSide = "north";
        }
        public void LinkWestDoor(Door toTheEast)
        {
            this.destinationDoor = toTheEast;
            this.doorLinkSide = "west";

            toTheEast.destinationDoor = this;
            toTheEast.doorLinkSide = "east";
        }
        
        //Used to transition room through doors
        public void RoomTransition()
        {
            GameStuff.Instance.gameStatePrev = GameStuff.Instance.gameStateCurr;
            GameStuff.Instance.gameStateCurr = destinationGamestate;

            switch (doorLinkSide)
            {
                case "north":
                    GameStuff.Instance.player.playerPosition = destinationDoor.position + 
                        new Vector2(0, (GameStuff.Instance.player.playerTexture.Height/2 + this.texture.Height/2 + 1));
                    break;
                case "east":
                    GameStuff.Instance.player.playerPosition = destinationDoor.position + 
                        new Vector2(- (GameStuff.Instance.player.playerTexture.Width/2 + this.texture.Width/2 + 1), 0);
                    break;
                case "south":
                    GameStuff.Instance.player.playerPosition = destinationDoor.position + 
                        new Vector2(0, - (GameStuff.Instance.player.playerTexture.Height/2 + this.texture.Height/2 + 1));
                    break;
                case "west":
                    GameStuff.Instance.player.playerPosition = destinationDoor.position + 
                        new Vector2((GameStuff.Instance.player.playerTexture.Width/2 + this.texture.Width/2 + 1), 0);
                    break;

            }

            GameStuff.Instance.player.setCurrStartPosition(
                                GameStuff.Instance.player.playerPosition);
        }

        protected override void OnPlayerCollision()
        {
            base.OnPlayerCollision();
            
        }

    }
}
