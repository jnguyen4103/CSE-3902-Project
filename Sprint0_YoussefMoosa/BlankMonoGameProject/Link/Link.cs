using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class Link : ILink
    {

        public enum LinkState
        {
            Attacking,
            Idle,
            Damaged,
            UsingItem
        }

        public enum LinkDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public LinkSprite Sprite;
        public LinkStateMachine LinkSM;
        public Game1 monoProcess;
        public IEffect[] Secondaries;
        public LinkState State = LinkState.Idle;
        public LinkDirection Direction = LinkDirection.Down;
        public int hitpoints;
        public int maxHP = 3;

        LinkStateMachine ILink.StateMachine
        {
            get { return LinkSM; }
            set { }
        }

        public LinkSprite SpriteLink
        {
            get { return Sprite; }
            set { }
        }



        public Link(LinkSprite sprite, IEffect[] _secondaries, Game1 monoInstance)
        {
            Secondaries = _secondaries;
            monoProcess = monoInstance;
            Sprite = sprite;
            hitpoints = 3;
            State = LinkState.Idle;
            LinkSM = new LinkStateMachine(this);
        }

        public void TakeDamage()
        {
            LinkSM.DamagedState();
        }

        public void Draw()
        {
            this.Update();
            SpriteLink.DrawSprite();
        }

        public void Update()
        {

            switch (State)
            {
                case (LinkState.Idle):
                    LinkSM.IdleState();
                    break;
                case (LinkState.Damaged):
                    LinkSM.DamagedState();
                    break;
                case (LinkState.Attacking):
                    LinkSM.AttackState();
                    State = LinkState.Idle;
                    break;
                case (LinkState.UsingItem):
                    LinkSM.UsingItemState();
                    break;
                default:
                    State = LinkState.Idle;
                    break;
            }
        }
    }
}
