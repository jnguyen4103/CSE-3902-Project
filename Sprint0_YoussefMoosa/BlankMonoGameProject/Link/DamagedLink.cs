using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public class DamagedLink : ILink
    {

        public LinkSprite Sprite;
        public LinkStateMachine LinkSM;
        public Link decoratedLink;
        public Game1 monoProcess;
        int DamageTimer = 0;


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



        public DamagedLink(Link _link, Game1 monoInstance)
        {
            decoratedLink = _link;
            monoProcess = monoInstance;
            Sprite = _link.SpriteLink;
            LinkSM = _link.LinkSM;
        }

        public void TakeDamage()
        {
 
        }

        public void Draw()
        {
            this.Update();
            Sprite.DrawSprite();
        }

        public void Update()
        {
            DamageTimer++;
            if(DamageTimer > 180)
            {
                DamageTimer = 0;
                RemoveDecorator();
                monoProcess.Link.StateMachine.IdleState();

            }
        }

        public void RemoveDecorator()
        {
            monoProcess.Link = decoratedLink;
        }
    }
}
