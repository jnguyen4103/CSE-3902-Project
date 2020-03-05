using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sprint03
{
    public class MonsterFactory
    {
        protected Game1 Game;
        public readonly Dictionary<string, Func<Vector2, Monster>> Monsters = new Dictionary<string, Func<Vector2, Monster>>(1);

        public MonsterFactory(Game1 game)
        {
            Game = game;
            Monsters["Stalfos"] = SpawnStalfos;
        }

        public void SpawnMonster(string name, Vector2 spawn)
        {
            Game.MonstersList.Add(Monsters[name](spawn));

        }

        private Monster SpawnStalfos(Vector2 spawn)
        {
            // Creating Sprite for Stalfos
            Sprite StalfosSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
            StalfosSprite.FPS = 8;
            StalfosSprite.BaseSpeed = 0.5f;
            StalfosSprite.FPS = 4;

            // Setting up Monster object for Stalfos to hold stats
            Monster Stalfos = new Monster(StalfosSprite, "Stalfos", Game);
            Stalfos.hitpoints = 2;
            Stalfos.maxHP = 2;
            Stalfos.attackDamage = 1;
            Stalfos.StateMachine = new StalfosSM(Stalfos, Game);

            // Spawning dat boii
            return Stalfos;
        }
    }
}
