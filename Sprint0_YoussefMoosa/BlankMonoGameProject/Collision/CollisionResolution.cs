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

        public void HurtLink(int damage, int direction)
        {
            if (Game.Link.GetState() != Link.LinkState.Damaged)
            {
                Game.Link.TakeDamage(damage, direction);
            }
        }

        public void DamageMonster(Monster monster, int direction)
        {
            monster.StateMachine.DamagedState();

        }

        public void DamageLinkEffect(int damage, int direction, IEffect effect)
        {
            Game.Link.TakeDamage(effect.Damage, direction);

        }

        public void DamageMonsterEffect(Monster monster, int direction, IEffect effect)
        {
            monster.StateMachine.DamagedState();
            effect.Sprite.KillSprite();

        }


        public void PickupItem(Item item)
        {
            item.ActivateItem();
        }

    }
}
