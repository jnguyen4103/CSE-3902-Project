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

        public void HurtLink(Monster monster, int direction)
        {
            if (Game.Link.GetState() != Link.LinkState.Damaged 
                && monster.State != Monster.MonsterState.Dead
                && monster.State != Monster.MonsterState.Damaged)
            {
                Game.Link.TakeDamage(monster.attackDamage, direction);
            }
        }

        

        public void DestroyEffect(IEffect effect)
        {
            if(!effect.Sprite.Name.Equals("SwordBeamExplosion") && !effect.Sprite.Name.Equals("SwordSwing") && !effect.Sprite.Name.Equals("SwordSwingHorizontal"))
            {
                effect.Sprite.KillSprite();
            }
        }


        public void DamageMonster(Monster monster, int direction, IEffect effect)
        {
            if(!effect.Sprite.Name.Equals("SwordBeamExplosion"))
            {
                monster.TakeDamage(effect.Damage, direction);
                DestroyEffect(effect);
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
