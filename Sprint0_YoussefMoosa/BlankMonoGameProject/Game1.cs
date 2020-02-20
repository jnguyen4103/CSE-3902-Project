using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Sprint03
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public SpriteFactory Factory;

        // Link Object & Sprite
        public ILink Link;
        public ISprite SpriteLink;

        /* Link SpriteSheet Breakdown
         * Each element in the array is a rectangle which covers
         * all the frame for one type of sprite animation
         * Element 0 : Walking Up
         * Element 1: Walking Down
         * Element 2: Walking Right
         * Element 3: Walking Left
         * Element 4: Hurt Walking Up
         * Element 5: Hurt Walking Down
         * Element 6: Hurt Walking Right
         * Element 7: Hurt Walking Left
         * Element 8: Attacking Up (Needs flipped)
         * Element 9: Attacking Down
         * Element 10: Attacking Right (Needs flipped for left attacking)
         */
        public Rectangle[] LinkAnimationFrames = new Rectangle[11];

        private Texture2D LinkSpriteSheet;
        private Texture2D MonsterSpriteSheet;
        private Texture2D ItemSpriteSheet;
        private Texture2D EffectSpriteSheet;



        // Monster is the current NPC displayed
        public NPC Monster { get; set; }

        /* MonsterList has a list of all the monsters so Monster can just be swapped for a NPC on the list
         * Element 0: Stalfos
         * Element 1: Geese
         * Element 2: Gel
         * Element 3: Aquamentus
         * Element 4: Fairy
         * Element 5: Goriyas
         */
        public NPC[] MonsterList = new NPC[6];
        public int currentMonsterPosition = 0;


        // ItemList keeps track of all the items and allows the command to just swap which item is displayed
        public ItemFactory Item { get; set; }
        public ItemFactory[] ItemList = new ItemFactory[13];
        public int currentItemPosition = 0;

        /* Each effect (items used or NPC projectile attacks) have an effect which spawns the sprite.
         * The sprite is loaded into an effects List which then is drawn in the Draw() method
         * Whenever an effect is created, a new sprite is added to the EffectsList
         */
        public List<ISprite> EffectsList = new List<ISprite>();

        // A list of all the secondary weapons Link can use (only boomerang is implemented)
        public IEffect[] LinkSecondaries = new IEffect[1];

        private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.O, Keys.P, Keys.U, Keys.I, Keys.Q, Keys.D1, Keys.R, Keys.Z, Keys.E };
        private ICommand[] keyboardCommands = new ICommand[13];
        private KeyboardController keyboardController;

        // Spawn positions of all the items, NPCs and Link so they can be used in the Reset command
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
            Factory = new SpriteFactory();

            // Adding all of the commands into the keyboard controller
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
            keyboardCommands[12] = new DamageLink(this);

            // Setting all of the elements in the Animation array to their proper locations on Link's sprite sheet
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

            // Creating Link and loading in his effects so he can use them when the command is called
            LinkSpriteSheet = Content.Load<Texture2D>("Link Sprite Sheet");
            MonsterSpriteSheet = Content.Load<Texture2D>("Monster Sprite Sheet");
            ItemSpriteSheet = Content.Load<Texture2D>("Item Sprite SHeet");
            EffectSpriteSheet = Content.Load<Texture2D>("Effects Sprite Sheet");
            SpriteLink = new LinkSprite(this, "WalkDown", LinkSpriteSheet, LinkSpawn, spriteBatch);
            //LinkSecondaries[0] = new BoomerangEffect(Content.Load<Texture2D>("BoomerangEffect"), spriteBatch, this);
            //Link = new Link(SpriteLink, LinkSecondaries, this);



            // Loading in the monster list
            MonsterList[0] = new Stalfos(new StalfosSprite (this, "StalfosWalk", MonsterSpriteSheet, spawnPosition, spriteBatch));
            //MonsterList[1] = new Gel(new GelSprite(Content.Load<Texture2D>("GelDefault"), spawnPosition, screenDimensions, spriteBatch));
            //MonsterList[2] = new Geese(new GeeseSprite(Content.Load<Texture2D>("GeeseDefault"), spawnPosition, screenDimensions, spriteBatch));
            //MonsterList[3] = new Aquamentus(new AquamentusSprite(Content.Load<Texture2D>("AquamentusDefault"), spawnPosition, screenDimensions, spriteBatch), new FireballEffect(Content.Load<Texture2D>("AquamentusFireball"), spriteBatch, this));
            //MonsterList[4] = new Fairy(new FairySprite(Content.Load<Texture2D>("FairyDefault"), spawnPosition, screenDimensions, spriteBatch));
            //MonsterList[5] = new Goriyas(new GoriyasSprite(Content.Load<Texture2D>("GoriyasDefault"), spawnPosition, screenDimensions, spriteBatch), new BoomerangEffect(Content.Load<Texture2D>("BoomerangEffect"), spriteBatch, this));

            // Loading in the items list
            //ItemList[0] = new RedHeart(Content.Load<Texture2D>("RedHeart"), itemSpawnPosition, spriteBatch);
            //ItemList[1] = new HeartContainer(Content.Load<Texture2D>("HeartContainer"), itemSpawnPosition, spriteBatch);
            //ItemList[2] = new Bomb(Content.Load<Texture2D>("Bomb"), itemSpawnPosition, spriteBatch);
            //ItemList[3] = new Clock(Content.Load<Texture2D>("Clock"), itemSpawnPosition, spriteBatch);
            //ItemList[4] = new Map(Content.Load<Texture2D>("Map"), itemSpawnPosition, spriteBatch);
            //ItemList[5] = new Compass(Content.Load<Texture2D>("Compass"), itemSpawnPosition, spriteBatch);
            //ItemList[6] = new YellowRupee(Content.Load<Texture2D>("YellowRupee"), itemSpawnPosition, spriteBatch);
            //ItemList[7] = new BlueRupee(Content.Load<Texture2D>("BlueRupee"), itemSpawnPosition, spriteBatch);
            //ItemList[8] = new YellowTriforce(Content.Load<Texture2D>("YellowTriforce"), itemSpawnPosition, spriteBatch);
            //ItemList[9] = new Bow(Content.Load<Texture2D>("Bow"), itemSpawnPosition, spriteBatch);
            //ItemList[10] = new Boomerang(Content.Load<Texture2D>("Boomerang"), itemSpawnPosition, spriteBatch);
            //ItemList[11] = new Key(Content.Load<Texture2D>("Key"), itemSpawnPosition, spriteBatch);
            //ItemList[12] = new LionKey(Content.Load<Texture2D>("LionKey"), itemSpawnPosition, spriteBatch);

            // Defining the first monster and item so they can be drawn later on
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


            spriteBatch.Begin();
            Monster.Draw();
            SpriteLink.DrawSprite();
            //Item.DrawItem();
            //Link.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}