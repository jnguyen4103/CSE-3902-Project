using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class VisualBag
    {
        private List<StaticSprite> sprites;

        public VisualBag() { sprites = new List<StaticSprite>(); }

        public void Add(StaticSprite sprite)
        {
            sprites.Add(sprite);
        }

        public void Draw()
        {
            foreach (StaticSprite spr in sprites)
            {
                spr.DrawSprite();
            }
        }
    }
}
