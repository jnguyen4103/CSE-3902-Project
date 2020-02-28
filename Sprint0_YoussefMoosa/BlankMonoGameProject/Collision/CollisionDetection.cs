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
        public CollisionDetection(Game1 game)
        {
            this.Monsters = game.MonsterList;
            this.Link = game.Link;
            this.Items = game.ItemsList;
            this.Effects = game.EffectsList;
            Game = game;
            ColRes = new CollisionResolution(game);
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
        }

        public void CollisionHandler()
        {
            string detection;
            // Monster vs. Link
            foreach(Monster monster in Monsters)
            {
                detection = collisionFound(Link.SpriteLink, monster.Sprite);
                if (!detection.Equals("None") && !detection.Equals(null))
                {
                    //ColRes.HurtLink(monster.attackDamage, detection);
                    Console.WriteLine("Enemy Contact " + detection);
                }
            }

            // Link vs. Effects
            foreach (IEffect effect in Effects)
            {
                detection = collisionFound(Link.SpriteLink, effect.Sprite);
                if (!detection.Equals("None") && !detection.Equals(null))
                {
                    ColRes.DamageLinkEffect(2, detection, effect);
                    Console.WriteLine("Effect Contact " + detection);

                }
            }


            // Monster vs. Effects
            foreach (IEffect effect in Effects)
            {
                foreach(Monster monster in Monsters)
                {
                    detection = collisionFound(monster.Sprite, effect.Sprite);
                    if (!detection.Equals("None") && !detection.Equals(null))
                    {
                        ColRes.DamageMonsterEffect(monster, detection, effect);
                        Console.WriteLine("Monster Effect Contact " + detection);

                    }
                }

            }

            // Link vs. Items
            foreach (Item item in Items)
            {
                detection = collisionFound(Link.SpriteLink, item.Sprite);
                if (!detection.Equals("None") && !detection.Equals(null))
                {
                    ColRes.PickupItem(item);
                    Console.WriteLine("Item Pickup " + detection);

                }
            }

        }
    }
}
