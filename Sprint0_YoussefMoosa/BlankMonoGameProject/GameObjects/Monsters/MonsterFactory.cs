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
            Monsters["Goriyas"] = SpawnGoriyas;
            Monsters["Darknut"] = SpawnDarknut;
            Monsters["BladeTrap"] = SpawnBladeTrap;



        }

        public void SpawnMonster(string name, Vector2 spawn)
        {
            Game.MonstersList.Add(Monsters[name](spawn));

        }

        private Monster SpawnStalfos(Vector2 spawn)
        {
            // Creating Sprite for Stalfos
            MonsterSprite StalfosSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
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
            MonsterSprite GelSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
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
            MonsterSprite KeeseSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
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

        private Monster SpawnGoriyas(Vector2 spawn)
        {
            // Creating Sprite for Goriyas
            MonsterSprite GoriyasSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
            GoriyasSprite.FPS = 8;
            GoriyasSprite.BaseSpeed = 0.5f;

            // Setting up Monster object for Stalfos to hold stats
            Monster Goriyas = new Monster(GoriyasSprite, "GoriyasDown", Game);
            Goriyas.hitpoints = 2;
            Goriyas.maxHP = 2;
            Goriyas.attackDamage = 1;
            Goriyas.StateMachine = new GoriyasSM(Goriyas, Game);

            // Spawning dat boii
            return Goriyas;
        }

        private Monster SpawnDarknut(Vector2 spawn)
        {
            // Creating Sprite for Darknut
            MonsterSprite DarknutSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
            DarknutSprite.FPS = 8;
            DarknutSprite.BaseSpeed = 0.64f;

            // Setting up Monster object for Stalfos to hold stats
            Monster Darknut = new Monster(DarknutSprite, "DarknutDown", Game);
            Darknut.hitpoints = 4;
            Darknut.maxHP = 4;
            Darknut.attackDamage = 2;
            Darknut.StateMachine = new DarknutSM(Darknut, Game);

            // Spawning dat boii
            return Darknut;
        }

        private Monster SpawnBladeTrap(Vector2 spawn)
        {
            // Creating Sprite for Darknut
            MonsterSprite BladeTrapSprite = new MonsterSprite(Game, "BladeTrap", Game.MonsterSpriteSheet, spawn, Game.spriteBatch);
            BladeTrapSprite.BaseSpeed = 0f;
            BladeTrapSprite.IgnoresBoundaries = true;

            // Setting up Monster object for Stalfos to hold stats
            Monster BladeTrap = new Monster(BladeTrapSprite, "BladeTrap", Game);
            BladeTrap.attackDamage = 2;
            BladeTrap.StateMachine = new BladeTrapSM(BladeTrap, Game);

            // Spawning dat boii
            return BladeTrap;
        }
    }
}
