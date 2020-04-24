/* Contributors
* Nico Negrete
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class CollisionHandler
    {
        public static void MonsterHitLink(Monster monster, ILink link)
        {
            if (monster.CanDamage && link.State != States.LinkState.Damaged && link.State != States.LinkState.Dead)
            {
                States.Direction knockbackDirection = DirectionResolution(monster, link);
                link.TakeDamage(knockbackDirection, monster.AttackDamage);
            }
        }

        public static void AttackHitMonster(IAttack attack, Monster monster)
        {
            if (attack.IsCreator(monster) && !attack.CanDamage)
            {
                attack.Sprite.Remove();
            }

            else if (!attack.IsCreator(monster) && attack.CanDamage && monster.State != States.MonsterState.Damaged && monster.State != States.MonsterState.Dead)
            {
                States.Direction knockbackDirection = DirectionResolution(attack, monster);
                monster.TakeDamage(knockbackDirection, attack.Damage);
                attack.OnHit();
            }
        }

        public static void AttackHitLink(Game1 game, IAttack attack, ILink link)
        {
            if(!attack.IsCreator(link))
            {
                if (attack.CanDamage)
                {
                    States.Direction knockbackDirection = DirectionResolution(attack, link);
                    link.TakeDamage(knockbackDirection, attack.Damage);
                    attack.OnHit();

                }
            }
            else if (attack.Sprite.Name.Equals("BoomerangEffect") || attack.Sprite.Name.Equals("ProjectileHit"))
            {
                attack.Sprite.Remove();
            }

        }

        public static void TrapHitLink(Game1 game, ITrap trap, ILink link)
        {
            if (trap.CanDamage)
            {
                States.Direction knockbackDirection = DirectionResolution(trap, link);
                link.TakeDamage(knockbackDirection, trap.Damage);
            }
            trap.OnHit();
        }


        public static void LinkHitBlock(ILink link, Rectangle block)
        {
            GetOffBlock(link, block);
        }

        public static void MonsterHitBlock(Monster monster, Rectangle block)
        {
            if(!monster.Flies)
            {
                GetOffBlock(monster, block);
                monster.StateMachine.ResetMovement(monster.Direction);
            }
        }

        public static void MonsterHitWall(Monster monster, Rectangle block)
        {
            GetOffBlock(monster, block);
            monster.StateMachine.ResetMovement(monster.Direction);
        }

        public static void ItemHitWall(IItem item, Rectangle block)
        {
            GetOffBlock(item, block);
            
        }

        public static void ItemPickup(IItem item,ILink link)
        {
            item.ActivateItem();
            if(item.Sprite.Name == "Triforce" || item.Sprite.Name == "Skull")
                link.PickupItem();

        }

        public static void TrapHitWall(ITrap trap, Rectangle block)
        {
            GetOffBlock(trap, block);
        }

        public static void LinkMoveBlock(ILink link, MovableBlock block)
        {
            States.Direction direction = DirectionResolution(block, link);
            block.PushBlock(direction);
            GetOffBlock(link, block.Hitbox);
        }

        private static States.Direction DirectionResolution(IGameObject Attacker, IGameObject Damagee)
        {
            // Obtaining part of the Damagee's rectangle that was intersected
            Rectangle Intersection = Rectangle.Intersect(Attacker.Hitbox, Damagee.Hitbox);
            Intersection = Rectangle.Intersect(Intersection, Damagee.Hitbox);

            // Splitting hitbox into 4 sections, whichever the intersection has the most area in is the
            // direction the damagee was hit
            Rectangle Up = new Rectangle(Damagee.Hitbox.X, Damagee.Hitbox.Y, Damagee.Hitbox.Width, Damagee.Hitbox.Height/4);
            Rectangle Down = new Rectangle(Damagee.Hitbox.X, Damagee.Hitbox.Bottom - Damagee.Hitbox.Size.Y/4, Damagee.Hitbox.Width, Damagee.Hitbox.Height / 4);
            Rectangle Left = new Rectangle(Damagee.Hitbox.X, Damagee.Hitbox.Y, Damagee.Hitbox.Width / 4, Damagee.Hitbox.Height);
            Rectangle Right = new Rectangle(Damagee.Hitbox.Right - Damagee.Hitbox.Size.X/4, Damagee.Hitbox.Y, Damagee.Hitbox.Width / 4, Damagee.Hitbox.Height);


            List<KeyValuePair<States.Direction, float>> sortedArea = new List<KeyValuePair<States.Direction, float>>(4);
            sortedArea.Add(new KeyValuePair<States.Direction, float>(States.Direction.Up, GetArea(Rectangle.Intersect(Intersection, Up))));
            sortedArea.Add(new KeyValuePair<States.Direction, float>(States.Direction.Down, GetArea(Rectangle.Intersect(Intersection, Down))));
            sortedArea.Add(new KeyValuePair<States.Direction, float>(States.Direction.Left, GetArea(Rectangle.Intersect(Intersection, Left))));
            sortedArea.Add(new KeyValuePair<States.Direction, float>(States.Direction.Right, GetArea(Rectangle.Intersect(Intersection, Right))));
            sortedArea.Sort((p1, p2) => p1.Value.CompareTo(p2.Value));

            return sortedArea[3].Key;
        }

        private static float GetArea(Rectangle rec)
        {
            return (rec.Height * rec.Width);
        } 

        private static void GetOffBlock(IGameObject obj, Rectangle block)
        {
            Rectangle Intersection = Rectangle.Intersect(block, obj.Hitbox);
            Vector2 offset = new Vector2(0, 0);

            if (Intersection.Height > Intersection.Width)
            {
                // Hit block on right
                if (obj.Hitbox.Left < block.Right && obj.Hitbox.Left < block.Left)
                {
                    offset.X = -Intersection.Width;
                }

                // Hit block on left
                if (obj.Hitbox.Right > block.Left && obj.Hitbox.Left > block.Left)
                {
                    offset.X = Intersection.Width;
                }
            }
            else if (Intersection.Height < Intersection.Width)
            {
                // Hit block on top
                if (obj.Hitbox.Bottom > block.Top && obj.Hitbox.Top < block.Top)
                {
                    offset.Y = -Intersection.Height;
                }

                //Hit block on bottom
                if (obj.Hitbox.Top < block.Bottom && obj.Hitbox.Top > block.Top)
                {
                    offset.Y = Intersection.Height;
                }
            }
            else
            {
                // Hit block on right
                if (obj.Hitbox.Left < block.Right && obj.Hitbox.Left < block.Left)
                {
                    offset.X = -Intersection.Width;
                }

                // Hit block on left
                if (obj.Hitbox.Right > block.Left && obj.Hitbox.Left > block.Left)
                {
                    offset.X = Intersection.Width;
                }
                // Hit block on top
                if (obj.Hitbox.Bottom > block.Top && obj.Hitbox.Top < block.Top)
                {
                    offset.Y = -Intersection.Height;
                }

                //Hit block on bottom
                if (obj.Hitbox.Top < block.Bottom && obj.Hitbox.Top > block.Top)
                {
                    offset.Y = Intersection.Height;
                }
            }

            obj.Position += offset;            
        }
    }
}
