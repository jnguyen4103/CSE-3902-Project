using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Sprint03
{
    class CollisionDetection
    {
        private Game1 Game;
        private CollisionResolution ColRes;

        public CollisionDetection(Game1 game)
        {
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
            Console.WriteLine("Height " + Collision.Height);
            Console.WriteLine("Width " + Collision.Width);
            if (Collision.Height < Collision.Width)
            {
                //Collision happend on the Top
                Console.WriteLine(Receiver.GetPosition.Y + Receiver.GetSize.Y > Collision.Top);
                if (Math.Round(Receiver.GetPosition.Y) <= Math.Round(Collision.Top) && Receiver.GetPosition.Y < Collision.Bottom && Receiver.GetPosition.Y + Receiver.GetSize.Y >= Collision.Bottom)
                { returnVal = 0; }

                //Collision Happened on the bottom
                else if (Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) == Math.Round(Collision.Bottom) && Receiver.GetPosition.Y + Receiver.GetSize.Y > Collision.Top && Receiver.GetPosition.Y > Collision.Top)
                { returnVal = 1; }
                else { returnVal = 1; }

            }
            else if (Collision.Width < Collision.Height)
            {
                //Collision happend on the Left
                if (Receiver.GetPosition.X < Collision.Right && Math.Round(Receiver.GetPosition.X) == Math.Round(Collision.Left) && Receiver.GetPosition.Y <= Collision.Top
                    && Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) >= Math.Round(Collision.Top) && Receiver.GetPosition.Y < Collision.Bottom)
                { returnVal = 2; }


                //Collision happened on the right 
                else if (Receiver.GetPosition.X + Receiver.GetSize.X > Collision.Left &&
                    Math.Round(Receiver.GetPosition.X + Receiver.GetSize.X) == Math.Round(Collision.Right)
                    && Receiver.GetPosition.Y < Collision.Bottom
                    && Math.Round(Receiver.GetPosition.Y + Receiver.GetSize.Y) >= Math.Round(Collision.Top))
                { returnVal = 3; }

                else { returnVal = 1; }
            }

            //Our intersection happend on the top or the bottom 

            return returnVal;
        }


        public void CollisionHandler()
        {


            int direction;

            FRectangle monsterHitbox;
            FRectangle linkHitbox = new FRectangle(Game.Link.SpriteLink.Position.X, Game.Link.SpriteLink.Position.Y + (Game.Link.SpriteLink.GetSize.Y / 2), (int)Game.Link.SpriteLink.GetSize.X, (int)Game.Link.SpriteLink.GetSize.Y / 2);
            FRectangle effectHitbox;
            //  FRectangle tileHitBox;
            FRectangle itemHitbox;
            FRectangle screenDimensions = new FRectangle(Game.CurrentScreen.X, Game.CurrentScreen.Y, Game.CurrentScreen.Width, Game.CurrentScreen.Height);

            // 
            foreach (FRectangle box in Game.Blocks)
            {
                if (linkHitbox.Intersects(box))
                {
                    direction = CollisionDirection(Game.Link.SpriteLink, FRectangle.Intersection(linkHitbox, box));
                    ColRes.StopSprite(Game.Link.SpriteLink, box, direction);
                }


                // Link & Item Collision
                foreach (Item item in Game.ItemsList)
            {
                itemHitbox = new FRectangle(item.Sprite.Position.X, item.Sprite.Position.Y, (int)item.Sprite.GetSize.X, (int)item.Sprite.GetSize.Y);

                if (linkHitbox.Intersects(itemHitbox))
                {
                    ColRes.PickupItem(item);
                    Console.WriteLine("Item Pickup " + item.Sprite.Name);

                }
            }

            // Link & Monster Collision
            foreach (Monster monster in Game.MonsterList)
            {
                // Monster vs. Link
                monsterHitbox = new FRectangle(monster.Sprite.Position.X, monster.Sprite.Position.Y, (int)monster.Sprite.GetSize.X, (int)monster.Sprite.GetSize.Y);
                if (monsterHitbox.Intersects(linkHitbox))
                {
                    direction = CollisionDirection(Game.Link.SpriteLink, FRectangle.Intersection(monsterHitbox, linkHitbox));
                    ColRes.HurtLink(monster, direction);
                    Console.WriteLine("Enemy Contact");
                }
            }

            // Effects Collision
            foreach (IEffect effect in Game.EffectsList.ToArray())
            {
                effectHitbox = new FRectangle(effect.Sprite.Position.X, effect.Sprite.Position.Y, (int)effect.Sprite.GetSize.X, (int)effect.Sprite.GetSize.Y);

                // Effects & Link Collision
                if (effectHitbox.Intersects(linkHitbox))
                {
                    if (effect.IsCreator(Game.Link.SpriteLink) && effect.Sprite.Name.Equals("BoomerangEffect"))
                    {
                        Console.WriteLine("Boomerang Contact");
                        Game.Link.StateMachine.CatchBoomerang(effect);
                    }
                    else if (!effect.IsCreator(Game.Link.SpriteLink))
                    {
                        direction = CollisionDirection(Game.Link.SpriteLink, FRectangle.Intersection(effectHitbox, linkHitbox));
                        ColRes.DamageLinkEffect(effect.Damage, direction, effect);
                        effect.Sprite.KillSprite();
                        Console.WriteLine("Link Effect Contact");
                    }


                    // Effects & Monster Collision
                    foreach (Monster monster in Game.MonsterList)
                    {
                        monsterHitbox = new FRectangle(monster.Sprite.Position.X, monster.Sprite.Position.Y, (int)monster.Sprite.GetSize.X, (int)monster.Sprite.GetSize.Y);
                        if (monsterHitbox.Intersects(effectHitbox))
                        {
                            if (effect.IsCreator(Game.Link.SpriteLink))
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
