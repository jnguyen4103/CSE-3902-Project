using Microsoft.Xna.Framework;

namespace Sprint03
{
    public interface IEffect
    {
        Sprite Sprite {get; set;}
        void CreateEffect();
    }
}