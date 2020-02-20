using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace Sprint03
{

    public class oldLs : Game
    {
        //will be responsible for loading textures for sprites
        public enum Direction { Left, Right, Up, Down };
        public Direction facingDirection = Direction.Right;
        public ISprite mySprite;
        public bool isHurt = false;
        public bool isAttacking = true;
        public Rectangle linkSource = new Rectangle(100, 100, 100, 100);


        public oldLs()
        {

        }


        public void ChangeLeft()
        {
            facingDirection = Direction.Left;
        }

        public void ChangeRight()
        {
            facingDirection = Direction.Right;
        }

        public void ChangeUp()
        {
            facingDirection = Direction.Up;
        }

        public void ChangeDown()
        {
            facingDirection = Direction.Down;
        }


        public void Attack()
        {
            isAttacking = !isAttacking;

        }

        public void moveRight()
        {
            ChangeRight();
            linkSource.X += 10;
            ChangeRight();
        }

        public void moveLeft()
        {
            ChangeLeft();
            linkSource.X -= 10;
            ChangeLeft();
        }

        public void moveUp()
        {
            ChangeUp();
            linkSource.Y -= 10;
            ChangeUp();
        }

        public void moveDown()
        {
            ChangeDown();
            linkSource.Y += 10;
            ChangeDown();

        }

        public void beHurt()
        {
            isHurt = !isHurt;

        }
    }
}