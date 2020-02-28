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
        public SpriteBatch spriteBatch;

        public SpriteFactory SFactory;
        public ItemFactory IFactory;

        // Link Object & Sprite
        public ILink Link;
        public LinkSprite SpriteLink;
        public int RupeeCounter = 0;

        // Sprite Sheets
        private Texture2D LinkSpriteSheet;
        private Texture2D MonsterSpriteSheet;
        private Texture2D ItemSpriteSheet;
        public Texture2D EffectSpriteSheet;



        CollisionDetection handler;

        public List<Monster> MonsterList = new List<Monster>();
        public List<Item> ItemsList = new List<Item>();
        public List<IEffect> EffectsList = new List<IEffect>();

        private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Q, Keys.D1, Keys.D2, Keys.R, Keys.Z, Keys.E, Keys.H };
        private ICommand[] keyboardCommands = new ICommand[11];
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
            SFactory = new SpriteFactory();
            IFactory = new ItemFactory(this);

            // Adding all of the commands into the keyboard controller
            keyboardCommands[0] = new LinkWalkUp(this);
            keyboardCommands[1] = new LinkWalkDown(this);
            keyboardCommands[2] = new LinkWalkLeft(this);
            keyboardCommands[3] = new LinkWalkRight(this);
            keyboardCommands[4] = new QuitCommand(this);
            keyboardCommands[5] = new LinkUseBoomerang(this);
            keyboardCommands[6] = new LinkUseArrow(this);
            keyboardCommands[7] = new ResetCommand(this);
            keyboardCommands[8] = new LinkAttack(this);
            keyboardCommands[9] = new DamageLink(this);
            keyboardCommands[10] = new IdleLink(this);
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
            Link = new Link(SpriteLink, this);

            MonsterList.Add(new Stalfos(new StalfosSprite(this, "StalfosWalk", MonsterSpriteSheet, spawnPosition, spriteBatch)));
            ItemsList.Add(new Item(this, "Heart", "Heart", ItemSpriteSheet, itemSpawnPosition, spriteBatch));
            handler = new CollisionDetection(this);


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
            Link.Update();
            foreach (Monster monster in MonsterList)
            {
                monster.Update();
            }

            handler.CollisionHandler();
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

            foreach (IEffect effect in EffectsList)
            {
                effect.Sprite.DrawSprite();
            }
            foreach (Monster monster in MonsterList)
            {
                monster.Draw();
            }
            foreach(Item item in ItemsList)
            {
                item.Draw();
            }
            Link.Draw();

         

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}