﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprint03
{
    class CollisionDetection
    {
        public ILink Link;
        private List<Item> Items;
        private List<Monster> Monsters;
        public List<IEffect> Effects;
        public List<Tile> Tiles;
        private Game1 Game;
        private CollisionResolution ColRes;

        public CollisionDetection(Game1 game)
        {
            this.Monsters = game.MonsterList;
            this.Link = game.Link;
            this.Items = game.ItemsList;
            this.Effects = game.EffectsList;
           // this.Tiles = game.TileList;
            Game = game;
            ColRes = new CollisionResolution(game);
        }

        private int CollisionDirection(Sprite Receiver, FRectangle Collision)
        {
            Vector2 CollisionCenter = Collision.Center;
            int returnVal = 0;
            /* 0 - TOp
            * 1 - Bottom
            * 2 - Left
            * 3 - Right
            */

            //Our intersection happened on the left or the right
            Console.WriteLine("WIDTH :"+Collision.Width);
            Console.WriteLine("HEIGHT: " + Collision.Height);
            Console.WriteLine("TOP OF SPRITE " + Receiver.GetPosition.Y);
            Console.WriteLine("Bottom OF SPRITE " + Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y));
            Console.WriteLine("TOP OF Collision" + Math.Round(Collision.Top));
            Console.WriteLine(Receiver.GetPosition.X + Receiver.GetSize.X > Collision.Left );
            Console.WriteLine(Math.Round(Receiver.GetPosition.X + Receiver.GetSize.X) >= Math.Round(Collision.Right));
            Console.WriteLine("Right Side Of Sprite: " + Math.Round(Receiver.GetPosition.X + Receiver.GetSize.X));
            Console.WriteLine("Right Side Of Collision: "+Math.Round(Collision.Right));
            if (Collision.Width < Collision.Height)
            {
                //Collision happend on the Left
                


                if (Receiver.GetPosition.X < Collision.Right && Math.Round(Receiver.GetPosition.X) == Math.Round(Collision.Left) && Receiver.GetPosition.Y <= Collision.Top
                    && Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) >= Math.Round(Collision.Top))
                {
                    returnVal = 2;
                }
                //Collision happened on the right 
                else if (Receiver.GetPosition.X + Receiver.GetSize.X > Collision.Left && 
                    Math.Round(Receiver.GetPosition.X + Receiver.GetSize.X) == Math.Round(Collision.Right)
                    && Receiver.GetPosition.Y<= Collision.Top 
                    && Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) >= Math.Round(Collision.Top))
                {
                    returnVal = 3;
                }
             
            }
            //Our intersection happend on the top or the bottom 
            else if (Collision.Height < Collision.Width)
            {
                //Collision happend on the Top
                if (Receiver.GetPosition.Y == Collision.Top && Receiver.GetPosition.Y > Collision.Bottom)
                {
                    returnVal = 0;
                }
      
                //Collision Happened on the bottom
                else if (Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) == Math.Round(Collision.Bottom) && Receiver.GetPosition.Y + Receiver.GetSize.Y > Collision.Top)
                {
                    returnVal = 1;
                }
  


            }
            Console.WriteLine("Value is "+returnVal);
            return returnVal;
        }


        public void CollisionHandler()
        {


            int direction;

            FRectangle monsterHitbox;
            FRectangle linkHitbox = new FRectangle(Link.SpriteLink.Position.X, Link.SpriteLink.Position.Y, (int)Link.SpriteLink.GetSize.X, (int)Link.SpriteLink.GetSize.Y);
            FRectangle effectHitbox;
          //  FRectangle tileHitBox;
            FRectangle itemHitbox;
            FRectangle screenDimensions= new FRectangle(Game.CurrentScreen.X, Game.CurrentScreen.Y, Game.CurrentScreen.Width, Game.CurrentScreen.Height);

            foreach (Monster monster in Monsters)
            {
                // Monster vs. Link
                monsterHitbox = new FRectangle(monster.Sprite.Position.X, monster.Sprite.Position.Y, (int)monster.Sprite.GetSize.X, (int)monster.Sprite.GetSize.Y);
                if (monsterHitbox.Intersects(linkHitbox))
                {
                    direction = CollisionDirection(Link.SpriteLink, FRectangle.Intersection(monsterHitbox, linkHitbox));
                    ColRes.HurtLink(monster.attackDamage, direction);
                    Console.WriteLine("Enemy Contact");
                }


                /*Makes Sure Monsters Stay InBounds*/

                foreach (IEffect effect in Effects.ToArray())
                {
                    effectHitbox = new FRectangle(effect.Sprite.Position.X, effect.Sprite.Position.Y, (int)effect.Sprite.GetSize.X, (int)effect.Sprite.GetSize.Y);

                    // Monster vs. Effects
                    if (monsterHitbox.Intersects(effectHitbox))
                    {
                        if (!effect.IsCreator(monster.Sprite))
                        {
                            direction = CollisionDirection(monster.Sprite, FRectangle.Intersection(effectHitbox, monsterHitbox));
                            ColRes.DamageMonster(monster, direction, effect);
                            Console.WriteLine("Enemy Effect Contact");
                        }
                    }


                    // Link vs. Effects
                    if (effectHitbox.Intersects(linkHitbox))
                    {
                        if (!effect.IsCreator(Link.SpriteLink))
                        {
                            direction = CollisionDirection(Link.SpriteLink, FRectangle.Intersection(effectHitbox, linkHitbox));
                            ColRes.DamageLinkEffect(effect.Damage, direction, effect);
                            Console.WriteLine("Link Effect Contact");
                        }

                    }
                

                    

                }
            }

            // Link vs. Items
            foreach (Item item in Items)
            {
                itemHitbox = new FRectangle(item.Sprite.Position.X, item.Sprite.Position.Y, (int)item.Sprite.GetSize.X, (int)item.Sprite.GetSize.Y);

                if (linkHitbox.Intersects(itemHitbox))
                {
                    direction = CollisionDirection(Link.SpriteLink, FRectangle.Intersection(itemHitbox, linkHitbox));
                    ColRes.PickupItem(item);
                    Console.WriteLine("Item Pickup " + item.Sprite.Name);

                }
            }

        }
    }
}
