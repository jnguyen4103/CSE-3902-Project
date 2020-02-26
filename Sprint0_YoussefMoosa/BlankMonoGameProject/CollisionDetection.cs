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
        Link link;
        NPC[] enemies;
        ItemFactory[] items;
        IEffect[] effects;
        Rectangle linkRect;
        Rectangle enemyRect;
        Rectangle itemRect;
        Rectangle effectRect;
        
        public CollisionDetection(NPC[] Enemies,Link Link, ItemFactory[] Items,IEffect[] Effects)
        {
            enemies = Enemies;
            link = Link;
            items = Items;
            effects = Effects;
        }

        public bool mosterCollisionDetection()
        {
            /*Go over the  list of enemies */
            bool collisionFound = false;

            linkRect = new Rectangle((int)link.Sprite.Position.X, (int)link.Sprite.Position.Y, (int)link.Sprite.GetSize.X, (int)link.Sprite.GetSize.Y);

            for(int i =0; i< enemies.Length; i++)
            {

                /*Create Rectangel for the current enemey */
                enemyRect = new Rectangle((int)enemies[i].Sprite.Position.X, (int)enemies[i].Sprite.Position.Y, (int)enemies[i].Sprite.GetSize.X, (int)enemies[i].Sprite.GetSize.Y);
                

                /*Go over the list of items*/

                for (int j = 0; j< effects.Length; j++ )
                {

                    /*Create Rectangle for effect*/
                    effectRect = new Rectangle(effects[j].);
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

        public void linkCollisionDetection()
        {

        }
        
        public void itemCollisionDetection()
        {

        }
    }
}
