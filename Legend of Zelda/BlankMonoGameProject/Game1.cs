using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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
        public Camera2D Camera;
        public HUD hud;
        

        public SpriteFactory SFactory;
        public ItemFactory IFactory;

        // Link Object & Sprite
        public ILink Link;
        public LinkSprite SpriteLink;
        public bool ClockActivated = false;
        public int RupeeCounter = 0;
        public int KeyCounter = 12;
        public int BombCounter = 0;

        // Sprite Sheets
        public Texture2D LinkSpriteSheet;
        public Texture2D MonsterSpriteSheet;
        public Texture2D ItemSpriteSheet;
        public Texture2D EffectSpriteSheet;
        public Texture2D TileSpriteSheet;
        public Texture2D DungeonMain;
        public Texture2D DungeonDoorFrames;

        public Song song;

        // Random for everything
        public static Random random = new Random();

        public Dungeon Dungeon01;
        public string DefaultDungeon = "../../../../Dungeon/Dungeon1/Dungeon01.txt";
        public CollisionDetection Detection;
        
        private Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Z, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.R, Keys.Q };
        public ICommand[] keyboardCommands = new ICommand[12];
        private KeyboardController keyboardController;
        //private MouseController mouseController;
        
        //(32,96). w = 192 H =112

        // Spawn positions of all the items, NPCs and Link so they can be used in the Reset command
        public readonly Vector2 LinkSpawn = new Vector2(640, 1200);

        public Vector2 screenDimensions = new Vector2(1024.0f, 960.0f);
        public float ScreenScale = 4.0f;

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
            Camera = new Camera2D(this, GraphicsDevice.Viewport);
            SFactory = new SpriteFactory();
            IFactory = new ItemFactory(this);
            Detection = new CollisionDetection(this);

            
            // Adding all of the commands into the keyboard controller
            keyboardCommands[0] = new LinkWalkUp(this);
            keyboardCommands[1] = new LinkWalkDown(this);
            keyboardCommands[2] = new LinkWalkLeft(this);
            keyboardCommands[3] = new LinkWalkRight(this);
            keyboardCommands[4] = new LinkAttack(this);
            keyboardCommands[5] = new LinkBomb(this);
            keyboardCommands[6] = new LinkArrow(this);
            keyboardCommands[7] = new LinkCandle(this);
            keyboardCommands[8] = new LinkBoomerang(this);
            keyboardCommands[9] = new Reset(this);
            keyboardCommands[10] = new Quit(this);
            keyboardController = new KeyboardController(this, keyboardKeys, keyboardCommands);


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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LinkSpriteSheet = Content.Load<Texture2D>("Donald Trump Sprite Sheet");
            MonsterSpriteSheet = Content.Load<Texture2D>("Monster Sprite Sheet");
            ItemSpriteSheet = Content.Load<Texture2D>("Item Sprite SHeet");
            EffectSpriteSheet = Content.Load<Texture2D>("Effects Sprite Sheet");
            TileSpriteSheet = Content.Load<Texture2D>("Tile Sprite Sheet");
            DungeonMain = Content.Load<Texture2D>("Dungeon1_Main");
            DungeonDoorFrames = Content.Load<Texture2D>("Dungeon1_Door_Frames");

            SpriteLink = new LinkSprite(this, "WalkUp", LinkSpriteSheet, spriteBatch);
            Link = new Link(this, SpriteLink, LinkSpawn);
            hud = new HUD(this);
            this.song = Content.Load<Song>("musicForGame");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            Dungeon01 = new Dungeon(this, DefaultDungeon);
        }

        public void changeSong()
        {
            this.song = Content.Load<Song>("winningGameSong");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.8f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
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

            Link.Update();
            Camera.Update();
            Dungeon01.Update();
            Detection.Update();
            keyboardController.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform);

            spriteBatch.Draw(DungeonMain, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
            Dungeon01.Draw();
            Link.Draw();
            spriteBatch.Draw(DungeonDoorFrames, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.75f);
            hud.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}