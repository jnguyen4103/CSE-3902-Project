using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class CollisionDetection
    {
        public Link link;
        public Monster[] enemies;
        public ItemFactory[] items;
        public List<ISprite> effects;
        Rectangle linkRect;
        Rectangle enemyRect;
        Rectangle itemRect;
        Rectangle effectRect;
        
        public CollisionDetection(NPC[] Enemies,Link Link, ItemFactory[] Items,List<ISprite> Effects)
        {
            this.enemies = Enemies;
            Console.WriteLine(enemies.Length);
            this.link = Link;
            this.items = Items;
            this.effects = Effects;
        }

        public bool mosterCollisionDetection()
        {
            /*Go over the  list of enemies */
            bool collisionFound = false;

            linkRect = new Rectangle((int)link.Sprite.Position.X, (int)link.Sprite.Position.Y, (int)link.Sprite.GetSize.X, (int)link.Sprite.GetSize.Y);

            for (int i = 0; i < enemies.Length; i++)
            {

                /*Create Rectangel for the current enemey */
                enemyRect = new Rectangle((int)enemies[i].Sprite.Position.X, (int)enemies[i].Sprite.Position.Y, (int)enemies[i].Sprite.GetSize.X, (int)enemies[i].Sprite.GetSize.Y);


                /*Go over the list of items*/


                for (int j = 0; j < effects.Count; j++)
                {

                    /*Create Rectangle for effect*/
                    effectRect = new Rectangle((int)effects[j].Position.X,(int)effects[j].Position.Y,(int)effects[j].GetSize.X,(int)effects[j].GetSize.Y);
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

            linkRect = new Rectangle((int)link.Sprite.Position.X, (int)link.Sprite.Position.Y, (int)link.Sprite.GetSize.X, (int)link.Sprite.GetSize.Y);

            for (int i = 0; i < enemies.Length; i++)
            {

                /*Create Rectangel for the current enemey */
                enemyRect = new Rectangle((int)enemies[i].Sprite.Position.X, (int)enemies[i].Sprite.Position.Y, (int)enemies[i].Sprite.GetSize.X, (int)enemies[i].Sprite.GetSize.Y);


                if (enemyRect.Intersects(linkRect))
                {
                    collisionFound = true;
                }

            }

            return collisionFound;

        }

      /*
        public bool linkCollisionDetectionEffect()
        {
            linkRect = new Rectangle((int)link.Sprite.Position.X, (int)link.Sprite.Position.Y, (int)link.Sprite.GetSize.X, (int)link.Sprite.GetSize.Y);
            bool collisionFound = false;

            for (int j = 0; j < effects.Count; j++)
            {

                /*Create Rectangle for effect*/
                effectRect = new Rectangle((int)effects[j].Position.X, (int)effects[j].Position.Y, (int)effects[j].GetSize.X, (int)effects[j].GetSize.Y);
                if (linkRect.Intersects(itemRect))
                {
                    collisionFound = true;
                }
            }
            return collisionFound;
        }

        public bool linkCollisionDetectionItem()
        {
            linkRect = new Rectangle((int)link.Sprite.Position.X, (int)link.Sprite.Position.Y, (int)link.Sprite.GetSize.X, (int)link.Sprite.GetSize.Y);
            bool collisionFound = false;
            for (int k = 0; k < items.Length; k++)
            {
                itemRect = new Rectangle((int)items[k].itemLocation.X, (int)items[k].itemLocation.Y, (int)items[k].itemTexture.Width, (int)items[k].itemTexture.Height);
                if (linkRect.Intersects(itemRect))
                {
                    collisionFound = true;
                }
            }
            return collisionFound;

        } */
    }
}
