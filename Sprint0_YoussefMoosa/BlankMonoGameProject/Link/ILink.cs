using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public interface ILink
    {
        LinkStateMachine StateMachine { get; set; }
        LinkSprite SpriteLink { get; set; }
        void Update();
        void Draw();
        void TakeDamage();
    }
}
