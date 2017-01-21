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
            GameStuff.Instance.player = new Player(Content.Load<Texture2D>("template thomas"), new Vector2(100, 100));
            GameStuff.Instance.guard1 = new Guards(Content.Load<Texture2D>("guardian"), new Vector2(0, 40), new Vector2(600, 40), new Vector2(0, 40), 1.5f, "east");
            GameStuff.Instance.tileMap = new Tilemap(new Texture2D[] { Content.Load<Texture2D>("Wall"), Content.Load<Texture2D>("Floor") }, Content.Load<Texture2D>("bitMap"), 16);
           GameStuff.Instance.guard2 =  new Guards(Content.Load<Texture2D>("guardian"), new Vector2(200, 80), new Vector2(800, 80), new Vector2(200, 80), 1.5f, "east");
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
            GameStuff.Instance.player.Update();
            GameStuff.Instance.guard1.Update();
            GameStuff.Instance.guard2.Update();
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

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
