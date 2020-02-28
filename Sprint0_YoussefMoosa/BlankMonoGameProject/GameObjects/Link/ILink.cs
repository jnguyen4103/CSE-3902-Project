using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public interface ILink
    {
        // Allows for public access of Link's state machine and sprite for his decorators
        LinkStateMachine StateMachine { get; set; }
        LinkSprite SpriteLink { get; set; }
        int HP { get; set; }
        int MaxHP { get; set; }
        void Update();
        void Draw();
        void TakeDamage();
    }
}