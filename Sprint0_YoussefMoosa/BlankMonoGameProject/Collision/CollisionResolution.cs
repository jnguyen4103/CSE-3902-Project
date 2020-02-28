using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{

    public class CollisionResolution
    {
        private Game1 Game;
        public CollisionResolution(Game1 game)
        {
            Game = game;
        }

        public void HurtLink(int damage, string direction)
        {
            Game.Link.TakeDamage();
        }

        public void DamageMonster(Monster monster, string direction)
        {
            monster.StateMachine.DamagedState();

        }

        public void DamageLinkEffect(int damage, string direction, IEffect effect)
        {
            Game.Link.TakeDamage();
        }

        public void DamageMonsterEffect(Monster monster, string direction, IEffect effect)
        {
            monster.StateMachine.DamagedState();
        }


        public void PickupItem(Item item)
        {
            item.ActivateItem();
        }

    }
}
