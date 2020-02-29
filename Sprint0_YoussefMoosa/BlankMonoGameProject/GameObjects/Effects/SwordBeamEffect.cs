using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class SwordBeamEffect : IEffect
    {
        Sprite Creator;
        Sprite EffectSprite;
        int EffectDamage;
        Game1 Game;
        Link.LinkDirection Direction;
        Texture2D Texture;
        SpriteBatch Batch;

        public Sprite Sprite { get => EffectSprite; set => EffectSprite = value; }
        public int Damage { get => EffectDamage; set => EffectDamage = value; }

        public SwordBeamEffect(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Game = game;
            Direction = direction;
            Batch = batch;
            Texture = texture;
            Damage = 1;
        }

        public void CreateEffect()
        {
            // Adds new sprite effect to the EffectsList array so it'll be drawn on screen
            EffectSprite = new SwordBeamSprite(Creator, Game, Direction, Texture, Batch);
            Game.EffectsList.Add(this);

        }

        public bool IsCreator(Sprite sprite)
        {
            return (Creator.Equals(sprite));
        }
    }
}
