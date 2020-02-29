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
        public ILink link;
        public Monster[] enemies;
        public Item[] items;
        public List<ISprite> effects;
        public 
    
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

        private String collisionFound(Sprite A, Sprite B)
        {
            //Determines if there is an interesection between two Rectangles 
            Rectangle ARectangle = new Rectangle((int)A.GetPosition.X, (int)A.GetPosition.Y, (int)A.GetSize.X, (int)A.GetSize.Y);
            Rectangle BRectangle = new Rectangle((int)B.GetPosition.X, (int)B.GetPosition.Y, (int)B.GetSize.X, (int)B.GetSize.Y);

            String toReturn = "NONE";
            if(ARectangle.Right< BRectangle.Left)
            {
                toReturn = "RIGHT"; 
            }
            else if(ARectangle.Left>BRectangle.Right)
            {
                toReturn = "LEFT";
            }
            else if(ARectangle.Top<BRectangle.Bottom)
            {
                toReturn = "TOP";
            }
            else if(ARectangle.Bottom>BRectangle.Top)
            {
                toReturn = "BOTTOM";
            }
            else
            {
                toReturn = "NONE";
            }
            return toReturn;
        }

        public void collisionHandler()
        {
            
        }
        /*
        public bool linkCollisionDetectionMonster()
        {

            bool collisionFound = false;

            linkRect = new Rectangle((int)link.SpriteLink.GetPosition.X, (int)link.SpriteLink.GetPosition.Y, (int)link.SpriteLink.GetSize.X, (int)link.SpriteLink.GetSize.Y);

            for (int i = 0; i < enemies.Length; i++)
            {

                *//*Create Rectangel for the current enemey *//*
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

        } */
    }
}
