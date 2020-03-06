using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    class BombEffect : IEffect
    {
        Sprite Creator;
        Link.LinkDirection Direction;
        Game1 Game;
        Texture2D Texture;
        SpriteBatch Batch;

        public Sprite Sprite { get; set; }
        public int Damage { get; set; }

        public BombEffect(Sprite creator, Game1 game, Link.LinkDirection direction, Texture2D texture, SpriteBatch batch)
        {
            Creator = creator;
            Direction = direction;
            Game = game;
            Batch = batch;
            Texture = texture;
            Damage = 0;
        }

        public void CreateEffect()
        {
            // Adds new sprite effect to the EffectsList array so it'll be drawn on screen
            Sprite = new BombSprite(Creator, Game, Creator.Position, Texture, Batch);
            Game.EffectsList.Add(this);
        }

        public bool IsCreator(Sprite sprite)
        {
            return (Creator.Equals(sprite));
        }
    }
}