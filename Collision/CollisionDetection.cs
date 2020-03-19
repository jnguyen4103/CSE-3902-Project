using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace Sprint03
{
    public class CollisionDetection
    {
        private Game1 Game;
        public CollisionDetection(Game1 game)
        {
            Game = game;
        }

        public void Update()
        {
            ObjectCollision();
            BlockCollision();
            BoundaryCollision();
            DoorCollision();
            TransitionCollision();
            MovableBlockCollision();
            TrapCollision();
            ItemCollision(); 
        }

        private void ObjectCollision()
        {
            foreach(Monster monster in Game.Dungeon01.Monsters.ToArray())
            {
                if(monster.Hitbox.Intersects(Game.Link.Hitbox))
                {
                    CollisionHandler.MonsterHitLink(monster, Game.Link);
                }
            }

            foreach (IAttack attack in Game.Dungeon01.Attacks.ToArray())
            {
                if (attack.Hitbox.Intersects(Game.Link.Hitbox))
                {
                    CollisionHandler.AttackHitLink(Game, attack, Game.Link);
                }
  

                foreach (Monster monster in Game.Dungeon01.Monsters)
                {
                    if (monster.Hitbox.Intersects(attack.Hitbox))
                    {
                        CollisionHandler.AttackHitMonster(attack, monster);
                    }
                }
            }
        }

        private void BlockCollision()
        {
            foreach(Rectangle block in Game.Dungeon01.Blocks)
            {
                if(block.Intersects(Game.Link.Hitbox))
                {
                    CollisionHandler.LinkHitBlock(Game.Link, block);
                }

                foreach(Monster monster in Game.Dungeon01.Monsters)
                {
                    if(block.Intersects(monster.Hitbox))
                    {
                        CollisionHandler.MonsterHitBlock(monster, block);
                    }
                }
            }
        }

        private void BoundaryCollision()
        {
            foreach (Rectangle wall in Game.Dungeon01.Walls)
            {
                if (wall.Intersects(Game.Link.Hitbox))
                {
                    CollisionHandler.LinkHitBlock(Game.Link, wall);
                }
                
                foreach(IAttack attack in Game.Dungeon01.Attacks.ToArray())
                {
                    if(attack.Hitbox.Intersects(wall))
                    {
                        attack.OnHit();
                    }
                }
                foreach(IItem item in Game.Dungeon01.Items)
                {
                    if(item.Hitbox.Intersects(wall))
                    {
                        CollisionHandler.ItemHitWall(item,wall);
                    }
                }

                foreach (Monster monster in Game.Dungeon01.Monsters)
                {
                    if (wall.Intersects(monster.Hitbox))
                    {
                        CollisionHandler.MonsterHitWall(monster, wall);
                    }
                }

                foreach(ITrap trap in Game.Dungeon01.Traps)
                {
                    if(wall.Intersects(trap.Hitbox)) {
                        CollisionHandler.TrapHitWall(trap, wall);
                    }
                }
            }
        }

        private void DoorCollision()
        {
            foreach (IDoor door in Game.Dungeon01.Doors.ToArray())
            {
                if(door.Hitbox.Intersects(Game.Link.Hitbox))
                {
                    door.OnContact("Link");
                    if(!door.Sprite.Colour.Equals(Color.TransparentBlack))
                    {
                        CollisionHandler.LinkHitBlock(Game.Link, door.Hitbox);
                    }
                }

                foreach(IAttack attack in Game.Dungeon01.Attacks.ToArray())
                {
                    if (door.Hitbox.Intersects(attack.Hitbox))
                    {
                        if (attack.Sprite.Name.EndsWith("ExplosionEffect"))
                        {
                            door.OnContact("Explosion");
                        }
                        attack.OnHit();
                    }
 
                }
            }
        }

        private void MovableBlockCollision()
        {
            foreach(MovableBlock movable in Game.Dungeon01.Movables)
            {
                foreach(Monster monster in Game.Dungeon01.Monsters)
                {
                    if(monster.Hitbox.Intersects(movable.Hitbox))
                    {
                        CollisionHandler.MonsterHitBlock(monster, movable.Hitbox);
                    }
                }

                if(Game.Link.Hitbox.Intersects(movable.Hitbox))
                {
                    CollisionHandler.LinkMoveBlock(Game.Link, movable);
                }
            }
        }

        private void TrapCollision()
        { 
            foreach(ITrap trap in Game.Dungeon01.Traps)
            {
                if (trap.Hitbox.Intersects(Game.Link.Hitbox))
                {
                    CollisionHandler.TrapHitLink(Game, trap, Game.Link);
                }
            }
        }

        private void ItemCollision()
        {
            foreach(IItem item in Game.Dungeon01.Items)
            {
                if(item.Hitbox.Intersects(Game.Link.Hitbox))
                {
                    item.ActivateItem();
                }
            }
        }


        private void TransitionCollision()
        {
            foreach (ScreenTransition transition in Game.Dungeon01.Transitions.ToArray())
            {
                if (Game.Link.Hitbox.Intersects(transition.Hitbox))
                {
                    Game.Dungeon01.TransitionToRoom(transition.NextRoom);
                }
            }
        }
    }
}
