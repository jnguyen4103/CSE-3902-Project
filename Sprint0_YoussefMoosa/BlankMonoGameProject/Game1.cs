using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        public MonsterFactory MFactory;
        public RoomFactory RFactory;

        // Link Object & Sprite
        public ILink Link;
        public LinkSprite SpriteLink;
        public int RupeeCounter = 0;
        public int KeyCounter = 0;

        // Sprite Sheets
        public Texture2D LinkSpriteSheet;
        public Texture2D MonsterSpriteSheet;
        public Texture2D ItemSpriteSheet;
        public Texture2D EffectSpriteSheet;
        public Texture2D TileSpriteSheet;
        public Texture2D Background;
        public Song song;


        CollisionDetection handler;

        public List<Monster> MonsterList = new List<Monster>();
        public List<Item> ItemsList = new List<Item>();
        public List<IEffect> EffectsList = new List<IEffect>();
        public List<Door> DoorList = new List<Door>(4);
        public List<FRectangle> blocks = new List<FRectangle>();

        private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Q, Keys.D1, Keys.D2, Keys.R, Keys.Z, Keys.E, Keys.H };
        private ICommand[] keyboardCommands = new ICommand[11];
        private KeyboardController keyboardController;
        //(32,96). w = 192 H =112

        // Spawn positions of all the items, NPCs and Link so they can be used in the Reset command
        public readonly Vector2 LinkSpawn = new Vector2(120, 192);

        public Vector2 screenDimensions = new Vector2(1024.0f, 960.0f);
        public Rectangle CurrentScreen = new Rectangle(0, 0, 256, 240);
        public Rectangle WalkingRect = new Rectangle(32, 96, 208, 191);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = (int)screenDimensions.X;
            graphics.PreferredBackBufferHeight = (int)screenDimensions.Y;


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
            SFactory = new SpriteFactory(this);
            IFactory = new ItemFactory(this);
            MFactory = new MonsterFactory(this);
            RFactory = new RoomFactory(this);
            this.song = Content.Load<Song>("musicForGame");

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
            blocks.Add(new FRectangle(100f,150f,16,16));
            keyboardController = new KeyboardController(this, keyboardKeys, keyboardCommands);
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            base.Initialize();
        }

        void MediaPlayer_MediaStateChanged(object sender, System.
                                        EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
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
            TileSpriteSheet = Content.Load<Texture2D>("Tile Sprite Sheet");
            Background = Content.Load<Texture2D>("Background");

            SpriteLink = new LinkSprite(this, "WalkUp", LinkSpriteSheet, LinkSpawn, spriteBatch);
            Link = new Link(SpriteLink, this);
           
            RFactory.LoadRoom(0);
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


            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateScale(4, 4, 1.0f));

            spriteBatch.Draw(Background, CurrentScreen, Color.White);

            foreach (Door door in DoorList)
            {
                door.Draw();
            }

            foreach (IEffect effect in EffectsList.ToArray())
            {
                effect.Sprite.DrawSprite();
                if(effect.Sprite.Colour == Color.Transparent)
                {
                    EffectsList.Remove(effect);
                }
            }

            foreach (Monster monster in MonsterList.ToArray())
            {
                if(monster.Sprite.Colour == Color.Transparent)
                {
                    MonsterList.Remove(monster);
                }

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