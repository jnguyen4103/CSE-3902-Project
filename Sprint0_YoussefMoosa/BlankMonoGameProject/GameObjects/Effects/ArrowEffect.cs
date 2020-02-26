using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class ArrowEffect : IEffect
    {
        Sprite Creator;
        Game1 Game;
        Link.LinkDirection Direction;
        Texture2D Texture;
        SpriteBatch Batch;
        // Fireball effect requires a sprite, a game process to access the EffectsList array
        // and a batch so the sprite can draw

        public ArrowEffect(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Game = game;
            Direction = direction;
            Batch = batch;
            Texture = texture;
        }

        public void CreateEffect()
        {
            // Adds new sprite effect to the EffectsList array so it'll be drawn on screen
            Game.EffectsList.Add(new ArrowSprite(Creator, Game, Direction, Texture, Batch));

        }
    }
}
