
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
    public class AttackingLink : ILink
    {

        public LinkSprite Sprite;
        public LinkStateMachine LinkSM;
        public Link decoratedLink;
        public Game1 monoProcess;
        // Attack timer so the decorator is removed after certain amount of frames
        int AttackTimer = 0;


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



        public AttackingLink(Link _link, Game1 monoInstance)
        {
            decoratedLink = _link;
            monoProcess = monoInstance;
            Sprite = _link.SpriteLink;
            LinkSM = _link.LinkSM;
        }

        public void TakeDamage()
        {
            // You can be damaged while attacking
            // So it removes the attacking decorator and adds the damaged one
            RemoveDecorator();
            monoProcess.Link.StateMachine.DamagedState();
        }

        public void Draw()
        {
            this.Update();
            Sprite.DrawSprite();
        }

        public void Update()
        {
            // Leaves the attack animation after 10 frames
            AttackTimer++;
            if(AttackTimer == 10)
            {
                AttackTimer = 0;
                RemoveDecorator();
            }
        }

        public void RemoveDecorator()
        {
            monoProcess.Link = decoratedLink;
        }
    }
}


*/