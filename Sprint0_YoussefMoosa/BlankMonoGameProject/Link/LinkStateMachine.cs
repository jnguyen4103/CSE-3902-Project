using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class LinkStateMachine
    {
        // Current velocity and direction Link is moving
        // vector is (0, 0) is Link is not moving
        public Vector2 directionMoving;
        Link Link;
        public LinkStateMachine(Link _link)
        {
            Link = _link;
            directionMoving.X = 0f;
            directionMoving.Y = 0f;
        }


        public void UpState()
        {
            // Moves up at speed 0.6 per frame
            directionMoving.X = 0;
            directionMoving.Y = -0.6f;
            Link.Direction = Link.LinkDirection.Up;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[0], true, 0);
        }

        public void DownState()
        {
            // Moves down at speed 0.6 per frame

            directionMoving.X = 0;
            directionMoving.Y = 0.6f;
            Link.Direction = Link.LinkDirection.Down;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[1], true, 0);
        }

        public void LeftState()
        {
            // Moves left at speed 0.6 per frame

            directionMoving.X = -0.6f;
            directionMoving.Y = 0;
            Link.Direction = Link.LinkDirection.Left;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[3], true, 0);
        }

        public void RightState()
        {
            // Moves right at speed 0.6 per frame

            directionMoving.X = 0.6f;
            directionMoving.Y = 0;
            Link.Direction = Link.LinkDirection.Right;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[2], true, 0);
        }

        public void IdleState()
        {
            // Changes Link state to be Idle and stops his movement
            // Also updates Link's animation frame to be consistant with what
            // direction he was facing. This is important when he attacks so he
            // will be facing in the same direction of his attack after the
            // animation ends 

            Link.State = Link.LinkState.Idle;
            directionMoving.X = 0;
            directionMoving.Y = 0;
            switch (Link.Direction)
            {
                case (Link.LinkDirection.Down):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[1], true, 0);
                    break;
                case (Link.LinkDirection.Up):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[0], true, 0);
                    break;
                case (Link.LinkDirection.Left):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[3], true, 0);
                    break;
                case (Link.LinkDirection.Right):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[2], true, 0);
                    break;
                default:
                    DownState();
                    break;
            }


        }

        public void AttackState()
        {
            // Update Link's object to be a decorator and changes the
            // animation frames so it corresponds with his current direction
            Link.State = Link.LinkState.Attacking;

            if (Link.hitpoints == Link.maxHP)
            {
                UseBeamSword();

            }

            else {
                switch (Link.Direction)
                {
                    case (Link.LinkDirection.Down):
                        Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[9], false, new SpriteEffects());
                        break;
                    case (Link.LinkDirection.Up):
                        Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[8], false, SpriteEffects.FlipVertically);
                        break;
                    case (Link.LinkDirection.Left):
                        Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[10], false, SpriteEffects.FlipHorizontally);
                        break;
                    case (Link.LinkDirection.Right):
                        Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[10], false, new SpriteEffects());
                        break;
                    default:
                        break;
                }
            }
            Link.monoProcess.Link = new AttackingLink(Link, Link.monoProcess);
            Link.State = Link.LinkState.Idle;
        }


        public void DamagedState()
        {
            // Gives Link the damaged decorator while also changing his animation frames
            // to the damaged onces.

            Link.State = Link.LinkState.Damaged;
            switch (Link.Direction)
            {
                case (Link.LinkDirection.Down):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[5], false, new SpriteEffects());
                    break;
                case (Link.LinkDirection.Up):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[4], false, new SpriteEffects());
                    break;
                case (Link.LinkDirection.Left):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[7], false, new SpriteEffects());
                    break;
                case (Link.LinkDirection.Right):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[6], false, new SpriteEffects());
                    break;
                default:
                    break;
            }
            Link.monoProcess.Link = new DamagedLink(Link, Link.monoProcess);
        }

        /*        public void UsingItemState()
                {
                    // TODO: Implement for sprint03
                    // Perhaps create a UsingItem decorator

                }*/


        public void UseArrow(int itemPosition)
        {
            // Spawns item on Link's position, throwing it in
            // his direction

            Link.State = Link.LinkState.UseArrow;


            int xVelocity = 0;
            int yVelocity = 0;

            // Switch statement determines velocity of item
            switch (Link.Direction)
            {
                case (Link.LinkDirection.Down):
                    yVelocity = 1;
                    itemPosition += 1;
                    break;
                case (Link.LinkDirection.Up):
                    yVelocity = -1;
                    break;
                case (Link.LinkDirection.Left):
                    xVelocity = -1;
                    itemPosition += 2;
                    break;
                case (Link.LinkDirection.Right):
                    xVelocity = 1;
                    itemPosition += 3;
                    break;
                default:
                    break;
            }


            Link.Secondaries[itemPosition].createEffectSprite(new Vector2(Link.SpriteLink.position.X + 5f, Link.SpriteLink.position.Y + 5f), xVelocity, yVelocity);
            Link.State = Link.LinkState.Idle;
        }

        public void UseBeamSword()
        {
            int xVelocity = 0;
            int yVelocity = 0;
            int itemPost = 5;
            switch (Link.Direction) {
                case (Link.LinkDirection.Down):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[9], false, new SpriteEffects());
                    yVelocity = 1;
                    itemPost += 1;
                    break;
                case (Link.LinkDirection.Up):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[8], false, SpriteEffects.FlipVertically);
                    yVelocity = -1;
                    itemPost += 0;
                    break;
                case (Link.LinkDirection.Left):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[10], false, SpriteEffects.FlipHorizontally);
                    xVelocity = -1;
                    itemPost += 2;
                    break;
                case (Link.LinkDirection.Right):
                    Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[10], false, new SpriteEffects());
                    xVelocity = 1;
                    itemPost += 3;
                    break;
                default:
                    break;
            }

            Link.Secondaries[itemPost].createEffectSprite(new Vector2(Link.SpriteLink.position.X + 5f, Link.SpriteLink.position.Y + 5f), xVelocity, yVelocity);

        
    }

    public void UseBoomerRange(int itemPosition)
        {
            // Spawns item on Link's position, throwing it in
            // his direction

            Link.State = Link.LinkState.UseBoomerRange;
            int xVelocity = 0;
            int yVelocity = 0;

            // Switch statement determines velocity of item
            switch (Link.Direction)
            {
                case (Link.LinkDirection.Down):
                    yVelocity = 1;
                    break;
                case (Link.LinkDirection.Up):
                    yVelocity = -1;
                    break;
                case (Link.LinkDirection.Left):
                    xVelocity = -1;
                    break;
                case (Link.LinkDirection.Right):
                    xVelocity = 1;
                    break;
                default:
                    break;
            }

            // The itemPosition parameter determines which secondary item Link will use
            Link.Secondaries[itemPosition].createEffectSprite(Link.SpriteLink.position, xVelocity, yVelocity);
            Link.State = Link.LinkState.Idle;
        }
    }
}
