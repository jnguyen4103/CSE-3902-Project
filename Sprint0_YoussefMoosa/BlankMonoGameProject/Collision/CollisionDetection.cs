using Microsoft.Xna.Framework;
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
        private Game1 Game;
        private CollisionResolution ColRes;

        public CollisionDetection(Game1 game)
        {
            this.Monsters = game.MonsterList;
            this.Link = game.Link;
            this.Items = game.ItemsList;
            this.Effects = game.EffectsList;
            Game = game;
            ColRes = new CollisionResolution(game);
        }

        private int CollisionDirection(Sprite Receiver, FRectangle Collision)
        {
            Vector2 CollisionCenter = Collision.Center;
            int returnVal=0;
            /* 0 - TOp
            * 1 - Bottom
            * 2 - Left
            * 3 - Right
            */

            Console.WriteLine("X: "+Collision.Width+" "+ "Y: "+Collision.Height);
            //Our intersection happened on the left or the right
            if (Collision.Height == 0)
            {

                if (Receiver.GetPosition.X < Collision.Right && Math.Round(Receiver.GetPosition.X) == Math.Round(Collision.Left))
                {
                    returnVal = 2;
                }
                //Collision happened on the right 
                else if (Receiver.GetPosition.X + Receiver.GetSize.X > Collision.Left && Math.Round(Receiver.GetPosition.X + Receiver.GetSize.X) == Math.Round(Collision.Right))
                {
                    returnVal = 3;
                }
            }
            else
            {
                if (Collision.Width < Collision.Height)
                {
                    //Collision happend on the Left



                    if (Receiver.GetPosition.X < Collision.Right && Math.Round(Receiver.GetPosition.X) == Math.Round(Collision.Left))
                    {
                        returnVal = 2;
                    }
                    //Collision happened on the right 
                    else if (Receiver.GetPosition.X + Receiver.GetSize.X > Collision.Left && Math.Round(Receiver.GetPosition.X + Receiver.GetSize.X) == Math.Round(Collision.Right))
                    {
                        returnVal = 3;
                    }
                }
                //Our intersection happend on the top or the bottom 
                else if (Collision.Height < Collision.Width)
                {
                    //Collision happend on the Top

                    Console.WriteLine("TOP: " + Collision.Top);
                    Console.WriteLine("BOTTOM: " + Collision.Bottom);
                    Console.WriteLine("Top OF SPRITE: " + Receiver.GetPosition.Y);
                    Console.WriteLine("Bottom OF SPRITE: " + (Receiver.GetPosition.Y + Receiver.GetSize.Y));
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
            }
            Console.WriteLine(returnVal);
            return returnVal;
        }

        public void CollisionHandler()
        {


            int direction;

            FRectangle monsterHitbox;
            FRectangle linkHitbox = new FRectangle(Link.SpriteLink.Position.X, Link.SpriteLink.Position.Y, (int)Link.SpriteLink.GetSize.X, (int)Link.SpriteLink.GetSize.Y);
            FRectangle effectHitbox;
            FRectangle itemHitbox;

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
                
                foreach (IEffect effect in Effects)
                {
                    effectHitbox = new FRectangle(effect.Sprite.Position.X, effect.Sprite.Position.Y, (int)effect.Sprite.GetSize.X, (int)effect.Sprite.GetSize.Y);

                    // Monster vs. Effects
                    if (monsterHitbox.Intersects(effectHitbox))
                    {
                        if (!effect.IsCreator(monster.Sprite))
                        {
                            direction = CollisionDirection(monster.Sprite, FRectangle.Intersection(effectHitbox, monsterHitbox));
                            ColRes.DamageMonsterEffect(monster, direction, effect);
                            Console.WriteLine("Enemy Effect Contact");
                        }
                    }

                    // Link vs. Effects
                    if (effectHitbox.Intersects(linkHitbox))
                    {
                        if(!effect.IsCreator(Link.SpriteLink))
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
