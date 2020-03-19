/* Contributors
* Stephen Hogg
* Nico Negrete
*/
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
            Monsters["Lynel"] = SpawnLynel;
            Monsters["Goriyas"] = SpawnGoriyas;
            Monsters["Darknut"] = SpawnDarknut;
            //Monsters["BladeTrap"] = SpawnBladeTrap;
            Monsters["Aquamentus"] = SpawnAquamentus;
            Monsters["Zol"] = SpawnZol;



        }

        public Monster SpawnMonster(string name, Vector2 spawn)
        {
            return Monsters[name](spawn);

        }

        private Monster SpawnStalfos(Vector2 spawn)
        {
            // Creating Sprite for Stalfos
            MonsterSprite StalfosSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            StalfosSprite.FPS = 8;

            // Setting up Monster object for Stalfos to hold stats
            Monster Stalfos = new Monster(StalfosSprite, spawn, "Stalfos", Game);
            Stalfos.BaseSpeed = 0.5f;
            Stalfos.HP = 2;
            Stalfos.MaxHP = 2;
            Stalfos.AttackDamage = 1;
            Stalfos.StateMachine = new StalfosSM(Stalfos, Game);

            return Stalfos;
        }

        

        private Monster SpawnGel(Vector2 spawn)
        {
            // Creating Sprite for Gel
            MonsterSprite GelSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            GelSprite.FPS = 16;

            // Setting up Monster object for Gel to hold stats
            Monster Gel = new Monster(GelSprite, spawn, "Gel", Game);
            Gel.BaseSpeed = 0.5f;
            Gel.HP = 1;
            Gel.MaxHP = 1;
            Gel.AttackDamage = 1;
            Gel.StateMachine = new GelSM(Gel, Game);

            return Gel;
        }


        private Monster SpawnKeese(Vector2 spawn)
        {
            // Creating Sprite for Keese
            MonsterSprite KeeseSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            KeeseSprite.FPS = 16;

            // Setting up Monster object for Keese to hold stats
            Monster Keese = new Monster(KeeseSprite, spawn, "Keese", Game);
            Keese.Flies = true;
            Keese.BaseSpeed = 0.5f;
            Keese.HP = 1;
            Keese.MaxHP = 1;
            Keese.AttackDamage = 1;
            Keese.StateMachine = new KeeseSM(Keese, Game);

            return Keese;
        }
        
        private Monster SpawnLynel(Vector2 spaw)
        {
            // Creating Sprite for Lynel
            MonsterSprite LynelSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            LynelSprite.FPS = 8;

            // Setting up Monster object for Stalfos to hold stats
            Monster Lynel = new Monster(LynelSprite,spaw, "LynelDown", Game);
            Lynel.BaseSpeed = 0.5f;
            Lynel.HP = 5;
            Lynel.MaxHP = 5;
            Lynel.AttackDamage = 2;
            Lynel.StateMachine = new LynelSM(Lynel, Game);

            // Spawning dat boii
            return Lynel;
        }
        private Monster SpawnDarknut(Vector2 spawn)
        {
            // Creating Sprite for Lynel
            MonsterSprite DarknutSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            DarknutSprite.FPS = 8;

            // Setting up Monster object for Stalfos to hold stats
            Monster Darknut = new Monster(DarknutSprite, spawn, "DarknutDown", Game);
            Darknut.BaseSpeed = 0.5f;
            Darknut.HP = 4;
            Darknut.MaxHP = 4;
            Darknut.AttackDamage = 1;
            Darknut.StateMachine = new DarknutSM(Darknut, Game);

            // Spawning dat boii
            return Darknut;
        }


        
        private Monster SpawnGoriyas(Vector2 spawn)
        {
            MonsterSprite GoriyasSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            GoriyasSprite.FPS = 8;

            // Setting up Monster object for Stalfos to hold stats
            Monster Goriyas = new Monster(GoriyasSprite, spawn, "GoriyasDown", Game);
            Goriyas.BaseSpeed = 0.5f;
            Goriyas.HP = 2;
            Goriyas.MaxHP = 2;
            Goriyas.AttackDamage = 1;
            Goriyas.StateMachine = new GoriyasSM(Goriyas, Game);

            // Spawning dat boii
            return Goriyas;
        }

        private Monster SpawnAquamentus(Vector2 spawn)
        {
            // Creating Sprite for Aquamentus
            MonsterSprite AquamentusSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            AquamentusSprite.FPS = 8;

            // Setting up Monster object for Stalfos to hold stats
            Monster Aquamentus = new Monster(AquamentusSprite, spawn, "Aquamentus", Game);
            Aquamentus.BaseSpeed = 0.25f;
            Aquamentus.HP = 6;
            Aquamentus.MaxHP = 6;
            Aquamentus.AttackDamage = 1;
            Aquamentus.StateMachine = new AquamentusSM(Aquamentus, Game);

            // Spawning dat boii
            return Aquamentus;
        }

        private Monster SpawnZol(Vector2 spawn)
        {
            MonsterSprite ZolSprite = new MonsterSprite(Game, "SpawningCloud", Game.MonsterSpriteSheet, Game.spriteBatch);
            ZolSprite.FPS = 8;

            // Setting up Monster object for Stalfos to hold stats
            Monster Zol = new Monster(ZolSprite, spawn, "Stalfos", Game);
            Zol.BaseSpeed = 0.5f;
            Zol.HP = 3;
            Zol.MaxHP = 3;
            Zol.AttackDamage = 1;
            Zol.StateMachine = new ZolSM(Zol, Game);

            return Zol;
        }

        /*
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




            */
    }
}
