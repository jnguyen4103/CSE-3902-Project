using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * Sources:
 * 
 * RB Whitaker's wiki (Monogame text): http://rbwhitaker.wikidot.com/monogame-drawing-text-with-spritefonts
 * CSE 3902 Sprint 0 page:             http://web.cse.ohio-state.edu/~boggus.2/3902/sprint0.html
 */

namespace Sprint0_YoussefMoosa
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D linkSpriteSheet;
        SpriteFont font;
        public ISprite LinkSprite { get; set; }

        private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Escape };
        private ICommand[] keyboardCommands = new ICommand[5];
        private KeyboardController keyboardController;

        private Rectangle[] screenSections = new Rectangle[]
        {
            new Rectangle(0, 0, 400, 240), new Rectangle(400, 0, 400, 240),
            new Rectangle(0, 240, 400, 240), new Rectangle(400, 240, 400, 240)
        };

        private ICommand[] lmbCommands = new ICommand[4];
        private ICommand[] rmbCommands = new ICommand[4];

        private MouseController mouseController;

        private Rectangle[] spritesheetAnimation = new Rectangle[]
        {
            new Rectangle(34, 10, 16, 16), new Rectangle(50, 10, 16, 16)
        };
        private int animationTime = 15;

        private Rectangle initialDest = new Rectangle(64, 64, 128, 128);

        private Vector2 screenDimensions = new Vector2(800.0f, 480.0f);

        private Vector2 spriteMoveVertVelocity = new Vector2(0.0f, 10.0f);
        private Vector2 spriteMoveHorzVelocity = new Vector2(10.0f, 0.0f);

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

            // TODO: use this.Content to load your game content here
            linkSpriteSheet = this.Content.Load<Texture2D>("LinkSpritesheet");
            font = this.Content.Load<SpriteFont>("Credits");

            keyboardCommands[0] = new ChangeSpriteToStaticCommand(this, linkSpriteSheet, spritesheetAnimation[0],
                initialDest, spriteBatch
                );
            keyboardCommands[1] = new ChangeSpriteToAnimateCommand
                (this, linkSpriteSheet, spritesheetAnimation, initialDest, animationTime, spriteBatch);
            keyboardCommands[2] = new ChangeSpriteToMoveCommand
                (this, linkSpriteSheet, spritesheetAnimation[0], initialDest, spriteMoveVertVelocity, screenDimensions, spriteBatch);
            keyboardCommands[3] = new ChangeSpriteToFullCommand(this, linkSpriteSheet, spritesheetAnimation, initialDest, spriteMoveHorzVelocity, screenDimensions, spriteBatch, animationTime);
            keyboardCommands[4] = new QuitCommand(this);

            for (int i = 0; i < lmbCommands.Length; i++)
            {
                lmbCommands[i] = keyboardCommands[i];
                rmbCommands[i] = keyboardCommands[4];
            }

            keyboardController = new KeyboardController(keyboardKeys, keyboardCommands);
            mouseController = new MouseController(screenSections, lmbCommands, rmbCommands);

            LinkSprite = new NoMoveNoAnimSprite(
                linkSpriteSheet,
                spritesheetAnimation[0],
                initialDest,
                spriteBatch
            );

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


            // TODO: Add your update logic here
            mouseController.Update();
            keyboardController.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            LinkSprite.DrawSprite();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Credits:", new Vector2(20.0f, 300.0f), Color.Black);
            spriteBatch.DrawString(font, "Program made by: Youssef Moosa", new Vector2(20.0f, 330.0f), Color.Black);
            spriteBatch.DrawString(font, "Sprites from: https://www.spriters-resource.com/nes/legendofzelda/sheet/8366/", new Vector2(20.0f, 360.0f), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
