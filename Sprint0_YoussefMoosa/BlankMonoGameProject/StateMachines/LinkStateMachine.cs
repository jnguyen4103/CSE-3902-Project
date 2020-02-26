using Microsoft.Xna.Framework;

namespace Sprint03
{
    public class LinkStateMachine
    {
        Link Link;
        private int UseItemTimer = 0;
        public LinkStateMachine(Link _link)
        {
            Link = _link;
            UseItemTimer = 60 / Link.SpriteLink.FPS;
        }


        public void UpState()
        {
            Link.Direction = Link.LinkDirection.Up;
            Link.State = Link.LinkState.Moving;
            Link.SpriteLink.ChangeSpriteAnimation("WalkUp");
        }

        public void DownState()
        {
            Link.State = Link.LinkState.Moving;
            Link.Direction = Link.LinkDirection.Down;
            Link.SpriteLink.ChangeSpriteAnimation("WalkDown");
        }

        public void LeftState()
        {
            Link.Direction = Link.LinkDirection.Left;
            Link.State = Link.LinkState.Moving;
            Link.SpriteLink.ChangeSpriteAnimation("WalkLeft");
        }

        public void RightState()
        {
            Link.Direction = Link.LinkDirection.Right;
            Link.State = Link.LinkState.Moving;
            Link.SpriteLink.ChangeSpriteAnimation("WalkRight");
        }

        public void IdleState()
        {
            if (UseItemTimer <= (60 / Link.SpriteLink.FPS))
            {
                UseItemTimer++;
            } else
            {
                Link.State = Link.LinkState.Idle;
                Link.SpriteLink.ChangeSpriteAnimation("Walk" + GetDirection());
            }
        }

        public void AttackState()
        {
            // Update Link's object to be a decorator and changes the
            // animation frames so it corresponds with his current direction
            Link.State = Link.LinkState.Attacking;
            Link.Game.Link = new AttackingLink(Link, Link.Game, Link.SpriteLink.Name, Link.Direction);
            Link.SpriteLink.ChangeSpriteAnimation("Effect" + GetDirection());
        }



        public void DamagedState()
        {
            // Gives Link the damaged decorator while also changing his animation frames
            // to the damaged onces.

            Link.State = Link.LinkState.Damaged;
            Link.Game.Link = new DamagedLink(Link, Link.Game);
        }



        public void UseArrow()
        {
            IEffect Arrow = new ArrowEffect(Link.SpriteLink, Link.Game, Link.Direction, Link.Game.EffectSpriteSheet, Link.Game.spriteBatch);
            Link.SpriteLink.ChangeSpriteAnimation("Effect" + GetDirection());
            UseItemTimer = 0;
            Arrow.CreateEffect();
            Link.State = Link.LinkState.Idle;
        }

        public void UseBoomerang()
        {
            IEffect Boomerang = new BoomerangEffect(Link.SpriteLink, Link.Game, Link.Direction, Link.Game.EffectSpriteSheet, Link.Game.spriteBatch);
            Link.SpriteLink.ChangeSpriteAnimation("Effect" + GetDirection());
            UseItemTimer = 0;
            Boomerang.CreateEffect();
            Link.State = Link.LinkState.Idle;
        }

        private string GetDirection()
        {
            string dir = "";
            switch (Link.Direction)
            {
                case (Link.LinkDirection.Down):
                    dir = "Down";
                    break;
                case (Link.LinkDirection.Up):
                    dir = "Up";
                    break;
                case (Link.LinkDirection.Left):
                    dir = "Left";
                    break;
                case (Link.LinkDirection.Right):
                    dir = "Right";
                    break;
                default:
                    break;
            }
            return dir;
        }
    }
}
