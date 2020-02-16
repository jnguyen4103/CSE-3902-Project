using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint02
{
    public interface ISprite
    {
        void DrawSprite();
        void MoveToPosition(Vector2 newPosition);
        void UpdatePosition(Vector2 newPosition);
    }
}
