using BlankMonoGameProject.Commands.Camera;
using BlankMonoGameProject.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        // Required for monogame
        GraphicsDeviceManager graphics;

        // SpriteBatch & Sprite Factory
        public SpriteBatch spriteBatch;
        public SpriteFactory SFactory;
        public ItemFactory IFactory;

        // GameState
        public IGameState CurrentGameState;

        // Camera
        public Camera2D Camera;

        // HUD
        public HUD hud;

        // Inventory
        public Inventory inv;
        public int[] roomsExplored = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        // Link Object & Sprite
        public ILink Link;
        public LinkSprite SpriteLink;
        public bool ClockActivated = false;
        public int RupeeCounter = 0;
        public int KeyCounter = 0;
        public int BombCounter = 0;

        // Sprite Sheets
        public Texture2D LinkSpriteSheet;
        public Texture2D MonsterSpriteSheet;
        public Texture2D ItemSpriteSheet;
        public Texture2D EffectSpriteSheet;
        public Texture2D TileSpriteSheet;
        public Texture2D DungeonMain;
        public Texture2D DungeonDoorFrames;

        // Song
        public Song song;
        public List<SoundEffect> soundEffects;

        // if game is paused or if user is in inventory screen
        public bool Paused = false;
        public bool InInventory = false;

        // Random number generator variable for game wide use
        public static Random random = new Random();

        public Dungeon CurrDungeon;
        public string DefaultDungeon = "../../../../Dungeon/Dungeon1/Dungeon01.txt";

        // Collision Detector
        public CollisionDetection Detection;
        
        // Controller
        //  TODO: add return/enter button to be assigned to inventory screen transition
        public Keys[] keyboardKeys = { Keys.W, Keys.S, Keys.A, Keys.D, Keys.Z, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.R, Keys.Q, Keys.P, Keys.Enter };
        public ICommand[] keyboardCommands = new ICommand[13];
        public KeyboardController keyboardController;
        //private MouseController mouseController;
        
        //(32,96). w = 192 H =112

        // Spawn positions of all the items, NPCs and Link so they can be used in the Reset command
        public  Vector2 LinkSpawn = new Vector2(640, 1200);

        // Screen dimensions
        public Vector2 screenDimensions = new Vector2(1024.0f, 960.0f);
        public float ScreenScale = 4.0f;

        public VisualBag vsbag;
        public SelectionMenu sel;

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
            //Camera
            Camera = new Camera2D(this, GraphicsDevice.Viewport);

            // Factories - item and sprite
            SFactory = new SpriteFactory();
            IFactory = new ItemFactory(this);

            // Collision Detector
            Detection = new CollisionDetection(this);
            soundEffects = new List<SoundEffect>();


            
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
            keyboardCommands[11] = new Pause(this);
            keyboardCommands[12] = new EnterInventory(this);
            keyboardController = new KeyboardController(this, keyboardKeys, keyboardCommands);


            vsbag = new VisualBag();

            //Game State
            CurrentGameState = new GamePlayingState(this);

            // Game base
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
            // Sprites
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LinkSpriteSheet = Content.Load<Texture2D>("Link Sprite Sheet");
            MonsterSpriteSheet = Content.Load<Texture2D>("Monster Sprite Sheet");
            ItemSpriteSheet = Content.Load<Texture2D>("Item Sprite SHeet");
            EffectSpriteSheet = Content.Load<Texture2D>("Effects Sprite Sheet");
            TileSpriteSheet = Content.Load<Texture2D>("Tile Sprite Sheet");

            // Dungeon
            DungeonMain = Content.Load<Texture2D>("Dungeon1_Main");
            DungeonDoorFrames = Content.Load<Texture2D>("Dungeon1_Door_Frames");

            /*
             * addes sound Effects
             * 0.LOZ_Arrow_Boomerang
             * 1 LOZ_Bomb_Blow
             * 2 LOZ_Bomb_Drop
             * 3 LOZ_Boss_Scream1
             * 4 LOZ_Candle
             * 5 LOZ_Door_Unlock
             * 6 LOZ_Enemy_Die
             * 7 LOZ_Enemy_Hit
             * 8 LOZ_Fanfare
             * 9 LOZ_Get_Heart
             * 10 LOZ_Get_Item
             * 11 LOZ_Get_Rupee
             * 12 LOZ_Key_Appear
             * 13 LOZ_Link_Die
             * 14 LOZ_Link_Hurt
             * 15 LOZ_LowHealth
             * 16 LOZ_Secret
             * 17 LOZ_Sword_Shoot
             * 18 LOZ_Sword_Slash
             */
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Arrow_Boomerang"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Bomb_Blow"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Bomb_Drop"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Boss_Scream1"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Candle"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Door_Unlock"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Enemy_Die"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Enemy_Hit"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Fanfare"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Get_Heart"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Get_Item"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Get_Rupee"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Key_Appear"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Link_Die"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Link_Hurt"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_LowHealth"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Secret"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Sword_Shoot"));
            soundEffects.Add(Content.Load<SoundEffect>("LOZ_Sword_Slash"));

            SpriteLink = new LinkSprite(this, "WalkUp", LinkSpriteSheet, spriteBatch);
            Link = new Link(this, SpriteLink, LinkSpawn);
            hud = new HUD(this);
            inv = new Inventory(this);
            sel = new SelectionMenu(this);
            this.song = Content.Load<Song>("musicForGame");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            CurrDungeon = new Dungeon(this, DefaultDungeon);
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
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardController.Update();
            CurrentGameState.Update();
            base.Update(gameTime);

            //if (!Paused)
            //{
            //    Link.Update();
            //    CurrDungeon.Update();
            //    Camera.Update();
            //    Detection.Update();
            //    base.Update(gameTime);
            //}
            //keyboardController.Update();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform);

            CurrentGameState.Draw();

            spriteBatch.End();
            base.Draw(gameTime);

            //if (!Paused)
            //{
            //    GraphicsDevice.Clear(Color.Black);
            //    spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
            //        samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform);

                

            //    spriteBatch.Draw(DungeonMain, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
            //    CurrDungeon.Draw();
            //    Link.Draw();
            //    spriteBatch.Draw(DungeonDoorFrames, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.75f);
            //    hud.Draw();
            //    if (InInventory)
            //    {
            //        inv.Draw();
            //    }
            //    spriteBatch.End();
            //    base.Draw(gameTime);
            //}
            //if(InInventory)
            //{
            //    GraphicsDevice.Clear(Color.Black);
            //    spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: Camera.Transform);
            //    inv.Draw();
            //    spriteBatch.End();

            //    spriteBatch.Begin();
            //    sel.Draw();
            //    spriteBatch.End();

            //    base.Draw(gameTime);
            //}

        }
    }
}