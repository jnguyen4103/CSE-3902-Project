/* Contributors
* Stephen Hogg
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint03
{
    public class Room
    {
        public Dungeon Level;
        public Vector2 Position;
        public List<Monster> AliveMonsters;
        public List<IItem> Items;
        public List<IItem> HiddenItems;
        public List<Rectangle> Walls;
        public List<MovableBlock> Movables;
        public List<Rectangle> Blocks;
        public List<IDoor> Doors;
        public List<ITrap> Traps;
        public List<ScreenTransition> RoomTransitions;
        public string Name;

        private string FileName;

        public Room(Game1 game, Dungeon level, string name, string filename)
        {
            Name = name;
            Level = level;
            FileName = filename;
            Position = new Vector2(0, 0);
            AliveMonsters = new List<Monster>();
            Items = new List<IItem>();
            HiddenItems = new List<IItem>();
            Walls = new List<Rectangle>();
            Movables = new List<MovableBlock>();
            Blocks = new List<Rectangle>();
            Doors = new List<IDoor>();
            Traps = new List<ITrap>();
            RoomTransitions = new List<ScreenTransition>();
            RoomLoader.LoadRoom(game, Level, this, FileName);

        }
    }

    public struct ScreenTransition
    {
        public Rectangle Hitbox { get; set; }
        public Rectangle doorHitbox { get; set; }
        public string NextRoom;

        public ScreenTransition(Vector2 position, Vector2 size, string nextRoom, Vector2 doorCoords)
        {
            NextRoom = nextRoom;
            Hitbox = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            doorHitbox = new Rectangle((int)doorCoords.X, (int)doorCoords.Y, (int)size.X, (int)size.Y);
        }
    }
}
