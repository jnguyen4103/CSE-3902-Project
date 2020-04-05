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
    public class Link: ILink
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public LinkSprite Sprite { get; set; }
        public LinkStateMachine StateMachine { get; set; }
        public States.LinkState State { get; set; } = States.LinkState.Idle;
        public States.Direction Direction { get; set; } = States.Direction.Up;
        public int HP { get; set; } = 6;
        public int MaxHP { get; set; } = 6;
        public float BaseSpeed { get; set; } = 1f;
        public bool CanMove { get; set; } = true;


        private Game1 Game;
        private string directionName;
        private string secondaryName;

        public Link(Game1 game, LinkSprite sprite, Vector2 spawn)
        {
            Game = game;
            Sprite = sprite;
            Position = spawn;
            Sprite.UpdatePosition(spawn);
            Sprite.ChangeSpriteAnimation("WalkUp");
            directionName = "Up";
            StateMachine = new LinkStateMachine(game, this);

        }

        public void PickupItem()
        {
       
            StateMachine.PickupState();
            State = States.LinkState.Pickup;
        }

        public void TakeDamage(States.Direction directionHit, int damage)
        {
            if(State != States.LinkState.Damaged)
            {
                HP -= damage;
                Game.hud.UpdateCurrentHealth(Game.Link.HP);
                if (HP < 1 && State != States.LinkState.Dead)
                {
                    State = States.LinkState.Dead;
                    StateMachine.DeadState();
                }
                else if (State != States.LinkState.Damaged)
                {
                    Sprite.ChangeSpriteAnimation("Damaged" + directionName);
                    Direction = directionHit;
                    State = States.LinkState.Damaged;
                }
            }
        }

        public void Attack()
        {
            if(State == States.LinkState.Idle || State == States.LinkState.Moving)
            {
                State = States.LinkState.Attacking;
                Sprite.ChangeSpriteAnimation("Attack" + directionName);
            }
        }

        public void SecondaryAttack(string attackName)
        {
            if (State == States.LinkState.Idle || State == States.LinkState.Moving)
            {
                State = States.LinkState.SecondaryAttack;
                secondaryName = attackName;
                Sprite.ChangeSpriteAnimation("Attack" + directionName);
                Sprite.TotalFrames = 1;
            }
        }

        public void ChangeDirection(States.Direction direction)
        {

            if (Direction != direction && State != States.LinkState.Damaged)
            {
                switch (direction)
                {
                    case (States.Direction.Up):
                        directionName = "Up";
                        Sprite.ChangeSpriteAnimation("WalkUp");
                        break;
                    case (States.Direction.Down):
                        directionName = "Down";
                        Sprite.ChangeSpriteAnimation("WalkDown");
                        break;
                    case (States.Direction.Left):
                        directionName = "Left";
                        Sprite.ChangeSpriteAnimation("WalkLeft");
                        break;
                    case (States.Direction.Right):
                        directionName = "Right";
                        Sprite.ChangeSpriteAnimation("WalkRight");
                        break;
                }
                Direction = direction;
            }
        }


        public void Stop()
        {
            State = States.LinkState.Idle;
        }

        public void Update()
        {
            Hitbox = new Rectangle((int)Position.X + 2, (int)Position.Y + 4, (int)Sprite.Size.X - 4, (int)Sprite.Size.Y - 4);
            switch (State)
            {
                case (States.LinkState.Pickup):
                    StateMachine.PickupState();
                    break;

                case (States.LinkState.Attacking):
                    StateMachine.AttackState();
                    break;

                case (States.LinkState.Damaged):
                    StateMachine.DamagedState();
                    break;

                case (States.LinkState.SecondaryAttack):
                    StateMachine.SecondaryAttackState(secondaryName);
                    break;

                case (States.LinkState.Dead):
                    StateMachine.DeadState();
                    break;

                default:
                    State = States.LinkState.Idle;
                    break;
            }
        }

        public void ResetSprite()
        {
            Sprite.Colour = Color.White;
            Sprite.FPS = 8;
            Sprite.ChangeSpriteAnimation("Walk" + directionName);
        }
        public void Draw()
        {
            Sprite.DrawSprite();
            
        }
    }
}
