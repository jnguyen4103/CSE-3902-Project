using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public interface ISprite
    {
        Vector2 GetPosition { get; }
        Vector2 GetSize { get; }
        void DrawSprite();
        void Move();
        void Animate();
        void ChangeSpriteAnimation(string newSpriteName);
    }
}