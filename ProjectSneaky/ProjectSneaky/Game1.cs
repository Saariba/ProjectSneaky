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
            GameStuff.Instance.player = new Player(Content.Load<Texture2D>("Player/Player"), new Vector2(60, 410), 5);
            GameStuff.Instance.guard1 = new Guards(Content.Load<Texture2D>("Guards/guard"), new Vector2(280, 30), new Vector2(280, 310), new Vector2(280, 30), 1.5f, "east");
            GameStuff.Instance.tileMap = new Tilemap(new Texture2D[] { Content.Load<Texture2D>("Wall"), Content.Load<Texture2D>("Floor") }, Content.Load<Texture2D>("trialbitmap"), 16);
            GameStuff.Instance.guard2 =  new Guards(Content.Load<Texture2D>("Guards/guard"), new Vector2(75, 40), new Vector2(630, 40), new Vector2(75, 40), 1.5f, "south");
            //GameStuff.Instance.goal = new Goal(Content.Load<Texture2D>("DollarBill"), new Vector2(20, 20));
            GameStuff.Instance.guard3 = new Guards(Content.Load<Texture2D>("Guards/guard"), new Vector2(745, 40), new Vector2(745, 340), new Vector2(745, 40), 1.5f, "south");
            base.Initialize();
            GameStuff.Instance.camera = new Camera(graphics.GraphicsDevice.Viewport);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameStuff.Instance.items.Add(new Goal(Content.Load<Texture2D>("DollarBill"), new Vector2(20, 20)));
            
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
            
            GameStuff.Instance.tileMap.Update(gameTime);
            GameStuff.Instance.player.Update(GameStuff.Instance.tileMap);
            GameStuff.Instance.guard1.Update();
            GameStuff.Instance.guard2.Update();
            GameStuff.Instance.guard3.Update();
            
           
            for (int i = GameStuff.Instance.items.Count - 1 ; i >= 0; i--)
            {
                if (GameStuff.Instance.items[i].alive)
                    GameStuff.Instance.items[i].Update(gameTime);
                else
                {
                    GameStuff.Instance.items.RemoveAt(i);
                }
            }


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin(transformMatrix: GameStuff.Instance.camera.GetViewMatrix());

            GameStuff.Instance.tileMap.Draw(spriteBatch);

            GameStuff.Instance.player.Draw(spriteBatch);
            GameStuff.Instance.guard1.Draw(spriteBatch);
            GameStuff.Instance.guard2.Draw(spriteBatch);
            GameStuff.Instance.guard3.Draw(spriteBatch);

            foreach (Item item in GameStuff.Instance.items)
                item.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
