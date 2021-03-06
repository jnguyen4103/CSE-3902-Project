﻿/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace Sprint03
{
    public class SpriteFactory
    {

        // We've lost count of how many sprites we have, so this number is probably inflated
        public readonly Dictionary<string, Tuple<Rectangle, Vector2, int>> Sprites = new Dictionary<string, Tuple<Rectangle, Vector2, int>>(200);

        // Al off the Sprites are size 16 by 16
        private readonly Vector2 Spritesize = new Vector2(16, 16);

        // Most of the Sprites are size 16 by 16, new vectors will be made for special cases
        private readonly Vector2 defaultMonsterSize = new Vector2(16, 16);
        private readonly Vector2 largeMonsterSize = new Vector2(28, 16);
        private readonly Vector2 dodongoDamage = new Vector2(32,16);
        private readonly Vector2 offSet = new Vector2(0, 4);
        private readonly Vector2 skullSize = new Vector2(12,12);

        private readonly Vector2 bigZombieDown = new Vector2(40,44);
        private readonly Vector2 bigZombieUP = new Vector2(40,40);
        private readonly Vector2 bigZombieLeftRight = new Vector2(36,40);

        private readonly Vector2 bigZombieAttack = new Vector2(36,60);

        // Most effects and items are 8 by 16 size
        private readonly Vector2 defaultItemSize = new Vector2(8, 16);
        private readonly Vector2 rotatedItemSize = new Vector2(16, 8);

        // Room Sizes
        private readonly Vector2 doorSize = new Vector2(32, 32);
        private readonly Vector2 floorSize = new Vector2(192, 112);





        // Small Items & Effects
        private readonly Vector2 smallItemSize = new Vector2(8, 8);
        private readonly Vector2 tinyItemSize = new Vector2(3, 3);

        public SpriteFactory()
        {
            // Iniitalizing all Link Sprites into a dictionary
            Sprites["WalkUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 40), Spritesize, 2);
            Sprites["WalkDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 0, 16, 40), Spritesize, 2);
            Sprites["WalkRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 40), Spritesize, 2);
            Sprites["WalkLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 0, 16, 40), Spritesize, 2);
            Sprites["DamagedUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 40), Spritesize, 6);
            Sprites["DamagedDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 40), Spritesize, 6);
            Sprites["DamagedRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 16, 40), Spritesize, 6);
            Sprites["DamagedLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(168, 0, 16, 40), Spritesize, 6);
            Sprites["AttackUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(264, 0, 16, 88), Spritesize, 4);
            Sprites["AttackDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(192, 0, 16, 88), Spritesize, 4);
            Sprites["AttackRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(216, 0, 16, 88), Spritesize, 4);
            Sprites["AttackLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(240, 0, 16, 88), Spritesize, 4);
            Sprites["Pickup"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(288, 0, 16, 40), Spritesize, 4);
            Sprites["DeathSpin"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(312, 0, 16, 208), Spritesize, 9);
            Sprites["LinkDeath"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(336, 0, 16, 64), Spritesize, 4);


            // Initializing all Monster Sprites into the dictionary
            Sprites["Stalfos"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["StalfosDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 64, 16, 40), defaultMonsterSize, 2);

            Sprites["Keese"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 0, 16, 40), defaultMonsterSize, 2);

            Sprites["GoriyasDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 64, 16, 40), defaultMonsterSize, 2);


            Sprites["GoriyasDownDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 64, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasUpDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 64, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasRightDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 64, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasLeftDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 88, 16, 40), defaultMonsterSize, 2);


            Sprites["BladeTrap"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 16), defaultMonsterSize, 1);

            Sprites["WallMaster"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 16, 40), defaultMonsterSize, 2);

            Sprites["Zol"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 48, 16, 40), defaultMonsterSize, 2);
            Sprites["ZolDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 96, 16, 40), defaultMonsterSize, 2);

            Sprites["Rope"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(168, 48, 16, 40), defaultMonsterSize, 2);

            Sprites["Gel"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(168, 0, 8, 40), defaultItemSize, 2);

            Sprites["WizzrobeUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(184, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["WizzrobeDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(208, 0, 16, 40), defaultMonsterSize, 2);

            Sprites["DarknutDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 112, 16, 40), defaultMonsterSize, 2);
            Sprites["DarknutUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 112, 16, 40), defaultMonsterSize, 2);
            Sprites["DarknutRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 112, 16, 40), defaultMonsterSize, 2);
            Sprites["DarknutLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 112, 16, 40), defaultMonsterSize, 2);

            Sprites["DarknutDownDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 160, 16, 40), defaultMonsterSize, 2);
            Sprites["DarknutUpDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 160, 16, 40), defaultMonsterSize, 2);
            Sprites["DarknutRightDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 160, 16, 40), defaultMonsterSize, 2);
            Sprites["DarknutLeftDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 160, 16, 40), defaultMonsterSize, 2);

            Sprites["LynelDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 208, 16, 40), defaultMonsterSize, 2);
            Sprites["LynelUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 208, 16, 40), defaultMonsterSize, 2);
            Sprites["LynelRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 208, 16, 40), defaultMonsterSize, 2);
            Sprites["LynelLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 208, 16, 40), defaultMonsterSize, 2);

            Sprites["LynelDownDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 256, 16, 40), defaultMonsterSize, 2);
            Sprites["LynelUpDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 256, 16, 40), defaultMonsterSize, 2);
            Sprites["LynelRightDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 256, 16, 40), defaultMonsterSize, 2);
            Sprites["LynelLeftDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 256, 16, 40), defaultMonsterSize, 2);


            Sprites["DoDongoDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(103, 208, 16, 40), defaultMonsterSize, 4);
            Sprites["DoDongoUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 208, 16, 40), defaultMonsterSize, 4);
            Sprites["DoDongoRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(150, 208, 16, 40), largeMonsterSize, 2);
            Sprites["DoDongoLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(187, 208, 16, 40), largeMonsterSize, 2);

            Sprites["DoDongoDownDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(103, 304, 16, 40), defaultMonsterSize, 2);
            Sprites["DoDongoUpDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(127, 304, 16, 40), defaultMonsterSize, 2);
            Sprites["DoDongoRightDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(151, 256, 16, 40), dodongoDamage, 2);
            Sprites["DoDongoLeftDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(191, 256, 16, 40), dodongoDamage, 2);


            Sprites["BigZombieDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(414, 0, 40, 40), bigZombieDown, 3);
            Sprites["BigZombieUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(364, 0, 40, 40), bigZombieUP, 3);
            Sprites["BigZombieRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(464, 0, 40, 40), bigZombieLeftRight, 3);
            Sprites["BigZombieLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(505, 0, 40,40), bigZombieLeftRight, 3);
            Sprites["BigZombieAttack"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(549, 0, 40, 40), bigZombieAttack, 2);


            Sprites["BigZombieDownDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(414, 170, 40, 40), bigZombieDown, 2);
            Sprites["BigZombieUpDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(364, 170, 40, 40), bigZombieUP, 2);
            Sprites["BigZombieRightDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(464, 170, 40, 40), bigZombieLeftRight, 2);
            Sprites["BigZombieLeftDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(505, 170, 40, 40), bigZombieLeftRight, 2);
            Sprites["BigZombieAttackDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(549, 170, 40, 4), bigZombieAttack, 2);


            Sprites["RopeRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 304, 16, 40), defaultMonsterSize, 2);
            Sprites["RopeLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 305, 16, 40), defaultMonsterSize, 2);
            Sprites["RopeLeftDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(23, 350, 16, 40), defaultMonsterSize, 2);
            Sprites["RopeRightDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 349, 16, 40), defaultMonsterSize, 2);



            Sprites["Gibo"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 112, 16, 40), defaultMonsterSize, 2);

            Sprites["Aquamentus"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(232, 0, 24, 72), new Vector2(24, 32), 2);
            Sprites["AquamentusDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(232, 80, 24, 72), new Vector2(24, 32), 2);
            Sprites["AquamentusAttack"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(264, 0, 24, 72), new Vector2(24, 32), 2);





            Sprites["Death"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(296, 0, 16, 184), defaultMonsterSize, 8);
            Sprites["SpawningCloud"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(320, 0, 16, 64), defaultMonsterSize, 3);




            // Initializing all Item Sprites into the dictionary
            Sprites["Heart"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 24), Vector2.Divide(defaultMonsterSize, 2), 2);
            Sprites["HeartContainer"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Fairy"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(40, 0, 8, 16), defaultItemSize, 2);
            Sprites["Clock"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(56, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Rupee"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 8, 40), defaultItemSize, 2);
            Sprites["BlueRupee"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 24, 8, 16), defaultItemSize, 1);
            Sprites["Map"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 8, 16), defaultItemSize, 1);
            Sprites["Boomerang"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 0, 8, 8), smallItemSize, 1);
            Sprites["Bomb"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 0, 8, 16), defaultItemSize, 1);
            Sprites["Bow"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 8, 16), defaultItemSize, 1);
            Sprites["Arrow"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(160, 0, 8, 16), defaultItemSize, 1);
            Sprites["Book"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(176, 0, 8, 16), defaultItemSize, 1);
            Sprites["Key"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(192, 0, 8, 16), defaultItemSize, 1);
            Sprites["LionKey"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(208, 0, 8, 16), defaultItemSize, 1);
            Sprites["Compass"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(224, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Triforce"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(248, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["OldMan"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(269, 0, 16, 16), defaultMonsterSize+offSet, 1);
            Sprites["OldManFire"]= new Tuple<Rectangle, Vector2, int>(new Rectangle(293, 0, 16, 16), defaultMonsterSize + offSet , 2);
            Sprites["Merchant"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(269, 24, 16, 16), defaultMonsterSize+offSet, 1);
            Sprites["Gun"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(107, 27, 17, 10), new Vector2(17, 10), 1);
            Sprites["Skull"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 64, 16, 16), skullSize, 2);

            // Initializing all Effects Sprites into the dictionary
            Sprites["Sword"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 16), defaultItemSize, 1);
            Sprites["SwordBeam"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 88), defaultItemSize, 4);
            Sprites["SwordBeamHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 96, 16, 56), rotatedItemSize, 4);

            Sprites["SwordBeamExplosion"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 8, 88), defaultItemSize, 4);
            Sprites["BoomerangEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 8, 120), smallItemSize, 8);
            Sprites["BombEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 8, 16), defaultItemSize, 4);
            Sprites["ArrowEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 0, 8, 16), defaultItemSize, 1);
            Sprites["ArrowEffectHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 24, 16, 8), rotatedItemSize, 1);
            Sprites["ProjectileHit"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 8, 8), smallItemSize, 1);
            Sprites["BulletHit"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(366, 15, 15, 14), new Vector2(15, 14), 1);
            Sprites["ExplosionEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 64), defaultMonsterSize, 3);
            Sprites["RedExplosionEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 72, 16, 64), defaultMonsterSize, 3);
            Sprites["GreyExplosionEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 144, 16, 64), defaultMonsterSize, 3);
            Sprites["FireEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 16), defaultMonsterSize, 2);
            Sprites["Fireball"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 8, 88), defaultItemSize, 4);
            Sprites["SwordSwing"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(160, 0, 8, 64), defaultItemSize, 3);
            Sprites["SwordSwingHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(176, 0, 16, 40), rotatedItemSize, 3);
            Sprites["Bullet"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(367, 1, 4, 4), new Vector2(4, 4) , 3);

            Sprites["RedLightsaber"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(200, 0, 8, 64), defaultItemSize, 3);
            Sprites["RedLightsaberHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(216, 0, 16, 40), rotatedItemSize, 3);
            Sprites["BlueLightsaber"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(200, 72, 8, 64), defaultItemSize, 3);
            Sprites["BlueLightsaberHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(216, 72, 16, 40), rotatedItemSize, 3);

            Sprites["Blackhole"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(240, 0, 16, 112), defaultMonsterSize, 5);
            Sprites["Spike"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(264, 0, 16, 64), defaultMonsterSize, 3);

            Sprites["Dragonhead"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(288, 0, 16, 88), defaultMonsterSize, 4);
            Sprites["DragonheadHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(312, 0, 16, 88), defaultMonsterSize, 4);


            // Creating Tile Sprites
            Sprites["Floor"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Block"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 0, 16, 16), Spritesize, 1);
            Sprites["FishStatue"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 16), Spritesize, 1);
            Sprites["DragonStatue"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 0, 16, 16), Spritesize, 1);
            Sprites["BlackFloor"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 16), Spritesize, 1);
            Sprites["Gravel"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 16), Spritesize, 1);
            Sprites["Water"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 16, 16), Spritesize, 1);
            Sprites["Stairs"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(168, 0, 16, 16), Spritesize, 1);
            Sprites["Brick"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(192, 0, 16, 16), Spritesize, 1);
            Sprites["Steps"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(216, 0, 16, 16), Spritesize, 1);

            // Door Sprites
            Sprites["WallUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 24, 32, 32), doorSize, 1);
            Sprites["LockedDoorUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 24, 32, 32), doorSize, 1);
            Sprites["ClosedDoorUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 24, 32, 32), doorSize, 1);

            Sprites["WallDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 144, 32, 32), doorSize, 1);
            Sprites["LockedDoorDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 144, 32, 32), doorSize, 1);
            Sprites["ClosedDoorDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 144, 32, 32), doorSize, 1);

            Sprites["WallLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 64, 32, 32), doorSize, 1);
            Sprites["LockedDoorLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 64, 32, 32), doorSize, 1);
            Sprites["ClosedDoorLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 64, 32, 32), doorSize, 1);

            Sprites["WallRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 104, 32, 32), doorSize, 1);
            Sprites["LockedDoorRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 104, 32, 32), doorSize, 1);
            Sprites["ClosedDoorRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 104, 32, 32), doorSize, 1);


            // HUD Sprites
            Sprites["HUD"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 256, 240), new Vector2(256, 240), 1);

            Sprites["EmptyHeart"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 8), smallItemSize, 1);
            Sprites["HalfHeart"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 8, 8), smallItemSize, 1);
            Sprites["FullHeart"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 8, 8), smallItemSize, 1);

            Sprites["Dungeon01Map"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 72, 64, 40), new Vector2(64, 40), 1);
            Sprites["X"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 8, 8), smallItemSize, 1);
            Sprites["0"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 16, 8, 8), smallItemSize, 1);
            Sprites["1"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 16, 8, 8), smallItemSize, 1);
            Sprites["2"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 16, 8, 8), smallItemSize, 1);
            Sprites["3"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 16, 8, 8), smallItemSize, 1);
            Sprites["4"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 16, 8, 8), smallItemSize, 1);
            Sprites["5"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 32, 8, 8), smallItemSize, 1);
            Sprites["6"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 32, 8, 8), smallItemSize, 1);
            Sprites["7"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 32, 8, 8), smallItemSize, 1);
            Sprites["8"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 32, 8, 8), smallItemSize, 1);
            Sprites["9"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 32, 8, 8), smallItemSize, 1);

            Sprites["SelectSquare"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 48, 16, 16), defaultItemSize, 1);
            Sprites["LinkLocationMap"] = new Tuple<Rectangle, Vector2, int> (new Rectangle(24, 48, 3, 3), tinyItemSize, 1);
            Sprites["LinkLocationMiniMap"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(40, 48, 3, 3), tinyItemSize, 1);

            /* Room Types:
             * example: left, right, up, down (this is a room with openings on all 4 sides, each direction denotes an opening)
             * 
             * a: left
             * b: left, right
             * c: left, down
             * d: left, up
             * e: left, up, right
             * f: left, right, down
             * g: left, up, down
             * h: left, up, right, down
             * i: right
             * j: right, up
             * k: right, down
             * l: right, up, down
             * m: down
             * n: down, up
             * o: up
             */
            Sprites["MapRoomTypeA"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 32, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeB"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeC"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 0, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeD"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 16, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeE"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 32, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeF"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 16, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeG"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 48, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeH"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 48, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeI"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 16, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeJ"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 0, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeK"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 32, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeL"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 48, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeM"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 16, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeN"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 48, 8, 8), smallItemSize, 1);
            Sprites["MapRoomTypeO"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 32, 8, 8), smallItemSize, 1);
        }
    }
}
