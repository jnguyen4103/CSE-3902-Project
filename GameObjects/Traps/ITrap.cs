using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public interface ITrap : IGameObject
    {
        StaticSprite Sprite { get; set; }
        int Damage { get; set; }
        bool CanDamage { get; set; }
        void Active();

        void Inactive();
        
        void Resetting();

        void OnHit();
        void Removed();
    }
}
