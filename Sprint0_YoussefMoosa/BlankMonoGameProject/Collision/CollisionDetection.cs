﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class CollisionDetection
    {
        public ILink link;
        public Monster[] enemies;
        public Item[] items;
        public List<ISprite> effects;
        Rectangle linkRect;
        Rectangle enemyRect;
        Rectangle itemRect;
        Rectangle effectRect;
        
        public CollisionDetection(Game1 game)
        {
            this.enemies = game.MonsterList;
            this.link = game.Link;
            this.items = game.ItemList;
            this.effects = game.EffectsList;
        }

        public bool mosterCollisionDetection()
        {
            /*Go over the  list of enemies */
            bool collisionFound = false;

            linkRect = new Rectangle((int)link.SpriteLink.GetPosition.X, (int)link.SpriteLink.GetPosition.Y, (int)link.SpriteLink.GetSize.X, (int)link.SpriteLink.GetSize.Y);

            for (int i = 0; i < enemies.Length; i++)
            {

                /*Create Rectangel for the current enemey */
                enemyRect = new Rectangle((int)enemies[i].Sprite.GetPosition.X, (int)enemies[i].Sprite.GetPosition.Y, (int)enemies[i].Sprite.GetSize.X, (int)enemies[i].Sprite.GetSize.Y);


                /*Go over the list of items*/


                for (int j = 0; j < effects.Count; j++)
                {

                    /*Create Rectangle for effect*/
                    effectRect = new Rectangle((int)effects.ElementAt(j).GetPosition.X, (int)effects.ElementAt(j).GetPosition.Y, (int)effects.ElementAt(j).GetSize.X, (int)effects.ElementAt(j).GetSize.Y);
                    if (enemyRect.Intersects(itemRect))
                    {
                        collisionFound = true;
                    }
                }

                if (enemyRect.Intersects(linkRect))
                {
                    collisionFound = true;
                }

            }
            return collisionFound;
        }

        public bool linkCollisionDetectionMonster()
        {

            bool collisionFound = false;

            linkRect = new Rectangle((int)link.SpriteLink.GetPosition.X, (int)link.SpriteLink.GetPosition.Y, (int)link.SpriteLink.GetSize.X, (int)link.SpriteLink.GetSize.Y);

            for (int i = 0; i < enemies.Length; i++)
            {

                /*Create Rectangel for the current enemey */
                enemyRect = new Rectangle((int)enemies[i].Sprite.GetPosition.X, (int)enemies[i].Sprite.GetPosition.Y, (int)enemies[i].Sprite.GetSize.X, (int)enemies[i].Sprite.GetSize.Y);


                if (enemyRect.Intersects(linkRect))
                {
                    collisionFound = true;
                }

            }

            return collisionFound;

        }

      
        public bool linkCollisionDetectionEffect()
        {
            linkRect = new Rectangle((int)link.SpriteLink.GetPosition.X, (int)link.SpriteLink.GetPosition.Y, (int)link.SpriteLink.GetSize.X, (int)link.SpriteLink.GetSize.Y);
            bool collisionFound = false;

            for (int j = 0; j < effects.Count; j++)
            {

                effectRect = new Rectangle((int)effects.ElementAt(j).GetPosition.X, (int)effects.ElementAt(j).GetPosition.Y, (int)effects.ElementAt(j).GetSize.X, (int)effects.ElementAt(j).GetSize.Y);
                if (linkRect.Intersects(itemRect))
                {
                    collisionFound = true;
                }
            }
            return collisionFound;
        }

        public bool linkCollisionDetectionItem()
        {
            bool collisionFound = false;
            linkRect = new Rectangle((int)link.SpriteLink.GetPosition.X, (int)link.SpriteLink.GetPosition.Y, (int)link.SpriteLink.GetSize.X, (int)link.SpriteLink.GetSize.Y);
            for (int k = 0; k < items.Length; k++)
            {
                itemRect = new Rectangle((int)items[k].Sprite.GetPosition.X, (int)items[k].Sprite.GetPosition.Y, (int)items[k].Sprite.GetSize.X, (int)items[k].Sprite.GetSize.Y);
                if (linkRect.Intersects(itemRect))
                {
                    collisionFound = true;
                }
            }
            return collisionFound;

        } 
    }
}
