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
    public class AttackingLink: ILink
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public LinkSprite Sprite { get; set; }
        public LinkStateMachine StateMachine { get; set; }
        public States.LinkState State { get; set; } = States.LinkState.Damaged;
        public States.Direction Direction { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public float BaseSpeed { get; set; } = 1f;
        public bool CanMove { get; set; } = false;


        private Game1 Game;
        private Link decoratedLink;
        private IAttack SwordAttack;
        private int Timer = 0;
        private int AttackDuration = 15;
        private int SwordStartTime = 5;

        public AttackingLink(Game1 game, Link link)
        {
            Game = game;
            decoratedLink = link;
            Sprite = decoratedLink.Sprite;
            Sprite.FPS = 12;
            Hitbox = decoratedLink.Hitbox;
            Position = decoratedLink.Position;
            Direction = decoratedLink.Direction;
            HP = decoratedLink.HP;
            MaxHP = decoratedLink.MaxHP;
            SwordAttack = new Sword(Game, this);

        }
        public void PickupItem()
        {
            /*
             * Cannot PickUPItem  
            */
        }
        public void TakeDamage(States.Direction directionHit, int damage)
        {
            RemoveDecorator();
            decoratedLink.TakeDamage(directionHit, damage);
        }

        public void Attack()
        {
            //Perhaps have a special attack on double press
        }

        public void SecondaryAttack(string attackName)
        {
            // Does nothing
        }

        public void ChangeDirection(States.Direction direction)
        {
            // Cannot move
        }


        public void Stop()
        {
            // Link cannot move thus he does not need to stop
        }
        /*
      * addes sound Effects
      * 0.LOZ_Arrow_Boomerang
      * 1 LOZ_Bomb_Blow
      * 2 LOZ_Bomb_Drop
      * 3 LOZ_Boss_Scream1
      * 4 LOZ_Candle
      * 5 LOZ_Door_Unlock
      * 6 LOZ_Enemy_Die
      * 7 LOZ_Enemy_Hit
      * 8 LOZ_Fanfare
      * 9 LOZ_Get_Heart
      * 10 LOZ_Get_Item
      * 11 LOZ_Get_Rupee
      * 12 LOZ_Key_Appear
      * 13 LOZ_Link_Die
      * 14 LOZ_Link_Hurt
      * 15 LOZ_Secret
      * 16 LOZ_Stairs
      * 17 LOZ_Sword_Shoot
      * 18 LOZ_Sword_Slash
      */

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X + 2, (int)Position.Y + 4, (int)Sprite.Size.X - 4, (int)Sprite.Size.Y - 4);
            Timer++;
            if(Timer == SwordStartTime)
            {
                SwordAttack.Attack();
                Game.soundEffects[18].Play();
            }
            else if(Timer >= AttackDuration)
            {
                if(HP == MaxHP)
                {
                    IAttack swordBeam = new SwordBeam(Game, decoratedLink, Direction);
                    swordBeam.Attack();
                    Game.soundEffects[17].Play();
                }
                RemoveDecorator();
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }



        private void RemoveDecorator()
        {
            decoratedLink.State = States.LinkState.Idle;
            decoratedLink.Position = Position;
            decoratedLink.Hitbox = Hitbox;
            Game.Link = decoratedLink;
            decoratedLink.ResetSprite();
            decoratedLink.CanMove = true;
        }
    }
}
