using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint03
{
    public class FireballEffect: IEffect
    {
        Sprite Creator;
        Game1 Game;
        Vector2 Direction;
        Texture2D Texture;
        SpriteBatch Batch;

        public Sprite Sprite { get; set; }
        public int Damage { get; set; }

        public FireballEffect(Sprite creator, Game1 game, Vector2 direction, Texture2D texture, SpriteBatch batch)
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
            Sprite = new FireballSprite(Creator, Game, Direction, Texture, Batch);
            Game.EffectsList.Add(this);
        }

        public bool IsCreator(Sprite sprite)
        {
            return (Creator.Equals(sprite));
        }
    }
}
