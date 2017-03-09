using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectSneaky.Items;
using ProjectSneaky.Levels;

namespace ProjectSneaky
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Class Member Variable

        KeyboardState keyboardState;
        KeyboardState keyboardStatePrev;
                
        Color backgroudPopup; 

        Texture2D startScreen;
        Texture2D winScreen; 
        Texture2D defeatScreen;        
        Vector2 screenPos;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameStuff.Instance.camera = new Camera(graphics.GraphicsDevice.Viewport);            

            GameStuff.Instance.player = new Player(Content.Load<Texture2D>("Player/Player"), new Vector2(15 * 16, 7 * 16), 5);
            
            //LevelTextures
                //Default
            GameStuff.Instance.defaultLevelTextures = new Texture2D[] { Content.Load<Texture2D>("Wall"), Content.Load<Texture2D>("Floor") };
            GameStuff.Instance.defaultDoorTextures = new Texture2D[] { Content.Load<Texture2D>("Items/DoorPlaceholderHorizontal"),
                Content.Load<Texture2D>("Items/DoorPlaceholderVertical") };

            // MuseumTest
            
                // Levels
            GameStuff.Instance.museumEntryLevel = new MuseumRoom(Content.Load<Texture2D>("BitMaps/MuseumTestEntry"), 
                GameStuff.GameStates.museumLeftRoomLowerDoorStart, GameStuff.GameStates.museumRightRoomLowerDoorStart);
            GameStuff.Instance.museumLeftRoomLevel = new MuseumRoom(Content.Load<Texture2D>("BitMaps/MuseumTestLeftRoom"),
                GameStuff.GameStates.museumTopRoomLeftDoorStart, GameStuff.GameStates.museumEntryLeftDoorStart);
            GameStuff.Instance.museumRightRoomLevel = new MuseumRoom(Content.Load<Texture2D>("BitMaps/MuseumTestRightRoom"),
                GameStuff.GameStates.museumTopRoomRightDoorStart, GameStuff.GameStates.museumEntryRightDoorStart);
            GameStuff.Instance.museumTopRoomLevel = new MuseumRoom(Content.Load<Texture2D>("BitMaps/MuseumTestTopRoom"),
                GameStuff.GameStates.museumLeftRoomUpperDoorStart, GameStuff.GameStates.museumRightRoomUpperDoorStart);

            // Door Linking
            GameStuff.Instance.museumEntryLevel.getLevelDoors()[0].LinkEastDoor(GameStuff.Instance.museumLeftRoomLevel.getLevelDoors()[1]);
            GameStuff.Instance.museumLeftRoomLevel.getLevelDoors()[0].LinkWestDoor(GameStuff.Instance.museumTopRoomLevel.getLevelDoors()[0]);
            GameStuff.Instance.museumTopRoomLevel.getLevelDoors()[1].LinkWestDoor(GameStuff.Instance.museumRightRoomLevel.getLevelDoors()[0]);
            GameStuff.Instance.museumRightRoomLevel.getLevelDoors()[1].LinkEastDoor(GameStuff.Instance.museumEntryLevel.getLevelDoors()[1]);

            // Adding Guards to Levels
            GameStuff.Instance.museumEntryLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumEntryLevel.getTileMap().getVariousPositions()[0],
                GameStuff.Instance.museumEntryLevel.getTileMap().getVariousPositions()[0], GameStuff.Instance.museumEntryLevel.getTileMap().getVariousPositions()[1],
                3, "east"));
            GameStuff.Instance.museumEntryLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumEntryLevel.getTileMap().getVariousPositions()[3],
                GameStuff.Instance.museumEntryLevel.getTileMap().getVariousPositions()[3], GameStuff.Instance.museumEntryLevel.getTileMap().getVariousPositions()[2],
                3, "west"));

            GameStuff.Instance.museumLeftRoomLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumLeftRoomLevel.getTileMap().getVariousPositions()[0],
                GameStuff.Instance.museumLeftRoomLevel.getTileMap().getVariousPositions()[0], GameStuff.Instance.museumLeftRoomLevel.getTileMap().getVariousPositions()[2],
                3, "south"));
            GameStuff.Instance.museumLeftRoomLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumLeftRoomLevel.getTileMap().getVariousPositions()[3],
                GameStuff.Instance.museumLeftRoomLevel.getTileMap().getVariousPositions()[3], GameStuff.Instance.museumLeftRoomLevel.getTileMap().getVariousPositions()[1],
                3, "north"));

            GameStuff.Instance.museumRightRoomLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumRightRoomLevel.getTileMap().getVariousPositions()[0],
                GameStuff.Instance.museumRightRoomLevel.getTileMap().getVariousPositions()[0], GameStuff.Instance.museumRightRoomLevel.getTileMap().getVariousPositions()[2],
                3, "south"));
            GameStuff.Instance.museumRightRoomLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumRightRoomLevel.getTileMap().getVariousPositions()[3],
                GameStuff.Instance.museumRightRoomLevel.getTileMap().getVariousPositions()[3], GameStuff.Instance.museumRightRoomLevel.getTileMap().getVariousPositions()[1],
                3, "north"));

            GameStuff.Instance.museumTopRoomLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumTopRoomLevel.getTileMap().getVariousPositions()[0],
                GameStuff.Instance.museumTopRoomLevel.getTileMap().getVariousPositions()[0], GameStuff.Instance.museumTopRoomLevel.getTileMap().getVariousPositions()[1],
                3, "east"));
            GameStuff.Instance.museumTopRoomLevel.addGuard(
                new Guards(Content.Load<Texture2D>("Guards/Guard"), GameStuff.Instance.museumTopRoomLevel.getTileMap().getVariousPositions()[3],
                GameStuff.Instance.museumTopRoomLevel.getTileMap().getVariousPositions()[3], GameStuff.Instance.museumTopRoomLevel.getTileMap().getVariousPositions()[2],
                3, "west"));

                // Adding Goal to Level
            GameStuff.Instance.museumTopRoomLevel.addItem(new Goal(Content.Load<Texture2D>("Items/DollarBill"),
                GameStuff.Instance.museumTopRoomLevel.getTileMap().getVariousPositions()[4]));


            GameStuff.Instance.StageWon = false;

            GameStuff.Instance.gameStateCurr = GameStuff.GameStates.stage1Start;            

            
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startScreen = Content.Load<Texture2D>("StartScreen");
            defeatScreen = Content.Load<Texture2D>("Caught");
            winScreen = Content.Load<Texture2D>("EndScreen");                     
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();



            
            
            //Updates

            //Updates GameStates
            switch (GameStuff.Instance.gameStateCurr)
            {
                case GameStuff.GameStates.stage1Start:

                    screenPos.X = GameStuff.Instance.player.playerPosition.X - startScreen.Width / 2;
                    screenPos.Y = GameStuff.Instance.player.playerPosition.Y - startScreen.Height / 2;
                    backgroudPopup = Color.Yellow;
                    break;

                case GameStuff.GameStates.stage1Lost:

                    screenPos.X = GameStuff.Instance.player.playerPosition.X - defeatScreen.Width / 2;
                    screenPos.Y = GameStuff.Instance.player.playerPosition.Y - defeatScreen.Height / 2;
                    backgroudPopup = Color.White;
                    break;

                case GameStuff.GameStates.stage1Won:

                    screenPos.X = GameStuff.Instance.player.playerPosition.X - winScreen.Width / 2;
                    screenPos.Y = GameStuff.Instance.player.playerPosition.Y - winScreen.Height / 2;
                    backgroudPopup = Color.White;
                    break;

                case GameStuff.GameStates.museumEntryLeftDoorStart:
                case GameStuff.GameStates.museumEntryRightDoorStart:
                case GameStuff.GameStates.museumEntryStandardStart:

                    GameStuff.Instance.player.Update(GameStuff.Instance.museumEntryLevel.getTileMap());
                    GameStuff.Instance.museumEntryLevel.Update();
                    break;

                case GameStuff.GameStates.museumLeftRoomLowerDoorStart:
                case GameStuff.GameStates.museumLeftRoomUpperDoorStart:

                    GameStuff.Instance.player.Update(GameStuff.Instance.museumLeftRoomLevel.getTileMap());
                    GameStuff.Instance.museumLeftRoomLevel.Update();
                    break;

                case GameStuff.GameStates.museumRightRoomLowerDoorStart:
                case GameStuff.GameStates.museumRightRoomUpperDoorStart:

                    GameStuff.Instance.player.Update(GameStuff.Instance.museumRightRoomLevel.getTileMap());
                    GameStuff.Instance.museumRightRoomLevel.Update();
                    break;

                case GameStuff.GameStates.museumTopRoomLeftDoorStart:
                case GameStuff.GameStates.museumTopRoomRightDoorStart:

                    GameStuff.Instance.player.Update(GameStuff.Instance.museumTopRoomLevel.getTileMap());
                    GameStuff.Instance.museumTopRoomLevel.Update();
                    break;

            }
            
            // GameStates Transitions

            switch (GameStuff.Instance.gameStateCurr)
            {
                case GameStuff.GameStates.stage1Start:

                        // Start
                    if (keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrev.IsKeyDown(Keys.Space))
                    {                        
                        GameStuff.Instance.gameStateCurr = GameStuff.GameStates.museumEntryStandardStart;
                        GameStuff.Instance.gameStatePrev = GameStuff.GameStates.stage1Start;                        
                    }
                        break;

                case GameStuff.GameStates.stage1Lost:

                        // Restart Room
                    if (keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrev.IsKeyDown(Keys.Space))
                    {
                        GameStuff.Instance.gameStateCurr = GameStuff.Instance.gameStatePrev;
                        GameStuff.Instance.gameStatePrev = GameStuff.GameStates.stage1Lost;

                            // Resetting Positions
                        GameStuff.Instance.player.playerPosition = GameStuff.Instance.player.getCurrStartPosition();

                        foreach(MuseumRoom museumRoom in GameStuff.Instance.museumRoomLevels)
                        {
                            foreach(Guards guard in museumRoom.getLevelGuards())
                            {
                                guard.guardPosition = guard.getGuardStartPosition();
                            }
                        }
                    }
                    break;

                case GameStuff.GameStates.stage1Won:

                        // Restart Stage
                    if (keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrev.IsKeyDown(Keys.Space))
                    {
                        GameStuff.Instance.gameStateCurr = GameStuff.GameStates.stage1Start;
                        GameStuff.Instance.gameStatePrev = GameStuff.GameStates.stage1Won;

                            // Resetting Positions
                        GameStuff.Instance.player.playerPosition = GameStuff.Instance.player.getStageStartPosition();

                        foreach (MuseumRoom museumRoom in GameStuff.Instance.museumRoomLevels)
                        {
                            foreach (Guards guard in museumRoom.getLevelGuards())
                            {
                                guard.guardPosition = guard.getGuardStartPosition();
                            }
                        }

                        GameStuff.Instance.StageWon = false;
                    }
                    break;

                case GameStuff.GameStates.museumEntryLeftDoorStart:
                case GameStuff.GameStates.museumEntryRightDoorStart:
                case GameStuff.GameStates.museumEntryStandardStart:

                        // Transition through Door
                    foreach (Door door in GameStuff.Instance.museumEntryLevel.getLevelDoors())
                    {
                        if (door.getPlayerCollision())
                        {
                            door.RoomTransition();                            
                            door.setPlayerCollision(false);
                        }
                    }

                        // Transition throug getting caught
                    foreach(Guards guard in GameStuff.Instance.museumEntryLevel.getLevelGuards())
                    {
                        if (guard.playerDetected)
                        {
                            GameStuff.Instance.gameStatePrev = GameStuff.Instance.gameStateCurr;
                            GameStuff.Instance.gameStateCurr = GameStuff.GameStates.stage1Lost;
                            guard.playerDetected = false;
                        }
                    }
                    break;

                case GameStuff.GameStates.museumLeftRoomLowerDoorStart:
                case GameStuff.GameStates.museumLeftRoomUpperDoorStart:

                        //Transition through Door
                    foreach (Door door in GameStuff.Instance.museumLeftRoomLevel.getLevelDoors())
                    {
                        if (door.getPlayerCollision())
                        {
                            door.RoomTransition();
                            door.setPlayerCollision(false);
                        }
                    }

                        // Transition throug getting caught
                    foreach (Guards guard in GameStuff.Instance.museumLeftRoomLevel.getLevelGuards())
                    {
                        if (guard.playerDetected)
                        {
                            GameStuff.Instance.gameStatePrev = GameStuff.Instance.gameStateCurr;
                            GameStuff.Instance.gameStateCurr = GameStuff.GameStates.stage1Lost;
                            guard.playerDetected = false;
                        }
                    }
                    break;

                case GameStuff.GameStates.museumRightRoomLowerDoorStart:
                case GameStuff.GameStates.museumRightRoomUpperDoorStart:

                        //Transition through Door
                    foreach (Door door in GameStuff.Instance.museumRightRoomLevel.getLevelDoors())
                    {
                        if (door.getPlayerCollision())
                        {
                            door.RoomTransition();
                            door.setPlayerCollision(false);
                        }
                    }

                        // Transition throug getting caught
                    foreach (Guards guard in GameStuff.Instance.museumRightRoomLevel.getLevelGuards())
                    {
                        if (guard.playerDetected)
                        {
                            GameStuff.Instance.gameStatePrev = GameStuff.Instance.gameStateCurr;
                            GameStuff.Instance.gameStateCurr = GameStuff.GameStates.stage1Lost;
                            guard.playerDetected = false;
                        }
                    }
                    break;

                case GameStuff.GameStates.museumTopRoomLeftDoorStart:
                case GameStuff.GameStates.museumTopRoomRightDoorStart:

                        //Transition through Door
                    foreach (Door door in GameStuff.Instance.museumTopRoomLevel.getLevelDoors())
                    {
                        if (door.getPlayerCollision())
                        {
                            door.RoomTransition();
                            door.setPlayerCollision(false);
                        }
                    }

                        // Transition throug getting caught
                    foreach (Guards guard in GameStuff.Instance.museumTopRoomLevel.getLevelGuards())
                    {
                        if (guard.playerDetected)
                        {
                            GameStuff.Instance.gameStatePrev = GameStuff.Instance.gameStateCurr;
                            GameStuff.Instance.gameStateCurr = GameStuff.GameStates.stage1Lost;
                            guard.playerDetected = false;
                        }
                    }

                    // Transition through reaching Goal
                    if (GameStuff.Instance.StageWon)
                    {
                        GameStuff.Instance.gameStatePrev = GameStuff.Instance.gameStateCurr;
                        GameStuff.Instance.gameStateCurr = GameStuff.GameStates.stage1Won;
                    }
                    break;

            }
            

            keyboardStatePrev = keyboardState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin(transformMatrix: GameStuff.Instance.camera.GetViewMatrix());

            //Draw GameStates

            switch (GameStuff.Instance.gameStateCurr)
            {
                case GameStuff.GameStates.stage1Start:

                    spriteBatch.Draw(startScreen, screenPos, backgroudPopup);
                    break;

                case GameStuff.GameStates.stage1Lost:

                    spriteBatch.Draw(defeatScreen, screenPos, backgroudPopup);
                    break;

                case GameStuff.GameStates.stage1Won:

                    spriteBatch.Draw(winScreen, screenPos, backgroudPopup);
                    break;

                case GameStuff.GameStates.museumEntryLeftDoorStart:
                case GameStuff.GameStates.museumEntryRightDoorStart:
                case GameStuff.GameStates.museumEntryStandardStart:

                    GameStuff.Instance.museumEntryLevel.Draw(spriteBatch);
                    GameStuff.Instance.player.Draw(spriteBatch);
                    break;

                case GameStuff.GameStates.museumLeftRoomLowerDoorStart:
                case GameStuff.GameStates.museumLeftRoomUpperDoorStart:

                    GameStuff.Instance.museumLeftRoomLevel.Draw(spriteBatch);
                    GameStuff.Instance.player.Draw(spriteBatch);
                    break;

                case GameStuff.GameStates.museumRightRoomLowerDoorStart:
                case GameStuff.GameStates.museumRightRoomUpperDoorStart:

                    GameStuff.Instance.museumRightRoomLevel.Draw(spriteBatch);
                    GameStuff.Instance.player.Draw(spriteBatch);
                    break;

                case GameStuff.GameStates.museumTopRoomLeftDoorStart:
                case GameStuff.GameStates.museumTopRoomRightDoorStart:

                    GameStuff.Instance.museumTopRoomLevel.Draw(spriteBatch);
                    GameStuff.Instance.player.Draw(spriteBatch);
                    break;

            }
                              



            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
