/* Contributors
* Stephen Hogg
*/
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
        Color Colour { get; set; }
        void DrawSprite();
        void ChangeSpriteAnimation(string name);
        void Remove();
    }
}