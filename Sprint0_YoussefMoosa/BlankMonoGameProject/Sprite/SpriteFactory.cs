using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class SpriteFactory
    {

        /* There are 4 Sprite Sheets:
         *      Link
         *      Monster
         *      Item
         *      Effects
         * 
         * In order to store all of the necessary sprites and their animation
         * frames they'll be loaded into a dictionary.
         * 
         * Each key will be the name of an animation, such as LinkMoveUp
         * and the values would be a rectangle that points to all of the frames
         * for moving up, a vector for the size of the sprite (such as 16x16),
         * and an integer for the number of animation frames.
         * 
         * An example of a return value would be like
         * Key: "Gel"
         * Value: Rectangle(16, 0, 8, 8), Vector(8, 32), int 2
         * 
         * The rectangle has all of the animation frames for the Gel
         * The Vector is the default size of the sprite, in this case
         * it's 8x8
         * 
         * The integer holds the number of frames, in this case the
         * Gel only has 2 frames and they're used for walking.
         * 
         */

        public readonly Dictionary<String, Tuple<Rectangle, Vector2, int>> Sprites = new Dictionary<string, Tuple<Rectangle, Vector2, int>>(69);

        // Al off the Sprites are size 16 by 16
        private readonly Vector2 Spritesize = new Vector2(16, 16);

        // Most of the Sprites are size 16 by 16, new vectors will be made for special cases
        private readonly Vector2 defaultMonsterSize = new Vector2(16, 16);

        // Most effects and items are 8 by 16 size
        private readonly Vector2 defaultItemSize = new Vector2(8, 16);
        private readonly Vector2 rotatedItemSize = new Vector2(16, 8);


        // Small Items & Effects
        private readonly Vector2 smallItemSize = new Vector2(8, 8);

        public SpriteFactory()
        {
            // Iniitalizing all Link Sprites into a dictionary
            Sprites["WalkUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 40), Spritesize, 2);
            Sprites["WalkDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 0, 16, 40), Spritesize, 2);
            Sprites["WalkRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 40), Spritesize, 2);
            Sprites["WalkLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 0, 16, 40), Spritesize, 2);
            Sprites["DamagedUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 40), Spritesize, 2);
            Sprites["DamagedDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 40), Spritesize, 2);
            Sprites["DamagedRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 16, 40), Spritesize, 2);
            Sprites["DamagedLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(168, 0, 16, 40), Spritesize, 2);
            Sprites["EffectUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(264, 0, 16, 16), Spritesize, 1);
            Sprites["EffectDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(192, 0, 16, 16), Spritesize, 1);
            Sprites["EffectRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(216, 0, 16, 16), Spritesize, 1);
            Sprites["EffectLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(240, 0, 16, 16), Spritesize, 1);
            Sprites["Pickup"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(288, 0, 16, 40), Spritesize, 2);


            // Initializing all Monster Sprites into the dictionary
            Sprites["StalfosWalk"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["StalfosDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 64, 16, 40), defaultMonsterSize, 2);

            Sprites["GeeseWalk"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 0, 16, 40), defaultMonsterSize, 2);

            Sprites["GoriyasWalkDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasWalkUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 0, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasWalkSide"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 40), defaultMonsterSize, 2);

            Sprites["GoriyasDamageUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 64, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasDamageDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 64, 16, 40), defaultMonsterSize, 2);
            Sprites["GoriyasDamageSide"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 64, 16, 40), defaultMonsterSize, 2);

            Sprites["BladeTrap"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 16), defaultMonsterSize, 1);

            Sprites["WallMaster"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 16, 32), defaultMonsterSize, 2);

            Sprites["Gel"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(168, 0, 8, 40), defaultItemSize, 2);

            Sprites["WizzrobeUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(184, 0, 8, 40), defaultMonsterSize, 2);
            Sprites["WizzrobeDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(208, 0, 8, 40), defaultMonsterSize, 2);

            Sprites["AquamentusWalk"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(232, 0, 24, 72), new Vector2(24, 32), 2);
            Sprites["AquamentusDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(232, 80, 24, 72), new Vector2(24, 32), 2);
            Sprites["AquamentusAttack"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(264, 0, 24, 72), new Vector2(24, 32), 2);

            Sprites["Death"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(296, 0, 16, 184), defaultMonsterSize, 8);
            Sprites["SpawningCloud"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(320, 0, 16, 64), defaultMonsterSize, 3);




            // Initializing all Item Sprites into the dictionary
            Sprites["Heart"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 24), Vector2.Divide(defaultMonsterSize, 2), 2);
            Sprites["HeartContainer"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Clock"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(56, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Rupee"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 8, 40), defaultItemSize, 2);
            Sprites["BlueRupee"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 16, 8, 16), defaultItemSize, 1);
            Sprites["Map"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 8, 16), defaultItemSize, 1);
            Sprites["Boomerang"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 0, 8, 8), smallItemSize, 1);
            Sprites["Bomb"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 0, 8, 16), defaultItemSize, 1);
            Sprites["Bow"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 8, 16), defaultItemSize, 1);
            Sprites["Arrow"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(160, 0, 8, 16), defaultItemSize, 1);
            Sprites["Book"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(176, 0, 8, 16), defaultItemSize, 1);
            Sprites["Key"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(192, 0, 8, 16), defaultItemSize, 1);
            Sprites["LionKey"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(208, 0, 8, 16), defaultItemSize, 1);
            Sprites["Compass"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(224, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Triforce"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(248, 0, 16, 32), defaultMonsterSize, 2);

            // Initializing all Effects Sprites into the dictionary
            Sprites["Sword"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 16), defaultItemSize, 1);
            Sprites["SwordBeam"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 88), defaultItemSize, 4);
            Sprites["SwordBeamHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 96, 16, 56), rotatedItemSize, 4);

            Sprites["SwordBeamExplosion"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 8, 88), defaultItemSize, 4);
            Sprites["BoomerangEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 8, 120), smallItemSize, 8);
            Sprites["BombEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 8, 16), smallItemSize, 1);
            Sprites["ArrowEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 0, 8, 16), defaultItemSize, 1);
            Sprites["ArrowEffectHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 24, 16, 8), rotatedItemSize, 1);
            Sprites["ProjectileHit"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 8, 8), smallItemSize, 1);
            Sprites["ExplosionEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 64), defaultMonsterSize, 3);
            Sprites["Fire"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 16), defaultMonsterSize, 1);
            Sprites["Fireball"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 8, 88), defaultItemSize, 4);
            Sprites["SwordSwing"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(160, 0, 8, 64), defaultItemSize, 3);
            Sprites["SwordSwingHorizontal"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(176, 0, 16, 40), rotatedItemSize, 3);



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

        }

    }
}
