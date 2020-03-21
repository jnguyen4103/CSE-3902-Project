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
    public interface ILink: IGameObject
    {
        LinkSprite Sprite { get; set; }
        LinkStateMachine StateMachine { get; set; }
        States.LinkState State { get; set; }
        States.Direction Direction { get; set; }
        int HP { get; set; }
        int MaxHP { get; set; }
        float BaseSpeed { get; set; }
        bool CanMove { get; set; }
        void TakeDamage(States.Direction directionHit, int damage);
        void Attack();
        void SecondaryAttack(string attackName);
        void ChangeDirection(States.Direction direction);
        void StopLink();
    }
}
