using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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
        // Reduces hitbox detection to border of sprites
        private float ShrinkFactor = 4;
        private FRectangle actorHitbox;
        private FRectangle receiverHitbox;

        public CollisionDetection(Game1 game)
        {
            this.Monsters = game.MonsterList;
            this.Link = game.Link;
            this.Items = game.ItemsList;
            this.Effects = game.EffectsList;
            Game = game;
            ColRes = new CollisionResolution(game);
        }

        private bool Intersect(Sprite Actor, Sprite Receiver)
        {
            actorHitbox = new FRectangle(Actor.Position.X, Actor.Position.Y, (int)Actor.GetSize.X, (int)Actor.GetSize.Y);
            receiverHitbox = new FRectangle(Receiver.Position.X, Receiver.Position.Y, (int)Receiver.GetSize.X, (int)Receiver.GetSize.Y);
            return receiverHitbox.Intersects(actorHitbox);
        }

        /*

        private bool RightCollision(Sprite Actor, Sprite Receiver)
        {
            return (Actor.Position.X > Receiver.Position.X
                && (Receiver.Position.X + Receiver.GetSize.X) >= Actor.Position.X
                && Actor.Position.Y >= (Receiver.Position.Y + (Receiver.GetSize.Y / ShrinkFactor))
                && (Actor.Position.Y + Actor.GetSize.Y) <= (Receiver.Position.Y + Receiver.GetSize.Y - (Receiver.GetSize.Y / ShrinkFactor)));
        }
        private bool LeftCollision(Sprite Actor, Sprite Receiver)
        {
            return (Actor.Position.X < Receiver.Position.X
                && (Receiver.Position.X) < (Actor.Position.X + Actor.GetSize.X)
                && Actor.Position.Y >= (Receiver.Position.Y + (Receiver.GetSize.Y / ShrinkFactor))
                && (Actor.Position.Y + Actor.GetSize.Y) <= (Receiver.Position.Y + Receiver.GetSize.Y - (Receiver.GetSize.Y / ShrinkFactor)));
        }
        private bool UpCollision(Sprite Actor, Sprite Receiver)
        {
            return false;
        }
        private bool DownCollision(Sprite Actor, Sprite Receiver)
        {
            return false;
        }




        private String collisionFound(Sprite A, Sprite B)
        {
            //Determines if there is an interesection between two Rectangles 
            Rectangle ARectangle = new Rectangle((int)A.GetPosition.X, (int)A.GetPosition.Y, (int)A.GetSize.X, (int)A.GetSize.Y);
            Rectangle BRectangle = new Rectangle((int)B.GetPosition.X, (int)B.GetPosition.Y, (int)B.GetSize.X, (int)B.GetSize.Y);

            String toReturn = "NONE";
            if (ARectangle.Right < BRectangle.Left)
            {
                toReturn = "RIGHT";
            }
            else if (ARectangle.Left > BRectangle.Right)
            {
                toReturn = "LEFT";
            }
            else if (ARectangle.Top < BRectangle.Bottom)
            {
                toReturn = "TOP";
            }
            else if (ARectangle.Bottom > BRectangle.Top)
            {
                toReturn = "BOTTOM";
            }
            else
            {
                toReturn = "NONE";
            }
            return toReturn;
        } */

        public void CollisionHandler()
        {
            // Monster vs. Link
            foreach(Monster monster in Monsters)
            {
                if (Intersect(monster.Sprite, Link.SpriteLink))
                {
                    //ColRes.HurtLink(monster.attackDamage, detection);
                    ColRes.HurtLink(1);
                    Console.WriteLine("Enemy Contact");
                }
            }

            // Link vs. Effects
            foreach (IEffect effect in Effects)
            {
                if (Intersect(effect.Sprite, Link.SpriteLink))
                {
                    //ColRes.DamageLinkEffect(2, detection, effect);
                    if(!effect.IsCreator(Link.SpriteLink))
                    {
                        Console.WriteLine("Effect Contact");
                    }

                }
            }


            // Monster vs. Effects
            foreach (IEffect effect in Effects)
            {
                foreach(Monster monster in Monsters)
                {
                    if (Intersect(effect.Sprite, monster.Sprite))
                    {
                        //ColRes.DamageMonsterEffect(monster, detection, effect);
                        Console.WriteLine("Monster Effect Contact");

                    }
                }

            }

            // Link vs. Items
            foreach (Item item in Items)
            {
                if (Intersect(item.Sprite, Link.SpriteLink))
                {
                    ColRes.PickupItem(item);
                    Console.WriteLine("Item Pickup " + item.ToString());

                }
            }

        }
    }
}
