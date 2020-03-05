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
            Monsters["Gel"] = SpawnGel;
            Monsters["Keese"] = SpawnKeese;


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

            // Setting up Monster object for Stalfos to hold stats
            Monster Stalfos = new Monster(StalfosSprite, "Stalfos", Game);
            Stalfos.hitpoints = 2;
            Stalfos.maxHP = 2;
            Stalfos.attackDamage = 1;
            Stalfos.StateMachine = new StalfosSM(Stalfos, Game);

            // Spawning dat boii
            return Stalfos;
        }


        private Monster SpawnGel(Vector2 spawn)
        {
            // Creating Sprite for Gel
            Sprite GelSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
            GelSprite.FPS = 16;
            GelSprite.BaseSpeed = 0.64f;

            // Setting up Monster object for Stalfos to hold stats
            Monster Gel = new Monster(GelSprite, "Gel", Game);
            Gel.hitpoints = 1;
            Gel.maxHP = 1;
            Gel.attackDamage = 1;
            Gel.StateMachine = new GelSM(Gel, Game);

            // Spawning dat boii
            return Gel;
        }

        private Monster SpawnKeese(Vector2 spawn)
        {
            // Creating Sprite for Keese
            Sprite KeeseSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
            KeeseSprite.FPS = 16;
            KeeseSprite.BaseSpeed = 0.64f;

            // Setting up Monster object for Stalfos to hold stats
            Monster Keese = new Monster(KeeseSprite, "Keese", Game);
            Keese.hitpoints = 1;
            Keese.maxHP = 1;
            Keese.attackDamage = 1;
            Keese.StateMachine = new KeeseSM(Keese, Game);

            // Spawning dat boii
            return Keese;
        }
    }
}
