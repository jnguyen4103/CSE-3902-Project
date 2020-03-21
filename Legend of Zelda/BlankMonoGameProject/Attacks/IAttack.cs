/* Contributors
* Stephen Hogg
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public interface IAttack: IGameObject
    {
        int Damage { get; set; }
        bool CanDamage { get; set; }
        StaticSprite Sprite { get; set; }
        void Attack();
        void OnHit();
        bool IsCreator(IGameObject obj);
    }
}
