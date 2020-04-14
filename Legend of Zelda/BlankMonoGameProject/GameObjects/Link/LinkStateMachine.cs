/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class LinkStateMachine
    {
        public Link Link;
        private Game1 Game;
        public LinkStateMachine(Game1 game, Link link)
        {
            Link = link;
            Game = game;
        }

        public void IdleState()
        {
            // Do nothing, literally
            Game.Link = new Link(Game,  new LinkSprite(Game, "WalkUp", Game.LinkSpriteSheet, Game.spriteBatch),Game.LinkSpawn);

        }

        public void AttackState()
        {
            Game.Link = new AttackingLink(Game, Link);
        }

        public void SecondaryAttackState(string attackName)
        {
            Game.Link = new SecondaryAttackLink(Game, Link);
            IAttack attack = GetSecondaryAttack(attackName);
            attack.Attack();

        }

        public void DamagedState()
        {
            Game.Link = new DamagedLink(Game, Link);
        }

        public void DeadState()
        {
            Game.CurrDungeon.Monsters.Clear();
            Game.CurrDungeon.Items.Clear();
            Game.Link = new DeadLink(Game, Link);
        }

        public void PickupState()
        {
            Game.Link = new PickupLink(Game, Link);
        }

        // Method only here since inventory doesn't exist
        private IAttack GetSecondaryAttack(string attackName)
        {
            IAttack secondaryAttack = new Bomb(Game, Link);
            switch(attackName)
            {
                case ("Bomb"):
                    secondaryAttack = new Bomb(Game, Link);
                    break;
                case ("Arrow"):
                    secondaryAttack = new Arrow(Game, Link, Link.Direction);
                    break;
                case ("BlueCandle"):
                    secondaryAttack = new BlueCandle(Game, Link, Link.Direction);
                    break;
                case ("Boomerang"):
                    secondaryAttack = new Boomerang(Game, Link, Link.Direction);
                    break;
            }
            return secondaryAttack;
        }
    }
}
