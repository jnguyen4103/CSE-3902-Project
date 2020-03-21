/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class Arrow : IAttack
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = true;
        public int Damage { get; set; }
        public States.Direction Direction;

        private Game1 Game;
        private IGameObject Creator;
        private Vector2 velocity;
        private Vector2 hitCorrect;
        private bool madeContact = false;
        private int Timer = 0;
        private int Lifespan = 5;

        public Arrow(Game1 game, IGameObject creator, States.Direction direction)
        {
            Game = game;
            Creator = creator;
            Position = Creator.Position;
            Direction = direction;
            Damage = 1;
        }

        public void Attack()
        {
            SetupAttack();
            Game.Dungeon01.Attacks.Add(this);
        }

        public void OnHit()
        {
            madeContact = true;
            CanDamage = false;
        }

        public void Draw()
        {
            Sprite.DrawSprite();
        }

        public bool IsCreator(IGameObject obj)
        {
            return obj.Hitbox.Equals(Creator.Hitbox);

        }

        public void Update()
        {
            if (!madeContact)
            {
                Position += velocity;

            }
            else
            {
                Timer++;
                if(Timer == 1)
                {
                    Sprite.ChangeSpriteAnimation("ProjectileHit");
                    Position += hitCorrect;
                }
                if (Timer >= Lifespan)
                {
                    Sprite.Colour = Color.Transparent;
                }
            }
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            Sprite.UpdatePosition(Position);
        }

        private void SetupAttack()
        {
            string spriteName = "";
            SpriteEffects spriteEffect = SpriteEffects.None;
            velocity = Vector2.Zero;
            hitCorrect = Vector2.Zero;

            switch (Direction)
            {
                case (States.Direction.Down):
                    Position = new Vector2(Creator.Position.X + 4, Creator.Position.Y + 12);
                    velocity.Y = 2f;
                    spriteName = "ArrowEffect";
                    hitCorrect.Y += 8;
                    spriteEffect = SpriteEffects.FlipVertically;
                    break;
                case (States.Direction.Up):
                    Position = new Vector2(Creator.Position.X + 4, Creator.Position.Y - 12);
                    velocity.Y = -2f;
                    spriteName = "ArrowEffect";
                    break;
                case (States.Direction.Left):
                    Position = new Vector2(Creator.Position.X - 12, Creator.Position.Y + 4);
                    velocity.X = -2f;
                    spriteName = "ArrowEffectHorizontal";
                    break;
                case (States.Direction.Right):
                    Position = new Vector2(Creator.Position.X + 12, Creator.Position.Y + 4);
                    velocity.X = 2f;
                    hitCorrect.X += 8;
                    spriteName = "ArrowEffectHorizontal";
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }
            Sprite = new StaticSprite(Game, spriteName, Position, Game.EffectSpriteSheet, Game.spriteBatch);
            Sprite.Layer = 0.25f;
            Sprite.SpriteEffect = spriteEffect;
        }

    }
}
