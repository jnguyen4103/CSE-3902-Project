using Microsoft.Xna.Framework;
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

        public void StopLink()
        {

        }

        public void StopMonster()
        {

        }

        public void DestroyEffect(IEffect effect)
        {
            effect.Sprite.KillSprite();
        }

        public void DamageMonster(Monster monster, int direction, IEffect effect)
        {
            monster.TakeDamage(effect.Damage, direction);

            if (!effect.Sprite.Name.Equals("SwordSwing"))
            {
                effect.Sprite.KillSprite();
            }
        }

        public void DamageLinkEffect(int damage, int direction, IEffect effect)
        {
            Game.Link.TakeDamage(effect.Damage, direction);

        }


        public void PickupItem(Item item)
        {
            item.ActivateItem();
        }

    }
}
