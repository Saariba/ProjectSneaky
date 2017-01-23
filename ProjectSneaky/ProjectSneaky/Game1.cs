using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        SpriteFont fontPopup;
        Color backgroudPopup;
        Rectangle screenPopup;
        Texture2D screenPopupTexture;
        string stringPopup;
        Texture2D startScreen;
        Texture2D winScreen; 
        Texture2D defeatScreen;
        Vector2 startPos;
        Vector2 screenPos;
        Vector2 endPos;

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

            GameStuff.Instance.player = new Player(Content.Load<Texture2D>("Player/Player"), new Vector2(60, 410), 5);
            GameStuff.Instance.tileMap = new Tilemap(new Texture2D[] { Content.Load<Texture2D>("Wall"), Content.Load<Texture2D>("Floor") }, Content.Load<Texture2D>("trialbitmap"), 16);

            GameStuff.Instance.levelWon = false;
            GameStuff.Instance.items.Add(new Goal(Content.Load<Texture2D>("DollarBill"), new Vector2(20, 20)));

            GameStuff.Instance.gameStateCurr = GameStuff.GameStates.Level1Start;
            screenPopup = new Rectangle((int)GameStuff.Instance.player.playerPosition.X - 300, (int)GameStuff.Instance.player.playerPosition.Y - 150, 600, 300);
            

            GameStuff.Instance.guard1 = new Guards(Content.Load<Texture2D>("Guards/guard"), new Vector2(280, 30), new Vector2(280, 310), new Vector2(280, 30), 1.5f, "east");
            GameStuff.Instance.guard2 = new Guards(Content.Load<Texture2D>("Guards/guard"), new Vector2(75, 40), new Vector2(630, 40), new Vector2(75, 40), 1.5f, "south");
            GameStuff.Instance.guard3 = new Guards(Content.Load<Texture2D>("Guards/guard"), new Vector2(745, 40), new Vector2(745, 330), new Vector2(745, 40), 1.5f, "south");

            screenPos = new Vector2(GameStuff.Instance.player.playerPosition.X, GameStuff.Instance.player.playerPosition.Y); //broken! NEED Fix!
            startPos = new Vector2(-250, 170);
            endPos = new Vector2(-300, -190);
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
            fontPopup = Content.Load<SpriteFont>("Popups/SpriteFont Popups");
            screenPopupTexture = Content.Load<Texture2D>("Popups/WhiteBox 30x30");            
            
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

            // GameStates Transitions
            if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Start)
            {
                if (keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrev.IsKeyDown(Keys.Space))
                    GameStuff.Instance.gameStateCurr = GameStuff.GameStates.Level1; GameStuff.Instance.gameStatePrev = GameStuff.GameStates.Level1Start;
            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1)
            {
                if (GameStuff.Instance.guard1.playerDetected || GameStuff.Instance.guard2.playerDetected || GameStuff.Instance.guard3.playerDetected)
                    GameStuff.Instance.gameStateCurr = GameStuff.GameStates.Level1Lost; GameStuff.Instance.gameStatePrev = GameStuff.GameStates.Level1;

                if (GameStuff.Instance.levelWon)
                    GameStuff.Instance.gameStateCurr = GameStuff.GameStates.Level1Won; GameStuff.Instance.gameStatePrev = GameStuff.GameStates.Level1;

            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Lost)
            {
                if (keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrev.IsKeyDown(Keys.Space))
                    GameStuff.Instance.gameStateCurr = GameStuff.GameStates.Level1; GameStuff.Instance.gameStatePrev = GameStuff.GameStates.Level1Lost;
            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Won)
            {
                if (keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrev.IsKeyDown(Keys.Space))
                    GameStuff.Instance.gameStateCurr = GameStuff.GameStates.Level1; GameStuff.Instance.gameStatePrev = GameStuff.GameStates.Level1Won;
            }

            //Updates
            screenPopup = new Rectangle((int)GameStuff.Instance.player.playerPosition.X - 300, (int)GameStuff.Instance.player.playerPosition.Y - 150, 600, 300);

            //Updates GameStates
            if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Start)
            {
                backgroudPopup = Color.Yellow;
            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1)
            {   
                // Reset
                if (keyboardState.IsKeyDown(Keys.Space) && keyboardStatePrev.IsKeyDown(Keys.Space))                 
                {
                    GameStuff.Instance.player.playerPosition = new Vector2(60, 410);

                    GameStuff.Instance.guard1.guardPosition = new Vector2(280, 30);
                    GameStuff.Instance.guard2.guardPosition = new Vector2(75, 40);
                    GameStuff.Instance.guard3.guardPosition = new Vector2(745, 40);

                    GameStuff.Instance.guard1.changeDetectionStatus(false);
                    GameStuff.Instance.guard2.changeDetectionStatus(false);
                    GameStuff.Instance.guard3.changeDetectionStatus(false);

                    GameStuff.Instance.levelWon = false;             
                }

                GameStuff.Instance.tileMap.Update(gameTime);

                GameStuff.Instance.player.Update(GameStuff.Instance.tileMap);

                GameStuff.Instance.guard1.Update();
                GameStuff.Instance.guard2.Update();
                GameStuff.Instance.guard3.Update();


                for (int i = GameStuff.Instance.items.Count - 1; i >= 0; i--)
                {
                    if (GameStuff.Instance.items[i].alive)
                        GameStuff.Instance.items[i].Update(gameTime);
                    else
                    {
                        GameStuff.Instance.items.RemoveAt(i);
                    }
                }
            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Lost)
            {
                backgroudPopup = Color.White;
                
            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Won)
            {
                backgroudPopup = Color.White;
               
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
            if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Start)
            {
                spriteBatch.Draw(startScreen, startPos, backgroudPopup);
            }

            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1)
            {
                GameStuff.Instance.tileMap.Draw(spriteBatch);

                GameStuff.Instance.player.Draw(spriteBatch);
                GameStuff.Instance.guard1.Draw(spriteBatch);
                GameStuff.Instance.guard2.Draw(spriteBatch);
                GameStuff.Instance.guard3.Draw(spriteBatch);

                foreach (Item item in GameStuff.Instance.items)
                    item.Draw(spriteBatch);
            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Lost)
            {
                GameStuff.Instance.tileMap.Draw(spriteBatch);

                GameStuff.Instance.player.Draw(spriteBatch);
                GameStuff.Instance.guard1.Draw(spriteBatch);
                GameStuff.Instance.guard2.Draw(spriteBatch);
                GameStuff.Instance.guard3.Draw(spriteBatch);

                foreach (Item item in GameStuff.Instance.items)
                    item.Draw(spriteBatch);

                spriteBatch.Draw(defeatScreen, screenPos, backgroudPopup);
            }
            else if (GameStuff.Instance.gameStateCurr == GameStuff.GameStates.Level1Won)
            {
                GameStuff.Instance.tileMap.Draw(spriteBatch);

                GameStuff.Instance.player.Draw(spriteBatch);
                GameStuff.Instance.guard1.Draw(spriteBatch);
                GameStuff.Instance.guard2.Draw(spriteBatch);
                GameStuff.Instance.guard3.Draw(spriteBatch);

                foreach (Item item in GameStuff.Instance.items)
                    item.Draw(spriteBatch);

                spriteBatch.Draw(winScreen, endPos, backgroudPopup);
            }

            

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
