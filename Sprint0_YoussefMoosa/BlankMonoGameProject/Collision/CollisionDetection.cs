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
            /* 0 - Top
            * 1 - Bottom
            * 2 - Left
            * 3 - Right
            */

            if (Collision.Width < Collision.Height)
            {
                //Collision happend on the Left
                if (Receiver.GetPosition.X < Collision.Right && Math.Round(Receiver.GetPosition.X) == Math.Round(Collision.Left) && Receiver.GetPosition.Y <= Collision.Top
                    && Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) >= Math.Round(Collision.Top))
                { returnVal = 2; }


                //Collision happened on the right 
                else if (Receiver.GetPosition.X + Receiver.GetSize.X > Collision.Left &&
                    Math.Round(Receiver.GetPosition.X + Receiver.GetSize.X) == Math.Round(Collision.Right)
                    && Receiver.GetPosition.Y <= Collision.Top
                    && Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) >= Math.Round(Collision.Top))
                { returnVal = 3; }

                else { returnVal = 1; }
            }

            //Our intersection happend on the top or the bottom 
            else if (Collision.Height < Collision.Width)
            {
                //Collision happend on the Top
                if (Receiver.GetPosition.Y == Collision.Top && Receiver.GetPosition.Y > Collision.Bottom)
                { returnVal = 0; }

                //Collision Happened on the bottom
                else if (Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) == Math.Round(Collision.Bottom) && Receiver.GetPosition.Y + Receiver.GetSize.Y > Collision.Top)
                { returnVal = 1; }
            }
            return returnVal;
        }


        public void CollisionHandler()
        {


            int direction;

            FRectangle monsterHitbox;
            FRectangle linkHitbox = new FRectangle(Link.SpriteLink.Position.X, Link.SpriteLink.Position.Y + (Link.SpriteLink.GetSize.Y / 2), (int)Link.SpriteLink.GetSize.X, (int)Link.SpriteLink.GetSize.Y / 2);
            FRectangle effectHitbox;
            FRectangle doorHitBox;
            //  FRectangle tileHitBox;
            FRectangle itemHitbox;
            FRectangle screenDimensions = new FRectangle(Game.CurrentScreen.X, Game.CurrentScreen.Y, Game.CurrentScreen.Width, Game.CurrentScreen.Height);


            // Link & Door Collision
            foreach (Door door in Game.DoorList)
            {
                doorHitBox = new FRectangle(door.Sprite.Position.X, door.Sprite.Position.Y, (int)door.Sprite.GetSize.X, (int)door.Sprite.GetSize.Y);
                if (linkHitbox.Intersects(doorHitBox))
                {
                    Console.WriteLine("Enter new Room");
                }
            }

            // Link & Item Collision
            foreach (Item item in Items)
            {
                itemHitbox = new FRectangle(item.Sprite.Position.X, item.Sprite.Position.Y, (int)item.Sprite.GetSize.X, (int)item.Sprite.GetSize.Y);

                if (linkHitbox.Intersects(itemHitbox))
                {
                    ColRes.PickupItem(item);
                    Console.WriteLine("Item Pickup " + item.Sprite.Name);

                }
            }

            // Link & Monster Collision
            foreach (Monster monster in Monsters)
            {
                // Monster vs. Link
                monsterHitbox = new FRectangle(monster.Sprite.Position.X, monster.Sprite.Position.Y, (int)monster.Sprite.GetSize.X, (int)monster.Sprite.GetSize.Y);
                if (monsterHitbox.Intersects(linkHitbox))
                {
                    direction = CollisionDirection(Link.SpriteLink, FRectangle.Intersection(monsterHitbox, linkHitbox));
                    ColRes.HurtLink(monster, direction);
                    Console.WriteLine("Enemy Contact");
                }
            }

            // Effects Collision
            foreach (IEffect effect in Effects.ToArray())
            {
                effectHitbox = new FRectangle(effect.Sprite.Position.X, effect.Sprite.Position.Y, (int)effect.Sprite.GetSize.X, (int)effect.Sprite.GetSize.Y);

                // Effects & Link Collision
                if (effectHitbox.Intersects(linkHitbox))
                {
                    if (effect.IsCreator(Link.SpriteLink))
                    {
                        Console.WriteLine("Boomerang Contact");
                        Game.Link.StateMachine.CatchBoomerang(effect);
                    }
                    else
                    {
                        direction = CollisionDirection(Link.SpriteLink, FRectangle.Intersection(effectHitbox, linkHitbox));
                        ColRes.DamageLinkEffect(effect.Damage, direction, effect);
                        effect.Sprite.KillSprite();
                        Console.WriteLine("Link Effect Contact");
                    }


                    // Effects & Monster Collision
                    foreach (Monster monster in Monsters)
                    {
                        monsterHitbox = new FRectangle(monster.Sprite.Position.X, monster.Sprite.Position.Y, (int)monster.Sprite.GetSize.X, (int)monster.Sprite.GetSize.Y);
                        if (monsterHitbox.Intersects(effectHitbox))
                        {
                            if (!effect.IsCreator(monster.Sprite) && effect.Sprite.Layer != 0.5f)
                            {
                                direction = CollisionDirection(monster.Sprite, FRectangle.Intersection(effectHitbox, monsterHitbox));
                                ColRes.DamageMonster(monster, direction, effect);
                                Console.WriteLine("Enemy Effect Contact");
                            }
                        }

                    }

                }
            }
        }
    }
}
