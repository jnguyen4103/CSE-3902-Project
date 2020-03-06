using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class ExplosionEffect : IEffect
    {
        Sprite Creator;
        Game1 Game;
        Texture2D Texture;
        SpriteBatch Batch;
        public Vector2 Spawn;

        public Sprite Sprite { get; set; }
        public int Damage { get; set; }


        public ExplosionEffect(Sprite creator, Game1 game, Vector2 spawn, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Game = game;
            Spawn = spawn;
            Batch = batch;
            Texture = texture;
            Damage = 2;
        }

        public void CreateEffect()
        {
            Sprite = new ExplosionSprite(Creator, Game, Spawn, Texture, Batch);
            Game.EffectsList.Add(this);
        }

        public bool IsCreator(Sprite sprite)
        {
            return (Creator.Equals(sprite));
        }
    }
}
