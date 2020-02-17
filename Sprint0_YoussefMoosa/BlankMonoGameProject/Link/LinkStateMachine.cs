using Microsoft.Xna.Framework;
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
            Link.SpriteLink.updateLinkDirection(0);
            Link.SpriteLink.UpdateLocation(directionMoving);
        }

        public void DownState()
        {
            directionMoving.X = 0;
            directionMoving.Y = 0.6f;
            Link.Direction = Link.LinkDirection.Down;
            Link.SpriteLink.updateLinkDirection(1);
            Link.SpriteLink.UpdateLocation(directionMoving);
        }

        public void LeftState()
        {
            directionMoving.X = -0.6f;
            directionMoving.Y = 0;
            Link.Direction = Link.LinkDirection.Left;
            Link.SpriteLink.updateLinkDirection(3);
            Link.SpriteLink.UpdateLocation(directionMoving);
        }

        public void RightState()
        {
            directionMoving.X = 0.6f;
            directionMoving.Y = 0;
            Link.Direction = Link.LinkDirection.Right;
            Link.SpriteLink.updateLinkDirection(2);
            Link.SpriteLink.UpdateLocation(directionMoving);
        }

        public void IdleState()
        {
            Link.State = Link.LinkState.Idle;
            directionMoving.X = 0;
            directionMoving.Y = 0;
        }

        public void AttackState()
        {
        }

        public void DamagedState()
        {
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
