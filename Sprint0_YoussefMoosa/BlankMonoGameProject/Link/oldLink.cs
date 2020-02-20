using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint02
{
    public class oldLink : Game
    {
        //public variables
        public int healthTotal = 3;
        public Tuple<string, int>[] ItemList { get; set; }
        public Vector2 position;
        public int speed = 1;
        public SpriteBatch batch;
        public ISprite[] myAnimatedSprite;
        public ISprite[] myNonAnimatedSprite;
        public ISprite mySprite;
        //private variables
        public oldLs stateMachine;

        //constructor 
        // TODO: add default sprite instanciation
        public oldLink(Game1 game, ISprite[] nonAnSprite, ISprite[] anSprite)
        {
            //batch = game.spriteBatch;
            stateMachine = new oldLs();
            myNonAnimatedSprite = nonAnSprite;
            myAnimatedSprite = anSprite;
            mySprite = myNonAnimatedSprite[0];
            position = new Vector2(stateMachine.linkSource.X, stateMachine.linkSource.Y);
        }


        // Move methods

        public void idleRight()
        {
            mySprite = myNonAnimatedSprite[0];
        }
        public void idleLeft()
        {
            mySprite = myNonAnimatedSprite[1];
        }
        public void idleUp()
        {
            mySprite = myNonAnimatedSprite[2];
        }
        public void idleDown()
        {
            mySprite = myNonAnimatedSprite[3];
        }
        public void MoveRight()
        {
            stateMachine.ChangeRight();
            mySprite = myNonAnimatedSprite[0];
            stateMachine.moveRight();
            mySprite = myAnimatedSprite[0];
            stateMachine.ChangeRight();
        }

        public void MoveLeft()
        {
            stateMachine.ChangeLeft();
            mySprite = myNonAnimatedSprite[1];
            stateMachine.moveLeft();
            mySprite = myAnimatedSprite[1];
            stateMachine.ChangeLeft();


        }

        public void MoveUp()
        {
            stateMachine.ChangeUp();
            mySprite = myNonAnimatedSprite[2];
            stateMachine.moveUp();
            mySprite = myAnimatedSprite[2];
            stateMachine.ChangeUp();

        }

        public void MoveDown()
        {
            stateMachine.ChangeDown();
            mySprite = myNonAnimatedSprite[3];
            stateMachine.moveDown();
            mySprite = myAnimatedSprite[3];
            stateMachine.ChangeDown();

        }

        // Changes sprite reference to the hurt version
        // TODO: implement - not done as of sprint2
        /*
		* Animated: WR, WL, WU, WD
		*           AR, AL, AU, AD
		*            HU
		*        
		* IDLE        R,L,U,D
		*/
        public void BeHurt()
        {

            mySprite = myAnimatedSprite[8];
            stateMachine.beHurt();

        }

        // Changes sprite to attack sprite based on direction
        // TODO: Add collision logic/handling 
        // TODO: implement - not done as of sprint2
        /*
		* Animated: WR, WL, WU, WD
		*           AR, AL, AU, AD
		*            HU
		*        
		* IDLE        R,L,U,D
		*/
        public void Attack()
        {
            if (stateMachine.isAttacking)
            {
                switch (stateMachine.facingDirection)
                {
                    case oldLs.Direction.Down:
                        mySprite = myAnimatedSprite[7];
                        break;
                    case oldLs.Direction.Up:
                        mySprite = myAnimatedSprite[6];
                        break;
                    case oldLs.Direction.Left:
                        mySprite = myAnimatedSprite[5];
                        break;
                    case oldLs.Direction.Right:
                        mySprite = myAnimatedSprite[4];
                        break;
                    default:
                        break;
                }
            }
            stateMachine.Attack();
        }

        // Updates: item list, status of link/player, and sprite
        // TODO: implement - empty as of sprint2

        //public void UseItem()
        //{

        //}

        //public void Update()
        //{

        //}

        // TODO: hide info better?

        public void Draw()
        {
            //mySprite.Update();
            //mySprite.Draw(batch, position);

        }

    }
}