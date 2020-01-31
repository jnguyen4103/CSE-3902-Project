using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<IController> controllerList;
        private ISprite Sprite;
        private ISprite TextSprite;
        private Texture2D Texture;
        private SpriteFont Font;

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
            // Initialize controllerList and add controllers to it. 
            // The two controllers for this project are the keyboard and mouse.
            controllerList = new List<IController>();
            controllerList.Add(new KeyboardController(this));
            controllerList.Add(new MouseController(this));
            this.IsMouseVisible = true;
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

            // Load in the sprite sheet and initialize it.
            Texture = Content.Load<Texture2D>("GokuBlue");
            Sprite = new StillSprite(Texture);

            // Load in the spritefont used for the credits.
            Font = Content.Load<SpriteFont>("TextSprite");
            TextSprite = new StillTextSprite(Font, "Credits \nProgram Made By: John Nguyen\nSprites from:" +
                " https://www.spriters-resource.com/custom_edited/\ndragonballcustoms/sheet/85806/");
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
            foreach (IController controller in controllerList)
            {
                controller.controllerAction();
            }
            Sprite.Update();
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            Sprite.DrawSprite(spriteBatch, new Vector2(350,100));
            TextSprite.DrawSprite(spriteBatch, new Vector2(100, 300));
        }
        /// <summary>
        /// This method can be called to set the currently displayed sprite
        /// </summary>
        /// <param name="sprite">What the sprite should be set to</param>
        public void SetSprite (ISprite sprite)
        {
            Sprite = sprite;
        }
        public Texture2D GetTexture()
        {
            return Texture;
        }
    }
}