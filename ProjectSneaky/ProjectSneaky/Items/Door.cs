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

        private bool playerCollision;

        private Door destinationDoor;

        private string doorLinkSide;

        // Getters
        public GameStuff.GameStates getDestinationGamestate() { return destinationGamestate; }

        public bool getPlayerCollision() { return playerCollision; }        

        public Door getDestinationDoor() { return destinationDoor; }

        public string getDoorLinkSide() { return doorLinkSide; }

        // Setters
        public void setPlayerCollision(bool _playerCollision) { playerCollision = _playerCollision; }

        // Constructors
        public Door (Texture2D _texture, Vector2 _position, GameStuff.GameStates _destinationGameState) : base(_texture, _position)
        {
            destinationGamestate = _destinationGameState;
            playerCollision = false;
        }

        //links Door to the destination Door, needs to be done in game initialization for every door pairs manualy
        public void LinkVerticalDoors(Door westDoor, Door eastDoor)
        {
            eastDoor.destinationDoor = westDoor;
            eastDoor.doorLinkSide = "east";

            westDoor.destinationDoor = eastDoor;
            westDoor.doorLinkSide = "west";
        }

        public void LinkHorizontalDoors(Door northDoor, Door southDoor)
        {
            northDoor.destinationDoor = southDoor;
            northDoor.doorLinkSide = "north";

            southDoor.destinationDoor = northDoor;
            southDoor.doorLinkSide = "south";
        }

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
        }

        protected override void OnPlayerCollision()
        {
            if (!playerCollision)
            {
                playerCollision = true;
            }
        }

    }
}
