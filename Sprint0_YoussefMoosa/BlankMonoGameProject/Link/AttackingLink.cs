using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class AttackingLink : ILink
    {

        public LinkSprite Sprite;
        public LinkStateMachine LinkSM;
        public Link decoratedLink;
        public Game1 monoProcess;
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
