using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        //public ISprite LinkSprite { get; set; }
        public ILink Link;
        public LinkSprite SpriteLink;

        /* Link SpriteSheet Breakdown
         * Each element in the array is a rectangle which covers
         * all the frame for one type of sprite animation
         * Index 0 : Walking Up
         * Index 1: Walking Down
         * Index 2: Walking Right
         * Index 3: Walking Left
         * Index 4: Hurt Walking Up
         * Index 5: Hurt Walking Down
         * Index 6: Hurt Walking Right
         * Index 7: Hurt Walking Left
         * Index 8: Attacking Up (Needs flipped)
         * Index 9: Attacking Down
         * Index 10: Attacking Right (Needs flipped for left attacking)
         */
        public Rectangle[] LinkAnimationFrames = new Rectangle[11];


        public NPC Monster { get; set; }
        public NPC[] MonsterList = new NPC[6];
        public int currentMonsterPosition = 0;
        public ItemFactory Item { get; set; }
        public ItemFactory[] ItemList = new ItemFactory[13];
        public int currentItemPosition = 0;

        public List<ISprite> EffectsList = new List<ISprite>();
        public IEffect[] LinkSecondaries = new IEffect[1];

        private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.O, Keys.P, Keys.U, Keys.I, Keys.Q, Keys.D1, Keys.R, Keys.Z };
        private ICommand[] keyboardCommands = new ICommand[12];
        private KeyboardController keyboardController;

        public readonly Vector2 itemSpawnPosition = new Vector2(100, 240);
        public readonly Vector2 spawnPosition = new Vector2(400, 240);
        public readonly Vector2 LinkSpawn = new Vector2(600f, 100f);

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


            keyboardCommands[0] = new LinkWalkUp(this);
            keyboardCommands[1] = new LinkWalkDown(this);
            keyboardCommands[2] = new LinkWalkLeft(this);
            keyboardCommands[3] = new LinkWalkRight(this);
            keyboardCommands[4] = new IncrementNPC(this);
            keyboardCommands[5] = new DecrementNPC(this);
            keyboardCommands[6] = new IncrementItem(this);
            keyboardCommands[7] = new DecrementItem(this);
            keyboardCommands[8] = new QuitCommand(this);
            keyboardCommands[9] = new LinkUseBoomerang(this);
            keyboardCommands[10] = new ResetCommand(this);
            keyboardCommands[11] = new LinkAttack(this);


            // ILink Link will use these
            LinkAnimationFrames[0] = new Rectangle(0, 0, 16, 32);
            LinkAnimationFrames[1] = new Rectangle(16, 0, 16, 32);
            LinkAnimationFrames[2] = new Rectangle(32, 0, 16, 32);
            LinkAnimationFrames[3] = new Rectangle(48, 0, 16, 32);

            // ILink DamagedLink will use these
            LinkAnimationFrames[4] = new Rectangle(64, 0, 16, 32);
            LinkAnimationFrames[5] = new Rectangle(80, 0, 16, 32);
            LinkAnimationFrames[6] = new Rectangle(96, 0, 16, 32);
            LinkAnimationFrames[7] = new Rectangle(112, 0, 16, 32);

            // ILink AttackingLink will use these
            LinkAnimationFrames[8] = new Rectangle(128, 0, 16, 44);
            LinkAnimationFrames[9] = new Rectangle(144, 0, 16, 44);
            LinkAnimationFrames[10] = new Rectangle(160, 0, 28, 32);

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

            SpriteLink = new LinkSprite(Content.Load<Texture2D>("LinkSpriteSheet"), LinkSpawn, spriteBatch, LinkAnimationFrames);
            LinkSecondaries[0] = new BoomerangEffect(Content.Load<Texture2D>("BoomerangEffect"), spriteBatch, this);
            Link = new Link(SpriteLink, LinkSecondaries, this);



            //linkSpriteSheet = this.Content.Load<Texture2D>("LinkSpritesheet");
            MonsterList[0] = new Stalfos(new StalfosSprite(Content.Load<Texture2D>("StalfosDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[1] = new Gel(new GelSprite(Content.Load<Texture2D>("GelDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[2] = new Geese(new GeeseSprite(Content.Load<Texture2D>("GeeseDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[3] = new Aquamentus(new AquamentusSprite(Content.Load<Texture2D>("AquamentusDefault"), spawnPosition, screenDimensions, spriteBatch), new FireballEffect(Content.Load<Texture2D>("AquamentusFireball"), spriteBatch, this));
            MonsterList[4] = new Fairy(new FairySprite(Content.Load<Texture2D>("FairyDefault"), spawnPosition, screenDimensions, spriteBatch));
            MonsterList[5] = new Goriyas(new GoriyasSprite(Content.Load<Texture2D>("GoriyasDefault"), spawnPosition, screenDimensions, spriteBatch), new BoomerangEffect(Content.Load<Texture2D>("BoomerangEffect"), spriteBatch, this));

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
            Link.Draw();

            foreach (ISprite effect in EffectsList)
            {
                effect.DrawSprite();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}