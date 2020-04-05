﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class PickupLink : ILink
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public LinkSprite Sprite { get; set; }
        public LinkStateMachine StateMachine { get; set; }
        public States.LinkState State { get; set; } = States.LinkState.Damaged;
        public States.Direction Direction { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public float BaseSpeed { get; set; } = 1f;
        public bool CanMove { get; set; } = false;
        private Texture2D YOUWON;
        private Texture2D TriForcePepe;
        

        private Game1 Game;
        private Link decoratedLink;
        private IAttack SwordAttack;
        private int Timer = 0;
        private int PickupDuration = 4;
        private int wonScreenDelay = 48;
        public PickupLink(Game1 game, Link link)
        {
            Game = game;
            decoratedLink = link;
            Sprite = decoratedLink.Sprite;
            Sprite.FPS = 12;
            Hitbox = decoratedLink.Hitbox;
            Position = decoratedLink.Position;
            Direction = decoratedLink.Direction;
            HP = decoratedLink.HP;
            MaxHP = decoratedLink.MaxHP;
            SwordAttack = new Sword(Game, this);
            TriForcePepe = game.Content.Load<Texture2D>("WinningGame");
            YOUWON = game.Content.Load<Texture2D>("YOU WON");
            Game.changeSong();

        }

        public void TakeDamage(States.Direction directionHit, int damage)
        {
            RemoveDecorator();
            decoratedLink.TakeDamage(directionHit, damage);
        }

        public void Attack()
        {
            //Perhaps have a special attack on double press
        }

        public void SecondaryAttack(string attackName)
        {
            // Does nothing
        }

        public void ChangeDirection(States.Direction direction)
        {
            // Cannot move
        }


        public void Stop()
        {
            // Link cannot move thus he does not need to stop
        }

        public void Update()
        {
            Timer++;
            if (Timer >= PickupDuration)
            {
                Sprite.ChangeSpriteAnimation("Pickup");
                Sprite.FPS = 2;
            }
            if (Timer >= wonScreenDelay)
            {
                Game.hud.HideHud();
            }

        }
        public void PickupItem()
        {
            /*
             * Cannot PickUPItem  
            */
        }
        public void Draw()
        {
           
            Sprite.DrawSprite();
            if (Timer >= wonScreenDelay)
            {
       
                Game.spriteBatch.Draw(YOUWON, new Vector2(Game.Dungeon01.ActiveRoom.Position.X, Game.Dungeon01.ActiveRoom.Position.Y + 100), null, Color.White * 0.85f, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);
                Game.spriteBatch.Draw(TriForcePepe, new Vector2(Game.Dungeon01.ActiveRoom.Position.X, Game.Dungeon01.ActiveRoom.Position.Y - 64), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);

            }
        }



        private void RemoveDecorator()
        {
            decoratedLink.State = States.LinkState.Idle;
            decoratedLink.Position = Position;
            decoratedLink.Hitbox = Hitbox;
            Game.Link = decoratedLink;
            decoratedLink.ResetSprite();
            decoratedLink.CanMove = true;
        }
    }
}