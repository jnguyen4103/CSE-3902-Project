/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class Link : ILink
    {

        // Basic states that control what action he is doing
        // Idle just means he is not taking damage, attacking or using items.
        // The idle allows for movement
        public enum LinkState
        {
            Attacking,
            Idle,
            Damaged,
            UsingItem
        }

        // 4 directional states of Link, these are used to determine which directional
        // sprite to drawn in certain states
        public enum LinkDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        // Creating references to Link's Sprite and StateMachine
        public LinkSprite Sprite;
        public LinkStateMachine LinkSM;

        // Creating a reference to the Game so Link's decorators can
        // have access to link by using monoProcess
        public Game1 monoProcess;

        // List of Link's secondary weapons
        public IEffect[] Secondaries;

        // Setting initial action and movement states
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
            // Based on which action state Link is in, it'll call it's respective function
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
*/