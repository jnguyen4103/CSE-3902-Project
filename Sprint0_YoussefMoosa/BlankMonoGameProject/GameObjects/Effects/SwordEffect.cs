using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class SwordEffect : IEffect
    {
        Sprite Creator;
        Sprite EffectSprite;
        Game1 Game;
        Link.LinkDirection Direction;
        Texture2D Texture;
        SpriteBatch Batch;

        public Sprite Sprite { get => EffectSprite; set => EffectSprite = value; }

        public SwordEffect(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Game = game;
            Direction = direction;
            Batch = batch;
            Texture = texture;
        }

        public void CreateEffect()
        {
            EffectSprite = new SwordSprite(Creator, Game, Direction, Texture, Batch);
            Game.EffectsList.Add(this);

        }
    }
}
