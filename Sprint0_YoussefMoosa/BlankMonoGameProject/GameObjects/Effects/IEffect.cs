using Microsoft.Xna.Framework;

namespace Sprint03
{
    public interface IEffect
    {
        Sprite Sprite {get; set;}
        int Damage { get; set; }
        void CreateEffect();
        bool IsCreator(Sprite sprite);
    }
}