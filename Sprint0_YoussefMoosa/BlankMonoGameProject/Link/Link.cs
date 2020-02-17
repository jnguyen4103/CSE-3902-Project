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

        public IEffect[] Secondaries;
        public LinkState State = LinkState.Idle;
        public LinkDirection Direction = LinkDirection.Down;
        public LinkSprite SpriteLink;
        public LinkStateMachine StateMachine;
        public int hitpoints;
        public int maxHP = 3;


        public Link(LinkSprite sprite, IEffect[] _secondaries)
        {
            Secondaries = _secondaries;
            SpriteLink = sprite;
            hitpoints = 3;
            State = LinkState.Idle;
            StateMachine = new LinkStateMachine(this);
        }

        public void TakeDamage()
        {
            throw new NotImplementedException();
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
                    StateMachine.IdleState();
                    break;
                case (LinkState.Damaged):
                    StateMachine.DamagedState();
                    break;
                case (LinkState.Attacking):
                    StateMachine.AttackState();
                    break;
                case (LinkState.UsingItem):
                    StateMachine.UsingItemState();
                    break;
                default:
                    State = LinkState.Idle;
                    break;
            }
        }
    }
}
