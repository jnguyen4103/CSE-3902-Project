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

            double TopDist = Math.Sqrt(
                Math.Pow((CollisionCenter.X - Receiver.Position.X + (Receiver.GetSize.X / 2)), 2) +
                Math.Pow((CollisionCenter.Y - Receiver.Position.Y), 2));
        
            double BottomDist = Math.Sqrt(
                Math.Pow((CollisionCenter.X - Receiver.Position.X + (Receiver.GetSize.X / 2)), 2) +
                Math.Pow((CollisionCenter.Y - Receiver.Position.Y + Receiver.GetSize.Y), 2));

            double LeftDist = Math.Sqrt(
                Math.Pow((CollisionCenter.X - Receiver.Position.X), 2) +
                Math.Pow((CollisionCenter.Y - Receiver.Position.Y + (Receiver.GetSize.Y / 2)), 2));

            double RightDist = Math.Sqrt(
                Math.Pow((CollisionCenter.X - Receiver.Position.X + Receiver.GetSize.X), 2) +
                Math.Pow((CollisionCenter.Y - Receiver.Position.Y + (Receiver.GetSize.Y / 2)), 2));

            double[] Distances = { TopDist, BottomDist, LeftDist, RightDist };

            /* 0 - Up
            * 1 - Down
            * 2 - Left
            * 3 - Right
            */
            return Distances.ToList().IndexOf(Distances.Max());

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
