﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class Door
    {
        public StaticSprite Sprite;
        public bool DestroyableWall;
        private bool Destroyed = false;
        private Game1 Game;
        private string NextRoom;
        private string Side;

        public Door(Game1 game, StaticSprite sprite, string next, string side, bool destroyable)
        {
            Game = game;
            Sprite = sprite;
            NextRoom = next;
            Side = side;
            DestroyableWall = destroyable;
        }

        public void DestroyWall()
        {
            if (DestroyableWall && !Destroyed)
            {
                Sprite.ChangeSpriteAnimation("DestroyedWall");
                Destroyed = true;
            }
        }

        public void UnlockDoor()
        {
            if (Game.KeyCounter > 0 && Sprite.Name.Equals("KeyDoor"))
            {
                Game.KeyCounter--;
                Sprite.ChangeSpriteAnimation("OpenDoor");
            } else if(Game.KeyCounter > 0 && Sprite.Name.Equals("KeyDoorHorizontal"))
            {
                Game.KeyCounter--;
                Sprite.ChangeSpriteAnimation("OpenDoorHorizontal");
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public void EnterDoor()
        {
            if(Sprite.Name.Equals("KeyDoor") || Sprite.Name.Equals("KeyDoorHorizontal"))
            {
                UnlockDoor();
            } else
            {

                if (Sprite.Name.Equals("OpenDoor") || Sprite.Name.Equals("OpenDoorHorizontal")
                    || Sprite.Name.Equals("DestroyedWall") || Sprite.Name.Equals("DestroyedWallHorizontal"))
                {
                    Game.RFactory.LoadRoom(NextRoom);
                    PlaceLink();
                }
            }

        }

        private void PlaceLink()
        {
            switch (Side)
            {
                case "Up":
                    Game.Link.StateMachine.UpState();
                    Game.Link.StateMachine.IdleState();
                    Game.Link.SpriteLink.Position.X = 120;
                    Game.Link.SpriteLink.Position.Y = 192;
                    break;


                case "Down":
                    Game.Link.StateMachine.DownState();
                    Game.Link.StateMachine.IdleState();
                    Game.Link.SpriteLink.Position.X = 120;
                    Game.Link.SpriteLink.Position.Y = 96;
                    break;

                case "Left":
                    Game.Link.StateMachine.LeftState();
                    Game.Link.StateMachine.IdleState();
                    Game.Link.SpriteLink.Position.X = 208;
                    Game.Link.SpriteLink.Position.Y = 144;
                    break;

                case "Right":
                    Game.Link.StateMachine.RightState();
                    Game.Link.StateMachine.IdleState();
                    Game.Link.SpriteLink.Position.X = 32;
                    Game.Link.SpriteLink.Position.Y = 144;
                    break;

                default:
                    break;

            }

        }
    }
}