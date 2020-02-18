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
            directionMoving.X = 0;
            directionMoving.Y = -0.6f;
            Link.Direction = Link.LinkDirection.Up;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[0], true, 0);
        }

        public void DownState()
        {
            directionMoving.X = 0;
            directionMoving.Y = 0.6f;
            Link.Direction = Link.LinkDirection.Down;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[1], true, 0);
        }

        public void LeftState()
        {
            directionMoving.X = -0.6f;
            directionMoving.Y = 0;
            Link.Direction = Link.LinkDirection.Left;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[3], true, 0);
        }

        public void RightState()
        {
            directionMoving.X = 0.6f;
            directionMoving.Y = 0;
            Link.Direction = Link.LinkDirection.Right;
            Link.SpriteLink.UpdateLocation(directionMoving);
            Link.SpriteLink.UpdateLinkAnimationFrames(Link.SpriteLink.AnimationFrames[2], true, 0);
        }

        public void IdleState()
        {
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
            Link.State = Link.LinkState.Attacking;
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
            Link.monoProcess.Link = new AttackingLink(Link, Link.monoProcess);
        }


        public void DamagedState()
        {
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

        public void UsingItemState()
        {

        }

        public void UseItem(int itemPosition)
        {
            Link.State = Link.LinkState.UsingItem;
            int xVelocity = 0;
            int yVelocity = 0;

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

            Link.Secondaries[itemPosition].createEffectSprite(Link.SpriteLink.position, xVelocity, yVelocity);
        }
    }
}
