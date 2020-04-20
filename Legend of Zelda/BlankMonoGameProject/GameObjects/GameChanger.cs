using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace  Sprint03
{
    class GameChanger
    {
        Game1 Game;
        public GameChanger(Game1 game)
        {
            Game = game;
        }
        public void changeWinningSong()
        {
            Game.song = Game.Content.Load<Song>("winningGameSong");
            MediaPlayer.Play(Game.song);
            MediaPlayer.Volume = 0.8f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += Game.MediaPlayer_MediaStateChanged;
        }
        public void changeSong2()
        {
            Game.song = Game.Content.Load<Song>("Dungeon2Song");
            MediaPlayer.Play(Game.song);
            MediaPlayer.Volume = 0.8f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += Game.MediaPlayer_MediaStateChanged;
        }
        public void changeSong3()
        {
            Game.song = Game.Content.Load<Song>("Dungeon3Song");
            MediaPlayer.Play(Game.song);
            MediaPlayer.Volume = 0.8f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += Game.MediaPlayer_MediaStateChanged;
        }
        public void changeDungeon2()
        {
            Game.LinkSpawn = new Vector2(375, 1358);
            Game.Link.StateMachine.IdleState();
            Game.DungeonMain = Game.Content.Load<Texture2D>("Dungeon2_Main");
            Game.TileSpriteSheet = Game.Content.Load<Texture2D>("Dungeon2_Tiles");
            Game.Camera.Transition(Game.CurrDungeon.Rooms["Room0"].Position);
            Game.DungeonDoorFrames = Game.Content.Load<Texture2D>("Dungeon2_DoorFrames");
            Game.CurrDungeon = new Dungeon(Game, "../../../../Dungeon/Dungeon2/Dungeon02.txt");

        }
        public void changeDungeon3()
        {

        }
    }
}
