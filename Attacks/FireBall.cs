/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class Fireball : IAttack
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = true;
        public int Damage { get; set; }
        public States.Direction Direction { get; set; }
        private Vector2 Path;
        private Game1 Game;
        private IGameObject Creator;
        private Vector2 velocity;

        public Fireball(Game1 game, IGameObject creator, Vector2  path)
        {
            Game = game;
            Creator = creator;
            Position = Creator.Position;
            Damage = 1;
            Path = path;
        }

        public void Attack()
        {
            SetupAttack();
            Game.Dungeon01.Attacks.Add(this);
        }

        public void OnHit()
        {
            IAttack explosion = new Explosion(Game, Position, Creator);
            explosion.Attack();
            Sprite.Colour = Color.Transparent;
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
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Sprite.Size.X, (int)Sprite.Size.Y);
            Position += Path;
            Sprite.UpdatePosition(Position);
        }

        private void SetupAttack()
        {
            string spriteName = "";
            SpriteEffects spriteEffect = SpriteEffects.None;
            velocity = Vector2.Zero;
            Vector2 offset = Vector2.Zero;
            spriteName = "Fireball";
            Position += offset;
            Sprite = new StaticSprite(Game, spriteName, Position, Game.EffectSpriteSheet, Game.spriteBatch);
            Sprite.Layer = 0.25f;
            Sprite.FPS = 16;
            Sprite.SpriteEffect = spriteEffect;
        }

    }
}
