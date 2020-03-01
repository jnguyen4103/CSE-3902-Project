using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class BeamExplosionEffect : IEffect
    {
        Sprite Creator;
        Sprite EffectSprite;
        int EffectDamage;
        Game1 Game;
        Vector2 Speed;
        Vector2 Spawn;
        Texture2D Texture;
        SpriteBatch Batch;


        public Sprite Sprite { get => EffectSprite; set => EffectSprite = value; }
        public int Damage { get => EffectDamage; set => EffectDamage = value; }

        public BeamExplosionEffect(Sprite creator, Game1 game, Vector2 spawn, Vector2 speed, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Spawn = spawn;
            Game = game;
            Speed = speed;
            Batch = batch;
            Texture = texture;
            Damage = 0;
        }

        public void CreateEffect()
        {
            // Adds new sprite effect to the EffectsList array so it'll be drawn on screen
            EffectSprite = new BeamExplosionSprite(Game, Spawn, Speed, Texture, Batch);
            Game.EffectsList.Add(this);

        }

        public bool IsCreator(Sprite sprite)
        {
            return (Creator.Equals(sprite));
        }
    }
}
