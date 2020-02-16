using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint02
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Texture2D linkSpriteSheet;
        public ISprite LinkSprite { get; set; }
        public NPC Monster { get; set; }
        public NPC[] MonsterList = new NPC[5];
        public int currentMonsterPosition = 0;
        public ItemFactory Item { get; set; }
        public ItemFactory[] ItemList = new ItemFactory[13];
        public int currentItemPosition = 0;

        private Keys[] keyboardKeys = { Keys.O, Keys.P, Keys.U, Keys.I };
        private ICommand[] keyboardCommands = new ICommand[4];
        private KeyboardController keyboardController;

        public readonly Vector2 itemSpawnPosition = new Vector2(100, 240);
        public readonly Vector2 spawnPosition = new Vector2(400, 240);

        public Vector2 screenDimensions = new Vector2(800.0f, 480.0f);


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


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


            keyboardCommands[0] = new IncrementNPC(this);
            keyboardCommands[1] = new DecrementNPC(this);
            keyboardCommands[2] = new IncrementItem(this);
            keyboardCommands[3] = new DecrementItem(this);

            keyboardController = new KeyboardController(keyboardKeys, keyboardCommands);

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
            
            //linkSpriteSheet = this.Content.Load<Texture2D>("LinkSpritesheet");
            MonsterList[0] = new Stalfos(new StalfosSprite(Content.Load<Texture2D>("StalfosDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[1] = new Gel(new GelSprite(Content.Load<Texture2D>("GelDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[2] = new Geese(new GeeseSprite(Content.Load<Texture2D>("GeeseDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[3] = new Aquamentus(new AquamentusSprite(Content.Load<Texture2D>("AquamentusDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[4] = new Fairy(new FairySprite(Content.Load<Texture2D>("FairyDefault"), spawnPosition, screenDimensions, spriteBatch));
            ItemList[0] = new RedHeart(Content.Load<Texture2D>("RedHeart"), itemSpawnPosition, spriteBatch);
            ItemList[1] = new HeartContainer(Content.Load<Texture2D>("HeartContainer"), itemSpawnPosition, spriteBatch);
            ItemList[2] = new Bomb(Content.Load<Texture2D>("Bomb"), itemSpawnPosition, spriteBatch);
            ItemList[3] = new Clock(Content.Load<Texture2D>("Clock"), itemSpawnPosition, spriteBatch);
            ItemList[4] = new Map(Content.Load<Texture2D>("Map"), itemSpawnPosition, spriteBatch);
            ItemList[5] = new Compass(Content.Load<Texture2D>("Compass"), itemSpawnPosition, spriteBatch);
            ItemList[6] = new YellowRupee(Content.Load<Texture2D>("YellowRupee"), itemSpawnPosition, spriteBatch);
            ItemList[7] = new BlueRupee(Content.Load<Texture2D>("BlueRupee"), itemSpawnPosition, spriteBatch);
            ItemList[8] = new YellowTriforce(Content.Load<Texture2D>("YellowTriforce"), itemSpawnPosition, spriteBatch);
            ItemList[9] = new Bow(Content.Load<Texture2D>("Bow"), itemSpawnPosition, spriteBatch);
            ItemList[10] = new Boomerang(Content.Load<Texture2D>("Boomerang"), itemSpawnPosition, spriteBatch);
            ItemList[11] = new Key(Content.Load<Texture2D>("Key"), itemSpawnPosition, spriteBatch);
            ItemList[12] = new LionKey(Content.Load<Texture2D>("LionKey"), itemSpawnPosition, spriteBatch);



            Monster = MonsterList[0];
            Item = ItemList[0];

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
            //LinkSprite.DrawSprite();

            spriteBatch.Begin();
            Monster.Draw();
            Item.DrawItem();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
