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


        public readonly Dictionary<String, Tuple<Rectangle, Vector2, int>> LinkSprites = new Dictionary<string, Tuple<Rectangle, Vector2, int>>(13);
        public readonly Dictionary<String, Tuple<Rectangle, Vector2, int>> MonsterSprites = new Dictionary<string, Tuple<Rectangle, Vector2, int>>(12);
        public readonly Dictionary<String, Tuple<Rectangle, Vector2, int>> ItemSprites = new Dictionary<string, Tuple<Rectangle, Vector2, int>>(15);
        public readonly Dictionary<String, Tuple<Rectangle, Vector2, int>> EffectSprites = new Dictionary<string, Tuple<Rectangle, Vector2, int>>(11);
        public readonly Dictionary<String, Tuple<Rectangle, Vector2, int>> TileSprites = new Dictionary<string, Tuple<Rectangle, Vector2, int>>(10);


        // Al off the LinkSprites are size 16 by 16
        private readonly Vector2 LinkSpriteSize = new Vector2(16, 16);

        // Most of the MonsterSprites are size 16 by 16, new vectors will be made for special cases
        private readonly Vector2 defaultMonsterSize = new Vector2(16, 16);

        // Most effects and items are 8 by 16 size
        private readonly Vector2 defaultItemSize = new Vector2(8, 16);

        public SpriteFactory()
        {
            // Iniitalizing all Link Sprites into a dictionary
            LinkSprites["WalkUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["WalkDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["WalkRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["WalkLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["DamagedUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["DamagedDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["DamagedRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["DamagedLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 0, 16, 32), LinkSpriteSize, 2);
            LinkSprites["EffectUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(176, 0, 16, 16), LinkSpriteSize, 1);
            LinkSprites["EffectDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 0, 16, 16), LinkSpriteSize, 1);
            LinkSprites["EffectRight"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 16, 16), LinkSpriteSize, 1);
            LinkSprites["EffectLeft"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(160, 0, 16, 16), LinkSpriteSize, 1);
            LinkSprites["Pickup"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(192, 0, 16, 16), LinkSpriteSize, 2);


            // Initializing all Monster Sprites into the dictionary
            MonsterSprites["StalfosWalk"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 16), defaultMonsterSize, 2);
            MonsterSprites["StalfosDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 64, 16, 32), defaultMonsterSize, 2);

            MonsterSprites["GeeseWalk"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 16, 32), defaultMonsterSize, 2);

            MonsterSprites["GoriyasWalkDown"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 16, 16), defaultMonsterSize, 2);
            MonsterSprites["GoriyasWalkUp"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 16), defaultMonsterSize, 2);
            MonsterSprites["GoriyasWalkSide"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 0, 16, 32), defaultMonsterSize, 2);

            MonsterSprites["BladeTrap"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 16, 16), defaultMonsterSize, 1);

            MonsterSprites["WallMaster"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 32), defaultMonsterSize, 2);

            MonsterSprites["Gel"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 0, 8, 32), defaultItemSize, 2);

            MonsterSprites["AquamentusWalk"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(160, 0, 24, 64), new Vector2(24, 32), 2);
            MonsterSprites["AquamentusDamaged"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(160, 64, 24, 64), new Vector2(24, 32), 2);
            MonsterSprites["AquamentusAttack"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(184, 0, 24, 64), new Vector2(24, 32), 2);


            // Initializing all Item Sprites into the dictionary
            ItemSprites["Heart"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 16), Vector2.Divide(defaultMonsterSize, 2), 2);
            ItemSprites["HeartContainer"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(8, 0, 16, 16), defaultMonsterSize, 1);
            ItemSprites["Clock"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 16, 16), defaultMonsterSize, 1);
            ItemSprites["Rupee"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 8, 16), defaultItemSize, 2);
            ItemSprites["BlueRupee"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 16, 8, 16), defaultItemSize, 1);
            ItemSprites["Map"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(56, 0, 8, 16), defaultItemSize, 1);
            ItemSprites["Boomerang"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 0, 8, 8), Vector2.Divide(defaultMonsterSize, 2), 1);
            ItemSprites["Bomb"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(72, 0, 8, 16), defaultItemSize, 1);
            ItemSprites["Bow"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 8, 16), defaultItemSize, 1);
            ItemSprites["Arrow"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(88, 0, 8, 16), defaultItemSize, 1);
            ItemSprites["Book"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 8, 16), defaultItemSize, 1);
            ItemSprites["Key"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(104, 0, 8, 16), defaultItemSize, 1);
            ItemSprites["LionKey"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 0, 8, 16), defaultItemSize, 1);
            ItemSprites["Compass"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(120, 0, 16, 16), defaultMonsterSize, 1);
            ItemSprites["Triforce"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(136, 0, 16, 32), defaultMonsterSize, 2);

            // Initializing all Effects Sprites into the dictionary
            EffectSprites["Sword"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 16), defaultItemSize, 1);
            EffectSprites["SwordBeam"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 8, 64), defaultItemSize, 4);
            EffectSprites["SwordBeamExplosion"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(8, 0, 8, 64), defaultItemSize, 4);
            EffectSprites["Boomerang"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 8, 64), Vector2.Divide(defaultMonsterSize, 2), 8);
            EffectSprites["BombEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(24, 0, 8, 16), defaultItemSize, 1);
            EffectSprites["ArrowEffect"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 8, 16), defaultItemSize, 1);
            EffectSprites["ProjectileHit"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(40, 0, 8, 8), Vector2.Divide(defaultMonsterSize, 2), 1);
            EffectSprites["BombExplosion"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 48), defaultMonsterSize, 3);
            EffectSprites["Fire"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 0, 16, 16), defaultMonsterSize, 1);
            EffectSprites["Fireball"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 8, 64), defaultItemSize, 4);
            EffectSprites["SwordSwing"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(88, 0, 8, 48), defaultItemSize, 3);


            // Creating Tile Sprites
            TileSprites["Floor"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(0, 0, 16, 16), defaultMonsterSize, 1);
            TileSprites["Block"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(16, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["FishStatue"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(32, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["DragonStatue"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(48, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["BlackFloor"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(64, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["Gravel"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(80, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["Water"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(96, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["Stairs"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(112, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["Brick"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(128, 0, 16, 16), LinkSpriteSize, 1);
            TileSprites["Steps"] = new Tuple<Rectangle, Vector2, int>(new Rectangle(144, 0, 16, 16), LinkSpriteSize, 1);

        }

    }
}
