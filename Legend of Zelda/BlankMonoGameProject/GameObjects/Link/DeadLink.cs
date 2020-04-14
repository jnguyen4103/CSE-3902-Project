/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class DeadLink: ILink
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public LinkSprite Sprite { get; set; }
        public LinkStateMachine StateMachine { get; set; }
        public States.LinkState State { get; set; } = States.LinkState.Dead;
        public States.Direction Direction { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public float BaseSpeed { get; set; } = 0f;
        public bool CanMove { get; set; } = false;

        private Game1 Game;
        private Texture2D LinkPepe;
        private Texture2D YouDied;

        private int Timer;
        private readonly int DeathDelay = 180;
        private readonly int DeathScreenDelay = 240;
        private readonly int ResetDelay = 900;
        public DeadLink(Game1 game, Link link)
        {
            Game = game;
            Sprite = link.Sprite;
            Sprite.ChangeSpriteAnimation("DeathSpin");
            Sprite.Colour = Color.White;
            Sprite.Layer = 0.95f;
            CanMove = false;
            Sprite.FPS = 3;

            game.soundEffects[13].Play();
            LinkPepe = game.Content.Load<Texture2D>("Link Pepe");
            YouDied = game.Content.Load<Texture2D>("You Died");
            Timer = 0;
        }

        public void TakeDamage(States.Direction directionHit, int damage)
        {
            // Cannot take damage in damaged state
        }

        public void Attack()
        {
            // Cannot attack while damaged
        }

        public void SecondaryAttack(string attackName)
        {
            // I dunno, maybe sometime later
        }

        public void ChangeDirection(States.Direction direction)
        {
            // He is dead, don't you see
        }


        public void Stop()
        {
            // The dead cannot be stopped
        }

        public void Update()
        {
            Timer++;
            if(Timer >= DeathDelay)
            {
                Sprite.ChangeSpriteAnimation("LinkDeath");
                Sprite.FPS = 4;
            }
            if(Timer >= DeathScreenDelay)
            {
                Game.hud.HideHud();
            }
            if (Timer >= ResetDelay)
            {
                DeathBed();
            }
        }

        public void Draw()
        {
            Sprite.DrawSprite();
            if(Timer >= DeathScreenDelay)
            {
                Game.spriteBatch.Draw(YouDied, new Vector2(Game.CurrDungeon.ActiveRoom.Position.X, Game.CurrDungeon.ActiveRoom.Position.Y + 100), null, Color.White * 0.85f, 0, Vector2.Zero, 1, SpriteEffects.None, 1f);
                Game.spriteBatch.Draw(LinkPepe, new Vector2(Game.CurrDungeon.ActiveRoom.Position.X, Game.CurrDungeon.ActiveRoom.Position.Y - 64), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);

            }
        }


        private void DeathBed()
        {
            ICommand reset = new Reset(Game);
            reset.Execute();
        }

        public void PickupItem()
        {
            /*
             * Cannot PickUPItem  
            */
        }
    }
}
