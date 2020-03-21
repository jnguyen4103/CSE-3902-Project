/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class BeamExplosion : IAttack
    {
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public StaticSprite Sprite { get; set; }
        public bool CanDamage { get; set; } = false;
        public int Damage { get; set; }
        public States.Direction Direction { get; set; } = States.Direction.None;

        private Game1 Game;
        private IGameObject Creator;
        private StaticSprite[] explosions;
        private Vector2[] positions;
        private Vector2[] velocities;
        private int Timer = 0;
        private int lifeSpan = 20;

        public BeamExplosion(Game1 game, IGameObject creator)
        {
            Game = game;
            Creator = creator;
            Position = Creator.Position;
            Damage = 0;
            Sprite = new StaticSprite(game, "SwordBeam", new Vector2(0, 0), game.EffectSpriteSheet, game.spriteBatch);
        }

        public void Attack()
        {
            SetupAttack();
            Game.Dungeon01.Attacks.Add(this);
        }

        public void OnHit()
        {
        }

        public void Draw()
        {
            for (int i = 0; i < 4; i++)
            {
                explosions[i].DrawSprite();
            }
        }

        public bool IsCreator(IGameObject obj)
        {
            return obj.Hitbox.Equals(Creator.Hitbox);

        }

        public void Update()
        {
            Timer++;
            for (int i = 0; i < 4; i++)
            {
                positions[i] += velocities[i];
                explosions[i].UpdatePosition(positions[i]);
            }
            if(Timer == lifeSpan)
            {
                for (int i = 0; i < 4; i++)
                {
                    explosions[i].Colour = Color.Transparent;
                }
            }
        }

        private void SetupAttack()
        {
            explosions = new StaticSprite[4];
            positions = new Vector2[4];
            velocities = new Vector2[4];

            for (int i = 0; i < 4; i++)
            {
                explosions[i] = new StaticSprite(Game, "SwordBeamExplosion", Creator.Position, Game.EffectSpriteSheet, Game.spriteBatch);
                explosions[i].Layer = 0.9f;
                positions[i] = Position;
            }

            // Upper Right
            explosions[0].SpriteEffect = SpriteEffects.FlipHorizontally;
            velocities[0] = new Vector2(0.5f, -0.5f);

            // Upper Left
            velocities[1] = new Vector2(-0.5f, -0.5f);

            // Lower Right
            velocities[2] = new Vector2(0.5f, 0.5f);
            explosions[2].SpriteEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

            // Lower Left
            velocities[3] = new Vector2(-0.5f, 0.5f);
            explosions[3].SpriteEffect = SpriteEffects.FlipVertically;



        }

    }
}
