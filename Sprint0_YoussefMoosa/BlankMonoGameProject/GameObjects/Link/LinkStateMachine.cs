namespace Sprint03
{
    public class LinkStateMachine
    {
        Link Link;
        public LinkStateMachine(Link _link)
        {
            Link = _link;
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
            Link.State = Link.LinkState.Idle;
            Link.SpriteLink.ChangeSpriteAnimation("Walk" + GetDirection());
        }

        public void AttackState()
        {
            if (Link.State == Link.LinkState.Moving || Link.State == Link.LinkState.Idle)
            {
                Link.State = Link.LinkState.Attacking;
                Link.Game.Link = new AttackingLink(Link, Link.Game, Link.SpriteLink.Name, Link.Direction);
                Link.SpriteLink.ChangeSpriteAnimation("Effect" + GetDirection());
            }
        }



        public void DamagedState(int direction)
        {
            Link.State = Link.LinkState.Damaged;
            Link.Game.Link = new DamagedLink(Link, Link.Game, Link.SpriteLink.Name, Link.Direction, direction);
            Link.SpriteLink.ChangeSpriteAnimation("Damaged" + GetDirection());
        }

        public void DeadState()
        {
            IdleState();
            Link.Game.keyboardCommands[7].Execute();
        }



        public void UseArrow()
        {
            if(Link.State == Link.LinkState.Moving || Link.State == Link.LinkState.Idle)
            {
                Link.State = Link.LinkState.UsingSecondary;
                IEffect Arrow = new ArrowEffect(Link.SpriteLink, Link.Game, Link.Direction, Link.Game.EffectSpriteSheet, Link.Game.spriteBatch);
                Link.Game.Link = new UseSecondaryLink(Link, Link.Game, Link.SpriteLink.Name, Link.Direction, Arrow);
            }
        }

        public void UseBoomerang()
        {
            if (Link.State == Link.LinkState.Moving || Link.State == Link.LinkState.Idle)
            {
                Link.State = Link.LinkState.UsingSecondary;
                IEffect Boomerang = new BoomerangEffect(Link.SpriteLink, Link.Game, Link.Direction, Link.Game.EffectSpriteSheet, Link.Game.spriteBatch);
                Link.Game.Link = new UseSecondaryLink(Link, Link.Game, Link.SpriteLink.Name, Link.Direction, Boomerang);
            }
        }

        public void CatchBoomerang(IEffect boomerang)
        {
            if (Link.State == Link.LinkState.Moving || Link.State == Link.LinkState.Idle)
            {
                Link.State = Link.LinkState.UsingSecondary;
                Link.Game.Link = new UseSecondaryLink(Link, Link.Game, Link.SpriteLink.Name, Link.Direction, null);
                boomerang.Sprite.KillSprite();
            }
        }


        public string GetDirection()
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
