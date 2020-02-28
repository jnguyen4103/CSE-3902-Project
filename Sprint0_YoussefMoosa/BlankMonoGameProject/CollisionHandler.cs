using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class CollisionHandler
    {
        CollisionDetection detector;
        public CollisionHandler(CollisionDetection collisionDetection)
        {
            detector = collisionDetection;
        }


        /* These three handle what happens when a link interacts with 
            an item, monster or effect 
        */
        public void handleLinkHitByMonster()
        {
            if(detector.linkCollisionDetectionMonster())
            {
                /* Perform Action
                    This is just a test  
                */
                Console.WriteLine("LINK WAS HIT BY A MONSTER");
            }
        }
        public void handleLinkHitByEffect()
        {
            if(detector.linkCollisionDetectionEffect())
            {
                Console.WriteLine("LINK WAS HIT BY AN EFFECT");
            }
        }
        public void handleLinkGetsItem()
        {
            if(detector.linkCollisionDetectionItem())
            {
                Console.WriteLine("LINK PICKED UP AN ITEM ");
            }
        }

        /*
         * Monster Collision
         */
         public void handleMonsterHitByLink()
        {
            if(detector.mosterCollisionDetection())
            {
                Console.WriteLine("MONSTER WAS HIT");
            }
        }

        public void Update()
        {
            handleLinkHitByMonster();
            handleLinkHitByEffect();
            handleLinkGetsItem();
            handleMonsterHitByLink();
        }
    }
}
